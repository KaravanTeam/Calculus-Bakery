using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal sealed class ButtonsLocker : MonoBehaviour
    {
        [SerializeField] private Button[] _buttons;

        public void Lock()
        {
            SetInteractable(false);
        }

        public void Unlock()
        {
            SetInteractable(true);
        }

        private void SetInteractable(bool state)
        {
            foreach (var button in _buttons)
                button.enabled = state;
        }
    }
}
