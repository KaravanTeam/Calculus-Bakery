using System.Collections;
using UnityEngine;

namespace Model.Transporter
{
    [RequireComponent(typeof(Transform))]
    internal sealed class Platform : MonoBehaviour
    {
        public Equation Equation { get; set; }

        public IEnumerator MoveTo(Vector2 target, float time)
        {
            var start = transform.localPosition;
            var end = new Vector2(target.x, transform.localPosition.y);

            for (var t = 0f; t < 1; t += Time.deltaTime / time)
            {
                var easingTime = t < 0.5 ? t * t * 2 : 1 - (1 - t) * (1 - t) * 2;

                transform.localPosition = Vector2.Lerp(start, end, easingTime);

                yield return null;
            }

            transform.localPosition = end;
        }
    }
}
