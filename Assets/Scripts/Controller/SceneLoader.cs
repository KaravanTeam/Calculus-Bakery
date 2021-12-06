using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Controller
{
    [RequireComponent(typeof(Button))]
    internal sealed class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string _name;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Load);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Load);
        }

        private void Load()
        {
            SceneManager.LoadScene(_name);
        }
    }
}
