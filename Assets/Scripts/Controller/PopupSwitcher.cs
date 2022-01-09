using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [RequireComponent(typeof(Animator))]
    internal sealed class PopupSwitcher : MonoBehaviour
    {
        [SerializeField] private Image _background;

        [SerializeField] private string _entryTrigger;
        [SerializeField] private Button _entryButton;

        [SerializeField] private string _exitTrigger;
        [SerializeField] private Button _exitButton;

        private Animator _panelAnimator;

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
            _background.enabled = true;
            _panelAnimator.SetTrigger(_entryTrigger);
        }

        private void PlayExitAnimation()
        {
            _background.enabled = false;
            _panelAnimator.SetTrigger(_exitTrigger);
        }
    }
}
