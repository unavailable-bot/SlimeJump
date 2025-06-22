using UnityEngine;

namespace ModuleSystem.Modules
{
    public class SpawnAction : MonoBehaviour, IGameAction
    {
        public GameObject _prefab;
        public Vector3 offset = new(0f, 0.5f, 0f);
        public void Execute(GameObject target)
        {
            GameObject.Instantiate(_prefab, this.transform.position + offset, Quaternion.identity, this.transform);
        }
    }
}
