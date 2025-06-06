using UnityEngine;

namespace CamScript
{
    internal sealed class CamFollower : MonoBehaviour
    {
        private const float smoothTime = 0.25f; // Чем больше — тем плавнее (0.15–0.3 норм)
        private Transform _playerTransform;
        private Vector3 _velocity = Vector3.zero;

        private void Start()
        {
            _playerTransform = GameObject.Find("Player").transform;
        }

        private void LateUpdate()
        {
            // Двигаемся только вверх — камера не опускается
            if (_playerTransform.position.y > transform.position.y)
            {
                Vector3 targetPos = new Vector3(
                    transform.position.x,
                    _playerTransform.position.y,
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
}
