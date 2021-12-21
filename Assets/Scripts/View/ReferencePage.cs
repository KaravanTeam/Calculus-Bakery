using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Image))]
    internal sealed class ReferencePage : MonoBehaviour
    {
        [SerializeField] private Image _pointer;

        private Image _page;

        private void Awake()
        {
            _page = GetComponent<Image>();
        }

        public void Show(Sprite enabledPointer)
        {
            _page.enabled = true;

            _pointer.sprite = enabledPointer;
            _pointer.SetNativeSize();
        }

        public void Hide(Sprite disabledPointer)
        {
            _page.enabled = false;

            _pointer.sprite = disabledPointer;
            _pointer.SetNativeSize();
        }
    }
}
