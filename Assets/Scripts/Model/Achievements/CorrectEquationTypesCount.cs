using Model.Transporter;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Achievements
{
    internal class CorrectEquationTypesCount : MonoBehaviour, IBrainAchievement
    {
        [SerializeField] private int _target;
        [SerializeField] private int _points;

        [SerializeField] protected Chef _chef;

        private readonly Dictionary<EquationType, int> _types = 
            Enum.GetValues(typeof(EquationType))
                .Cast<EquationType>()
                .ToDictionary(type => type, _ => 0);

        public event Action<int> OnTargetAchieved;

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        protected void UpdateState(Cake cake)
        {
            _types[cake.Cream.EquationType] += _types[cake.Cream.EquationType] < _target ? 1 : 0;

            foreach (var count in _types.Values)
            {
                if (count < _target)
                    return;
            }

            OnTargetAchieved?.Invoke(_points);
            Debug.Log(string.Format("CorrectEquationTypes {0} +{1}", _target, _points));
            Unsubscribe();
        }

        protected virtual void Subscribe()
        {
            _chef.OnCorrectCakeChecked += UpdateState;
        }

        protected virtual void Unsubscribe()
        {
            _chef.OnCorrectCakeChecked -= UpdateState;
        }
    }
}
