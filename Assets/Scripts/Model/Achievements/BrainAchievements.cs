using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Achievements
{
    internal sealed class BrainAchievements : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private IReadOnlyList<IBrainAchievement> _achievements = new List<IBrainAchievement>();

        private void OnEnable()
        {
            Subscribe();   
        }

        private void Start()
        {
            _achievements = FindObjectsOfType<MonoBehaviour>()
                .OfType<IBrainAchievement>()
                .ToList();

            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            foreach (var achievement in _achievements)
                achievement.OnTargetAchieved += _player.UpdateProgress;
        }

        private void Unsubscribe()
        {
            foreach (var achievement in _achievements)
                achievement.OnTargetAchieved -= _player.UpdateProgress;
        }
    }
}
