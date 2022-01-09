using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Equation
    {
        public Equation(int id, Sprite value, EquationType type)
        {
            ID = id;
            Sprite = value;
            Type = type;
        }

        public int ID { get; }
        public Sprite Sprite { get; }
        public EquationType Type { get; }
    }
}
