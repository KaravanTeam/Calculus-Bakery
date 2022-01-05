using Model.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace View.SaveSystem
{
    internal sealed class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Text _points;

        [Header("RegisterForm")]
        [SerializeField] private GameObject _registerForm;
        [SerializeField] private InputField _nicknameField;
        [SerializeField] private InputField _nameField;
        [SerializeField] private InputField _groupField;

        [Header("InfoPanel")]
        [SerializeField] private GameObject _infoPanel;
        [SerializeField] private Text _nickname;
        [SerializeField] private Text _name;
        [SerializeField] private Text _group;
        [SerializeField] private Image _soundToggler;
        [SerializeField] private Sprite _soundOn;
        [SerializeField] private Sprite _soundOff;
        [SerializeField] private AudioListener _audioListener;

        public void OpenRegisterForm()
        {
            _infoPanel.SetActive(false);
            _registerForm.SetActive(true);
        }

        public void SignUp()
        {
            var nickname = _nicknameField.text;
            var name = _nameField.text;
            var group = _groupField.text;

            if (nickname.Length == 0 || name.Length == 0 || group.Length == 0)
                return;

            PlayerProfile.Save(nickname, name, group);
            UpdateFields();

            gameObject.SetActive(false);
            _infoPanel.SetActive(true);
            _registerForm.SetActive(false);

            _mainPanel.SetActive(true);
        }

        public void SignOut()
        {
            PlayerProfile.Clear();

            _mainPanel.SetActive(false);
            _points.text = "0%";
            OpenRegisterForm();
        }

        public void UpdateFields()
        {
            _nickname.text = PlayerProfile.Instance.Nickname;
            _name.text = PlayerProfile.Instance.Name;
            _group.text = PlayerProfile.Instance.Group;
        }

        public void ToggleSound()
        {
            var isOn = _soundToggler.sprite == _soundOn;

            _soundToggler.sprite = isOn ? _soundOff : _soundOn;
            _audioListener.enabled = isOn;
        }
    }
}