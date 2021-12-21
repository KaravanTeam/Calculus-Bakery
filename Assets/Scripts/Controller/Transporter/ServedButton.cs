using Model.Transporter;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [RequireComponent(typeof(Button))]
    internal sealed class ServedButton : MonoBehaviour
    {
        [SerializeField] private Transporter _transporter;
        [SerializeField] private CheckerButton _checkerButton;

        private Button _button;

        private bool _isInteractable = true;

        public event Action OnCreamServed;

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

        private void OnClick()
        {
            if (!_isInteractable)
                return;

            var solution = _transporter.BuildSolution();
            _transporter.ServicedPipe.Drop.FallDown();
            
            _checkerButton.SaveSolution(solution);
            _checkerButton.SetEnabledState();

            OnCreamServed?.Invoke();

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
