using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Factory : MonoBehaviour
    {
        [SerializeField] private TextAsset _source;

        private EquationInfo[] _database;
        private Dictionary<int, EquationInfo> _equations;

        private readonly System.Random _randGenerator = new System.Random();

        private void Awake()
        {
            
            _database = JsonUtility.FromJson<EquationsDatabase>(_source.text).Equations;

            _equations = GetNewUnsolvedEquations();
        }

        public List<Cake> BuildCakes(int count)
        {
            if (_equations.Count < count)
                _equations = GetNewUnsolvedEquations();

            var shuffledEquations = new Stack<EquationInfo>(_equations
                .Values
                .OrderBy(_ => _randGenerator.Next()));

            var cakes = new List<Cake>();

            for (var i = 0; i < count; i++)
            {
                var equation = shuffledEquations.Pop();

                var bread = new Equation(equation.ID, equation.Value, (EquationType)Enum.Parse(typeof(EquationType), equation.Type));
                var cream = new Solution(equation.ID, equation.Solution);

                cakes.Add(new Cake(bread, cream));
            }

            return cakes;
        }

        public void MarkSolvedEquation(int id)
        {
            if (!_equations.Remove(id))
                throw new InvalidOperationException($"Unknown equation id = {id}");
        }

        private void LoadEquations()
        {

        }

        private Dictionary<int, EquationInfo> GetNewUnsolvedEquations()
        {
            return _database
                .ToArray()
                .ToDictionary(eq => eq.ID, eq => eq);
        }
    }
}
