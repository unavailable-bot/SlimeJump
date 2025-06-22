using Player;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace UIScript
{
    public sealed class UIManager : MonoBehaviour
    {
        private float deltaTime;
        private float score;
        private float higherPlayerPosition;
        
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _fpsCounter;
        [SerializeField] private Transform _player;

        internal bool IsIceForm { get; set; } = true;
        
        internal void Initialize()
        {
            _scoreText.fontSize = 48;

            _scoreText.color = new Color32(250, 250, 150, 255);

            _scoreText.fontStyle = FontStyles.Bold;

            _scoreText.alignment = TextAlignmentOptions.TopLeft;

            _scoreText.outlineWidth = 1f;
            _scoreText.outlineColor = Color.black;

            var shadow = _scoreText.GetComponent<Shadow>();
            if (shadow == null)
                shadow = _scoreText.gameObject.AddComponent<Shadow>();
            shadow.effectColor = new Color32(0, 0, 0, 150);
            shadow.effectDistance = new Vector2(2f, -2f);

            _scoreText.enableVertexGradient = true;
            _scoreText.colorGradient = new VertexGradient(
                new Color32(10, 50, 0, 150),
                new Color32(150, 0, 0, 255),
                new Color32(10, 50, 0, 150),
                new Color32(150, 0, 0, 255)
            );

            _scoreText.text = $"Y | {(int)score} x {SwitchElement.Instance.ScoreMultiplier}";
        }
        
        private void Update()
        {
            const float DELTA = 0.1f;
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * DELTA;
            float fps = 1f / deltaTime;
            _fpsCounter.text = $"FPS: {Mathf.Ceil(fps)}";
            
            UpdateUI();
        }
        
        private void UpdateUI()
        {
            if(_player.transform.position.y <= higherPlayerPosition) return;
            
            higherPlayerPosition = _player.transform.position.y;
            score += (int)(_player.transform.position.y * SwitchElement.Instance.ScoreMultiplier) / 100f;
            _scoreText.text = $"Y | {(int)score} x {SwitchElement.Instance.ScoreMultiplier}";
        }
        
        public IEnumerator ScaleTo(Transform scaleObj, Vector3 targetScale, float duration)
        {
            var startScale = scaleObj.localScale;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                scaleObj.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
                yield return null;
            }
            scaleObj.localScale = targetScale;
        }
        
        internal IEnumerator SmoothScoreFontSize(float targetSize, float duration)
        {
            float startSize = _scoreText.fontSize;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                _scoreText.fontSize = Mathf.Lerp(startSize, targetSize, elapsed / duration);
                yield return null;
            }
            _scoreText.fontSize = targetSize;
        }
    }
}
