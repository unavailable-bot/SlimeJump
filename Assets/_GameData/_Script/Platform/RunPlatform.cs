using UnityEngine;
using Core;

namespace Platform
{
    internal sealed class RunPlatform : Platformer
    {
        [SerializeField] private SpriteRenderer currentSprite;
        
        private const float MIN_SPEED = 2f;
        private const float MAX_SPEED = 4f;
        private const float SPEED_MULTIPLIER = 0.05f;
        
        private float runSpeed;
        private float endPoint;
        
        private Vector3 _leftDir;
        private Vector3 _rightDir;
        private Vector3 _moveDir;
        
        private Camera _camera;
        
        private bool movingRight;
        
        private void Start()
        {
            _camera = Camera.main;
            
            runSpeed = Random.Range(MIN_SPEED, MAX_SPEED);
            
            endPoint = _camera!.transform.position.x - (_camera!.orthographicSize * _camera.aspect);
            
            float spriteWidth_world  = (currentSprite.sprite.bounds.size.x) / 2;
            
            _leftDir = new Vector3(endPoint + spriteWidth_world, transform.position.y, transform.position.z);
            _rightDir = new Vector3(-endPoint - spriteWidth_world, transform.position.y, transform.position.z);
            
            endPoint = RandomizeDir();
            _moveDir = new Vector3(endPoint, transform.position.y, transform.position.z);
            
            movingRight = Random.value > 0.5f;
            UpdateMoveDirection();
        }
        
        private void UpdateMoveDirection()
        {
            endPoint = movingRight ? _rightDir.x : _leftDir.x;
            _moveDir = new Vector3(endPoint, transform.position.y, transform.position.z);
        }

        private void Update()
        {
            float speed = (runSpeed + PlatformManager.Instance.RunPlatformSpeedMultiplier) * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, _moveDir, speed);

            if (Vector3.Distance(this.transform.position, _moveDir) < 0.01f)
            {
                movingRight = !movingRight;
                UpdateMoveDirection();
            }
        }

        internal override void PlayerOn()
        {
            PlatformManager.Instance.IncreaseMultiplier(SPEED_MULTIPLIER);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name == "Player" && other.gameObject.GetComponent<Rigidbody2D>().linearVelocityY <= 0f)
            {
                PlayerOn();
            }
        }

        private float RandomizeDir()
        {
            int randomCase = Random.Range(0, 2);
            return randomCase switch
            {
                0 => -endPoint,
                _ => endPoint
            };
        }
    }
}
