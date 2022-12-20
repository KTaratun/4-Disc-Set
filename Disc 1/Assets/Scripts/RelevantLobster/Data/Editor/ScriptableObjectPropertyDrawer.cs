using JetBrains.Annotations;
using RelevantLobster.Editor.Utilities;
using RelevantLobster.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace RelevantLobster.Data.Editor
{
    /// <summary>
    /// Base <see cref="PropertyDrawer"/> for all <see cref="ScriptableObject"/>s which provides the common functionality
    /// used by all of them (e.g. create a new one inline with the field, open the Focused Inspector for the object,
    /// etc.).
    /// </summary>
    /// <typeparam name="T">The type of <see cref="ScriptableObject"/> this <see cref="PropertyDrawer"/> is for.</typeparam>
    public abstract class ScriptableObjectPropertyDrawer<T> : PropertyDrawer where T : ScriptableObject
    {
        /// <summary>
        /// Contains information about the current state of a <see cref="ScriptableObject"/> field being drawn.
        /// </summary>
        private sealed class DrawerData
        {
            public bool IsUserCreatingObject = false;
            public bool ShouldFocusNameInputField = false;
            public string NewObjectName = string.Empty;
            public string HelpBoxText = string.Empty;
            public MessageType HelpBoxType = MessageType.Warning;

            public bool ShouldShowHelpBox => HelpBoxText.Length > 0;
        }

        private sealed class DrawerRects
        {
            public Rect PropertyRect;
            public Rect LabelRect;
            public Rect NoBtnsFieldRect;
            public Rect OneBtnFieldRect;
            public Rect OneBtnRect;
            public Rect TwoBtnsFieldRect;
            public Rect LeftBtnRect;
            public Rect RightBtnRect;
            public Rect HelpBtnRect;
        }

        protected const int Standard_Gutter_Width = 2;
        protected const int Standard_Button_Width = 24;

        protected static readonly float LineHeight = EditorGUIUtility.singleLineHeight;
        protected static readonly float vertSpacing = EditorGUIUtility.standardVerticalSpacing;

        protected readonly Type ObjectType = typeof(T);

        private const string _input_field_name = "assetName";
        private const string _no_name_provided_msg_template = "Name of new {0} must be provided.";
        private const string _creation_failed_error = "Data Object creation failed. See Console window for details.";

        private static readonly Color s_createWorkFlowLabelColor = Color.yellow;

        [NotNull] private static readonly GUIContent s_createWorkFlowLabel = new("Data Object Name:");
        [NotNull] private static readonly GUIContent s_createBtnContent = new("+", "Create New");
        [NotNull] private static readonly GUIContent s_confirmBtnContent = new("✓", "Create");
        [NotNull] private static readonly GUIContent s_cancelBtnContent = new("✗", "Cancel");
        [NotNull] private static readonly StringBuilder s_stringBuilder = new();

        [NotNull]
        private static readonly GUIContent s_propertiesBtnContent = new(EditorGUIUtility.FindTexture("SettingsIcon"), "Properties");

        private readonly Dictionary<string, DrawerData> _newObjectDataByPropertyPath = new();

        /// <summary>
        /// Override this property to provide a user-friendly name for the type of <see cref="ScriptableObject"/> handled
        /// by this <see cref="ScriptableObjectPropertyDrawer{T}"/>, which is shown in the inspector in various cases.
        /// </summary>
        [NotNull] protected virtual string TypeName => ObjectType.Name;

        public override float GetPropertyHeight([NotNull] SerializedProperty property, GUIContent label)
        {
            // Account for multiple-object editing
            if (property.serializedObject.isEditingMultipleObjects) { return EditorGUI.GetPropertyHeight(property); }

            DrawerData data = GetDrawerData(property);

            if (data.ShouldShowHelpBox)
            {
                return base.GetPropertyHeight(property, label) // Regular property height
                    + LineHeight                            // Height for help box to display message
                    + vertSpacing * 2;                      // Vertical padding for both lines
            }

            return base.GetPropertyHeight(property, label); // Regular property height
        }

        public override void OnGUI(Rect position, [NotNull] SerializedProperty property, GUIContent label)
        {
            // Account for multi-object editing
            if (property.serializedObject.isEditingMultipleObjects)
            {
                EditorGUI.PropertyField(position, property, label, true);

                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            DrawerData data = GetDrawerData(property);
            DrawerRects rects = CalculatePositionRects(position, label.Equals(GUIContent.none) == false);

            if (false == data.IsUserCreatingObject) { DrawRegularWorkFlow(property, label, data, rects); }
            else { DrawCreationWorkFlow(property, label, data, rects); }

            // Auto-focusing the text input field has to be done after all the controls are drawn in order for it to work
            // because newly drawn controls after would take focus
            if (data.ShouldFocusNameInputField)
            {
                EditorGUI.FocusTextInControl(_input_field_name);
                data.ShouldFocusNameInputField = false;
            }

            EditorGUI.EndProperty();
        }

        private DrawerData GetDrawerData([NotNull] SerializedProperty property)
        {
            string path = property.propertyPath;

            if (false == _newObjectDataByPropertyPath.TryGetValue(path, out DrawerData data))
            {
                data = new DrawerData();
                _newObjectDataByPropertyPath[path] = data;
            }

            return data;
        }

        [NotNull]
        private static DrawerRects CalculatePositionRects(Rect position, bool shouldDrawLabel)
        {
            DrawerRects rects = new();

            // First line (property) and optional second line (help box)
            rects.HelpBtnRect = RectUtility.SplitRectV(position, out rects.PropertyRect, LineHeight, vertSpacing);

            float labelWidth = shouldDrawLabel ? EditorGUIUtility.labelWidth : 0;

            // Label and field with no buttons
            rects.NoBtnsFieldRect = RectUtility.SplitRectH(rects.PropertyRect, out rects.LabelRect, labelWidth, Standard_Gutter_Width);

            // Field with one button and associated button
            rects.OneBtnRect = RectUtility.SplitRectH(rects.NoBtnsFieldRect, out rects.OneBtnFieldRect, 
                rects.NoBtnsFieldRect.width - Standard_Button_Width, Standard_Gutter_Width);

            // Field with two buttons and associated button area
            Rect totalTwoBtnArea = RectUtility.SplitRectH(rects.NoBtnsFieldRect, out rects.TwoBtnsFieldRect,
                rects.NoBtnsFieldRect.width - (Standard_Button_Width * 2) + Standard_Gutter_Width, Standard_Gutter_Width);

            // Individual two buttons
            rects.RightBtnRect = RectUtility.SplitRectH(totalTwoBtnArea, out rects.LeftBtnRect,
                totalTwoBtnArea.width - Standard_Button_Width, Standard_Gutter_Width);

            return rects;
        }

        private static void DrawRegularWorkFlow([NotNull] SerializedProperty property, 
            GUIContent label, [NotNull] DrawerData data, [NotNull] DrawerRects rects)
        {
            // Label
            EditorGUI.HandlePrefixLabel(rects.PropertyRect, rects.LabelRect, label, GUIUtility.GetControlID(FocusType.Passive) + 2);

            // Have to still create the named input filed in order for the control IDs to match for both flows
            GUI.SetNextControlName(_input_field_name);
            data.NewObjectName = EditorGUI.TextField(Rect.zero, data.NewObjectName);

            #region Description and Path tooltip

            string objectFieldTooltip = string.Empty;

            if (property.objectReferenceValue is ScriptableObject dataObject)
            {
                // If I were to do the new scriptobject desc thing
                //string description = dataObject.Des

                string path = AssetDatabase.GetAssetPath(dataObject);

                s_stringBuilder.Clear();

                if (string.IsNullOrWhiteSpace(path) == false) { s_stringBuilder.Append(path); }

                objectFieldTooltip = s_stringBuilder.ToString();
            }

            #endregion

            // Object field
            EditorGUI.BeginChangeCheck();

            UnityEngine.Object obj = EditorGuiUtility.ObjectFieldWithTootip(rects.OneBtnFieldRect, 
                objectFieldTooltip, property.objectReferenceValue, typeof(T), false);

            if (EditorGUI.EndChangeCheck()) { property.objectReferenceValue = obj; }

            if (null == property.objectReferenceValue)
            {
                // Create button
                if (GUI.Button(rects.OneBtnRect, s_createBtnContent))
                {
                    data.NewObjectName = string.Empty;
                    data.IsUserCreatingObject = true;
                    data.ShouldFocusNameInputField = true;
                }
            }
            else
            {
                data.IsUserCreatingObject = false;

                // Properties button
                if (GUI.Button(rects.OneBtnRect, s_propertiesBtnContent))
                {
                    EditorUtility.OpenPropertyEditor(property.objectReferenceValue);
                }
            }
        }

        private void DrawCreationWorkFlow([NotNull] SerializedProperty property, GUIContent label, 
            [NotNull] DrawerData data, [NotNull] DrawerRects rects)
        {
            // Label
            Color origGuiColor = GUI.color;
            GUI.color = s_createWorkFlowLabelColor;
            EditorGUI.PrefixLabel(rects.PropertyRect, GUIUtility.GetControlID(FocusType.Passive), s_createWorkFlowLabel);
            GUI.color = origGuiColor;

            // Name input field
            GUI.SetNextControlName(_input_field_name);
            data.NewObjectName = EditorGUI.TextField(rects.TwoBtnsFieldRect, data.NewObjectName);

            bool isNameInputFocused = GUI.GetNameOfFocusedControl() == _input_field_name;
            bool wasReturnPressed = Event.current.keyCode == KeyCode.Return && isNameInputFocused;
            bool wasEscapePressed = Event.current.keyCode == KeyCode.Escape && isNameInputFocused;

            // Confirm Button
            if (GUI.Button(rects.LeftBtnRect, s_confirmBtnContent) || wasReturnPressed)
            {
                if (data.NewObjectName.Length > 0)
                {
                    try 
                    { 
                        CreateAsset(property, data);

                        data.IsUserCreatingObject = false;
                        data.HelpBoxText = string.Empty;
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);

                        data.HelpBoxText = _creation_failed_error;
                        data.HelpBoxType = MessageType.Error;
                        data.ShouldFocusNameInputField = true;
                    }  
                }
                else
                {
                    data.HelpBoxType = MessageType.Warning;
                    data.HelpBoxText = string.Format(_no_name_provided_msg_template, TypeName);
                    data.ShouldFocusNameInputField = true;
                }
            }

            // Cancel Button
            if (GUI.Button(rects.RightBtnRect, s_cancelBtnContent) || wasEscapePressed)
            {
                data.IsUserCreatingObject = false;
                data.HelpBoxText = string.Empty;
            }

            // Help box
            if (data.ShouldShowHelpBox) { EditorGUI.HelpBox(rects.HelpBtnRect, data.HelpBoxText, data.HelpBoxType); }
        }

        private static void CreateAsset([NotNull] SerializedProperty property, [NotNull] DrawerData data)
        {
            // Determine the path for the asset
            string assetPath = AssetDatabase.GetAssetPath(property.serializedObject.targetObject);
            assetPath = assetPath == string.Empty ? "Assets/" : Path.GetDirectoryName(assetPath) + "/";

            // Create the asset
            T asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, assetPath + data.NewObjectName + ".asset");
            AssetDatabase.SaveAssets();
            EditorGUIUtility.PingObject(asset);

            property.objectReferenceValue = asset;

        }
    }
}