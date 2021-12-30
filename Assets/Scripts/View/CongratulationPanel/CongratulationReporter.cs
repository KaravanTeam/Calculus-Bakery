using Model.Achievements;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    internal sealed class CongratulationReporter : MonoBehaviour
    {
        [SerializeField] private RankAchievement[] _ranks;
        [SerializeField] private BrainAchievement[] _brains;

        private readonly Queue<MessageInfo> _messages = new Queue<MessageInfo>();

        private void OnEnable()
        {
            foreach (var rank in _ranks)
                rank.OnRankReached += ReportRank;

            foreach (var brain in _brains)
                brain.OnReached += ReportBrainAchievement;
        }

        private void OnDisable()
        {
            foreach (var rank in _ranks)
                rank.OnRankReached -= ReportRank;

            foreach (var brain in _brains)
                brain.OnReached -= ReportBrainAchievement;
        }

        public MessageInfo NextMessage() => _messages.Count > 0 ? _messages.Dequeue() : null;

        private void ReportRank(RankAchievement rank)
        {
            var message = $"Ты достиг звания\n\"{rank.Name}\"";
            Report(message);
        }

        private void ReportBrainAchievement(BrainAchievement brain)
        {
            var message = $"Ты выполнил достижение #{brain.OrderNumber}\n\"{brain.Text}\"";
            Report(message, brain.Points);
        }

        private void Report(string message, int? points = null)
        {
            _messages.Enqueue(new MessageInfo(message, points ?? 1));
        }
    }
}
