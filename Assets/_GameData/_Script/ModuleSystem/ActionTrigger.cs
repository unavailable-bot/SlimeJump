using System.Collections.Generic;
using UnityEngine;

namespace ModuleSystem
{
    public class ActionTrigger : MonoBehaviour
    {
        [SerializeField] private List<ActionEntry> actionEntries;

        private void Awake()
        {
            foreach (var entry in actionEntries)
            {
                if (entry.runOnAwake && entry.action is IGameAction act)
                {
                    act.Execute(gameObject);
                    if (entry.runOnce)
                        entry.hasRun = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D  other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            foreach (var entry in actionEntries)
            {
                if (entry.runOnce && entry.hasRun)
                    continue;

                if (entry.action is IGameAction act)
                {
                    act.Execute(gameObject);

                    if (entry.runOnce)
                        entry.hasRun = true;
                }
            }
        }
    }
}
