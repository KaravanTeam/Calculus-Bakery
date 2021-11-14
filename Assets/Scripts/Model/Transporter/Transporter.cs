using Model.Factory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Transporter
{
    internal sealed class Transporter : MonoBehaviour
    {
        public PipeType ServicedPipe { get; private set; }
        public bool IsMoving { get; private set; }

        [SerializeField] private float _timeTransitionInSeconds = 1;

        private Factory.Factory _factory;
        private Transform _platform;
        private IReadOnlyDictionary<PipeType, Vector3> _pipes;

        private Vector3 _defaultPlatformPosition; 

        private void Start()
        {
            _factory = FindObjectOfType<Factory.Factory>();
            _platform = FindObjectOfType<Platform>().GetComponent<Transform>();

            _pipes = FindObjectsOfType<Pipe>()
                .ToDictionary(pipe => pipe.Type, pipe => pipe.transform.localPosition);

            _defaultPlatformPosition = new Vector3(
                _pipes[PipeType.CakeBuilder].x,
                _platform.localPosition.y);

            _platform.transform.localPosition = _defaultPlatformPosition;
            ServicedPipe = PipeType.CakeBuilder;
        }

        public bool TryMoveTowards(Direction direction)
        {
            if (IsMoving)
                return false;

            var target = (int)ServicedPipe + (int)direction;
          
            if (target < (int)Direction.Left || target > (int)Direction.Right)
                return false;

            ServicedPipe = (PipeType)target;
            StartCoroutine(MoveTo((PipeType)target));

            return true;
        }

        public bool TryMoveToDefault()
        {
            if (IsMoving)
                return false;

            ServicedPipe = PipeType.CakeBuilder;
            StartCoroutine(MoveTo(PipeType.CakeBuilder));

            return true;
        }

        private IEnumerator MoveTo(PipeType target)
        {
            IsMoving = true;

            var nextPipe = _pipes[target];
            
            var start = _platform.transform.localPosition;
            var end = new Vector3(nextPipe.x, start.y);

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
