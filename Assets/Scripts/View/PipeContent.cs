using Model;
using Model.Transporter;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(Pipe))]
    internal sealed class PipeContent : MonoBehaviour
    {
        [SerializeField] private GameObject _center;
        [SerializeField] private Chef _chef;

        private Sprite _currentContent;
        private Pipe _pipe;

        private void Awake()
        {
            _pipe = GetComponent<Pipe>();
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
            Destroy(_currentContent);

            _currentContent = Instantiate(_pipe.Solution.Sprite, 
                _center.transform.localPosition, Quaternion.identity, _pipe.transform);
        }
    }
}
