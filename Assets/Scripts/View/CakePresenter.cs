using Model.Transporter;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(BoxCollider2D))]
    internal sealed class CakePresenter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _cream;
        [SerializeField] private Transporter _transporter;

        private void OnEnable()
        {
            _transporter.OnReseted += RemoveCreamSprite;
        }

        private void OnDisable()
        {
            _transporter.OnReseted -= RemoveCreamSprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out WaterDrop drop))
            {
                _cream.sprite = drop.Cream;
                Destroy(drop.gameObject);
            }
        }

        private void RemoveCreamSprite()
        {
            _cream.sprite = null;
        }
    }
}
