using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal sealed class PlayerRating : MonoBehaviour
    {
        [SerializeField] private RatingProgressBar _bar;
        [SerializeField] private Scrollbar _verticalScroll;

        public void Open()
        {
            _verticalScroll.value = 1;
            _bar.UpdateBar();

            var boxes = transform.GetComponentsInChildren<PlayerRatingBox>();

            foreach (var box in boxes)
                box.UpdateProfile();

            boxes = boxes
                .OrderByDescending(box => box.Progress)
                .ToArray();

            for (var i = 0; i < boxes.Length; i++)
            {
                boxes[i].transform.SetSiblingIndex(i);
                boxes[i].UpdatePosition(i + 1);

                if (boxes[i].Type == PlayerRatingBoxType.Player)
                    _verticalScroll.value = 1 - (i + 1f) / boxes.Length;
            }
        }
    }
}
