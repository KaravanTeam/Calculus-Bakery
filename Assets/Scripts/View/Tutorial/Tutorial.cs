using Model;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Button))]
    internal sealed class Tutorial : MonoBehaviour
    {
        [SerializeField] private Chef _chef;

        [SerializeField] private TutorialPanel[] _panels;
        [SerializeField] private Button[] _hudButtons;

        [Header("Tutorial panel")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _skipButton;
        [SerializeField] private GameObject _tutorialPanel;

        private void Awake()
        {
            _tutorialPanel.SetActive(true);
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(() => StartCoroutine(Run()));
            _skipButton.onClick.AddListener(Skip);
        }

        private void Start()
        {
            DoButtonsInteractable(false);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveAllListeners();
            _skipButton.onClick.RemoveAllListeners();
        }

        private void Skip()
        {
            _tutorialPanel.SetActive(false);
            DoButtonsInteractable();
        }

        private IEnumerator Run()
        {
            _tutorialPanel.SetActive(false);
            
            _chef.DistributeTutorial();
        
            foreach (var panel in _panels)
            {
                panel.gameObject.SetActive(true);

                yield return StartCoroutine(panel.Run());

                panel.gameObject.SetActive(false);
            }

            DoButtonsInteractable();
        }

        private void DoButtonsInteractable(bool isInteractable = true)
        {
            foreach (var button in _hudButtons)
                button.interactable = isInteractable;
        }
    }
}
