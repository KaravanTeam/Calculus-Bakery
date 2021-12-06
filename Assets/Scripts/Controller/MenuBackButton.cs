using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Controller
{
    [RequireComponent(typeof(Button))]
    internal sealed class MenuBackButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Exit);
        }

        private void Exit()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
