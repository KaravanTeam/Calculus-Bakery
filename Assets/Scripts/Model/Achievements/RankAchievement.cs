using Model.Transporter;
using System;
using UnityEngine;

namespace Model.Achievements
{
    internal interface IBrainAchievement
    {
        public event Action<int> OnTargetAchieved;
    }

    internal sealed class RankAchievement : MonoBehaviour, IBrainAchievement
    {
        [SerializeField] private string _name;
        [SerializeField] private int _target;
        [SerializeField] private int _points;

        [SerializeField] private Chef _chef;

        private int _correctCakesCount;

        public string Name => _name;
        public int Target => _target;
        public int Points => _points;

        public event Action<int> OnTargetAchieved;

        private void OnEnable()
        {
            _chef.OnCorrectCakeChecked += UpdateState;
        }

        private void Start()
        {
            BrainAchievements.Instance.Add(this);
        }

        private void OnDisable()
        {
            _chef.OnCorrectCakeChecked -= UpdateState;
        }

        private void UpdateState(Cake cake)
        {
            _correctCakesCount += 1;

            if (_correctCakesCount < _target)
                return;

            OnTargetAchieved?.Invoke(_points);
            _chef.OnCorrectCakeChecked -= UpdateState;
        }
    }
}