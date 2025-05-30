using Player;
using UnityEngine;

namespace Platform
{
    public class IcePlatform : MonoBehaviour
    {
        private static void PlayerOn()
        {
            SwitchElement.SetIceForm();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name != "Player") return;
            
            if (other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name == SwitchElement.MagmaSlime.name)
            {
                PlayerOn();
            }
        }
    }
}
