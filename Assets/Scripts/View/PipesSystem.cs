using Model.Transporter;
using System.Linq;
using UnityEngine;


namespace View
{
    internal sealed class PipesSystem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _boardCream;
        [SerializeField] private WaterDrop _waterDropPrefab;
        [SerializeField] private CreamView[] _creams;

        private readonly System.Random _randGenerator = new System.Random();

        public void InstantiateWaterDrops(Pipe[] pipes)
        {
            var creams = _creams
                .OrderBy(_ => _randGenerator.Next())
                .Take(pipes.Length)
                .ToArray();

            for (var i = 0; i < pipes.Length; i++)
            {
                var drop = Instantiate(_waterDropPrefab, pipes[i].SpawnWaterDrop.transform);
                drop.Initialize(creams[i]);

                pipes[i].Drop = drop;
            }
        }

        public void SetExpectedCream(Sprite cream)
        {
            _boardCream.sprite = cream;
        }
    }
}
