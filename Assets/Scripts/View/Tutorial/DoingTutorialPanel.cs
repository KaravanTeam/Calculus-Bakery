using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    internal sealed class DoingTutorialPanel : TutorialPanel
    {
        [SerializeField] private float _waitTime;
        [SerializeField] private GameObject _waitObjectActive;
        [SerializeField] private Button _target;

        private void OnEnable()
        {
            _target.onClick.AddListener(OnClickTarget);    
        }

        private void OnDisable()
        {
            _target.onClick.RemoveListener(OnClickTarget);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
        }


        public override IEnumerator Run()
        {
            _target.interactable = true;

            yield return new WaitUntil(() => _isEnd);

            _target.interactable = false;

            yield return new WaitForSeconds(_waitTime);
            yield return new WaitUntil(() => _waitObjectActive == null ? true : !_waitObjectActive.activeInHierarchy);
        }

        private void OnClickTarget()
        {
            _isEnd = true;
        }
    }
}
