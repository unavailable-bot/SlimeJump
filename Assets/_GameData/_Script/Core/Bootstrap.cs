using UnityEngine;
using SplashScreen = UIScript.SplashScreen;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        
        [SerializeField] private PlatformBuilder _platformBuilder;
        [SerializeField] private BackgroundManager _backgroundManager;
        [SerializeField] private PlatformManager _platformManager;
        [SerializeField] private SplashScreen _splashScreen;
        
        private void Awake()
        {
            _platformBuilder.Initialize(_mainCamera);
            _backgroundManager.Initialize();
            _splashScreen.Initialize();
            _platformManager.Initialize();
        }
    }
}
