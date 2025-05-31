using System.Collections.Generic;
using UnityEngine;
using Platform;

namespace Core
{
    public class PlatformBuilder : MonoBehaviour
    {
        private const int startFloorsCount = 3;
        private const float halfWidthPlatform = 1.2f;
        private const float leftBorderX = -10f + halfWidthPlatform;
        private const float rightBorderX = 10f - halfWidthPlatform;
        private const float minDistanceBetweenPlatform = 1f;
        private const float maxDistanceBetweenPlatform = 5.75f;
        private float currentMaxDistanceBetweenPlatforms = minDistanceBetweenPlatform;
        private int currentFloorIndex = 1;
        
        [SerializeField] private GameObject _startPlatform;
        [SerializeField] private List<GameObject> _platforms;
        private Queue<GameObject> _platformsQueue;
        private Camera _camera;
        private Transform _lastPlatform;
        
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameObject.Find("BackgroundHolder").GetComponent<GameManager>();
            _camera = Camera.main;
            _lastPlatform = _startPlatform.transform;
            _platformsQueue = new Queue<GameObject>();
            _startPlatform.GetComponent<PlatformLevelMarker>().levelIndex = 1;
            _platformsQueue.Enqueue(_startPlatform);
            BuildStartFloors();
        }

        private void Update()
        {
            if (_gameManager.IsBuildRequest)
            {
                UpdateCurrentMaxDistance();
                
                RemoveCompletedFloor(currentFloorIndex - startFloorsCount);
                
                float topY = _gameManager._backgrounds[^1].transform.position.y + _camera.orthographicSize;
                BuildFloor(topY, currentMaxDistanceBetweenPlatforms);
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
            foreach (var background in _gameManager._backgrounds)
            {
                float topY = background.transform.position.y + _camera.orthographicSize;
                BuildFloor(topY, currentMaxDistanceBetweenPlatforms);
            }
        }

        private void BuildFloor(float topY, float currentMaxDistance)
        {
            List<Vector2> newPositions = new();
            Vector2 _lastPosition = _lastPlatform.position;
            
            while (true)
            {
                float newPointX = Random.Range(leftBorderX, rightBorderX);
                float newPointY = Random.Range(_lastPosition.y + minDistanceBetweenPlatform, _lastPosition.y + currentMaxDistance);
                
                if(newPointY > topY) break;
                
                newPositions.Add(new Vector2(newPointX, newPointY));
                _lastPosition = new Vector2(newPointX, newPointY);
            }
            
            foreach (var position in newPositions)
            {
                GameObject newPlatform = Instantiate(_platforms[GetRandomNumber()], position, Quaternion.identity);
                newPlatform.GetComponent<PlatformLevelMarker>().levelIndex = currentFloorIndex;
                _platformsQueue.Enqueue(newPlatform);
                _lastPlatform = newPlatform.transform;
            }

            currentFloorIndex++;
            _gameManager.IsBuildRequest = false;
        }

        private void RemoveCompletedFloor(int passedLevel)
        {
            int count = _platformsQueue.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject platform = _platformsQueue.Dequeue();
                PlatformLevelMarker marker = platform.GetComponent<PlatformLevelMarker>();
                if (marker is not null && marker.levelIndex == passedLevel)
                {
                    Destroy(platform);
                }
                else
                {
                    _platformsQueue.Enqueue(platform);
                }
            }
        }

        private void UpdateCurrentMaxDistance()
        {
            if (currentFloorIndex % 10 != 0) return;
            
            currentMaxDistanceBetweenPlatforms++;
            
            if (currentMaxDistanceBetweenPlatforms >= maxDistanceBetweenPlatform)
            {
                currentMaxDistanceBetweenPlatforms = maxDistanceBetweenPlatform;
            }
        }
    }
}
