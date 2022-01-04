using UnityEngine;

namespace Model.SaveSystem
{
    [CreateAssetMenu]
    internal class PlayerProfile : ScriptableObject
    {
        public string Nickname;
        public string Name;
        public string Group;

        private static PlayerProfile _instance;

        private static readonly string _nicknameField = "nickname"; 
        private static readonly string _nameField = "name"; 
        private static readonly string _groupField = "group";

        private PlayerProfile()
        {
            _instance = this;
        }

        public static void Save(string nickname, string name, string group)
        {
            PlayerPrefs.SetString(_nicknameField, nickname);
            PlayerPrefs.SetString(_nameField, name);
            PlayerPrefs.SetString(_groupField, group);

            PlayerPrefs.Save();

            _instance.Nickname = nickname;
            _instance.Name = name;
            _instance.Group = group;
        }

        public static void Load()
        {
            _instance.Nickname = PlayerPrefs.GetString(_nicknameField);
            _instance.Name = PlayerPrefs.GetString(_nameField);
            _instance.Group = PlayerPrefs.GetString(_groupField);
        }

        public static bool IsExist()
        {
            return PlayerPrefs.HasKey(_nicknameField) 
                && PlayerPrefs.HasKey(_nameField) 
                && PlayerPrefs.HasKey(_groupField);
        }
    }
}
