using UnityEngine;

namespace CamScript
{
    internal sealed class CamFollower : MonoBehaviour
    {
        private const float followSpeedMultiplier = 2f;
        private Rigidbody2D _playerRb;

        private void Start()
        {
            _playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        }

        private void LateUpdate()
        {
            float distanceDiff = _playerRb.transform.position.y - this.transform.position.y;
            float maxDistanceDiff = 1f;
            
            if (!(distanceDiff > maxDistanceDiff)) return;
            
            float targetPositionY = Mathf.Max(_playerRb.transform.position.y, 0f);
            Vector3 targetPosition = new Vector3(this.transform.position.x, targetPositionY, this.transform.position.z);
            
            float followSpeed = followSpeedMultiplier * Mathf.Abs(_playerRb.linearVelocityY) * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, followSpeed);
        }
    }
}
