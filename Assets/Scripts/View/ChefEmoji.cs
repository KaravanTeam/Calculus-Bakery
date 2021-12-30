using Model;
using Model.Transporter;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Animator))]
    internal sealed class ChefEmoji : MonoBehaviour
    {
        [Header("Chef")]
        [SerializeField] private Chef _chef;
        [SerializeField] private Sprite _happyChef;
        [SerializeField] private Sprite _sadChef;
        [SerializeField] private Sprite _happyChefReplica;
        [SerializeField] private Sprite _sadChefReplica;

        [Header("Congratulations")]
        [SerializeField] private CongratulationPanel _congratulationPanel;

        [Header("Containers")]
        [SerializeField] private Image _replicaContainer;
        [SerializeField] private Image _chefContainer;

        private Sprite _nextReplica;

        private Animator _animator;
        private readonly string _openingTrigger = "Opening";
        private readonly string _closingTrigger = "Closing";
        private readonly string _manualControlTrigger = "ManualControl";
        private readonly string _changingReplicaTrigger = "ChangingReplica";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _chef.OnCorrectCakeChecked += ShowHappyChef;
            _chef.OnWrongCakeChecked += ShowSadChef;
        }

        private void OnDisable()
        {
            _chef.OnCorrectCakeChecked -= ShowHappyChef;
            _chef.OnWrongCakeChecked -= ShowSadChef;
        }

        public void ShowWith(Sprite replica)
        {
            _animator.SetTrigger(_manualControlTrigger);
            Open(_happyChef, replica);
        }

        public void Close()
        {
            _animator.SetTrigger(_closingTrigger);
        }

        public void NextReplica(Sprite replica)
        {
            _nextReplica = replica;

            _animator.SetTrigger(_manualControlTrigger);
            _animator.SetTrigger(_changingReplicaTrigger);
            _animator.SetTrigger(_closingTrigger);
        }

        #region AnimationEvents
        public void OnPanelDeactivated()
        {
            _congratulationPanel.Show();
        }

        public void OnReplicaClosed()
        {
            _replicaContainer.sprite = _nextReplica;
        }
        #endregion

        private void ShowHappyChef(Cake cake)
        {          
            Open(_happyChef, _happyChefReplica);
        }

        private void ShowSadChef(Cake cake)
        {
            Open(_sadChef, _sadChefReplica);
        }

        private void Open(Sprite chefEmoji, Sprite replica)
        {
            _chefContainer.sprite = chefEmoji;
            _replicaContainer.sprite = _nextReplica = replica;

            _animator.SetTrigger(_openingTrigger);
        }
    }
}
