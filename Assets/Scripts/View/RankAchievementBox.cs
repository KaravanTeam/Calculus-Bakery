﻿using Model.Achievements;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Text))]
    internal sealed class RankAchievementBox : MonoBehaviour
    {
        [SerializeField] private RankAchievement[] _achievements;

        private Text _field;

        private void Awake()
        {
            _field = GetComponent<Text>();
        }

        private void OnEnable()
        {
            foreach (var achievement in _achievements)
                achievement.OnRankReached += UpdateField;
        }

        private void OnDisable()
        {
            foreach (var achievement in _achievements)
                achievement.OnRankReached -= UpdateField;
        }

        private void UpdateField(RankAchievement rank)
        {
            _field.text = rank.Rank;


            rank.OnRankReached -= UpdateField;
        }
    }
}
