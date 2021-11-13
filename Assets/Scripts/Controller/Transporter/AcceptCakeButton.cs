using Model.Transporter;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [RequireComponent(typeof(Button))]
    internal class AcceptCakeButton : TransporterManipulationButton
    {
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
            _button.interactable = false;
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
            if (_transporter.TryMoveToDefault())
            {
                _serveButton.SetState(ButtonState.Disabled);

                SetDisabledState();
            }
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
