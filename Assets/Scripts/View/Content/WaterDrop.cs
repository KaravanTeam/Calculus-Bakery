using UnityEngine;

namespace View
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class WaterDrop : MonoBehaviour
    {
        private SpriteRenderer _render;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _render = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public Sprite Cream { get; private set; }
        public Color Color => _render.color;

        public void Initialize(CreamView cream)
        {
            Cream = cream.Sprite;
            _render.color = cream.WaterDropColor;
        }

        public void FallDown()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
