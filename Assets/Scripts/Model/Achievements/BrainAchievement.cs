using System;
using UnityEngine;

namespace Model.Achievements
{
    internal abstract class BrainAchievement : MonoBehaviour
    {
        public abstract int Score { get; }
        public abstract int Target { get; }
        public abstract int Points { get; }

        public abstract event Action OnStateUpdated;
    }
}
