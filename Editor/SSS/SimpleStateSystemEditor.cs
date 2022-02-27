//-----------------------------------------------------------------------
// <copyright file="SimpleStateSystemEditor.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost.SSS
{
    using System;
    using System.Linq;
    using Lost.EditorGrid;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(SimpleStateSystem))]
    public class SimpleStateSystemEditor : Editor
    {
        private static readonly Type[] ActionTypes;
        private static readonly string[] ActionNames;
        private static int selectedActionIndex = 0;

        static SimpleStateSystemEditor()
        {
            ActionTypes = BuildConfig.TypeUtil.GetAllTypesOf<Action>().ToArray();

            ActionNames = new string[ActionTypes.Length];

            for (int i = 0; i < ActionTypes.Length; i++)
            {
                var action = Activator.CreateInstance(ActionTypes[i]) as Action;
                ActionNames[i] = action.DisplayName;
            }
        }

        public override void OnInspectorGUI()
        {
            // Early out if no types were found
            if (ActionTypes == null || ActionNames.Length == 0)
            {
                return;
            }

            var simpleStateSystem = this.target as SimpleStateSystem;
            var statesProperty = this.serializedObject.FindProperty("states");

            // Drawing all out states
            int foldoutId = 45682315;
            for (int stateIndex = 0; stateIndex < simpleStateSystem.States.Count; stateIndex++)
            {
                var state = simpleStateSystem.States[stateIndex];

                using (new FoldoutScope(foldoutId++, state.Name, out bool visible, out Rect position))
                {
                    if (visible)
                    {
                        this.DrawState(state, statesProperty.GetArrayElementAtIndex(stateIndex));
                    }

                    if (this.DeleteStateButton(statesProperty, simpleStateSystem, stateIndex, position))
                    {
                        break;
                    }
                }
            }

            // Adding the Add State button
            if (GUILayout.Button("Add State", GUILayout.Width(EditorGUIUtility.currentViewWidth - 25)))
            {
                statesProperty.AddElementToArray(simpleStateSystem.States, new State());
            }

            this.serializedObject.ApplyModifiedProperties();
        }

        private void DrawState(State state, SerializedProperty stateProperty)
        {
            string newName = EditorGUILayout.TextField("Name", state.Name);
            if (newName != state.Name)
            {
                state.Name = newName;
            }

            bool newRepeat = EditorGUILayout.Toggle("Repeat", state.Repeat);
            if (newRepeat != state.Repeat)
            {
                state.Repeat = newRepeat;
            }

            bool newMustUpdate = EditorGUILayout.Toggle("Must Update Every Frame", state.MustUpdateEveryFrame);
            if (newMustUpdate != state.MustUpdateEveryFrame)
            {
                state.MustUpdateEveryFrame = newMustUpdate;
            }

            EditorGUILayout.Space();

            // Drawing all the actions
            var actionsProperty = stateProperty.FindPropertyRelative("actions");

            if (state.Actions?.Count > 0)
            {
                int actionFoldoutId = 8974135;
                for (int actionIndex = 0; actionIndex < state.Actions.Count; actionIndex++)
                {
                    SerializedProperty actionProperty = actionsProperty.GetArrayElementAtIndex(actionIndex);
                    Action action = state.Actions[actionIndex];

                    // Drawing the Action
                    string title = action != null ?
                        string.IsNullOrEmpty(action.Description) ? action.DisplayName : $"{action.DisplayName} - {action.Description}" :
                        "NULL";

                    using (new FoldoutScope(actionFoldoutId++, title, out bool visible, out Rect position))
                    {
                        if (visible)
                        {
                            this.DrawActionProperties(actionProperty, actionIndex);
                        }

                        if (this.DeleteActionButton(actionsProperty, actionIndex, position))
                        {
                            break;
                        }
                    }
                }
            }

            EditorGUILayout.Space();

            // Drawing the Add Action Button
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label("Add Action", GUILayout.Width(70));

                selectedActionIndex = EditorGUILayout.Popup(selectedActionIndex, ActionNames, GUILayout.Width(EditorGUIUtility.currentViewWidth - 160));

                if (GUILayout.Button("+", GUILayout.Width(50), GUILayout.Height(18)))
                {
                    var action = Activator.CreateInstance(ActionTypes[selectedActionIndex]) as Action;
                    action.Initialize();

                    actionsProperty.AddElementToArray(state.Actions, action);
                }
            }
        }

        private bool DeleteStateButton(SerializedProperty statesProperty, SimpleStateSystem simpleStateSystem, int stateIndex, Rect position)
        {
            Rect deleteButtonRect = position;
            deleteButtonRect.x = position.x + position.width - 18;
            deleteButtonRect.y += 2.0f;
            deleteButtonRect.width = 14;
            deleteButtonRect.height = 14;

            if (ButtonUtil.DrawDeleteButton(deleteButtonRect))
            {
                statesProperty.DeleteArrayElementAtIndex(stateIndex);
                return true;
            }

            return false;
        }

        private bool DeleteActionButton(SerializedProperty actionsProperty, int actionIndex, Rect position)
        {
            Rect deleteButtonRect = position;
            deleteButtonRect.x = position.x + position.width - 18;
            deleteButtonRect.y += 2.0f;
            deleteButtonRect.width = 14;
            deleteButtonRect.height = 14;

            if (ButtonUtil.DrawDeleteButton(deleteButtonRect))
            {
                actionsProperty.DeleteArrayElementAtIndex(actionIndex);
                return true;
            }

            return false;
        }

        private void DrawActionProperties(SerializedProperty prop, int actionIndex)
        {
            bool isFirstProperty = true;
            string propertyProcessed = null;

            while (prop.NextVisible(true))
            {
                // This method will eventually start drawing other elements in the array, so early out if that happens
                if (prop.propertyPath.Contains($"actions.Array.data[{actionIndex}]") == false)
                {
                    break;
                }

                // Making sure we don't re-render properties
                if (propertyProcessed != null && prop.propertyPath.StartsWith(propertyProcessed))
                {
                    continue;
                }
                else
                {
                    propertyProcessed = prop.propertyPath;
                }

                using (new IndentLevelScope(isFirstProperty || prop.isArray == false ? 0 : 1))
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PropertyField(prop, true);
                }

                isFirstProperty = false;
            }
        }
    }
}
