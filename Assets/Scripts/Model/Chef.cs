using Model.Transporter;
using UnityEngine;
using View;

namespace Model.Chef
{
    internal sealed class Chef : MonoBehaviour
    {
        private Player _player;
        private CakeCounterBar _progress;
     
        private void Start()
        {
            _player = FindObjectOfType<Player>();
            _progress = FindObjectOfType<CakeCounterBar>();
        }

        public bool IsGoodCake(Cake cake)
        {
            var isRightCake = cake.Equation.ID == cake.EquationType.ID;

            _player.CorrectCakesCount += isRightCake ? 1 : 0;
            _progress.SetCakeCount(_player.CorrectCakesCount);

            return isRightCake;
        }
    }
}
