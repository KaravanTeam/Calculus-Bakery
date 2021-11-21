using Model.Achievements;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    internal class Player : MonoBehaviour
    {
        private IReadOnlyList<IBrainAchievement> _achievements;

        public string Nickname { get; private set; }
        public string Name { get; private set; }
        public string Group { get; private set; }
        public int Progress { get; private set; }

        public event Action<int> OnProgressUpdated;


        private void OnEnable()
        {
            if (_achievements is null)
                return;

            foreach (var achivement in _achievements)
                achivement.OnTargetAchieved += UpdateProgress;
        }

        private void Start()
        {
            _achievements = BrainAchievements.Instance.Achievements;
            foreach (var achivement in _achievements)
                achivement.OnTargetAchieved += UpdateProgress;
        }

        private void OnDisable()
        {
            foreach (var achivement in _achievements)
                achivement.OnTargetAchieved -= UpdateProgress;
        }

        private void UpdateProgress(int value)
        {
            Progress += value;
            OnProgressUpdated?.Invoke(Progress);
        }
    }
}
