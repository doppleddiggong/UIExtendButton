using UnityEditor;
using UnityEditor.UI;

namespace DoppleLittleHelper
{
    [CustomEditor(typeof(DoppleLittleHelper.ExtendButton))]
    [CanEditMultipleObjects]
    public class ExtendButtonEditor : ButtonEditor
    {
        SerializedProperty onHoverEnterProperty;
        SerializedProperty onHoverExitProperty;

        SerializedProperty onPressProperty;
        SerializedProperty onReleaseProperty;

        SerializedProperty useAudioSourceProperty;
        SerializedProperty hoverSoundProperty;
        SerializedProperty clickSoundProperty;
        SerializedProperty audioSoundProperty;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("[Extend Button Property]", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(onHoverEnterProperty);
            EditorGUILayout.PropertyField(onHoverExitProperty);

            EditorGUILayout.PropertyField(onPressProperty);
            EditorGUILayout.PropertyField(onReleaseProperty);

            EditorGUILayout.PropertyField(useAudioSourceProperty);
            EditorGUILayout.PropertyField(audioSoundProperty);
            EditorGUILayout.PropertyField(hoverSoundProperty);
            EditorGUILayout.PropertyField(clickSoundProperty);

            serializedObject.ApplyModifiedProperties();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            onHoverEnterProperty = serializedObject.FindProperty("onHoverEnter");
            onHoverExitProperty = serializedObject.FindProperty("onHoverExit");
            onPressProperty = serializedObject.FindProperty("onPress");
            onReleaseProperty = serializedObject.FindProperty("onRelease");

            useAudioSourceProperty = serializedObject.FindProperty("useAudioSource");
            audioSoundProperty = serializedObject.FindProperty("audioSource");
            hoverSoundProperty = serializedObject.FindProperty("hoverSound");
            clickSoundProperty = serializedObject.FindProperty("clickSound");
        }
    }
}