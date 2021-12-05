using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [RequireComponent(typeof(Animator))]
    internal sealed class ReferencePanelMover : MonoBehaviour
    {
        [SerializeField] private Button _entryButton;
        [SerializeField] private Button _exitButton;

        private Animator _panelAnimator;

        private readonly string _entryTrigger = "IsEntry";
        private readonly string _exitTrigger = "IsExit";

        private void Awake()
        {
            _panelAnimator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _entryButton.onClick.AddListener(PlayEntryAnimation);
            _exitButton.onClick.AddListener(PlayExitAnimation);
        }

        private void OnDisable()
        {
            _entryButton.onClick.RemoveListener(PlayEntryAnimation);
            _exitButton.onClick.RemoveListener(PlayExitAnimation);
        }

        private void PlayEntryAnimation()
        {
            _panelAnimator.SetTrigger(_entryTrigger);
        }

        private void PlayExitAnimation()
        {
            _panelAnimator.SetTrigger(_exitTrigger);
        }
    }
}
