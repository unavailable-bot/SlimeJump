using UnityEngine;

namespace ModuleSystem.Modules
{
    public class PlayParticlesAction : MonoBehaviour, IGameAction
    {
        public void Execute(GameObject target)
        {
            // Найти ParticleSystem среди дочерних объектов (даже если он выключен)
            ParticleSystem ps = target.GetComponentInChildren<ParticleSystem>(true);
            if (ps != null)
            {
                // Отсоединить партиклы от бургера
                ps.transform.SetParent(null, true);

                // Активировать, если вдруг выключено
                ps.gameObject.SetActive(true);

                // Воспроизвести эффект
                ps.Play();

                // Уничтожить партиклы после окончания
                Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
            }

            // Уничтожить бургер сразу
            Destroy(target);
        }
    }
}
