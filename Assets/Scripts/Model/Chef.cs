using Model.Transporter;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using View;

namespace Model
{
    internal sealed class Chef : MonoBehaviour
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private Factory _factory;
        [SerializeField] private Transporter.Transporter _transporter;

        [Header("Factory")]
        [SerializeField] private int _finishedCakesCount;
        [SerializeField] private CakesCounterBar _bar;

        [Header("Pipe System")]
        [SerializeField] private PipesSystem _pipesSystem;
        [SerializeField] private Pipe[] _pipes;

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
            _bar.SetMaxValue(_finishedCakesCount);
            Distribute();
        }

        private void OnDisable()
        {
            _transporter.OnReseted -= Distribute;
        }

        public void Distribute()
        {
            ClearPipes();

            var cakes = _factory.BuildCakes(_pipes.Length);

            var expected = cakes[_randGenerator.Next(cakes.Count)];

            for (var i = 0; i < _pipes.Length; i++)
            {
                _pipes[i].Solution = cakes[i].Cream;
                var drop = _pipesSystem.InstantiateWaterDrop(_pipes[i]);

                if (cakes[i] == expected)
                    _pipesSystem.SetExpectedCream(drop.Cream);
            }

            _platform.Equation = expected.Bread;


            OnDistributed?.Invoke();
        }

        public void DistributeTutorial()
        {
            var stack = new Stack<Cake>(_factory.BuildCakes(_pipes.Length));

            foreach (var pipe in _pipes)
            {
                var cake = stack.Pop();

                pipe.Solution = cake.Cream;
                var drop = _pipesSystem.InstantiateWaterDrop(pipe);

                if (pipe.Type == PipeType.Center)
                {
                    _platform.Equation = cake.Bread;
                    _pipesSystem.SetExpectedCream(drop.Cream);
                }
            }

            OnDistributed?.Invoke();
        }

        public void CheckSolution(Cake solution)
        {
            var isCorrectCake = solution.Bread.ID == solution.Cream.ID;

            if (isCorrectCake)
            {
                _factory.MarkSolvedEquation(solution.Bread);
                OnCorrectCakeChecked?.Invoke(solution);
            }
            else
            {
                OnWrongCakeChecked?.Invoke(solution);
            }

            OnCakeChecked?.Invoke();          
        }

        private void ClearPipes()
        {
            foreach (var pipe in _pipes.Where(pipe => pipe.Drop != null))
                Destroy(pipe.Drop.gameObject);
        }
    }
}
