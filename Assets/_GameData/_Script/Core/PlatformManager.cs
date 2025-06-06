using UnityEngine;

namespace Core
{
    internal sealed class PlatformManager : MonoBehaviour
    {
        internal static PlatformManager Instance { get; private set; }
        internal float RunPlatformSpeedMultiplier { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            ResetMultiplier();
        }

        internal void IncreaseMultiplier(float delta)
        {
            if(RunPlatformSpeedMultiplier > 20f) return;
            
            if (delta is > 0.05f or < 0.05f)
            {
                delta = 0.05f;
            }
            RunPlatformSpeedMultiplier += delta;
            
            //DebugGetCurrentMultiplier("Increase to: ");
        }
        
        internal void DecreaseMultiplier(float delta)
        {
            if(RunPlatformSpeedMultiplier == 0f) return;
            
            if (delta is > 0.5f or < 0.5f)
            {
                delta = 0.5f;
            }
            RunPlatformSpeedMultiplier -= delta;
            
            //DebugGetCurrentMultiplier("Decrease to: ");
        }

        private void ResetMultiplier()
        {
            RunPlatformSpeedMultiplier = 0f;
            
            //DebugGetCurrentMultiplier("Reset to: ");
        }
        
        private void DebugGetCurrentMultiplier(string source)
        {
            Debug.Log(source+RunPlatformSpeedMultiplier);
        }
    }
}
