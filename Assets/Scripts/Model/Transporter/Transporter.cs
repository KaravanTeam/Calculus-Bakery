using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Transporter : MonoBehaviour
    {
        public PipeType? ServicedPipe => _servicedPipe?.Type;
        public bool IsMoving { get; private set; }

        [SerializeField] private float _timeTransitionInSeconds = 1;

        private Platform _platform;
        private IReadOnlyDictionary<PipeType, Pipe> _pipes;

        private Pipe _servicedPipe;
        private Vector3 _defaultPlatformPosition; 

        private void Start()
        {
            _platform = FindObjectOfType<Platform>();

            _pipes = FindObjectsOfType<Pipe>()
                .OrderBy(pipe => pipe.Type)
                .ToDictionary(pipe => pipe.Type);

            _defaultPlatformPosition = new Vector3(
                _pipes[PipeType.CakeBuilder].transform.localPosition.x,
                _platform.transform.localPosition.y);

            _platform.transform.localPosition = _defaultPlatformPosition;
            _servicedPipe = _pipes[PipeType.CakeBuilder];
        }

        public bool TryMoveTowards(Direction direction)
        {
            if (IsMoving)
                return false;

            var target = (int)_servicedPipe.Type + (int)direction;
          
            if (target < (int)Direction.Left || target > (int)Direction.Right)
                return false;

            _servicedPipe = _pipes[(PipeType)target];
            StartCoroutine(MoveTo((PipeType)target));

            return true;
        }

        public bool TryMoveToDefault()
        {
            if (IsMoving)
                return false;

            _servicedPipe = _pipes[PipeType.CakeBuilder];
            StartCoroutine(MoveTo(PipeType.CakeBuilder));

            return true;
        }

        private IEnumerator MoveTo(PipeType target)
        {
            IsMoving = true;

            var nextPipe = _pipes[target];
            
            var start = _platform.transform.localPosition;
            var end = new Vector3(nextPipe.transform.localPosition.x, start.y);
            for (var t = 0f; t < 1; t += Time.deltaTime / _timeTransitionInSeconds)
            {
                var easingTime = t < 0.5 ? t * t * 2 : 1 - (1 - t) * (1 - t) * 2;

                _platform.transform.localPosition = Vector3.Lerp(start, end, easingTime);

                yield return null;
            }

            _platform.transform.localPosition = end;

            IsMoving = false;
        }
    }
}
