using System.Collections.Generic;
using UnityEngine;

namespace View
{
    internal sealed class ObjectsSelector : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _renders;

        private Dictionary<SpriteRenderer, int> _previousLayers = new Dictionary<SpriteRenderer, int>();

        private int _layerID;

        private void Awake()
        {
            _layerID = SortingLayer.NameToID("ObjectsSelectable");
        }

        public void Select()
        {
            foreach (var render in _renders)
            {
                _previousLayers[render] = render.sortingLayerID;
                render.sortingLayerID = _layerID;
            }
        }
        
        public void Unselect()
        {
            foreach (var render in _renders)
            { 
                render.sortingLayerID = _previousLayers[render];
                _previousLayers[render] = _layerID;
            }
        }
    }
}
