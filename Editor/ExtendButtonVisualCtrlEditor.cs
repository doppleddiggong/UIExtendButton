using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DoppleLittleHelper.ExtendButtonVisualCtrl))]
public class ExtendButtonVisualCtrlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // 대상 스크립트 가져오기
        DoppleLittleHelper.ExtendButtonVisualCtrl script = (DoppleLittleHelper.ExtendButtonVisualCtrl)target;

        // Inspector 버튼 추가
        if (GUILayout.Button("Set Normal State"))
        {
            script.SetNormalState();
        }

        if (GUILayout.Button("Recommand Tint Color"))
        {
            script.RecommandTintColor();
        }
    }
}
