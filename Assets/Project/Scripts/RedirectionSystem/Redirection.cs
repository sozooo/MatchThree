using Project.Scripts.RedirectionSystem.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Scripts.RedirectionSystem
{
    public class Redirection : MonoBehaviour
    {
        [SerializeField] private Button _redirectButton;
        [SerializeField] private SceneID _sceneID;
        [SerializeField] private Image _loadScreen;

        private void OnEnable()
        {
            _redirectButton.onClick.AddListener(Redirect);
        }

        private void OnDisable()
        {
            _redirectButton.onClick.RemoveListener(Redirect);
        }

        private void Redirect()
        {
            _loadScreen.gameObject.SetActive(true);
            SceneManager.LoadScene((int)_sceneID);
        }
    }
}