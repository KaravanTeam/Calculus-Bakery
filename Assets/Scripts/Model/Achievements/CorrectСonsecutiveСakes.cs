using Model.SaveSystem;
using Model.Transporter;
using System;
using UnityEngine;

namespace Model.Achievements
{
    internal class CorrectСonsecutiveСakes : BrainAchievement
    {
        [TextArea]
        [SerializeField] private string _text;

        [SerializeField] private int _orderNumber;
        [SerializeField] private int _cakesTarget;
        [SerializeField] private int _points;

        [SerializeField] protected Chef _chef;

        private int _count;
        private string _hash;

        public override int OrderNumber => _orderNumber;
        public override string Text => _text;
        public override int Score => _count;
        public override int Target => _cakesTarget;
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
            Unsubcribe();
        }

        private void Start()
        {
            if (_count >= _cakesTarget)
                Unsubcribe();
        }

        protected void UpdateState(Cake cake)
        {
            _count += 1;
            Serialize();
            OnStateUpdated?.Invoke();

            if (_count < _cakesTarget)
                return;

            PlayerProfile.Instance.AddPoints(_points);
            OnReached?.Invoke(this);

            Unsubcribe();
        }

        protected void Reset(Cake cake)
        {
            _count = 0;
            Serialize();
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
