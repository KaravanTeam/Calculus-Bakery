using System;
using UnityEngine;

namespace Model.Achievements
{
    internal sealed class RankAchievement : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private int _targetPercents;

        [SerializeField] private Player _player;

        public event Action OnRankUpdated;

        private void OnEnable()
        {
            _player.OnProgressUpdated += UpdateState;
        }

        private void OnDisable()
        {
            _player.OnProgressUpdated -= UpdateState;
        }

        private void UpdateState(int progress)
        {
            if (progress < _targetPercents)
                return;

            Debug.Log(_name);
            OnRankUpdated?.Invoke();
            _player.OnProgressUpdated -= UpdateState;
        }
    }
}