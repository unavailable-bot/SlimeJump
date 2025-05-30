using Player;
using UnityEngine;

namespace Platform
{
    internal sealed class MagmaPlatform : MonoBehaviour
    {
        internal void PlayerOn()
        {
            SwitchElement.SetMagmaForm();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name != "Player" || !(other.gameObject.GetComponent<Rigidbody2D>().linearVelocityY <= 0f)) return;
            
            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name == SwitchElement.IceSlime.name)
            {
                PlayerOn();
            }
        }
    }
}
