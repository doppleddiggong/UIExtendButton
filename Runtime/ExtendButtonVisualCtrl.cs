using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DoppleLittleHelper
{
    [ExecuteInEditMode]
    public class ExtendButtonVisualCtrl : MonoBehaviour
    {
        enum STATE_TYPE : byte
        {
            NORMAL,
            HOVER_ENTER,
            ON_PRESS,
        }

        [Header("[Target]")]
        [SerializeField] Button targetButton;
        [SerializeField] Graphic targetGraphic;
        [SerializeField] List<Graphic> childSymbolList = new();

        [Header("[Color Tint]")]
        [SerializeField] float highlight_Lerp_Factor = 0.5f;
        [SerializeField] float pressed_Lerp_Factor = 0.7f;
        [SerializeField] float disabled_Alpha_Factor = 0.5f;

        [SerializeField] Color highlight_Target_Color = Color.gray;
        [SerializeField] Color pressed_Target_Color = Color.black;

        [Header("[Symbol Color]")]
        [SerializeField] Color normal_symbol_color = Color.black;
        [SerializeField] Color hover_enter_symbol_color = Color.white;
        [SerializeField] Color press_symbol_color = Color.white;

        Color GetHighlightColor(Color originalColor) => Color.Lerp(originalColor, highlight_Target_Color, highlight_Lerp_Factor);
        Color GetPressedColor(Color originalColor) => Color.Lerp(originalColor, pressed_Target_Color, pressed_Lerp_Factor);
        Color GetDisabledColor(Color originalColor)
        {
            Color disabledColor = GetPressedColor(originalColor);
            disabledColor.a *= disabled_Alpha_Factor;
            return disabledColor;
        }

        Color GetStateColor(STATE_TYPE color_type) => color_type switch
        {
            STATE_TYPE.NORMAL => normal_symbol_color,
            STATE_TYPE.HOVER_ENTER => hover_enter_symbol_color,
            STATE_TYPE.ON_PRESS => press_symbol_color,
            _ => Color.white
        };

        void Awake() => InitButton();

        void InitButton()
        {
            if (targetButton == null)
            {
                targetButton = GetComponent<Button>();

                if(targetButton == null)
                    targetButton = this.gameObject.AddComponent<Button>();
            }

            if (targetButton != null && targetGraphic == null)
                targetGraphic = targetButton.targetGraphic;

            if (childSymbolList.Count == 0)
            {
                var childSymbolArray = GetComponentsInChildren<Graphic>();
                foreach (var item in childSymbolArray)
                {
                    if (item == targetGraphic)
                        continue;

                    childSymbolList.Add(item);
                }
            }

            SetNormalState();
        }

        [ContextMenu("RecommandTintColor")]
        public void RecommandTintColor()
        {
            if (targetGraphic == null)
            {
                Debug.LogWarning("TargetGraphic is not assigned.");
                return;
            }

            Color originalColor = targetGraphic.color;

            ColorBlock colorBlock = targetButton.colors;

            colorBlock.normalColor = originalColor;
            colorBlock.highlightedColor = GetHighlightColor(originalColor);
            colorBlock.pressedColor = GetPressedColor(originalColor);
            colorBlock.selectedColor = GetHighlightColor(originalColor);
            colorBlock.disabledColor = GetDisabledColor(originalColor);

            targetButton.colors = colorBlock;

            normal_symbol_color = GetPressedColor(targetGraphic.color);
            hover_enter_symbol_color = new Color(0.196f, 0.200f, 0.133f);   // HexColor : #323333
            press_symbol_color = new Color(0.784f, 0.784f, 0.784f);         // HexColor : #C8C8C8
        }

        [ContextMenu("SetNormalState")]
        public void SetNormalState()
        {
            if (targetGraphic == null)
            {
                Debug.LogWarning("TargetGraphic is not assigned.");
                return;
            }

            var targetColor = GetStateColor(STATE_TYPE.NORMAL);
            foreach (var item in childSymbolList)
                item.color = targetColor;
        }

        public void OnHoverEnter()
        {
            var targetColor = GetStateColor(STATE_TYPE.HOVER_ENTER);
            foreach (var item in childSymbolList)
                item.color = targetColor;
        }

        public void OnHoverExit()
        {
            var targetColor = GetStateColor(STATE_TYPE.NORMAL);
            foreach (var item in childSymbolList)
                item.color = targetColor;
        }

        public void OnPress()
        {
            var targetColor = GetStateColor(STATE_TYPE.ON_PRESS);
            foreach (var item in childSymbolList)
                item.color = targetColor;
        }

        public void OnRelease()
        {
            var targetColor = GetStateColor(STATE_TYPE.NORMAL);
            foreach (var item in childSymbolList)
                item.color = targetColor;
        }
    }
}
