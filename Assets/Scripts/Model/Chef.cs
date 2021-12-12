using Model.Transporter;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    internal sealed class Chef : MonoBehaviour
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private Factory _factory;
        [SerializeField] private Transporter.Transporter _transporter;

        private IReadOnlyList<Pipe> _pipes;

        private readonly System.Random _randGenerator = new System.Random();

        public event Action OnCakeChecked;
        public event Action OnDistributed;
        public event Action<Cake> OnCorrectCakeChecked;
        public event Action<Cake> OnWrongCakeChecked;

        private void OnEnable()
        {
            _transporter.OnReseted += Distribute;
        }

        private void Start()
        {
            _pipes = FindObjectsOfType<Pipe>();
            Distribute();
        }

        private void OnDisable()
        {
            _transporter.OnReseted -= Distribute;
        }

        public void Distribute()
        {
            var cakes = _factory.BuildCakes(_pipes.Count);

            var expected = cakes[_randGenerator.Next(cakes.Count)];

            for (var i = 0; i < _pipes.Count; i++)
                _pipes[i].Solution = cakes[i].Cream;

            _platform.Equation = expected.Bread;

            OnDistributed?.Invoke();
        }

        public void DistributeTutorial()
        {
            var stack = new Stack<Cake>(_factory.BuildCakes(_pipes.Count));

            foreach (var pipe in _pipes)
            {
                var cake = stack.Pop();
                pipe.Solution = cake.Cream;
                
                if (pipe.Type == PipeType.Center)
                    _platform.Equation = cake.Bread;
            }

            OnDistributed?.Invoke();
        }

        public void CheckSolution(Cake solution)
        {
            var isCorrectCake = solution.Bread.ID == solution.Cream.ID;

            if (isCorrectCake)
            {
                _factory.MarkSolvedEquation(solution.Bread.ID);
                OnCorrectCakeChecked?.Invoke(solution);
            }
            else
            {
                OnWrongCakeChecked?.Invoke(solution);
            }

            OnCakeChecked?.Invoke();          
        }
    }
}
