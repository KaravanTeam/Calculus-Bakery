using Model;
using Model.Transporter;
using UnityEngine;

namespace View
{
    internal sealed class ChefEmoji : MonoBehaviour
    {
        [SerializeField] private Chef _chef;

        [SerializeField] private Animator _happyEmoji;
        [SerializeField] private Animator _angryEmoji;

        private readonly string _triggerName = "IsEntry";

        private void OnEnable()
        {
            _chef.OnCorrectCakeChecked += PlayHappy;
            _chef.OnWrongCakeChecked += PlayAngry;
        }

        private void OnDisable()
        {
            _chef.OnCorrectCakeChecked -= PlayHappy;
            _chef.OnWrongCakeChecked -= PlayAngry;
        }

        private void PlayHappy(Cake cake)
        {
            _happyEmoji.SetTrigger(_triggerName);
        }

        private void PlayAngry(Cake cake)
        {
            _angryEmoji.SetTrigger(_triggerName);
        }
    }
}
