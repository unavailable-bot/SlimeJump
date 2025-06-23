using Player;
using UIScript;
using UIScript.AllGUI;
using UnityEngine;

namespace Platform
{
    internal sealed class MagmaPlatform : Platformer
    {
        private UIHudCanvas _uiManager;
        
        private void Start()
        {
            _uiManager = GameObject.Find("Game{Canvas}").GetComponent<UIHudCanvas>();
        }
        
        internal override void PlayerOn()
        {
            SwitchElement.Instance.SetMagmaForm();
            StartCoroutine(_uiManager.SmoothScoreFontSize(48f, 0.5f));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name != "Player" || !(other.gameObject.GetComponent<Rigidbody2D>().linearVelocityY <= 0f)) return;
            
            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController == SwitchElement.Instance.IceSlime)
            {
                PlayerOn();
                return;
            }
            
            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController == SwitchElement.Instance.MagmaSlime)
            {
                SwitchElement.Instance.BoostScoreMultiplier();
            }
        }
    }
}
