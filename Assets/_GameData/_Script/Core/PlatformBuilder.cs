using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class PlatformBuilder : MonoBehaviour
    {
        private const float halfWidthPlatform = 1.2f;
        private const float leftBorderX = -10f + halfWidthPlatform;
        private const float rightBorderX = 10f - halfWidthPlatform;
        private const float minDistanceBetweenPlatform = 2f;
        private const float maxDistanceBetweenPlatform = 4f;
        
        [SerializeField] private GameObject _startPlatform;
        [SerializeField] private List<GameObject> _platforms;
        private Camera _camera;
        private Transform _lastPlatform;
        
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameObject.Find("BackgroundHolder").GetComponent<GameManager>();
            _camera = Camera.main;
            _lastPlatform = _startPlatform.transform;
            BuildStartFloors();
        }

        private void Update()
        {
            if (_gameManager.IsBuildRequest)
            {
                float topY = _gameManager._backgrounds[^1].transform.position.y + _camera.aspect;
                BuildFloor(topY);
            }
        }

        private int GetRandomNumber()
        {
            int[] numbers = {0, 1, 2, 3};
            float[] weights = { 10f, 20f, 50f, 50f };

            float total = 0f;
            foreach (var weight in weights)
            {
                total += weight;
            }
            
            float random = Random.Range(0f, total);
            float cumulative = 0f;
            for (int i = 0; i < weights.Length; i++)
            {
                cumulative += weights[i];
                if (random < cumulative)
                {
                    return numbers[i];
                }
            }
            return numbers[^1];
        }

        private void BuildStartFloors()
        {
            for (int i = 0; i < 3; i++)
            {
                float topY = _gameManager._backgrounds[i].transform.position.y + _camera.aspect;
                BuildFloor(topY);
            }
        }

        private void BuildFloor(float topY)
        {
            List<Vector2> newPositions = new();
            Vector2 _lastPosition = _lastPlatform.position;
            
            while (true)
            {
                float newPointX = Random.Range(leftBorderX, rightBorderX);
                float newPointY = Random.Range(_lastPosition.y + minDistanceBetweenPlatform, _lastPosition.y + maxDistanceBetweenPlatform);
                
                if(newPointY > topY) break;
                
                newPositions.Add(new Vector2(newPointX, newPointY));
                _lastPosition = new Vector2(newPointX, newPointY);
            }
            
            foreach (var position in newPositions)
            {
                GameObject newPlatform = Instantiate(_platforms[GetRandomNumber()], position, Quaternion.identity);
                _lastPlatform = newPlatform.transform;
            }
            
            _gameManager.IsBuildRequest = false;
        }
    }
}
