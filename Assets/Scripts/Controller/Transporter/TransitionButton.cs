using Model.Transporter;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [RequireComponent(typeof(Button))]
    internal class TransitionButton : TransporterManipulationButton
    {
        [SerializeField] private Direction _direction;

        private Transporter _transporter;

        private Button _button;
        private ServeCreamButton _serveButton;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void Start()
        {
            _transporter = FindObjectOfType<Transporter>();
            _serveButton = FindObjectOfType<ServeCreamButton>();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        protected override void OnClick()
        {
            _transporter.TryMoveTowards(_direction);

            if (_direction == Direction.Right
                && _transporter.ServicedPipe == PipeType.Left)
            {
                _serveButton.SetState(ButtonState.Enabled);
            }
        }

        protected override void SetEnabledState()
        {
            _button.interactable = true;
        }

        protected override void SetDisabledState()
        {
            _button.interactable = false;
        }
    }
}
