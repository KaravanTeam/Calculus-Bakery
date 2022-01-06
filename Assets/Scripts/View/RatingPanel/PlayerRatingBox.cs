using Model.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal sealed class PlayerRatingBox : MonoBehaviour
    {
        [SerializeField] private int _progress;
        [SerializeField] private Text _position;
        [SerializeField] private Text _nickname;
        [SerializeField] private Text _progressField;
        [SerializeField] private PlayerRatingBoxType _type;

        public int Progress => _progress;
        public PlayerRatingBoxType Type => _type;

        public void UpdateProfile()
        {
            if (_type == PlayerRatingBoxType.Player)
            {
                _progress = PlayerProfile.Instance.Points;
                _nickname.text = PlayerProfile.Instance.Nickname; 
            }

            _progressField.text = $"{_progress}%";
        }

        public void UpdatePosition(int position)
        {
            _position.text = position.ToString();
        }
    }
}
