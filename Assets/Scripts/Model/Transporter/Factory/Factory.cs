using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Factory : MonoBehaviour
    {
        private Dictionary<EquationType, List<Cake>> _database;
        private Dictionary<EquationType, Dictionary<int, Cake>> _unsolvedTypes;

        private readonly string _equationField = "Equation";

        private readonly System.Random _randGenerator = new System.Random();

        public Factory()
        {
            _database = new Dictionary<EquationType, List<Cake>>();
            _unsolvedTypes = new Dictionary<EquationType, Dictionary<int, Cake>>();
        }

        private void Awake()
        {
            var equtationsInfo = GetComponentsInChildren<EquationInfo>();

            for (var i = 0; i < equtationsInfo.Length; i++)
            {
                var equation = equtationsInfo[i];

                var bread = new Equation(i, equation.Value, equation.Type);
                var cream = new Solution(i, equation.Solution);

                if (!_database.ContainsKey(equation.Type))
                    _database[equation.Type] = new List<Cake>();

                _database[equation.Type].Add(new Cake(bread, cream));
            }

            foreach (var equations in _database)
            {
                var unsolvedCakes = equations
                    .Value
                    .Where(equation => PlayerPrefs.GetInt(_equationField + equation.Bread.ID) == 0)
                    .ToList();

                if (unsolvedCakes.Count == 0)
                    continue;

                _unsolvedTypes[equations.Key] = unsolvedCakes
                    .ToDictionary(equation => equation.Bread.ID, equation => equation);
            }

            if (_unsolvedTypes.Count == 0)
                AddUnsolvedEquations();
        }

        public List<Cake> BuildCakes(int count)
        {
            if (_unsolvedTypes.Count < count)
                AddUnsolvedEquations();

            var cakes = new List<Cake>();
            foreach (var equations in _unsolvedTypes.Values.OrderBy(_ => _randGenerator.Next()).Take(count))
            {
                cakes.Add(equations.OrderBy(_ => _randGenerator.Next()).First().Value);
            }

            return cakes;
        }

        public void MarkSolvedEquation(Equation solution)
        {
            if (!_unsolvedTypes[solution.Type].Remove(solution.ID))
                throw new InvalidOperationException($"Unknown equation id = {solution.ID}");

            if (_unsolvedTypes[solution.Type].Count == 0)
                _unsolvedTypes.Remove(solution.Type);

            PlayerPrefs.SetInt(_equationField + solution.ID, 1);
            PlayerPrefs.Save();
        }

        private void AddUnsolvedEquations()
        {
            foreach (var equations in _database)
            {
                if (_unsolvedTypes.ContainsKey(equations.Key))
                    continue;

                _unsolvedTypes[equations.Key] = equations
                    .Value
                    .ToDictionary(equation => equation.Bread.ID, equation => equation);

                foreach (var equation in _unsolvedTypes[equations.Key])
                    PlayerPrefs.SetInt(_equationField + equation.Key, 0);
            }

            PlayerPrefs.Save();
        }
    }
}
