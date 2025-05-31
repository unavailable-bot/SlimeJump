using Player;
using UnityEngine;

namespace Platform
{
    internal sealed class BoostPlatform : PlatformLevelMarker
    {
        internal void PlayerOn()
        {
            Movement.JumpForceMultiplier = 0.5f;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name == "Player" && other.gameObject.GetComponent<Rigidbody2D>().linearVelocityY <= 0f)
            {
                PlayerOn();
            }
        }
    }
}
