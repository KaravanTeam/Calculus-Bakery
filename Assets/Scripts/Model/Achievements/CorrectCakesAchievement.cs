using Model.Transporter;
using System;
using UnityEngine;

namespace Model.Achievements
{
    internal sealed class CorrectCakesAchievement : MonoBehaviour, IBrainAchievement
    {
        [SerializeField] private int _cakesTarget;
        [SerializeField] private int _points;

        [SerializeField] private Chef _chef;

        private int _count;

        public event Action<int> OnTargetAchieved;

        private void OnEnable()
        {
            _chef.OnCorrectCakeChecked += UpdateState;
            _chef.OnWrongCakeChecked += Reset;
        }

        private void Start()
        {
            BrainAchievements.Instance.Add(this);
        }

        private void OnDisable()
        {
            _chef.OnCorrectCakeChecked -= UpdateState;
            _chef.OnWrongCakeChecked -= Reset;
        }

        private void UpdateState(Cake cake)
        {
            _count += 1;

            if (_count < _cakesTarget)
                return;

            OnTargetAchieved?.Invoke(_points);

            _chef.OnCorrectCakeChecked -= UpdateState;
            _chef.OnWrongCakeChecked -= Reset;
        }

        private void Reset(Cake cake)
        {
            _count = 0;
        }
    }
}
