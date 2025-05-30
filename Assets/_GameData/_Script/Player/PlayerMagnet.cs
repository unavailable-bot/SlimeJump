using Platform;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerMagnet : MonoBehaviour
    {
        private Movement _playerMovement;
        private const float maxVelocityStepY = 1f;
        
        private void Start()
        {
            _playerMovement = GetComponent<Movement>();
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.name != "magnet" || !(this.gameObject.GetComponent<Rigidbody2D>().linearVelocityY <= maxVelocityStepY)) return;

            BoostPlatformCase(other);
            
            RunPlatformCase(other);

            IcePlatformCase(other);

            MagmaPlatformCase(other);

            _playerMovement.Jump();
        }

        private void BoostPlatformCase(Collider2D other)
        {
            if (other.gameObject.transform.parent.TryGetComponent(out BoostPlatform platform))
            {
                platform.PlayerOn();
            }
        }

        private void RunPlatformCase(Collider2D other)
        {
            if (other.gameObject.transform.parent.TryGetComponent(out RunPlatform platform))
            {
                platform.PlayerOn();
            }
        }

        private void IcePlatformCase(Collider2D other)
        {
            if (other.gameObject.transform.parent.TryGetComponent(out IcePlatform platform))
            {
                platform.PlayerOn();
            }
        }

        private void MagmaPlatformCase(Collider2D other)
        {
            if (other.gameObject.transform.parent.TryGetComponent(out MagmaPlatform platform))
            {
                platform.PlayerOn();
            }
        }
    }
}
