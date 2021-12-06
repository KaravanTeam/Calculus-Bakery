using Model.Transporter;
using System;
using UnityEngine;

namespace Model.Achievements
{
    internal class CorrectСonsecutiveСakesAchievement : BrainAchievement
    {
        [SerializeField] private int _cakesTarget;
        [SerializeField] private int _points;

        [SerializeField] protected Chef _chef;
        [SerializeField] private Player _player;

        private int _count;

        public override int Score => _count;
        public override int Target => _cakesTarget;
        public override int Points => _points;

        public override event Action OnStateUpdated;

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubcribe();
        }

        protected void UpdateState(Cake cake)
        {
            _count += 1;
            OnStateUpdated?.Invoke();

            if (_count < _cakesTarget)
                return;

            _player.AddProgress(_points);

            Unsubcribe();
        }

        protected void Reset(Cake cake)
        {
            _count = 0;
            OnStateUpdated?.Invoke();
        }

        protected virtual void Subscribe()
        {
            _chef.OnCorrectCakeChecked += UpdateState;
            _chef.OnWrongCakeChecked += Reset;
        }

        protected virtual void Unsubcribe()
        {
            _chef.OnCorrectCakeChecked -= UpdateState;
            _chef.OnWrongCakeChecked -= Reset;
        }
    }
}
