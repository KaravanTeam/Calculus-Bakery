using Model.Transporter;
using UnityEngine;


namespace View
{
    internal sealed class PipesSystem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _boardCream;
        [SerializeField] private WaterDrop _waterDropPrefab;
        [SerializeField] private CreamView[] _creams;

        private readonly System.Random _randGenerator = new System.Random();

        public WaterDrop InstantiateWaterDrop(Pipe pipe)
        {
            var cream = _creams[_randGenerator.Next(_creams.Length)];

            var drop = Instantiate(_waterDropPrefab, pipe.transform);
            drop.Initialize(cream);

            return drop;
        }

        public void SetExpectedCream(Sprite cream)
        {
            _boardCream.sprite = cream;
        }
    }
}
