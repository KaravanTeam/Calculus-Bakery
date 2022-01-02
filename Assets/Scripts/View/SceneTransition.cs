using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    [RequireComponent(typeof(Animator))]
    internal sealed class SceneTransition : MonoBehaviour
    {
        private Animator _animator;
        private bool _shouldLoading;

        private readonly string _openingTrigger = "Opening";
        private readonly string _closingTrigger = "Closing";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _animator.SetTrigger(_openingTrigger);
        }

        public void SwitchToScene(string name)
        {
            _animator.SetTrigger(_closingTrigger);

            StartCoroutine(Load(name));
        }

        public void OnAnimationOver()
        {
            _shouldLoading = true;
        }

        private IEnumerator Load(string name)
        {
            yield return new WaitWhile(() => !_shouldLoading);

            SceneManager.LoadScene(name);
        }
    }
}
