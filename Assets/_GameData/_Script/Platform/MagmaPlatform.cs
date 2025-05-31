using Player;
using UnityEngine;

namespace Platform
{
    internal sealed class MagmaPlatform : PlatformLevelMarker
    {
        private Transform _iceFire;
        private Transform _magmaFire;
        
        private void Start()
        {
            _iceFire = GameObject.Find("IceFire").transform;
            _magmaFire = GameObject.Find("MagmaFire").transform;
        }
        
        internal void PlayerOn()
        {
            SwitchElement.SetMagmaForm();
            StartCoroutine(SwitchElement.SmoothScoreFontSize(48f, 0.5f));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name != "Player" || !(other.gameObject.GetComponent<Rigidbody2D>().linearVelocityY <= 0f)) return;
            
            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name == SwitchElement.IceSlime.name)
            {
                PlayerOn();
                return;
            }
            
            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name == SwitchElement.MagmaSlime.name)
            {
                SwitchElement.BoostScoreMultiplier();
            }
        }
    }
}
