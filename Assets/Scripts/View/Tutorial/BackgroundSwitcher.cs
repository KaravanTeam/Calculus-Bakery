using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Image))]
    internal sealed class BackgroundSwitcher : MonoBehaviour
    {
        private Image _background;

        private void Awake()
        {
            _background = GetComponent<Image>();
        }

        public void SwitchOn()
        {
            _background.enabled = true;
        }

        public void SwitchOff()
        {
            _background.enabled = false;
        }
    }
}
