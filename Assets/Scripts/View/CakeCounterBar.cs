using Model;
using Model.Transporter;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Slider))]
    internal class CakeCounterBar : MonoBehaviour
    {
        [SerializeField] private Chef _chef;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.value = 0;
        }

        private void OnEnable()
        {
            _chef.OnCorrectCakeChecked += Increase;
        }

        private void Start()
        {
            _slider.maxValue = FindObjectOfType<Factory>().EquationsCount;
        }

        private void OnDisable()
        {
            _chef.OnCorrectCakeChecked -= Increase;
        }

        private void Increase(Cake cake)
        {
            _slider.value += _slider.value < _slider.maxValue ? 1 : 0;
        }
    }
}
