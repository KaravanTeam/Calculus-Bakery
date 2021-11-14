using UnityEngine;

namespace Model.Factory
{
    internal sealed class Pipe : MonoBehaviour
    {
        public PipeType Type;
        public Cream EquationType { get; set; }
    }
}
