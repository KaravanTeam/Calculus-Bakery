using Model;
using Model.Transporter;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(SpriteRenderer))]
    internal sealed class PipeContent : MonoBehaviour
    {
        [SerializeField] private Pipe _pipe;
        [SerializeField] private Chef _chef;

        private SpriteRenderer _render;

        private void Awake()
        {
            _render = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _chef.OnDistributed += UpdateState;
        }

        private void OnDisable()
        {
            _chef.OnDistributed -= UpdateState;
        }

        private void UpdateState()
        {
            _render.sprite = _pipe.Solution.Sprite;
        }
    }
}
