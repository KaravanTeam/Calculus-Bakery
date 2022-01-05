using UnityEngine;
using UnityEngine.UI;
using View.SaveSystem;

namespace Model.SaveSystem
{
    internal sealed class GameLoader : MonoBehaviour
    {
        [Header("MainPanel")]
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Text _points;

        [Header("SettingsPanel")]
        [SerializeField] private SettingsPanel _settingsPanel;
        
        private void Start()
        {
            PlayerProfile.Initialize();

            if (!PlayerProfile.IsExist())
            {
                PlayerProfile.Clear();

                _mainPanel.SetActive(false);
                _settingsPanel.gameObject.SetActive(true);
                _settingsPanel.OpenRegisterForm();
            }
            else
            {
                PlayerProfile.Load();
            }

            _points.text = $"{PlayerProfile.Instance.Points}%";
            _settingsPanel.UpdateFields();
        }
    }
}
