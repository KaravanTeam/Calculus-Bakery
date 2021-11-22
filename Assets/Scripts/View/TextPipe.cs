using Model.Transporter;
using UnityEngine;

namespace View
{
    // TODO: temp class
    [RequireComponent(typeof(TextMesh))]
    internal sealed class TextPipe : MonoBehaviour
    {
        [SerializeField] private Pipe _pipe;
        [SerializeField] private Factory _factory;

        private TextMesh _mesh;

        private void OnEnable()
        {
            _factory.OnFactoryDistributed += UpdateState;
        }

        private void Awake()
        {
            _mesh = GetComponent<TextMesh>();
        }

        private void OnDisable()
        {
            _factory.OnFactoryDistributed -= UpdateState;
        }

        private void UpdateState()
        {
            _mesh.text = _pipe.Cream.ID.ToString();
        }
    }
}
