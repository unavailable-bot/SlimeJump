using System.Collections.Generic;
using UnityEngine;
using Platform;

namespace Core
{
    public sealed class PlatformBuilder : MonoBehaviour
    {
        private const int START_FLOORS_COUNT = 3;
        private const float HALF_WIDTH_PLATFORM = 0.35f;
        private const float MIN_DISTANCE_BETWEEN_PLATFORM = 0.5f;
        private const float MAX_DISTANCE_BETWEEN_PLATFORM = 2.5f;
        private float leftBorderX;
        private float rightBorderX;
        private float currentMaxDistanceBetweenPlatforms = MIN_DISTANCE_BETWEEN_PLATFORM;
        private int currentFloorIndex = 1;
        
        [SerializeField] private float weightBoostPf = 10f;
        [SerializeField] private float weightRunPf = 20f;
        [SerializeField] private float weightMagmaPf = 50f;
        [SerializeField] private float weightIcePf = 50f;
        
        [SerializeField] private GameObject _startPlatform;
        [SerializeField] private List<GameObject> _platforms;
        private Queue<GameObject> _platformsQueue;
        private Camera _camera;
        private Transform _lastPlatform;
        
        private BackgroundManager _backgroundManager;

        internal void Initialize(Camera mainCam)
        {
            float worldScreenHeight = mainCam.orthographicSize * 2f;
            float worldScreenWidth = worldScreenHeight * Screen.width / Screen.height;
            leftBorderX = worldScreenWidth / 2 - HALF_WIDTH_PLATFORM;
            rightBorderX = -worldScreenWidth / 2 + HALF_WIDTH_PLATFORM;
            this.gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            _backgroundManager = GameObject.Find("BackgroundManager").GetComponent<BackgroundManager>();
            _camera = Camera.main;
            _lastPlatform = _startPlatform.transform;
            _platformsQueue = new Queue<GameObject>();
            _startPlatform.GetComponent<Platformer>().levelIndex = 1;
            _platformsQueue.Enqueue(_startPlatform);
            BuildStartFloors();
        }

        private void Update()
        {
            if (!_backgroundManager.IsBuildRequest) return;
            
            UpdateCurrentMaxDistance();
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            RemoveCompletedFloor(currentFloorIndex - START_FLOORS_COUNT);
                
            float topY = _backgroundManager._backgrounds[^1].transform.position.y + _camera.orthographicSize;
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            BuildFloor(topY, currentMaxDistanceBetweenPlatforms);
        }

        private int GetRandomNumber()
        {
            int[] numbers = {0, 1, 2, 3};
            float[] weights = { weightBoostPf, weightRunPf, weightMagmaPf, weightIcePf};

            float total = 0f;
            foreach (var weight in weights)
            {
                total += weight;
            }
            
            var random = Random.Range(0f, total);
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
            foreach (var background in _backgroundManager._backgrounds)
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
                var newPointX = Random.Range(leftBorderX, rightBorderX);
                var newPointY = Random.Range(_lastPosition.y + MIN_DISTANCE_BETWEEN_PLATFORM, _lastPosition.y + currentMaxDistance);
                
                if(newPointY > topY) break;
                
                newPositions.Add(new Vector2(newPointX, newPointY));
                _lastPosition = new Vector2(newPointX, newPointY);
            }
            
            foreach (var position in newPositions)
            {
                var newPlatform = Instantiate(_platforms[GetRandomNumber()], position, Quaternion.identity);
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                newPlatform.GetComponent<Platformer>().levelIndex = currentFloorIndex;
                _platformsQueue.Enqueue(newPlatform);
                _lastPlatform = newPlatform.transform;
            }

            currentFloorIndex++;
            _backgroundManager.IsBuildRequest = false;
        }

        private void RemoveCompletedFloor(int passedLevel)
        {
            int count = _platformsQueue.Count;
            for (int i = 0; i < count; i++)
            {
                var platform = _platformsQueue.Dequeue();
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                var marker = platform.GetComponent<Platformer>();
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
            
            currentMaxDistanceBetweenPlatforms += MIN_DISTANCE_BETWEEN_PLATFORM/2;
            
            if (currentMaxDistanceBetweenPlatforms >= MAX_DISTANCE_BETWEEN_PLATFORM)
            {
                currentMaxDistanceBetweenPlatforms = MAX_DISTANCE_BETWEEN_PLATFORM;
            }
        }
    }
}
