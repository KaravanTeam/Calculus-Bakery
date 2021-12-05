using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Factory : MonoBehaviour
    {
        [SerializeField] private TextAsset _source;
        [SerializeField] private IReadOnlyList<EquationInfo> _equations;

        private LinkedList<EquationInfo> _shuffleEquations = new LinkedList<EquationInfo>();
        private readonly System.Random _randGenerator = new System.Random();

        private void Awake()
        {
            _equations = JsonUtility.FromJson<EquationsDatabase>(_source.text).Equations;
        }

        public int EquationsCount => _equations.Count;

        public List<Cake> Build(int count)
        {       
            if (_shuffleEquations.Count < count)
                _shuffleEquations = new LinkedList<EquationInfo>(_equations.OrderBy(_ => _randGenerator.Next()));

            var cakes = new List<Cake>();

            for (var i = 0; i < count; i++)
            {
                var equation = _shuffleEquations.Last.Value;

                var bread = new Equation(equation.ID, equation.Value, equation.Type);
                var cream = new Solution(equation.ID, equation.Solution);

                cakes.Add(new Cake(bread, cream));

                _shuffleEquations.RemoveLast();
            }

            return cakes;
        }
    }
}
