using Model.Transporter;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [RequireComponent(typeof(Button))]
    internal sealed class TransitionButton : MonoBehaviour
    {
        [SerializeField] private Direction _direction;
        [SerializeField] private Transporter _transporter;

        private Button _button;

        private bool _isInteractable = true;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            _transporter.OnPlatformMovingStarted += SetDisabledState;
            _transporter.OnPlatformMovingEnded += SetEnabledState;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            _transporter.OnPlatformMovingStarted -= SetDisabledState;
            _transporter.OnPlatformMovingEnded -= SetEnabledState;
        }

        private void OnClick()
        {
            if (!_isInteractable)
                return;

            _transporter.TryMoveTowards(_direction);          
        }

        private void SetEnabledState(PipeType pipe)
        {
            _isInteractable = true;
        }
        private void SetDisabledState(PipeType pipe)
        {
            _isInteractable = false;
        }
    }
}
