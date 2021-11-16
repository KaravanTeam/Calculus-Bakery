using UnityEngine;

namespace Model.Transporter
{
    [RequireComponent(typeof(Transform))]
    internal sealed class Platform : MonoBehaviour
    {
        public Cake Cake { get; set; }
    }
}
