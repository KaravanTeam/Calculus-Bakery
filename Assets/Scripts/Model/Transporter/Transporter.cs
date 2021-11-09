using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Transporter : MonoBehaviour
    {
        public PipeType? ServedPipe => _servecedPipe?.Type;

        [SerializeField] private float _timeTransitionInSeconds = 1;
        private bool _isMoving; 

        private Platform _platform;
        private IReadOnlyDictionary<PipeType, Pipe> _pipes;

        private Pipe _servecedPipe;
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
            _servecedPipe = _pipes[PipeType.CakeBuilder];
        }

        public void TryMoveTowards(Direction direction)
        {
            if (_isMoving)
                return;

            var target = (int)_servecedPipe.Type + (int)direction;
          
            if (target < (int)Direction.Left || target > (int)Direction.Right)
                return;

            _servecedPipe = _pipes[(PipeType)target];
            StartCoroutine(MoveTo((PipeType)target));
        }

        public void TryMoveToDefault()
        {
            if (_isMoving)
                return;

            _servecedPipe = _pipes[PipeType.CakeBuilder];
            StartCoroutine(MoveTo(PipeType.CakeBuilder));
        }

        private IEnumerator MoveTo(PipeType target)
        {
            _isMoving = true;

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

            _isMoving = false;
        }
    }
}
