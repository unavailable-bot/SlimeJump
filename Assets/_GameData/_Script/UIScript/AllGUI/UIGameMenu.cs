using Core.EventBas;
using TMPro;
using UIScript.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIScript.AllGUI
{
    public class UIGameMenu : MonoBehaviour
    {
        [SerializeField] ViewManager _viewManager;
        [SerializeField] private TMP_Text _bestScoreText;
        [SerializeField] private TMP_Text _bestBurgerCount;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitGameButton;
        
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
            GameEventBas.OnSetBestScore += SetBestScore;
            _resumeButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
            _exitGameButton.onClick.AddListener((Application.Quit));
        }

        private void UnSubscribeFromEvents()
        {
            GameEventBas.OnSetBestScore -= SetBestScore;
            _resumeButton.onClick.RemoveAllListeners();
        }

        private void SetBestScore()
        {
            // когда заканчивается игра, например в GameOver:
            if (CharacterModel.BestScore < (int)UIHudCanvas.I.Score)
            {
                CharacterModel.BestScore = (int)UIHudCanvas.I.Score;
                PlayerPrefs.SetInt("BestScore", CharacterModel.BestScore);
                PlayerPrefs.Save();
            }

            if (CharacterModel.BestBurgerCount < CharacterModel.Burger)
            {
                CharacterModel.BestBurgerCount = CharacterModel.Burger;
                PlayerPrefs.SetInt("BestBurgerCount", CharacterModel.BestBurgerCount);
                PlayerPrefs.Save();
            }
            
            _bestScoreText.text = $"Best: {CharacterModel.BestScore}";
            _bestBurgerCount.text = CharacterModel.BestBurgerCount.ToString();
        }
    }
}
