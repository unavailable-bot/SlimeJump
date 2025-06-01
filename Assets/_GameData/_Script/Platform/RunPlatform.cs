using UnityEngine;
using Core;

namespace Platform
{
    internal sealed class RunPlatform : Platformer
    {
        private const float halfWidthPlatform = 1.2f;
        private float runSpeed;
        private const float speedMultiplier = 0.5f;
        private float endPoint;
        
        private Vector3 _leftDir;
        private Vector3 _rightDir;
        private Vector3 _moveDir;
        
        
        private void Start()
        {
            runSpeed = Random.Range(2f, 4f);
            
            endPoint =  Camera.main!.transform.position.x + halfWidthPlatform - (Camera.main!.orthographicSize * Camera.main.aspect);
            
            _leftDir = new Vector3(endPoint, transform.position.y, transform.position.z);
            _rightDir = new Vector3(-endPoint, transform.position.y, transform.position.z);
            
            endPoint = RandomizeDir();
            _moveDir = new Vector3(endPoint, transform.position.y, transform.position.z);
        }

        private void Update()
        {
            if (this.transform.position.x <= _leftDir.x || this.transform.position.x >= _rightDir.x)
            {
                endPoint = -endPoint;
                _moveDir = new Vector3(endPoint, transform.position.y, transform.position.z);
            }
            
            this.transform.position = Vector3.MoveTowards(this.transform.position, _moveDir, (runSpeed + PlatformManager.Instance.RunPlatformSpeedMultiplier) * Time.deltaTime);
        }

        internal override void PlayerOn()
        {
            PlatformManager.Instance.IncreaseMultiplier(speedMultiplier);
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
