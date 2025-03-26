using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DoppleLittleHelper
{
    public class ExtendButton : Button
    {
        [Header("[Hover Events]")]
        [SerializeField] UnityEvent onHoverEnter = new();
        [SerializeField] UnityEvent onHoverExit = new();

        [Header("[Press Events]")]
        [SerializeField] UnityEvent onPress = new();
        [SerializeField] UnityEvent onRelease = new();

        [Header("[Audio Properties]")]
        [SerializeField] bool useAudioSource = true;
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip hoverSound;
        [SerializeField] AudioClip clickSound;

        bool isHovering;
        bool isPressed;

        public UnityEvent OnHoverEnter => onHoverEnter;
        public UnityEvent OnHoverExit => onHoverExit;
        public UnityEvent OnPress => onPress;
        public UnityEvent OnRelease => onRelease;

        protected override void Awake()
        {
            base.Awake();

            if (useAudioSource)
            {
                if (audioSource == null)
                {
                    audioSource = GetComponent<AudioSource>();

                    if (audioSource == null)
                    {
                        audioSource = this.gameObject.AddComponent<AudioSource>();
                        audioSource.playOnAwake = false;
                    }
                }
            }
        }

        void PlaySound(AudioClip audioClip)
        {
            if (useAudioSource == false)
                return;

            if (audioClip != null && audioSource != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            if (isHovering == false)
            {
                isHovering = true;
                PlaySound(hoverSound);

                onHoverEnter.Invoke();
            }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (isHovering)
            {
                isHovering = false;
                onHoverExit.Invoke();
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            PlaySound(clickSound);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (isPressed == false)
            {
                isPressed = true;
                onPress.Invoke();
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (isPressed)
            {
                isPressed = false;
                onRelease.Invoke();
            }
        }
    }
}