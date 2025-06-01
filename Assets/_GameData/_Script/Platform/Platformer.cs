using UnityEngine;

namespace Platform
{
    internal abstract class Platformer : MonoBehaviour
    {
        internal int levelIndex;
        internal abstract void PlayerOn();
    }
}
