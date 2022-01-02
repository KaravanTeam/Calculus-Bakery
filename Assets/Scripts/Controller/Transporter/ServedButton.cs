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

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            _transporter.OnReseted += ResetHandlers;
            _transporter.OnPlatformMovingStarted += SetDisabled;
            _transporter.OnPlatformMovingEnded += SetEnabled;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            _transporter.OnReseted -= ResetHandlers;
            _transporter.OnPlatformMovingStarted -= SetDisabled;
            _transporter.OnPlatformMovingEnded -= SetEnabled;
        }

        private void ResetHandlers()
        {
            _transporter.OnPlatformMovingStarted += SetDisabled;
            _transporter.OnPlatformMovingEnded += SetEnabled;
        }

        private void OnClick()
        {
            var solution = _transporter.BuildSolution();
            _transporter.ThrowWaterDrop();
          
            _checkerButton.SaveSolution(solution);

            _button.onClick.RemoveListener(OnClick);
            _transporter.OnPlatformMovingStarted -= SetDisabled;
            _transporter.OnPlatformMovingEnded -= SetEnabled;
        }

        private void SetEnabled(PipeType pipe)
        {
            _button.onClick.AddListener(OnClick);
        }

        private void SetDisabled(PipeType pipe)
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}
