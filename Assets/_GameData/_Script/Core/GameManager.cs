using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 90;
            Time.fixedDeltaTime = 1f / Application.targetFrameRate;
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}
