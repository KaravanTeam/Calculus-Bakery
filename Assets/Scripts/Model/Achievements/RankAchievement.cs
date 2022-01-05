using Model.SaveSystem;
using System;
using UnityEngine;

namespace Model.Achievements
{
    internal sealed class RankAchievement : MonoBehaviour, ISerializable
    {
        [SerializeField] private string _name;
        [SerializeField] private int _targetPercents;

        private int _progress;

        public string Name => _name;

        public event Action<RankAchievement> OnRankReached;

        private void Awake()
        {
            Deserialize();
        }

        private void OnEnable()
        {
            PlayerProfile.Instance.OnProgressUpdated += UpdateState;
        }

        private void OnDisable()
        {
            PlayerProfile.Instance.OnProgressUpdated -= UpdateState;
        }

        private void Start()
        {
            if (_progress >= _targetPercents)
                PlayerProfile.Instance.OnProgressUpdated -= UpdateState;
        }

        private void UpdateState(int progress)
        {
            _progress = progress;

            if (progress < _targetPercents)
                return;

            PlayerProfile.Instance.Rank = Name;
            OnRankReached?.Invoke(this);
            PlayerProfile.Instance.OnProgressUpdated -= UpdateState;

            Serialize();
            PlayerProfile.SaveRank();
        }

        public void Serialize()
        {
            PlayerPrefs.SetInt(Name.GetHashCode().ToString(), _progress);
            PlayerPrefs.Save();
        }

        public void Deserialize()
        {
            _progress = PlayerPrefs.GetInt(Name.GetHashCode().ToString());
        }
    }
}