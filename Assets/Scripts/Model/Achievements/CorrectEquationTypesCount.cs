﻿using Model.Transporter;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Achievements
{
    internal class CorrectEquationTypesCount : BrainAchievement
    {
        [SerializeField] private int _target;
        [SerializeField] private int _points;

        [SerializeField] protected Chef _chef;
        [SerializeField] private Player _player;

        private readonly Dictionary<EquationType, int> _types = 
            Enum.GetValues(typeof(EquationType))
                .Cast<EquationType>()
                .ToDictionary(type => type, _ => 0);

        public override int Score => _types.Values.Sum();
        public override int Target => _target * _types.Count;
        public override int Points => _points;

        public override event Action OnStateUpdated;

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
            _types[cake.Bread.Type] += _types[cake.Bread.Type] < _target ? 1 : 0;
            OnStateUpdated?.Invoke();

            foreach (var count in _types.Values)
            {
                if (count < _target)
                    return;
            }

            _player.AddProgress(_points);
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
