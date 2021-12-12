using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Solution
    {
        public Solution(int id, Sprite value)
        {
            ID = id;
            Sprite = value;
        }

        public int ID { get; }
        public Sprite Sprite { get; }
    }
}
