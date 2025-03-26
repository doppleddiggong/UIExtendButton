using UnityEngine;
using System.Collections;

namespace DoppleLittleHelper
{
    public class UIElementScaler : MonoBehaviour
    {
        [SerializeField] float targetScaleMultiplier = 1.1f;
        [SerializeField] float animationDuration = 0.5f;

        Vector3 originalScale;
        Coroutine scaleCoroutine;

        void Start() => originalScale = transform.localScale;

        public void ScaleUp()
        {
            if (scaleCoroutine != null)
                StopCoroutine(scaleCoroutine);

            scaleCoroutine = StartCoroutine(ScaleOverTime(originalScale * targetScaleMultiplier, animationDuration));
        }

        public void ScaleDown()
        {
            if (scaleCoroutine != null)
                StopCoroutine(scaleCoroutine);

            scaleCoroutine = StartCoroutine(ScaleOverTime(originalScale, animationDuration));
        }

        IEnumerator ScaleOverTime(Vector3 targetScale, float duration)
        {
            Vector3 startScale = transform.localScale;
            float time = 0f;

            while (time < duration)
            {
                float t = time / duration;          // 정규화된 시간 (0~1)
                float easedT = ElasticEaseOut(t);   // OutElastic 적용
                transform.localScale = Vector3.LerpUnclamped(startScale, targetScale, easedT);

                time += Time.deltaTime;
                yield return null;
            }

            transform.localScale = targetScale;
        }

        private float ElasticEaseOut(float time)
        {
            if (time == 0 || time == 1) return time;

            float period = 0.3f;
            return Mathf.Pow(2, -10 * time) * Mathf.Sin((time - period / 4) * (2 * Mathf.PI) / period) + 1;
        }
    }
}