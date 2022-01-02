using System.Collections;
using UnityEngine;

namespace Model.Transporter
{
    [RequireComponent(typeof(Transform))]
    internal sealed class Platform : MonoBehaviour
    {
        [SerializeField] private float _transitionTime;
        [SerializeField] private SpriteRenderer[] _wheels;

        public Equation Equation { get; set; }

        public IEnumerator MoveTo(Vector2 target)
        {
            var start = transform.position;
            var end = new Vector2(target.x, transform.position.y);

            var previousPos = (Vector2)start;
            var direction = (previousPos - end).normalized;

            for (var t = 0f; t < 1; t += Time.deltaTime / _transitionTime)
            {
                var easingTime = t < 0.5 ? t * t * 2 : 1 - (1 - t) * (1 - t) * 2;

                var currentPosition = Vector2.Lerp(start, end, easingTime);
                transform.position = currentPosition;

                var delta = (currentPosition - previousPos).magnitude;
                foreach (var wheel in _wheels)
                {
                    var angle = delta / wheel.size.x * 360 / Mathf.PI;

                    wheel.transform.localRotation *= Quaternion.AngleAxis(direction.x * angle, Vector3.forward);
                }

                yield return null;

                previousPos = currentPosition;
            }

            transform.position = end;
        }
    }
}
