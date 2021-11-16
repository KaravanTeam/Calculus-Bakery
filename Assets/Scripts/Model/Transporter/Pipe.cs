using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Pipe : MonoBehaviour
    {
        public PipeType Type;
        public Cream EquationType { get; set; }
    }
}
