using Model;
using Model.Transporter;
using UnityEngine;

namespace View
{
    // TODO: temp class
    [RequireComponent(typeof(TextMesh))]
    internal sealed class TextPlatform : MonoBehaviour
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private Chef _chef;

        private TextMesh _mesh;

        private void OnEnable()
        {
            _chef.OnDistributed += UpdateState;
        }

        private void Awake()
        {
            _mesh = GetComponent<TextMesh>();
        }

        private void OnDisable()
        {
            _chef.OnDistributed -= UpdateState;
        }

        private void UpdateState()
        {
            _mesh.text = _platform.Equation.ID.ToString();
        }
    }
}
