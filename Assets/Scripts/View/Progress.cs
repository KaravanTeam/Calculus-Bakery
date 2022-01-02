using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal sealed class Progress : MonoBehaviour
    {
        [SerializeField] private Text _progress;
        [SerializeField] private Player _player;

        private void OnEnable()
        {
            _player.OnProgressUpdated += UpdateState;
        }

        private void Start()
        {
            _progress.text = "0%";
        }

        private void OnDisable()
        {
            _player.OnProgressUpdated -= UpdateState;
        }

        private void UpdateState(int progress)
        {
            _progress.text = progress.ToString() + "%";
        }
    }
}
