using UnityEngine;

namespace ModuleSystem.Modules
{
    public class DestroyAction : MonoBehaviour, IGameAction
    {
        public void Execute(GameObject target)
        {
            GameObject.Destroy(target);
        }
    }
}
