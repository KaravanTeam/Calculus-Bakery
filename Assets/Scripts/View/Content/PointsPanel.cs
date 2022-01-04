using Model.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal sealed class PointsPanel : MonoBehaviour
    {
        [SerializeField] private Text _textField;

        private void OnEnable()
        {
            PlayerProfile.Instance.OnProgressUpdated += UpdateState;
        }

        private void Start()
        {
            _textField.text = $"{PlayerProfile.Instance.Points}%";
        }

        private void OnDisable()
        {
            PlayerProfile.Instance.OnProgressUpdated -= UpdateState;
        }

        private void UpdateState(int progress)
        {
            _textField.text = progress.ToString() + "%";
        }
    }
}
