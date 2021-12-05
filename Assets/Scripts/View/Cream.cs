using Controller;
using Model.Transporter;
using UnityEngine;


namespace View
{
    [RequireComponent(typeof(SpriteRenderer))]
    internal sealed class Cream : MonoBehaviour
    {
        [SerializeField] private Transporter _transporter;
        [SerializeField] private ServedButton _servedButton;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _servedButton.OnCreamServed += Show;
            _transporter.OnReseted += Hide;
        }

        private void OnDisable()
        {
            _servedButton.OnCreamServed -= Show;
            _transporter.OnReseted -= Hide;
        }

        private void Show()
        {
            _renderer.enabled = true;
        }

        private void Hide()
        {
            _renderer.enabled = false;
        }
    }
}
