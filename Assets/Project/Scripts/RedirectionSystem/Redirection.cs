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
            SceneManager.LoadScene((int)_sceneID);
        }
    }
}