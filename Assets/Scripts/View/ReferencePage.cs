using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Image))]
    internal sealed class ReferencePage : MonoBehaviour
    {
        [SerializeField] private Image _pointer;

        private Image _page;

        private readonly float _transparentPointer = 0.5f;

        private void Awake()
        {
            _page = GetComponent<Image>();
        }

        public void Show()
        {
            _page.enabled = true;

            SetPointerAlpha(1);
        }

        public void Hide()
        {
            _page.enabled = false;

            SetPointerAlpha(_transparentPointer);
        }

        private void SetPointerAlpha(float value)
        {
            var color = _pointer.color;

            color.a = value;

            _pointer.color = color;
        }
    }
}
