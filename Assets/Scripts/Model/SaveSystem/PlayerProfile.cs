using UnityEngine;

namespace Model.SaveSystem
{
    internal static class PlayerProfile
    {
        private static readonly string _nicknameField = "nickname"; 
        private static readonly string _nameField = "name"; 
        private static readonly string _groupField = "group"; 

        public static void Save(string nickname, string name, string group)
        {
            PlayerPrefs.SetString(_nicknameField, nickname);
            PlayerPrefs.SetString(_nameField, name);
            PlayerPrefs.SetString(_groupField, group);

            PlayerPrefs.Save();
        }

        public static void Load(PlayerProfileInfo profile)
        {
            profile.Nickname = PlayerPrefs.GetString(_nicknameField);
            profile.Name = PlayerPrefs.GetString(_nameField);
            profile.Group = PlayerPrefs.GetString(_groupField);
        }

        public static bool IsExist()
        {
            return PlayerPrefs.HasKey(_nicknameField) 
                && PlayerPrefs.HasKey(_nameField) 
                && PlayerPrefs.HasKey(_groupField);
        }
    }
}
