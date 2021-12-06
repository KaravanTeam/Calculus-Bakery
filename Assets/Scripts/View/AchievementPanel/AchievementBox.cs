using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(BrainAchievementTask))]
    internal sealed class AchievementBox : MonoBehaviour
    {
        [SerializeField] private Text _numberField;
        [SerializeField] private Text _taskField;
        [SerializeField] private Text _progressField;
        [SerializeField] private Text _pointsField;

        private BrainAchievementTask _task;

        private void Awake()
        {
            _task = GetComponent<BrainAchievementTask>();

            UpdateFields();
        }

        private void OnEnable()
        {
            _task.Achievement.OnStateUpdated += UpdateFields;
        }

        private void OnDisable()
        {
            _task.Achievement.OnStateUpdated -= UpdateFields;
        }

        private void UpdateFields()
        {
            _numberField.text = $"#{_task.Achievement.OrderNumber}";
            _taskField.text = _task.Achievement.Text;
            _progressField.text = $"{_task.Achievement.Score} / {_task.Achievement.Target}";
            _pointsField.text = $"+{_task.Achievement.Points}%";
        }
    }
}
