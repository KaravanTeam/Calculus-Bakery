using Model.Chef;
using Model.Transporter;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [RequireComponent(typeof(Button))]
    internal class AcceptCakeButton : TransporterManipulationButton
    {
        private Transporter _transporter;
        private Factory _factory;
        private Chef _chef;

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
            _factory = FindObjectOfType<Factory>();
            _chef = FindObjectOfType<Chef>();

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
                Debug.Log(_chef.IsGoodCake(_factory.FinishedCake));
                _factory.Distribute();
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
