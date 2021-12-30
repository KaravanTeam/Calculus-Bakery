using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(CongratulationReporter))]
    internal sealed class CongratulationPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;

        [SerializeField] private Image _goodCakes;
        [SerializeField] private Image _badCakes;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Text _field;

        private CongratulationReporter _reporter;

        private bool _next;

        private void Awake()
        {
            _reporter = GetComponent<CongratulationReporter>();
        }

        private void OnEnable()
        {
            _nextButton.onClick.AddListener(ChangeMessage);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(ChangeMessage);
        }

        public void Show()
        {
            StartCoroutine(ShowAnimation());
        }

        private IEnumerator ShowAnimation()
        {
            MessageInfo info;

            while ((info = _reporter.NextMessage()) != null)
            {
                _panel.SetActive(true);
                _next = false;

                _goodCakes.enabled = info.AchievementPoints > 0;
                _badCakes.enabled = info.AchievementPoints <= 0;

                _field.text = info.Text;

                yield return null;
                yield return new WaitWhile(() => !_next);

                _panel.SetActive(false);
            }
        }

        private void ChangeMessage()
        {
            _next = true;
        }
    }
}
