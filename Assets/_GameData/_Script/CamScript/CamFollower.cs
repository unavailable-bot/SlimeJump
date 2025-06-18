using UnityEngine;

namespace CamScript
{
    internal sealed class CamFollower : MonoBehaviour
    {
        [SerializeField] private Transform _playerTarget;
        private const float smoothTime = 0.2f; // Чем больше — тем плавнее (0.15–0.3 норм)
        private Vector3 _velocity = Vector3.zero;

        private void LateUpdate()
        {
            // Двигаемся только вверх — камера не опускается
            if (!(_playerTarget.position.y > transform.position.y)) return;
            
            var targetPos = new Vector3(
                transform.position.x,
                _playerTarget.position.y,
                transform.position.z
            );

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPos,
                ref _velocity,
                smoothTime
            );
        }
    }
}
