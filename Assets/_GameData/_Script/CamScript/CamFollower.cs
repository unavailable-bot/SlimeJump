using UnityEngine;

namespace CamScript
{
    public class CamFollower : MonoBehaviour
    {
        private const float speedMultiplier = 2f;
        private Rigidbody2D _playerRb;

        private void Start()
        {
            _playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        }

        private void LateUpdate()
        {
            float diff = _playerRb.transform.position.y - this.transform.position.y;
            float maxDiff = 1f;
            
            if (!(diff > maxDiff)) return;
            
            float targetY = Mathf.Max(_playerRb.transform.position.y, 0f); // Всегда >= 0
            Vector3 targetPosition = new Vector3(this.transform.position.x, targetY, this.transform.position.z);
            
            float followSpeed = speedMultiplier * Mathf.Abs(_playerRb.linearVelocityY) * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, followSpeed);
        }
    }
}
