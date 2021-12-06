using Model.Transporter;
using System;
using UnityEngine;

namespace Model.Achievements
{
    internal sealed class CorrectCakesAchievement : BrainAchievement
    {
        [SerializeField] private int _targetCount;
        [SerializeField] private int _points;

        [SerializeField] private Chef _chef;
        [SerializeField] private Player _player;
        
        private int _count;

        public override int Score => _count;
        public override int Target => _targetCount;
        public override int Points => _points;

        public override event Action OnStateUpdated;

        private void OnEnable()
        {
            _chef.OnCorrectCakeChecked += UpdateState;
        }

        private void OnDisable()
        {
            _chef.OnCorrectCakeChecked -= UpdateState;
        }

        private void UpdateState(Cake cake)
        {
            _count += 1;
            OnStateUpdated?.Invoke();

            if (_count < _targetCount)
                return;

            _player.AddProgress(_points);

            _chef.OnCorrectCakeChecked -= UpdateState;
        }
    }
}
