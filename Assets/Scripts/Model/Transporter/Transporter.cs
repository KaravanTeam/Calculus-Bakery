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
        [SerializeField] private Vector2 _start;
        [SerializeField] private Vector2 _end;

        private Platform _platform;
        private Dictionary<PipeType, Pipe> _pipes;

        private Vector3 _defaultPosition;
        private PipeType _servicedPipe;

        public event Action OnReseted;
        public event Action<PipeType> OnPlatformMovingStarted;
        public event Action<PipeType> OnPlatformMovingEnded;

        private void Start()
        {
            _platform = FindObjectOfType<Platform>();

            _pipes = FindObjectsOfType<Pipe>()
                .ToDictionary(pipe => pipe.Type, pipe => pipe);

            var pipe = _pipes[PipeType.Left].transform.position;
            _defaultPosition = new Vector3(pipe.x, _platform.transform.position.y);
            _servicedPipe = PipeType.Left;


            _platform.transform.position = _defaultPosition;
        }

        public Cake Build()
        {
            return new Cake(_platform.Equation, _pipes[_servicedPipe].Solution);
        }
        
        public IEnumerator ResetPlatform()
        {
            OnPlatformMovingStarted?.Invoke(_servicedPipe);

            yield return _platform.MoveTo(_end, _timeTransition);

            _platform.transform.position = new Vector3(_start.x, _platform.transform.position.y);
            _servicedPipe = PipeType.Left;
            OnReseted?.Invoke();

            yield return _platform.MoveTo(_defaultPosition, _timeTransition);

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

        private IEnumerator MovePlatformTo(PipeType target)
        {
            OnPlatformMovingStarted?.Invoke(_servicedPipe);

            yield return _platform.MoveTo(_pipes[target].transform.position, _timeTransition);

            OnPlatformMovingEnded?.Invoke(_servicedPipe = target);
        }
    }
}
