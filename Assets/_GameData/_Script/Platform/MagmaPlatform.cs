using Player;
using UnityEngine;

namespace Platform
{
    public class MagmaPlatform : MonoBehaviour
    {
        private static void PlayerOn()
        {
            SwitchElement.SetMagmaForm();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name != "Player") return;
            
            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name == SwitchElement.IceSlime.name)
            {
                PlayerOn();
            }
        }
    }
}
