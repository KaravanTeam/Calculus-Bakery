using Model.Achievements;
using UnityEngine;

namespace Model
{
    internal class Player : MonoBehaviour
    {
        public string Nickname { get; private set; }
        public string Name { get; private set; }
        public string Group { get; private set; }
        public int Progress { get; private set; }

        private void Start()
        {
            foreach (var achivement in BrainAchievements.Instance.Achievements)
                achivement.OnTargetAchieved += UpdateProgress;
        }

        private void UpdateProgress(int value)
        {
            Progress += value;
            Debug.Log(Progress);
        }
    }
}
