using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal sealed class СongratulationPanel : MonoBehaviour
    {
        [SerializeField] private СongratulationReporter _reporter;

        [SerializeField] private Image _goodCakes;
        [SerializeField] private Image _badCakes;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Text _field;

        private void OnEnable()
        {
            Show(_reporter.NextMessage());
            _nextButton.onClick.AddListener(ChangeMessage);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(ChangeMessage);
        }

        private void ChangeMessage()
        {
            var info = _reporter.NextMessage();

            if (info is null)
            {
                gameObject.SetActive(false);
                return;
            }

            Show(info);
        }

        private void Show(MessageInfo info)
        {
            _goodCakes.enabled = false;
            _badCakes.enabled = false;

            _field.text = info.Text;

            if (info.AchievementPoints > 0)
            {
                _goodCakes.enabled = true;
                return;
            }

            _badCakes.enabled = true;
        }
    }
}
