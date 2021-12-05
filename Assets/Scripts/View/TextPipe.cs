using Model;
using Model.Transporter;
using UnityEngine;

namespace View
{
    // TODO: temp class
    [RequireComponent(typeof(TextMesh))]
    internal sealed class TextPipe : MonoBehaviour
    {
        [SerializeField] private Pipe _pipe;
        [SerializeField] private Chef _chef;

        private TextMesh _mesh;

        private void Awake()
        {
            _mesh = GetComponent<TextMesh>();
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
            _mesh.text = _pipe.Solution.ID.ToString();
        }
    }
}
