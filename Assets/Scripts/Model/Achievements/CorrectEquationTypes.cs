using Model.SaveSystem;
using Model.Transporter;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Achievements
{
    internal class CorrectEquationTypes : BrainAchievement
    {
        [TextArea]
        [SerializeField] private string _text;

        [SerializeField] private int _orderNumber;
        [SerializeField] private int _target;
        [SerializeField] private int _points;

        [SerializeField] protected Chef _chef;

        private string _hash;
        private EquationType? _changedType = null;

        private readonly Dictionary<EquationType, int> _types = new Dictionary<EquationType, int>();

        public override int OrderNumber => _orderNumber;
        public override string Text => _text;
        public override int Score => _types.Values.Sum();
        public override int Target => _target * _types.Count;
        public override int Points => _points;

        public override event Action OnStateUpdated;
        public override event Action<BrainAchievement> OnReached;

        private void Awake()
        {
            _hash = Text.GetHashCode().ToString();
            Deserialize();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Start()
        {
            if (_types.Values.All(count => count >= _target))
                Unsubscribe();
        }

        protected void UpdateState(Cake cake)
        {
            _types[cake.Bread.Type] += _types[cake.Bread.Type] < _target ? 1 : 0;
            OnStateUpdated?.Invoke();

            _changedType = cake.Bread.Type;
            Serialize();

            if (_types.Values.Any(count => count < _target))
                return;

            PlayerProfile.Instance.AddPoints(_points);
            OnReached?.Invoke(this);

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

        public override void Serialize()
        {
            PlayerPrefs.SetInt(_hash + _changedType.GetHashCode(), _types[_changedType.Value]);
            PlayerPrefs.Save();
        }

        public override void Deserialize()
        {
            foreach (var type in Enum.GetValues(typeof(EquationType)).Cast<EquationType>())
                _types[type] = PlayerPrefs.GetInt(_hash + type.GetHashCode());
        }
    }
}
