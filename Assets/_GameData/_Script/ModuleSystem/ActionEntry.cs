using UnityEngine;

namespace ModuleSystem
{
    [System.Serializable]
    public class ActionEntry
    {
        public MonoBehaviour action; // должен реализовать IGameAction
        public bool runOnce;
        public bool runOnAwake;

        [HideInInspector] public bool hasRun = false;
    }
}