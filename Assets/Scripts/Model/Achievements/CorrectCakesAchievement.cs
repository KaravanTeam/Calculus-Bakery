using Model.Transporter;
using System;
using UnityEngine;

namespace Model.Achievements
{
    internal sealed class CorrectCakesAchievement : MonoBehaviour, IBrainAchievement
    {
        [SerializeField] private int _targetCount;
        [SerializeField] private int _points;

        [SerializeField] private Chef _chef;

        private int _count;

        public event Action<int> OnTargetAchieved;

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

            if (_count < _targetCount)
                return;

            Debug.Log(string.Format("CorrectCakes {0} +{1}", _targetCount, _points));
            OnTargetAchieved?.Invoke(_points);

            _chef.OnCorrectCakeChecked -= UpdateState;
        }
    }
}
