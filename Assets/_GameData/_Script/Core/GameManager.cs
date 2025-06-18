using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
            Time.fixedDeltaTime = 1f / 90;
            Debug.Log(Time.fixedDeltaTime);
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}
