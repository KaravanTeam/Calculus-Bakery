using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Factory : MonoBehaviour
    {
        public Cake FinishedCake { get; private set; }

        [SerializeField] private TextAsset _source;
        [SerializeField] private IReadOnlyList<Equation> _equations;

        private Platform _platform;
        private IReadOnlyDictionary<PipeType, Pipe> _pipes;

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
            var i = 0;
            foreach (var pipe in _pipes.Values)
            {
                pipe.EquationType = new Cream(_equations[i].ID, _equations[i].Value);
                i++;
            }

            var q = _randGenerator.Next(0, 3);
            _platform.Equation = new Bread(_equations[q].ID, _equations[q].Type);
        }

        public void BuildCake(PipeType servicedPipe)
        {
            FinishedCake = new Cake(_platform.Equation, _pipes[servicedPipe].EquationType);
            Debug.Log($"Equation: {FinishedCake.Equation.ID}, Type: {FinishedCake.EquationType.ID}");
        }
    }
}
