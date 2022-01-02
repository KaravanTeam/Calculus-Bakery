using Model;
using Model.Achievements;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    internal sealed class CongratulationReporter : MonoBehaviour
    {
        [Header("Achievement")]
        [SerializeField] private RankAchievement[] _ranks;
        [SerializeField] private BrainAchievement[] _brains;

        [Header("GameOver")]
        [SerializeField] private CakesBar _bar;
        [SerializeField] private Player _player;
        [SerializeField] private Chef _chef;

        private readonly PriorityQueue<MessageInfo, int> _messages = new PriorityQueue<MessageInfo, int>();
        private readonly int _achievementPriority = 0;
        private readonly int _gameOverPriority = 10;

        private void OnEnable()
        {
            foreach (var rank in _ranks)
                rank.OnRankReached += ReportRank;

            foreach (var brain in _brains)
                brain.OnReached += ReportBrainAchievement;

            _bar.OnUpdated += ReportGameOver;
        }

        private void OnDisable()
        {
            foreach (var rank in _ranks)
                rank.OnRankReached -= ReportRank;

            foreach (var brain in _brains)
                brain.OnReached -= ReportBrainAchievement;

            _bar.OnUpdated -= ReportGameOver;
        }

        public IEnumerable<MessageInfo> Messages => _messages;

        private void ReportRank(RankAchievement rank)
        {
            var message = $"Ты достиг звания\n\"{rank.Name}\"";
            ReportAchievement(message);
        }

        private void ReportBrainAchievement(BrainAchievement brain)
        {
            var message = $"Ты выполнил достижение #{brain.OrderNumber}\n\"{brain.Text}\"";
            ReportAchievement(message, brain.Points);
        }

        private void ReportAchievement(string message, int? points = null)
        {
            _messages.Enqueue(new MessageInfo(message, points ?? 1, MessageType.Achievement), _achievementPriority);
        }

        private void ReportGameOver(int count)
        {
            if (count != _chef.MaxCakes)
                return;

            _messages.Enqueue(new MessageInfo(null, _player.Progress, MessageType.GameOver), _gameOverPriority);
            _bar.OnUpdated -= ReportGameOver;
        }
    }
}
