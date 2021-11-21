using Model.Transporter;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Achievements
{
    internal sealed class EquationTypesCount : MonoBehaviour, IBrainAchievement
    {
        [SerializeField] private int _target;
        [SerializeField] private int _points;

        [SerializeField] private Chef _chef;

        private readonly Dictionary<EquationType, int> _types = 
            Enum.GetValues(typeof(EquationType))
                .Cast<EquationType>()
                .ToDictionary(type => type, _ => 0);

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
            _types[cake.Cream.EquationType] += _types[cake.Cream.EquationType] < _target ? 1 : 0;

            foreach (var count in _types.Values)
            {
                if (count < _target)
                    return;
            }

            OnTargetAchieved?.Invoke(_points);
            _chef.OnCorrectCakeChecked -= UpdateState;
        }
    }
}
