using UnityEngine;

namespace Model
{
    internal class Player : MonoBehaviour
    {
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public int CorrectCakesCount { get; set; }
        public int Progress { get; set; }
    }
}
