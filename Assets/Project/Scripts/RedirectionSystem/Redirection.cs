using Project.Scripts.RedirectionSystem.Enums;
using Project.Scripts.UI;
using Project.Scripts.UI.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Scripts.RedirectionSystem
{
    public class Redirection : MonoBehaviour
    {
        [SerializeField] private Button _redirectButton;
        [SerializeField] private SceneID _sceneID;
        [SerializeField] private CanvasManager _canvasManager;

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
            _canvasManager.ChangeCanvas(GameCanvasType.Loading);
            
            SceneManager.LoadScene((int)_sceneID);
        }
    }
}