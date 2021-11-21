using Model.Transporter;
using System;
using UnityEngine;

namespace Model
{
    internal sealed class Chef : MonoBehaviour
    {
        public event Action<Cake> OnCakeChecked;
        public event Action<Cake> OnCorrectCakeChecked;
        public event Action<Cake> OnWrongCakeChecked;

        public bool IsCorrectCake(Cake cake)
        {
            var isCorrectCake = cake.Equation.ID == cake.EquationType.ID;

            OnCakeChecked?.Invoke(cake);

            if (isCorrectCake)
            {
                OnCorrectCakeChecked?.Invoke(cake);
            }
            else
            {
                OnWrongCakeChecked?.Invoke(cake);
            }

            return isCorrectCake;
        }
    }
}
