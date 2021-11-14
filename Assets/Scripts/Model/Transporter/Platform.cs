using Model.Factory;
using UnityEngine;

namespace Model.Transporter
{
    [RequireComponent(typeof(Transform))]
    internal sealed class Platform : MonoBehaviour
    {
        public Bread Equation { get; set; }
    }
}
