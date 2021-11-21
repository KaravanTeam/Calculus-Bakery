using System.Collections.Generic;
using UnityEngine;

namespace Model.Achievements
{
    internal sealed class BrainAchievements : MonoBehaviour
    {
        public IReadOnlyList<IBrainAchievement> Achievements => _achievements;
        public static BrainAchievements Instance { get; private set; }

        private readonly List<IBrainAchievement> _achievements = new List<IBrainAchievement>();

        private void Awake()
        {
            if (Instance is null)
                Instance = this;
        }

        public void Add(IBrainAchievement achievement)
        {
            _achievements.Add(achievement);
        }
    }
}
