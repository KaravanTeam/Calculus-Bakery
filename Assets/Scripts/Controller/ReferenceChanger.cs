using UnityEngine;
using UnityEngine.UI;
using View;

namespace Controller
{
    internal sealed class ReferenceChanger : MonoBehaviour
    {
        [SerializeField] private Sprite _enabledPointer;
        [SerializeField] private Sprite _disabledPointer;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private ReferencePage[] _pages;

        private int _position;

        private ReferencePage Current => _pages[_position];

        private void OnEnable()
        {
            _leftButton.onClick.AddListener(SetPreviousPage);
            _rightButton.onClick.AddListener(SetNextPage);
        }

        private void Start()
        {
            foreach (var page in _pages)
                page.Hide(_disabledPointer);

            Current.Show(_enabledPointer);
        }

        private void OnDisable()
        {
            _leftButton.onClick.RemoveListener(SetPreviousPage);
            _rightButton.onClick.RemoveListener(SetNextPage);
        }

        private void SetNextPage()
        {
            if (_position >= _pages.Length - 1)
                return;

            ToggleNextPage(1);
        }

        private void SetPreviousPage()
        {
            if (_position <= 0)
                return;

            ToggleNextPage(-1);
        }

        private void ToggleNextPage(int offset)
        {
            Current.Hide(_disabledPointer);

            _position += offset;
            Current.Show(_enabledPointer);
        }
    }
}
