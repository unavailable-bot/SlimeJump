using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private const float moveSpeed = 10f;
        private const float jumpForce = 8f;
        private float jumpForceMultiplier = 1f;
        private bool isJumping;

        private Rigidbody2D _rb;
        private Vector2 _moveDir;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            _moveDir = new Vector2(moveHorizontal * moveSpeed, _rb.linearVelocity.y);
            
            if(transform.position.x > 10f)
                transform.position = new Vector2(-10f, transform.position.y);
            if(transform.position.x < -10f)
                transform.position = new Vector2(10f, transform.position.y);
        }
        
        private void FixedUpdate()
        {
            
            _rb.linearVelocity = _moveDir;

            if (isJumping || !(_rb.linearVelocity.y <= 0)) return;
            
            _rb.AddForce(Vector2.up * (jumpForce * jumpForceMultiplier), ForceMode2D.Impulse);
            jumpForceMultiplier = 1f;
            isJumping = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag($"Platform") && _rb.linearVelocity.y <= 0)
            {
                isJumping = false;
                //_tJumpBoost(other);
            }
        }

        private void _tJumpBoost(Collision2D other)
        {
            if (other.gameObject.name == "MagmaPlatform")
            {
                jumpForceMultiplier += 1f;
            }
        }
    }
}
