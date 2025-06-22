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

        private static void BoostPlatformCase(Collider2D other)
        {
            if (other.gameObject.transform.parent.TryGetComponent(out BoostPlatform platform))
            {
                platform.PlayerOn();
            }
        }

        private static void RunPlatformCase(Collider2D other)
        {
            if (other.gameObject.transform.parent.TryGetComponent(out RunPlatform platform))
            {
                platform.PlayerOn();
            }
        }

        private void IcePlatformCase(Collider2D other)
        {
            if (!other.gameObject.transform.parent.TryGetComponent(out IcePlatform platform)) return;
            
            if (gameObject.GetComponent<Animator>().runtimeAnimatorController == SwitchElement.Instance.MagmaSlime)
            {
                platform.PlayerOn();
                return;
            }
            
            if (gameObject.GetComponent<Animator>().runtimeAnimatorController == SwitchElement.Instance.IceSlime)
            {
                SwitchElement.Instance.BoostScoreMultiplier();
            }
        }

        private void MagmaPlatformCase(Collider2D other)
        {
            if (!other.gameObject.transform.parent.TryGetComponent(out MagmaPlatform platform)) return;
            
            if (gameObject.GetComponent<Animator>().runtimeAnimatorController == SwitchElement.Instance.IceSlime)
            {
                platform.PlayerOn();
                return;
            }
            
            if (gameObject.GetComponent<Animator>().runtimeAnimatorController == SwitchElement.Instance.MagmaSlime)
            {
                SwitchElement.Instance.BoostScoreMultiplier();
            }
        }
    }
}
