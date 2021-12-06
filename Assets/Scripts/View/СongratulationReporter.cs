using Model.Achievements;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    internal sealed class СongratulationReporter : MonoBehaviour
    {
        [SerializeField] private СongratulationPanel _panel;

        [SerializeField] private RankAchievement[] _ranks;
        [SerializeField] private BrainAchievement[] _brains;

        private readonly Queue<string> _messages = new Queue<string>();

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

        public string NextMessage() => _messages.Count > 0 ? _messages.Dequeue() : null;

        private void ReportRank(RankAchievement rank)
        {
            var message = $"Вы достигли звания\n\"{rank.Name}\"";
            Report(message);
        }

        private void ReportBrainAchievement(BrainAchievement brain)
        {
            var message = $"Вы выполнили достижение #{brain.OrderNumber}\n\"{brain.Text}\"";
            Report(message);
        }

        private void Report(string message)
        {
            _messages.Enqueue(message);

            _panel.gameObject.SetActive(true);
        }
    }
}
