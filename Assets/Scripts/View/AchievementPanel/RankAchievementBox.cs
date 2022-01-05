using Model.Achievements;
using Model.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Text))]
    internal sealed class RankAchievementBox : MonoBehaviour
    {
        [SerializeField] private RankAchievement[] _achievements;

        private Text _field;

        private void Awake()
        {
            _field = GetComponent<Text>();
        }

        private void OnEnable()
        {
            foreach (var achievement in _achievements)
                achievement.OnRankReached += UpdateField;
        }

        private void OnDisable()
        {
            foreach (var achievement in _achievements)
                achievement.OnRankReached -= UpdateField;
        }

        private void Start()
        {
            _field.text = PlayerProfile.Instance.Rank;
        }

        private void UpdateField(RankAchievement rank)
        {
            _field.text = rank.Name;

            rank.OnRankReached -= UpdateField;
        }
    }
}
