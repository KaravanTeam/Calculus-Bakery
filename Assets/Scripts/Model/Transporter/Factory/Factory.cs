using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Factory : MonoBehaviour
    {
        public int EquationsCount => _equations.Count;

        [SerializeField] private TextAsset _source;
        [SerializeField] private IReadOnlyList<Equation> _equations;

        private Platform _platform;
        private IReadOnlyList<Pipe> _pipes;

        private readonly System.Random _randGenerator = new System.Random();
        private LinkedList<Equation> _shuffleEquations = new LinkedList<Equation>();

        public event Action OnFactoryDistributed;

        private void Awake()
        {
            _equations = JsonUtility.FromJson<EquationsInfo>(_source.text).Equations;
        }

        private void Start()
        {
            _platform = FindObjectOfType<Platform>();
            _pipes = FindObjectsOfType<Pipe>()
                .Where(pipe => pipe.Type != PipeType.CakeBuilder)
                .ToList();

            // TODO: temp
            Distribute();
        }

        public void Distribute()
        {       
            if (_shuffleEquations.Count < _pipes.Count)
                _shuffleEquations = new LinkedList<Equation>(_equations.OrderBy(_ => _randGenerator.Next()));

            var equations = new List<Equation>();
            foreach (var pipe in _pipes)
            {
                var equation = _shuffleEquations.Last.Value;

                pipe.Cream = new Cream(equation.ID, equation.Type);
                equations.Add(equation);

                _shuffleEquations.RemoveLast();
            }

            var expectedEquation = equations[_randGenerator.Next(0, equations.Count)];
            _platform.Equation = new Bread(expectedEquation.ID, expectedEquation.Value);

            OnFactoryDistributed?.Invoke();
        }

        public Cake BuildCake(PipeType servicedPipe)
        {
            var pipe = _pipes.FirstOrDefault(pipe => pipe.Type == servicedPipe);
            if (pipe is null)
                throw new UnityException("Unknown servicePipe");

            Debug.Log($"Equation: {_platform.Equation.ID}, Type: {pipe.Cream.ID}");
            return new Cake(_platform.Equation, pipe.Cream);
        }
    }
}
