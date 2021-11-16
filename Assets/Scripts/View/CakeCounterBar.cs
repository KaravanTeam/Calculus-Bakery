using Model.Transporter;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Slider))]
    internal class CakeCounterBar : MonoBehaviour
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void Start()
        {
            _slider.maxValue = FindObjectOfType<Factory>().EquationsCount;
        }

        public void SetCakeCount(int count)
        {
            if (count < 0 || count > _slider.maxValue)
                throw new ArgumentOutOfRangeException($"Cake's count is out of range: {count}");

            _slider.value = count;
        }
    }
}
