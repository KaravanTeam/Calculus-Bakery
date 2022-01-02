﻿using Model;
using Model.Transporter;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Slider))]
    internal class CakesCounterBar : MonoBehaviour
    {
        [SerializeField] private Chef _chef;

        private Slider _slider;

        public event Action<int> OnUpdated;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.value = 0;
        }

        private void OnEnable()
        {
            _chef.OnCorrectCakeChecked += Increase;
        }

        private void OnDisable()
        {
            _chef.OnCorrectCakeChecked -= Increase;
        }

        private void Start()
        {
            _slider.maxValue = _chef.MaxCakes;
        }

        private void Increase(Cake cake)
        {
            _slider.value += _slider.value < _slider.maxValue ? 1 : 0;
            OnUpdated?.Invoke((int)_slider.value);
        }
    }
}
