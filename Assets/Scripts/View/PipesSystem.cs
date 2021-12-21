using Model.Transporter;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace View
{
    internal sealed class PipesSystem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _boardCream;
        [SerializeField] private WaterDrop _waterDropPrefab;
        [SerializeField] private CreamView[] _creams;

        private Stack<CreamView> _stackCreams = new Stack<CreamView>();

        private readonly System.Random _randGenerator = new System.Random();

        public WaterDrop InstantiateWaterDrop(Pipe pipe)
        {
            if (_stackCreams.Count == 0)
                _stackCreams = new Stack<CreamView>(_creams.OrderBy(_ => _randGenerator.Next()));

            var cream = _stackCreams.Pop();

            var drop = Instantiate(_waterDropPrefab, pipe.SpawnWaterDrop.transform);
            drop.Initialize(cream);
            pipe.Drop = drop;

            return drop;
        }

        public void SetExpectedCream(Sprite cream)
        {
            _boardCream.sprite = cream;
        }
    }
}
