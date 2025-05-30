using UnityEngine;
using Random = System.Random;

namespace Platform
{
    public class RunPlatform : MonoBehaviour
    {
        private const float halfWidthPlatform = 1.2f;
        private static float runSpeed = 3f;
        private float endPoint;
        
        private Vector3 _leftDir;
        private Vector3 _rightDir;
        private Vector3 _moveDir;
        
        private void Start()
        {
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
            
            this.transform.position = Vector3.MoveTowards(this.transform.position, _moveDir, runSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name == "Player")
            {
                PlayerOn();
            }
        }

        private float RandomizeDir()
        {
            Random _r = new Random();
            int randomCase = _r.Next(0, 2);
            return randomCase switch
            {
                0 => -endPoint,
                _ => endPoint
            };
        }

        private static void PlayerOn()
        {
            runSpeed = 9f;
        }
    }
}
