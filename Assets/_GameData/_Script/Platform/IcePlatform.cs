using Player;
using UIScript;
using UIScript.AllGUI;
using UnityEngine;

namespace Platform
{
    internal sealed class IcePlatform : Platformer
    {
        private UIHudCanvas _uiHudCanvas;
        
        private void Start()
        {
            _uiHudCanvas = GameObject.Find("Game{Canvas}").GetComponent<UIHudCanvas>();
        }
        
        internal override void PlayerOn()
        {
            SwitchElement.Instance.SetIceForm();
            StartCoroutine(_uiHudCanvas.SmoothScoreFontSize(48f, 0.5f));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name != "Player" || !(other.gameObject.GetComponent<Rigidbody2D>().linearVelocityY <= 0f)) return;
            
            var animator = other.gameObject.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator is NULL!");
                return;
            }
            if (animator.runtimeAnimatorController == null)
            {
                Debug.LogWarning("runtimeAnimatorController is NULL!");
                return;
            }
            if (SwitchElement.Instance.MagmaSlime == null)
            {
                Debug.LogWarning("SwitchElement.MagmaSlime is NULL!");
                return;
            }
            
            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController == SwitchElement.Instance.MagmaSlime)
            {
                PlayerOn();
                return;
            }

            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController == SwitchElement.Instance.IceSlime)
            {
                SwitchElement.Instance.BoostScoreMultiplier();
            }
        }
    }
}
