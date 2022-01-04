using UnityEngine;

namespace Model.SaveSystem
{
    [CreateAssetMenu]
    internal sealed class PlayerProfileInfo : ScriptableObject
    {
        public string Nickname;
        public string Name;
        public string Group;
    }
}
