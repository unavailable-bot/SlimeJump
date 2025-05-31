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
            SwitchElement.Instance.SetIceForm();
            StartCoroutine(_uiManager.SmoothScoreFontSize(48f, 0.5f));
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
            
            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name == SwitchElement.Instance.MagmaSlime.name)
            {
                PlayerOn();
                return;
            }

            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name == SwitchElement.Instance.IceSlime.name)
            {
                SwitchElement.Instance.BoostScoreMultiplier();
            }
        }
    }
}
