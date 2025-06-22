using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        internal void Initialize()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
            Time.fixedDeltaTime = 1f / 90;
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}
