using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Button))]
    internal sealed class Tutorial : MonoBehaviour
    {
        [SerializeField] private TutorialPanel[] _panels;

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _skipButton;
        [SerializeField] private GameObject _tutorialPanel;

        //[Header("Backgrounds")]
        //[SerializeField] private SpriteRenderer _backgroundScene;
        //[SerializeField] private Image _backgroundCanvas;

        [SerializeField] private Button[] _mainButtons;

        private void Awake()
        {
            //_backgroundScene.enabled = true;
            _tutorialPanel.SetActive(true);
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(() => StartCoroutine(Run()));
            _skipButton.onClick.AddListener(Skip);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveAllListeners();
            _skipButton.onClick.RemoveAllListeners();
        }

        private void Skip()
        {
            _tutorialPanel.SetActive(false);
        }

        private IEnumerator Run()
        {
            _tutorialPanel.SetActive(false);
            foreach (var button in _mainButtons)
                button.interactable = false;

            foreach (var panel in _panels)
            {
                panel.gameObject.SetActive(true);

                yield return StartCoroutine(panel.Run());

                panel.gameObject.SetActive(false);
            }
        }
    }
}
