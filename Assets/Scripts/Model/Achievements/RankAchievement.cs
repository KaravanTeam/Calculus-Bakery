using Model.SaveSystem;
using System;
using UnityEngine;

namespace Model.Achievements
{
    internal sealed class RankAchievement : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private int _targetPercents;

        public string Name => _name;

        public event Action<RankAchievement> OnRankReached;

        private void OnEnable()
        {
            PlayerProfile.Instance.OnProgressUpdated += UpdateState;
        }

        private void OnDisable()
        {
            PlayerProfile.Instance.OnProgressUpdated -= UpdateState;
        }

        private void UpdateState(int progress)
        {
            if (progress < _targetPercents)
                return;

            OnRankReached?.Invoke(this);

            PlayerProfile.Instance.OnProgressUpdated -= UpdateState;
        }
    }
}