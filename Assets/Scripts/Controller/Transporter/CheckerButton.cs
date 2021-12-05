using Model;
using Model.Transporter;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [RequireComponent(typeof(Button))]
    internal sealed class CheckerButton : MonoBehaviour
    {
        [SerializeField] private Chef _chef;
        [SerializeField] private Transporter _transporter;
        [SerializeField] private ServedButton _servedButton;

        private Button _button;

        private Cake _actualSolution;
        private bool _isInteractable = true;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            _transporter.OnPlatformMovingStarted += IsMovingPlatform;
            _transporter.OnPlatformMovingEnded += IsNotMovingPlatform;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            _transporter.OnPlatformMovingStarted -= IsMovingPlatform;
            _transporter.OnPlatformMovingEnded -= IsNotMovingPlatform;
        }

        public void SetEnabledState()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnClick);
        }

        public void SetDisabledState()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void SaveSolution(Cake solution)
        {
            _actualSolution = solution;
        }

        private void OnClick()
        {
            if (!_isInteractable)
                return;

            _chef.CheckSolution(_actualSolution);
            StartCoroutine(_transporter.ResetPlatform());

            _servedButton.SetEnabledState();
            SetDisabledState();
        }

        private void IsMovingPlatform(PipeType pipe)
        {
            _isInteractable = false;
        }

        private void IsNotMovingPlatform(PipeType pipe)
        {
            _isInteractable = true;
        }
    }
}
