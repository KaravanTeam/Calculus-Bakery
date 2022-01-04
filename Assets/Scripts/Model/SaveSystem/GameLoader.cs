﻿using UnityEngine;
using View.SaveSystem;

namespace Model.SaveSystem
{
    internal sealed class GameLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private SettingsPanel _settingsPanel;
        

        private void Start()
        {
            if (!PlayerProfile.IsExist())
            {
                _mainPanel.SetActive(false);
                _settingsPanel.gameObject.SetActive(true);
                _settingsPanel.OpenRegisterForm();
                
                return;
            }

            PlayerProfile.Load();
            _settingsPanel.UpdateFields();
        }
    }
}
