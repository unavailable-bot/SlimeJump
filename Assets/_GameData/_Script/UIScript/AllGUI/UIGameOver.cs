using System.Globalization;
using Core.EventBas;
using TMPro;
using UIScript.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UIScript.AllGUI
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] ViewManager _viewManager;
        [SerializeField] private TMP_Text _totalScore;
        [SerializeField] private TMP_Text _totalBurgerCount;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartGameButton;
        
        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnSubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            GameEventBas.OnSetTotalScore += SetTotalScore;
            GameEventBas.OnGameOver += GameOver;
            _menuButton.onClick.AddListener(() =>
            {
                _viewManager.ActivateView(1);
            });
            _restartGameButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }

        private void UnSubscribeFromEvents()
        {
            GameEventBas.OnSetTotalScore -= SetTotalScore;
            GameEventBas.OnGameOver -= GameOver;
            _menuButton.onClick.RemoveAllListeners();
        }

        private void SetTotalScore()
        {
            var score = (int)UIHudCanvas.I.Score;
            _totalScore.text = $"Total: {score}";
            _totalBurgerCount.text = CharacterModel.Burger.ToString();
        }

        private void GameOver()
        {
            _viewManager.ActivateView(2);
        }
    }
}
