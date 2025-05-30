using Platform;
using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private const float moveSpeed = 10f;
        private const float jumpForce = 10f;
        private const float borderX = 10f;
        private const float fixedValue = 0.5f;
        private const float maxJumpForce = 1.5f;
        
        private static float jumpForceMultiplier = 1f;
        private bool isJumping = true;

        public static float JumpForceMultiplier
        {
            set
            {
                if (value is > fixedValue or < fixedValue)
                {
                    value = fixedValue;
                }
                
                if (jumpForceMultiplier >= maxJumpForce)
                {
                    jumpForceMultiplier = maxJumpForce;
                    return;
                }
                
                jumpForceMultiplier += value;
            }
        }

        private Rigidbody2D _rb;
        private Vector2 _moveDir;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            _moveDir = new Vector2(moveHorizontal * moveSpeed, _rb.linearVelocityY);
            
            if(transform.position.x > borderX)
                transform.position = new Vector2(-borderX, transform.position.y);
            if(transform.position.x < -borderX)
                transform.position = new Vector2(borderX, transform.position.y);
        }
        
        private void FixedUpdate()
        {
            _rb.linearVelocity = _moveDir;
            
            if (isJumping || !(_rb.linearVelocityY <= 0f)) return;
            
            _rb.AddForce(Vector2.up * (jumpForce * jumpForceMultiplier), ForceMode2D.Impulse);
            jumpForceMultiplier = 1f;
            isJumping = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag($"Platform") || !(_rb.linearVelocityY <= 0f)) return;
            
            isJumping = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name != "magnet") return;
            
            if (other.gameObject.transform.parent.TryGetComponent<BoostPlatform>(out var platform))
            {
                platform.PlayerOn();
            }
            
            isJumping = false;
        }
    }
}
