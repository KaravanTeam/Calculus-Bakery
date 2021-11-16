using Model.Transporter;
using UnityEngine;

namespace Model.Chef
{
    internal sealed class Chef : MonoBehaviour
    {
        public bool IsGoodCake(Cake cake)
        {
            
            return cake.Equation.ID == cake.EquationType.ID;
        }
    }
}
