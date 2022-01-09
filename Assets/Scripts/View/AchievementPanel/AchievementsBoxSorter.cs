using System.Linq;
using UnityEngine;

namespace View
{
    internal sealed class AchievementsBoxSorter : MonoBehaviour
    {
        private void Start()
        {
            var sorted = transform.GetComponentsInChildren<BrainAchievementTask>()
                .OrderBy(task => task.Achievement.OrderNumber).ToArray();


            for (var i = 0; i < sorted.Length; i++)
                sorted[i].transform.SetSiblingIndex(i);
        }
    }
}
