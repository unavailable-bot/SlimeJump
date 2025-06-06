using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    internal sealed class BackgroundManager : MonoBehaviour
    {
        private const float distanceBetweenBackgrounds = 10.8f;
        private const float distanceBetweenLastBackground = distanceBetweenBackgrounds * 3;
        private int floorsCompleted;
        private float halfHeightCam = 0.15f;
        private bool _scaled = false;
        
        public bool IsBuildRequest { get; set; }

        internal readonly List<GameObject> _backgrounds = new();
        private Transform _player;
        private Camera _camera;
        
        private void Start()
        {
            foreach (Transform child in this.transform)
            {
                _backgrounds.Add(child.gameObject);
            }
            
            _camera = Camera.main;
            if (_camera != null) halfHeightCam += _camera.orthographicSize;
            
            _player = GameObject.Find("Player").transform;
        }

        private void Update()
        {
            int currentFloor = Mathf.FloorToInt(_camera.transform.position.y / distanceBetweenBackgrounds);
            
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
            if (_scaled) return;
            
            foreach (Transform child in this.transform)
            {
                SetBackgroundSize(child);
            }
            _scaled = true;
        }

        private void TransitionToNextFloor()
        {
            _backgrounds[0].transform.position += new Vector3(0f, distanceBetweenLastBackground, 0f);
            _backgrounds.Add(_backgrounds[0]);
            _backgrounds.RemoveAt(0);
                
            floorsCompleted++;
        }

        private void SetBackgroundSize(Transform background)
        {
            SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
            if (sr == null || sr.sprite == null)
            {
                Debug.LogWarning("BackgroundScaler: Нет спрайта для масштабирования!");
                return;
            }
            
            // Размеры спрайта в мировых единицах
            float spriteWidth_world  = sr.sprite.bounds.size.x;
            float spriteHeight_world = sr.sprite.bounds.size.y;

            // Высота экрана (мировых единиц): orthoSize * 2 + Ширина экрана (мировых единиц) пропорциональна Aspect
            float worldScreenHeight = _camera.orthographicSize * 2f;
            float worldScreenWidth = worldScreenHeight * Screen.width / Screen.height;
            
            Debug.Log($"Border {worldScreenWidth}");

            // Коэффициенты масштабирования по осям
            float scaleX = worldScreenWidth  / spriteWidth_world;
            float scaleY = worldScreenHeight / spriteHeight_world;

            // Применяем масштаб к объекту
            background.localScale = new Vector3(background.localScale.x * scaleX, background.localScale.y * scaleY, 1f);
        }
    }
}
