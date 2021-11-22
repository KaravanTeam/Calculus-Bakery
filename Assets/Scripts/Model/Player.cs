using System;
using UnityEngine;

namespace Model
{
    internal class Player : MonoBehaviour
    {
        public string Nickname { get; private set; }
        public string Name { get; private set; }
        public string Group { get; private set; }
        public int Progress { get; private set; }

        public event Action<int> OnProgressUpdated;

        public void UpdateProgress(int value)
        {
            Progress += value;
            OnProgressUpdated?.Invoke(Progress);
        }
    }
}
