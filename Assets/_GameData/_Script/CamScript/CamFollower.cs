using UnityEngine;

namespace CamScript
{
    internal sealed class CamFollower : MonoBehaviour
    {
        private const float SMOOTH_TIME = 0.2f;
        
        [SerializeField] private Transform _playerTarget;
        private Vector3 _velocity = Vector3.zero;
        
        private void LateUpdate()
        {
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
                SMOOTH_TIME
            );
        }
    }
}
