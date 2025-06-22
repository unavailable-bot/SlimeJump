using UIScript;
using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        
        [SerializeField] private PlatformBuilder _platformBuilder;
        [SerializeField] private BackgroundManager _backgroundManager;
        [SerializeField] private PlatformManager _platformManager;
        [SerializeField] private SplashScreen _splashScreen;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameManager _gameManager;
        
        private void Awake()
        {
            _gameManager.Initialize();
            _platformBuilder.Initialize(_mainCamera);
            _backgroundManager.Initialize();
            _splashScreen.Initialize();
            _platformManager.Initialize();
            _uiManager.Initialize();
        }
    }
}
