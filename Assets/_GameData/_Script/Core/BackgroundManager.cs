using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    internal sealed class BackgroundManager : MonoBehaviour
    {
        private const float DISTANCE_BETWEEN_BACKGROUNDS = 10.8f;
        private const float DISTANCE_BETWEEN_LAST_BACKGROUND = DISTANCE_BETWEEN_BACKGROUNDS * 3;
        private int floorsCompleted;
        private float halfHeightCam = 0.15f;
        private bool scaled;
        
        public bool IsBuildRequest { get; set; }

        [SerializeField] internal List<GameObject> _backgrounds = new();
        [SerializeField] private Transform _player;
        [SerializeField] private Camera _camera;

        internal void Initialize()
        {
            _camera = Camera.main;
            if (_camera is not null) halfHeightCam += _camera.orthographicSize;
            _player = GameObject.Find("Player").transform;
        }

        private void Update()
        {
            var currentFloor = Mathf.FloorToInt(_camera.transform.position.y / DISTANCE_BETWEEN_BACKGROUNDS);
            
            if (currentFloor > floorsCompleted)
            {
                TransitionToNextFloor();
                IsBuildRequest = true;
            }

            if (_player.position.y < _camera.transform.position.y - halfHeightCam)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void LateUpdate()
        {
            if (scaled) return;
            
            foreach (Transform child in this.transform)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                SetBackgroundSize(child);
            }
            scaled = true;
        }

        private void TransitionToNextFloor()
        {
            _backgrounds[0].transform.position += new Vector3(0f , DISTANCE_BETWEEN_LAST_BACKGROUND, 0f);
            _backgrounds.Add(_backgrounds[0]);
            _backgrounds.RemoveAt(0);
                
            floorsCompleted++;
        }

        private void SetBackgroundSize(Transform background)
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            var sr = background.GetComponent<SpriteRenderer>();
            if (sr?.sprite is null)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                Debug.LogWarning("BackgroundScaler: Нет спрайта для масштабирования!");
                return;
            }
            
            float spriteWidth_world  = sr.sprite.bounds.size.x;
            float spriteHeight_world = sr.sprite.bounds.size.y;

            float worldScreenHeight = _camera.orthographicSize * 2f;
            float worldScreenWidth = worldScreenHeight * Screen.width / Screen.height;

            float scaleX = worldScreenWidth  / spriteWidth_world;
            float scaleY = worldScreenHeight / spriteHeight_world;

            background.localScale = new Vector3(background.localScale.x * scaleX, background.localScale.y * scaleY, 1f);
        }
    }
}
