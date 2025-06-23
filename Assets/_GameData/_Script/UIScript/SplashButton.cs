using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UIScript
{
    public class SplashButton : MonoBehaviour
    {
        [FormerlySerializedAs("animationFrames")] [SerializeField] private Sprite[] _animationFrames;
        [SerializeField] private Image _startImage;
        private const float frameRate = 0.03f;
        private int currentFrame;
        private float timer;
        private int direction;

        private void Awake()
        {
            _startImage = GetComponent<Image>();
            
            if (_animationFrames.Length > 0)
                _startImage.sprite = _animationFrames[0];
            
            Time.timeScale = 0f;
        }

        private void Update()
        {
            timer += Time.unscaledDeltaTime;
            if (timer >= frameRate)
            {
                timer = 0f;
                currentFrame += direction;

                if (currentFrame >= _animationFrames.Length - 1)
                {
                    currentFrame = _animationFrames.Length - 1;
                    direction = -1;
                }
                
                else if (currentFrame <= 0)
                {
                    currentFrame = 0;
                    direction = 1;
                }

                _startImage.sprite = _animationFrames[currentFrame];
            }
        }
    }
}
