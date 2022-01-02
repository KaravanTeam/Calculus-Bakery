using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal sealed class PointsPanel : MonoBehaviour
    {
        [SerializeField] private Text _textField;
        [SerializeField] private Player _player;

        private void OnEnable()
        {
            _player.OnProgressUpdated += UpdateState;
        }

        private void Start()
        {
            _textField.text = $"{_player.Progress}%";
        }

        private void OnDisable()
        {
            _player.OnProgressUpdated -= UpdateState;
        }

        private void UpdateState(int progress)
        {
            _textField.text = progress.ToString() + "%";
        }
    }
}
