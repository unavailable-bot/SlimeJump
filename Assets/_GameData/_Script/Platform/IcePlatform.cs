using Player;
using UnityEngine;

namespace Platform
{
    internal sealed class IcePlatform : MonoBehaviour
    {
        internal void PlayerOn()
        {
            SwitchElement.SetIceForm();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name != "Player" || !(other.gameObject.GetComponent<Rigidbody2D>().linearVelocityY <= 0f)) return;
            
            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name == SwitchElement.MagmaSlime.name)
            {
                PlayerOn();
            }
        }
    }
}
