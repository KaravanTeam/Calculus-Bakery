using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Factory : MonoBehaviour
    {
        [SerializeField] private TextAsset _source;
        [SerializeField] private IReadOnlyList<Equation> _equations;

        private Platform _platform;
        private IReadOnlyDictionary<PipeType, Pipe> _pipes;
        private Bread _currentBread;

        private readonly System.Random _randGenerator = new System.Random();

        private void Awake()
        {
            _equations = JsonUtility.FromJson<EquationsInfo>(_source.text).Equations;
        }

        private void Start()
        {
            _platform = FindObjectOfType<Platform>();
            _pipes = FindObjectsOfType<Pipe>()
                .Where(pipe => pipe.Type != PipeType.CakeBuilder)
                .ToDictionary(pipe => pipe.Type);

            // TODO: temp
            Distribute();
        }

        // TODO: temp
        public void Distribute()
        {
            _platform.Cake = null;

            var i = 0;
            foreach (var pipe in _pipes.Values)
            {
                pipe.Cream = new Cream(_equations[i].ID, _equations[i].Value);
                i++;
            }

            var q = _randGenerator.Next(0, 3);
            _currentBread = new Bread(_equations[q].ID, _equations[q].Type);
        }

        public void BuildCakeOnPlatform(PipeType servicedPipe)
        {
            var cake = new Cake(_currentBread, _pipes[servicedPipe].Cream);
            Debug.Log($"Equation: {cake.Equation.ID}, Type: {cake.EquationType.ID}");

            _platform.Cake = cake;
        }
    }
}
