using Model;
using Model.Transporter;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Controller
{
    [RequireComponent(typeof(Button))]
    internal sealed class CheckerButton : MonoBehaviour
    {
        [SerializeField] private Chef _chef;
        [SerializeField] private Transporter _transporter;
        [SerializeField] private CreamContainer _creamContainer;

        private Button _button;

        private Cake _actualSolution;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _creamContainer.OnCreamPlaced += SetWaitState;
        }

        private void OnDisable()
        {
            _creamContainer.OnCreamPlaced -= UnsetWaitState;
        }

        public void SaveSolution(Cake solution)
        {
            _actualSolution = solution;
        }

        private void OnClick()
        {
            _chef.CheckSolution(_actualSolution);
            StartCoroutine(_transporter.ResetPlatform());

            UnsetWaitState();
        }

        private void SetWaitState()
        {
            _button.onClick.AddListener(OnClick);
            _transporter.OnPlatformMovingStarted += SetDisabled;
            _transporter.OnPlatformMovingEnded += SetEnabled;
        }

        private void UnsetWaitState()
        {
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
