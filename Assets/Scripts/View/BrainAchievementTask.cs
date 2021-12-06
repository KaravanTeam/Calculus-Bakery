using Model.Achievements;
using UnityEngine;

namespace View
{
    internal sealed class BrainAchievementTask : MonoBehaviour
    {
        [TextArea]
        public string Text;
        public BrainAchievement Achievement;
    }
}
