using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private const float distanceBetweenBackgrounds = 10.8f;
        private int floorsCompleted;

        private List<GameObject> _backgrounds = new();
        private Transform _player;
        
        private void Start()
        {
            foreach (Transform child in this.transform)
            {
                _backgrounds.Add(child.gameObject);
            }
            
            //_camera = GameObject.Find("Main Camera").transform;
            _player = GameObject.Find("Player").transform;
        }

        private void Update()
        {
            int currentFloor = 1;//Mathf.FloorToInt(_camera.position.y / distanceBetweenBackgrounds);
            
            if (currentFloor > floorsCompleted)
            {
                TransitionToNextFloor();
            }
        }
        
        private void TransitionToNextFloor()
        {
            _backgrounds[0].transform.position += new Vector3(0f, distanceBetweenBackgrounds * 3, 0f);
            _backgrounds.Add(_backgrounds[0]);
            _backgrounds.RemoveAt(0);
                
            floorsCompleted++;
        }
    }
}
