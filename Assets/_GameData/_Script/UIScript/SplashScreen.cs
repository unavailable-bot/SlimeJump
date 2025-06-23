using UIScript.AllGUI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UIScript
{
    public class SplashScreen : MonoBehaviour
    {
        [FormerlySerializedAs("animationFrames")] [SerializeField] private Sprite[] _animationFrames;       // Кадры-анимации
        
        [SerializeField] private Canvas _hudCanvas;
        [SerializeField] private GameObject _platformBuilder;
        [SerializeField] private Image _splashImage;              // Image для показа спрайтов
        private const float frameRate = 0.03f;        // Скорость проигрывания (в секундах на кадр)
        private int currentFrame;
        private float timer;
        private bool canHide;
        private int direction; // 1 — вперёд, -1 — назад

        internal void Initialize()
        {
            //_platformBuilder = GameObject.Find("PlatformManager");
            _splashImage = GetComponent<Image>();
            
            if (_animationFrames.Length > 0)
                _splashImage.sprite = _animationFrames[0];
            
            Time.timeScale = 0f;
            canHide = true; // true — если сразу можно нажимать, false — если табличка должна проиграть анимацию полностью
        }

        private void Update()
        {
            // Анимация таблички
            timer += Time.unscaledDeltaTime;
            if (timer >= frameRate)
            {
                timer = 0f;
                currentFrame += direction;

                // Дошли до конца — меняем направление
                if (currentFrame >= _animationFrames.Length - 1)
                {
                    currentFrame = _animationFrames.Length - 1;
                    direction = -1;
                }
                // Дошли до начала — снова вперёд
                else if (currentFrame <= 0)
                {
                    currentFrame = 0;
                    direction = 1;
                }

                _splashImage.sprite = _animationFrames[currentFrame];
            }

            // Скрываем табличку по нажатию любой кнопки или мыши
            if ((canHide && (Input.anyKeyDown || Input.GetMouseButtonDown(0))) && _hudCanvas.enabled)
            {
                HideSplash();
            }
        }

        private void HideSplash()
        {
            _splashImage.gameObject.SetActive(false);

            // Запустить игру, если ставил Time.timeScale = 0
            Time.timeScale = 1f;

            _platformBuilder.SetActive(true);
            //Destroy(gameObject); // если не нужен на сцене дальше
        }
    }
}
