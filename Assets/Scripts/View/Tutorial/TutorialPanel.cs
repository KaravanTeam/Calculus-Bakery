using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    internal class TutorialPanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Sprite _replica;
        [SerializeField] private ChefEmoji _chefEmoji;

        private ObjectsSelector _selector;

        private bool _isLoaded;
        private bool _isVisibleReplica;
        protected bool _isEnd;

        private void Awake()
        {
            _selector = GetComponent<ObjectsSelector>();
        }

        private void OnEnable()
        {
            _chefEmoji.OnOpened += ToggleVisibleState;
            _chefEmoji.OnClosed += ToggleVisibleState;
        }

        private void OnDisable()
        {
            _chefEmoji.OnOpened -= ToggleVisibleState;
            _chefEmoji.OnClosed -= ToggleVisibleState;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (_isLoaded)
                _isEnd = true;
        }

        public virtual IEnumerator Run()
        {
            _selector?.Select();

            _chefEmoji.ShowWith(_replica);
            yield return new WaitWhile(() => !_isVisibleReplica);
            _isLoaded = true;

            yield return new WaitUntil(() => _isEnd);
            
            _chefEmoji.Close();
            yield return new WaitWhile(() => _isVisibleReplica);

            _selector?.Unselect();
        }

        private void ToggleVisibleState()
        {
            _isVisibleReplica = !_isVisibleReplica;
        }
    }
}
