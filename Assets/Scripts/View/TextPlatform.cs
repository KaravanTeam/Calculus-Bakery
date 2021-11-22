using Model.Transporter;
using UnityEngine;

namespace View
{
    // TODO: temp class
    [RequireComponent(typeof(TextMesh))]
    internal sealed class TextPlatform : MonoBehaviour
    {
        [SerializeField] private Platform _platform;
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
            _mesh.text = _platform.Equation.ID.ToString();
        }
    }
}
