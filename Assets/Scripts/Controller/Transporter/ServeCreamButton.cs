using Model.Transporter;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [RequireComponent(typeof(Button))]
    internal class ServeCreamButton : TransporterManipulationButton
    {
        public Cake FinishedCake { get; private set; }

        private Transporter _transporter;
        private Factory _factory;

        private Button _button;
        private AcceptCakeButton _acceptButton;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            _button.interactable = false;
        }

        private void Start()
        {
            _transporter = FindObjectOfType<Transporter>();
            _factory = FindObjectOfType<Factory>();
            _acceptButton = FindObjectOfType<AcceptCakeButton>();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        protected override void OnClick()
        {
            if (_transporter.IsMoving)
                return;

            FinishedCake = _factory.BuildCake(_transporter.ServicedPipe);

            _acceptButton.SetState(ButtonState.Enabled);

            SetDisabledState();
        }

        protected override void SetDisabledState()
        {
            _button.interactable = false;
        }

        protected override void SetEnabledState()
        {
            _button.interactable = true;
        }
    }
}
