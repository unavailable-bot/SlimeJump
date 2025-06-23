using System;
using UIScript;
using UIScript.AllGUI;
using UIScript.Model;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        
        [SerializeField] private PlatformBuilder _platformBuilder;
        [SerializeField] private BackgroundManager _backgroundManager;
        [SerializeField] private PlatformManager _platformManager;
        [SerializeField] private SplashScreen _splashScreen;
        [FormerlySerializedAs("_uiManager")] [SerializeField] private UIHudCanvas uiHudCanvas;
        [SerializeField] private GameManager _gameManager;
        
        private void Awake()
        {
            _gameManager.Initialize();
            _platformBuilder.Initialize(_mainCamera);
            _backgroundManager.Initialize();
            _splashScreen.Initialize();
            _platformManager.Initialize();
            uiHudCanvas.Initialize();
            CharacterModel.BestScore = PlayerPrefs.GetInt("BestScore", 0);
            CharacterModel.BestBurgerCount = PlayerPrefs.GetInt("BestBurgerCount", 0);
            CharacterModel.Burger = 0;
        }
    }
}
