using Model.Transporter;
using UnityEngine;

namespace Model.Chef
{
    internal sealed class Chef : MonoBehaviour
    {
        public bool IsGoodCake(Cake cake) => cake.Equation.ID == cake.EquationType.ID;
    }
}
