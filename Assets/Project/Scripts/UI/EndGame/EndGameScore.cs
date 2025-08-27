using TMPro;
using UnityEngine;

namespace Project.Scripts.UI.EndGame
{
    public class EndGameScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private ScoreCounter _scoreCounter;

        private void OnEnable()
        {
            _text.text = _scoreCounter.TargetScore.ToString("0");
        }
    }
}