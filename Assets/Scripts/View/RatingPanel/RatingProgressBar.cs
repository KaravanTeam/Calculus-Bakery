using Model.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Slider))]
    internal sealed class RatingProgressBar : MonoBehaviour
    {
        [SerializeField] private Color _bright;
        [SerializeField] private Color _dark;
        [SerializeField] private Text[] _headers;
        
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void UpdateBar()
        {
            _slider.value = PlayerProfile.Instance.Points;

            var stepMarkers = _slider.maxValue / 4f;
            var offsetHeader = stepMarkers / 2f;

            for (var i = 0; i < _headers.Length; i++)
                _headers[i].color = _slider.value >= i * stepMarkers + offsetHeader ? _dark : _bright;
        }
    }
}
