using Model;
using Model.Transporter;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(SpriteRenderer))]
    internal sealed class PlatformContent : MonoBehaviour
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private Chef _chef;

        private SpriteRenderer _render;

        private void OnEnable()
        {
            _chef.OnDistributed += UpdateState;
        }

        private void Awake()
        {
            _render = GetComponent<SpriteRenderer>();
        }

        private void OnDisable()
        {
            _chef.OnDistributed -= UpdateState;
        }

        private void UpdateState()
        {
            _render.sprite = _platform.Equation.Sprite;
        }
    }
}
