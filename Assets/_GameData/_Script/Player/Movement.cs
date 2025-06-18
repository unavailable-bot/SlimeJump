using UnityEngine;

namespace Player
{
    internal sealed class Movement : MonoBehaviour
    {
        private const float moveSpeed = 5f;
        private const float jumpForce = 15f;
        private float borderX;
        private const float fixedValue = 0.5f;
        private const float maxJumpForce = 1.5f;
        
        private static float jumpForceMultiplier = 1f;
        private bool isJumping = true;
        private const float jumpCooldown = 0.1f;
        private float jumpCooldownTimer;

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

        private Camera _camera;
        private Rigidbody2D _rb;
        private Collider2D _coll;
        private Vector2 _moveDir;

        public LayerMask groundLayer; // 🎯 Укажи в инспекторе слой с платформами
        [SerializeField] private float rayLength = 0.2f; // 🎯 Длина луча вниз
        
        private void Start()
        {
            _camera = Camera.main;
            borderX = _camera!.orthographicSize * Screen.width / Screen.height;
            Debug.Log($"Border {borderX}");
            _rb = GetComponent<Rigidbody2D>();
            _coll = GetComponent<Collider2D>();
        }

        private void Update()
        {
            float moveHorizontal = 0f;

            #if UNITY_ANDROID || UNITY_IOS
            
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    float halfWidth = Screen.width / 2f;

                    if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        if (touch.position.x < halfWidth)
                            moveHorizontal = -1f;
                        else
                            moveHorizontal = 1f;
                    }
                }
                
            #else
            
                moveHorizontal = Input.GetAxisRaw("Horizontal");
            
            #endif

            _moveDir = new Vector2(moveHorizontal * moveSpeed, _rb.linearVelocityY);
            
            if(transform.position.x > borderX)
                transform.position = new Vector2(-borderX, transform.position.y);
            if(transform.position.x < -borderX)
                transform.position = new Vector2(borderX, transform.position.y);
            
            float playerHalfWidth = _coll.bounds.extents.x;
            
            Vector2 originCenter = transform.position;
            Vector2 originLeft   = originCenter + Vector2.left * playerHalfWidth;
            Vector2 originRight  = originCenter + Vector2.right * playerHalfWidth;
            
            // 🎯 Визуализация
            Debug.DrawRay(originCenter, Vector2.down * rayLength, Color.red);
            Debug.DrawRay(originLeft,   Vector2.down * rayLength, Color.red);
            Debug.DrawRay(originRight,  Vector2.down * rayLength, Color.red);
            
            Debug.Log(jumpCooldownTimer);
        }
        
        private void FixedUpdate()
        {
            if (jumpCooldownTimer > 0f)
                jumpCooldownTimer -= Time.fixedDeltaTime;
            
            _rb.linearVelocity = _moveDir;
            
            if (isJumping || _rb.linearVelocity.y > 0f) return;
            
            if (IsGrounded())
                Jump();
        }
        
        private bool IsGrounded()
        {
            float halfWidth = _coll.bounds.extents.x;
            Vector2 originCenter = transform.position;
            Vector2 originLeft   = originCenter + Vector2.left * halfWidth;
            Vector2 originRight  = originCenter + Vector2.right * halfWidth;

            bool hitCenter = Physics2D.Raycast(originCenter, Vector2.down, rayLength, groundLayer);
            bool hitLeft   = Physics2D.Raycast(originLeft,   Vector2.down, rayLength, groundLayer);
            bool hitRight  = Physics2D.Raycast(originRight,  Vector2.down, rayLength, groundLayer);


            return hitCenter || hitLeft || hitRight;
        }
        
        internal void Jump()
        {
            const float startVelocityY = 0f;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, startVelocityY);
            
            _rb.AddForce(Vector2.up * (jumpForce * jumpForceMultiplier), ForceMode2D.Impulse);
            jumpForceMultiplier = 1f;
            isJumping = true;
            jumpCooldownTimer = jumpCooldown;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Platform") || _rb.linearVelocity.y > 0f || !isJumping || jumpCooldownTimer > 0f) return;
            isJumping = false;
        }
    }
}
