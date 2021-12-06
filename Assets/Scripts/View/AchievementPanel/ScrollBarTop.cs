using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Scrollbar))]
    internal sealed class ScrollBarTop : MonoBehaviour
    {
        private Scrollbar _bar;

        private void Awake()
        {
            _bar = GetComponent<Scrollbar>();
        }

        private void Start()
        {
            _bar.value = 1;
        }
    }
}
