using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Transporter : MonoBehaviour
    {
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;
        [SerializeField] private Platform _platform;
        [SerializeField] private Pipe[] _pipes;

        private Dictionary<PipeType, Pipe> _pipeTypes;

        private Vector3 _defaultPosition;
        private PipeType _servicedPipe;

        private Pipe Current => _pipeTypes[_servicedPipe];

        public event Action OnReseted;
        public event Action<PipeType> OnPlatformMovingStarted;
        public event Action<PipeType> OnPlatformMovingEnded;

        private void Start()
        {
            _pipeTypes = _pipes.ToDictionary(pipe => pipe.Type, pipe => pipe);

            var pipe = _pipeTypes[PipeType.Left].transform.position;
            _defaultPosition = new Vector3(pipe.x, _platform.transform.position.y);
            _servicedPipe = PipeType.Left;

            _platform.transform.position = _defaultPosition;
        }

        public Cake BuildSolution()
        {
            return new Cake(_platform.Equation, Current.Solution);
        }
        
        public IEnumerator ResetPlatform()
        {
            OnPlatformMovingStarted?.Invoke(_servicedPipe);

            yield return _platform.MoveTo(_end.position);

            _platform.transform.position = new Vector3(_start.position.x, _platform.transform.position.y);
            _servicedPipe = PipeType.Left;
            OnReseted?.Invoke();

            yield return _platform.MoveTo(_defaultPosition);

            OnPlatformMovingEnded?.Invoke(PipeType.Left);
        }


        public bool TryMoveTowards(Direction direction)
        {
            var target = (int)_servicedPipe + (int)direction;
          
            if (target < (int)PipeType.Left || target > (int)PipeType.Right)
                return false;

            StartCoroutine(MovePlatformTo((PipeType)target));

            return true;
        }

        public void ThrowWaterDrop()
        {
            Current.Drop.FallDown();
        }

        private IEnumerator MovePlatformTo(PipeType target)
        {
            OnPlatformMovingStarted?.Invoke(_servicedPipe);

            yield return _platform.MoveTo(_pipeTypes[target].transform.position);

            OnPlatformMovingEnded?.Invoke(_servicedPipe = target);
        }
    }
}
