using Player;
using UIScript;
using UnityEngine;

namespace Platform
{
    internal sealed class IcePlatform : PlatformLevelMarker
    {
        private UIManager _uiManager;
        
        private void Start()
        {
            _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        }
        
        internal void PlayerOn()
        {
            SwitchElement.SetIceForm();
            StartCoroutine(_uiManager.SmoothScoreFontSize(48f, 0.5f));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name != "Player" || !(other.gameObject.GetComponent<Rigidbody2D>().linearVelocityY <= 0f)) return;
            
            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name == SwitchElement.MagmaSlime.name)
            {
                PlayerOn();
                return;
            }

            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name == SwitchElement.IceSlime.name)
            {
                SwitchElement.BoostScoreMultiplier();
            }
        }
    }
}
