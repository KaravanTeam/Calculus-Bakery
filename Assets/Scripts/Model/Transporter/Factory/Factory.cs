using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Factory : MonoBehaviour
    {
        private Cake[] _database;
        private Dictionary<int, Cake> _unsolvedEquations;

        private readonly System.Random _randGenerator = new System.Random();

        private void Awake()
        {
            var equtationsInfo = GetComponentsInChildren<EquationInfo>();
            _database = new Cake[equtationsInfo.Length];

            for (var i = 0; i < equtationsInfo.Length; i++)
            {
                var equation = equtationsInfo[i];

                var bread = new Equation(i, equation.Value, equation.Type);
                var cream = new Solution(i, equation.Solution);

                _database[i] = new Cake(bread, cream);
            }

            _unsolvedEquations = GetNewUnsolvedEquations();
        }

        public List<Cake> BuildCakes(int count)
        {
            if (_unsolvedEquations.Count < count)
                _unsolvedEquations = GetNewUnsolvedEquations();

            return _unsolvedEquations
                .Values
                .OrderBy(_ => _randGenerator.Next())
                .Take(count)
                .ToList();
        }

        public void MarkSolvedEquation(int id)
        {
            if (!_unsolvedEquations.Remove(id))
                throw new InvalidOperationException($"Unknown equation id = {id}");
        }

        private Dictionary<int, Cake> GetNewUnsolvedEquations()
        {
            return _database
                .ToArray()
                .ToDictionary(cake => cake.Bread.ID, cake => cake);
        }
    }
}
