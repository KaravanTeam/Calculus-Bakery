using System;

namespace Model.Achievements
{
    internal interface IBrainAchievement
    {
        public event Action<int> OnTargetAchieved;
    }
}