using Model;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(ButtonsLocker))]
    internal sealed class Tutorial : MonoBehaviour
    {
        [SerializeField] private Chef _chef;

        [SerializeField] private TutorialPanel[] _panels;

        [Header("Tutorial panel")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _skipButton;
        [SerializeField] private GameObject _tutorialPanel;

        private ButtonsLocker _locker;

        private void Awake()
        {
            _tutorialPanel.SetActive(true);
            _locker = GetComponent<ButtonsLocker>();
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(() => StartCoroutine(Run()));
            _skipButton.onClick.AddListener(Skip);
        }

        private void Start()
        {
            _locker.Lock();
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveAllListeners();
            _skipButton.onClick.RemoveAllListeners();
        }

        private void Skip()
        {
            _tutorialPanel.SetActive(false);
            _locker.Unlock();
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

            _locker.Unlock();
        }
    }
}
