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
        
        private bool isBuildRequest;

        public bool IsBuildRequest
        {
            get => isBuildRequest;
            set => isBuildRequest = value;
        }

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
                isBuildRequest = true;
            }

            if (_player.position.y < _camera.transform.position.y - halfHeightCam)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        
        private void TransitionToNextFloor()
        {
            _backgrounds[0].transform.position += new Vector3(0f, distanceBetweenLastBackground, 0f);
            _backgrounds.Add(_backgrounds[0]);
            _backgrounds.RemoveAt(0);
                
            floorsCompleted++;
        }
    }
}
