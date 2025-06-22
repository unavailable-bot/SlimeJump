using UnityEngine;

namespace ModuleSystem
{
    public interface IGameAction
    {
        void Execute(GameObject target);
    }
}
