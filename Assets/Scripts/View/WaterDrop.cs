using UnityEngine;


namespace View
{
    [RequireComponent(typeof(SpriteRenderer))]
    internal sealed class WaterDrop : MonoBehaviour
    {
        private SpriteRenderer _render;

        private void Awake()
        {
            _render = GetComponent<SpriteRenderer>();
        }

        public Sprite Cream { get; private set; }

        public void Initialize(CreamView cream)
        {
            Cream = cream.Sprite;
            _render.color = cream.WaterDropColor;
        }
    }
}
