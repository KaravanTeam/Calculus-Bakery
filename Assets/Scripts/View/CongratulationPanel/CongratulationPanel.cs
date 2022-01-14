using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(CongratulationReporter))]
    internal sealed class CongratulationPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private ChefEmoji _chefEmoji;
        [SerializeField] private Button _nextButton;
        [SerializeField] private BackgroundSwitcher _backgroundSwitcher;

        [Header("Achievement")]
        [SerializeField] private GameObject _achievementContainer;
        [SerializeField] private Image _goodCakes;
        [SerializeField] private Image _badCakes;
        [SerializeField] private Text _achievementField;

        [Header("GameOver")]
        [SerializeField] private GameObject _gameOverContainter;
        [SerializeField] private Text _gameOverField;

        private CongratulationReporter _reporter;
        private bool _next;

        private void Awake()
        {
            _reporter = GetComponent<CongratulationReporter>();
        }

        private void OnEnable()
        {
            _chefEmoji.OnClosed += Show;
            _nextButton.onClick.AddListener(ChangeMessage);
        }

        private void OnDisable()
        {
            _chefEmoji.OnClosed -= Show;
            _nextButton.onClick.RemoveListener(ChangeMessage);
        }

        private void Start()
        {
            _achievementContainer.SetActive(false);
            _gameOverContainter.SetActive(false);
        }

        public void Show()
        {
            StartCoroutine(ShowAnimation());
        }

        private IEnumerator ShowAnimation()
        {
            foreach (var message in _reporter.Messages)
            {
                _backgroundSwitcher.SwitchOn();
                _panel.SetActive(true);
                _next = false;

                switch (message.Type)
                {
                    case MessageType.Achievement:
                    {
                        yield return ShowAchievementCongratulation(message);
                        break;
                    }

                    case MessageType.GameOver:
                    {
                        yield return ShowGameOver(message);
                        break;
                    }

                    default:
                        throw new System.Exception($"Unknown congratulation type {message.Type}");
                }

                _panel.SetActive(false);
                _backgroundSwitcher.SwitchOff();
            }
        }

        private IEnumerator ShowAchievementCongratulation(MessageInfo message)
        {
            _achievementContainer.SetActive(true);
            _goodCakes.enabled = message.Points > 0;
            _badCakes.enabled = message.Points <= 0;

            _achievementField.text = message.Text;

            yield return null;
            yield return new WaitWhile(() => !_next);

            _achievementContainer.SetActive(false);
        }

        private IEnumerator ShowGameOver(MessageInfo message)
        {
            _gameOverContainter.SetActive(true);

            _gameOverField.text = $"{message.Points}%";
            yield return null;
            yield return new WaitWhile(() => !_next);

            _gameOverContainter.SetActive(false);
        }

        private void ChangeMessage()
        {
            _next = true;
        }
    }
}
