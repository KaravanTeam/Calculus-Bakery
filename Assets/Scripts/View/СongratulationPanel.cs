using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal sealed class СongratulationPanel : MonoBehaviour
    {
        [SerializeField] private СongratulationReporter _reporter;

        [SerializeField] private Button _nextButton;
        [SerializeField] private Text _field;

        private void OnEnable()
        {
            _field.text = _reporter.NextMessage();
            _nextButton.onClick.AddListener(ChangeMessage);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(ChangeMessage);
        }

        private void ChangeMessage()
        {
            var message = _reporter.NextMessage();

            if (message is null)
            {
                gameObject.SetActive(false);
                return;
            }

            _field.text = message;
        }
    }
}
