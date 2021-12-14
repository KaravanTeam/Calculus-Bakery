using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    internal class TutorialPanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected Animator _replicaAnimator;

        protected bool _isEnd;
        protected bool _isLoadedPanel;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (_isLoadedPanel)
                _isEnd = true;
        }

        public virtual IEnumerator Run()
        { 
            var waitTimeEntry = _replicaAnimator.GetCurrentAnimatorStateInfo(0).length;
            for (var time = 0f; time < waitTimeEntry; time += Time.deltaTime)
            {
                yield return null;
            }

            _isLoadedPanel = true;

            yield return new WaitUntil(() => _isEnd);

            _replicaAnimator.SetTrigger("IsEnd");
            yield return null;

            var waitTime = _replicaAnimator.GetCurrentAnimatorStateInfo(0).length;
            for (var time = 0f; time < waitTime; time += Time.deltaTime)
            {
                yield return null;
            }
        }
    }
}
