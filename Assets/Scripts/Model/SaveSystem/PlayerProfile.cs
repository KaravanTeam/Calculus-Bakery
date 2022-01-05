using System;
using UnityEngine;

namespace Model.SaveSystem
{
    [CreateAssetMenu]
    internal class PlayerProfile : ScriptableObject
    {
        public static PlayerProfile Instance;

        private static readonly string _nicknameField = "nickname"; 
        private static readonly string _nameField = "name"; 
        private static readonly string _groupField = "group";
        private static readonly string _pointsField = "points";
        private static readonly string _cakesCountField = "cakesCount";
        private static readonly string _rankField = "rank";

        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public int Points { get; set; }
        public int CakesCount { get; set; }
        public string Rank { get; set; }

        public event Action<int> OnProgressUpdated;

        private PlayerProfile()
        {
            if (Instance is null)
                Instance = this;
        }

        public static void Initialize()
        {
            Instance = (PlayerProfile)CreateInstance(typeof(PlayerProfile));
        }

        public void AddPoints(int value)
        {
            Points += value;
            SavePoints(Points);
            OnProgressUpdated?.Invoke(Points);
        }

        public static void Save(string nickname, string name, string group)
        {
            Instance.Nickname = nickname;
            Instance.Name = name;
            Instance.Group = group;

            PlayerPrefs.SetString(_nicknameField, nickname);
            PlayerPrefs.SetString(_nameField, name);
            PlayerPrefs.SetString(_groupField, group);

            PlayerPrefs.Save();
        }

        public static void SaveRank()
        {
            PlayerPrefs.SetString(_rankField, Instance.Rank);
            PlayerPrefs.Save();
        }

        public static void SavePoints(int points)
        {
            Instance.Points = points;
            PlayerPrefs.SetInt(_pointsField, points);
            PlayerPrefs.Save();
        }

        public static void SaveCakesCount(int count)
        {
            Instance.CakesCount = count;
            PlayerPrefs.SetInt(_cakesCountField, count);
            PlayerPrefs.Save();
        }

        public static void Load()
        {
            Instance.Nickname = PlayerPrefs.GetString(_nicknameField);
            Instance.Name = PlayerPrefs.GetString(_nameField);
            Instance.Group = PlayerPrefs.GetString(_groupField);
            Instance.Points = PlayerPrefs.GetInt(_pointsField);
            Instance.CakesCount = PlayerPrefs.GetInt(_cakesCountField);
            Instance.Rank = PlayerPrefs.GetString(_rankField);
        }

        public static bool IsExist()
        {
            return PlayerPrefs.HasKey(_nicknameField) 
                && PlayerPrefs.HasKey(_nameField) 
                && PlayerPrefs.HasKey(_groupField);
        }

        public static void Clear()
        {
            Instance.Nickname = null;
            Instance.Name = null;
            Instance.Group = null;
            Instance.Points = 0;
            Instance.CakesCount = 0;
            Instance.Rank = null;

            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
