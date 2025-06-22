using UnityEngine;

namespace ModuleSystem.Modules
{
    public class DestroyAction : MonoBehaviour, IGameAction
    {
        public void Execute(GameObject target)
        {
            Debug.Log("Destroying");
            GameObject.Destroy(target);
        }
    }
}
