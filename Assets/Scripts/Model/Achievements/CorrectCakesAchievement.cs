using Model.SaveSystem;
using Model.Transporter;
using System;
using UnityEngine;

namespace Model.Achievements
{
    internal sealed class CorrectCakesAchievement : BrainAchievement
    {
        [TextArea]
        [SerializeField] private string _text;

        [SerializeField] private int _orderNumber;
        [SerializeField] private int _targetCount;
        [SerializeField] private int _points;

        [SerializeField] private Chef _chef;
        
        private int _count;
        private string _hash;

        public override int OrderNumber => _orderNumber;
        public override string Text => _text;
        public override int Score => _count;
        public override int Target => _targetCount;
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
            _chef.OnCorrectCakeChecked += UpdateState;
        }

        private void OnDisable()
        {
            _chef.OnCorrectCakeChecked -= UpdateState;
        }

        private void Start()
        {
            if (_count >= _targetCount)
                _chef.OnCorrectCakeChecked -= UpdateState;
        }

        private void UpdateState(Cake cake)
        {
            _count += 1;
            OnStateUpdated?.Invoke();
            Serialize();

            if (_count < _targetCount)
                return;

            PlayerProfile.Instance.AddPoints(_points);
            OnReached?.Invoke(this);

            _chef.OnCorrectCakeChecked -= UpdateState;
        }

        public override void Serialize()
        {
            PlayerPrefs.SetInt(_hash, _count);
            PlayerPrefs.Save();
        }

        public override void Deserialize()
        {
            _count = PlayerPrefs.GetInt(_hash);
        }
    }
}
