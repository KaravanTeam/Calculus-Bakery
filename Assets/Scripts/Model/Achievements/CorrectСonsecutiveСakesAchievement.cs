using Model.Transporter;
using System;
using UnityEngine;

namespace Model.Achievements
{
    internal class CorrectСonsecutiveСakesAchievement : MonoBehaviour, IBrainAchievement
    {
        [SerializeField] private int _cakesTarget;
        [SerializeField] private int _points;

        [SerializeField] protected Chef _chef;

        private int _count;

        public event Action<int> OnTargetAchieved;

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

            if (_count < _cakesTarget)
                return;

            OnTargetAchieved?.Invoke(_points);
            Debug.Log(string.Format("Consecutive {0} +{1}", _cakesTarget, _points));
            Unsubcribe();
        }

        protected void Reset(Cake cake)
        {
            _count = 0;
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
