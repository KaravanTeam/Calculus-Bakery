using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Transporter : MonoBehaviour
    {
        [SerializeField] private float _timeTransition = 1;

        private Platform _platform;
        private Dictionary<PipeType, Pipe> _pipes;

        private Vector3 _defaultPlatformPosition;
        private PipeType _servicedPipe;
        
        public event Action<PipeType> OnPlatformMovingStarted;
        public event Action<PipeType> OnPlatformMovingEnded;

        public int PipesCount { get; private set; }

        private void Start()
        {
            _platform = FindObjectOfType<Platform>();

            _pipes = FindObjectsOfType<Pipe>()
                .ToDictionary(pipe => pipe.Type, pipe => pipe);
            PipesCount = _pipes.Count;

            var pipe = _pipes[PipeType.Left].transform.localPosition;
            _defaultPlatformPosition = new Vector3(pipe.x, _platform.transform.localPosition.y);
            _servicedPipe = PipeType.Left;


            _platform.transform.localPosition = _defaultPlatformPosition;
        }

        public Cake Build()
        {
            return new Cake(_platform.Equation, _pipes[_servicedPipe].Solution);
        }    

        public bool TryMoveTowards(Direction direction)
        {
            var target = (int)_servicedPipe + (int)direction;
          
            if (target < (int)PipeType.Left || target > (int)PipeType.Right)
                return false;

            StartCoroutine(MovePlatformTo((PipeType)target));

            return true;
        }

        private IEnumerator MovePlatformTo(PipeType target)
        {
            OnPlatformMovingStarted?.Invoke(_servicedPipe);

            yield return _platform.MoveTo(_pipes[target].transform.localPosition, _timeTransition);

            OnPlatformMovingEnded?.Invoke(_servicedPipe = target);
        }
    }
}
