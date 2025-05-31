using Player;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace UIScript
{
    public sealed class UIManager : MonoBehaviour
    {
        private TMP_Text _scoreText;
        private Transform _player;
        
        private float score;
        private float higherPlayerPosition;

        internal bool IsIceForm { get; set; } = true;
        
        private void Start()
        {
            _scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
            _player = GameObject.Find("Player").transform;
            InitUI();
        }
        
        private void Update()
        {
            UpdateUI();
        }
        
        private void InitUI()
        {
            // Размер текста
            _scoreText.fontSize = 48;

            // Цвет текста
            _scoreText.color = new Color32(250, 250, 150, 255);

            // Жирность
            _scoreText.fontStyle = FontStyles.Bold;

            // Выравнивание по левому верхнему углу
            _scoreText.alignment = TextAlignmentOptions.TopLeft;

            // Обводка (outline)
            _scoreText.outlineWidth = 1f;
            _scoreText.outlineColor = Color.black;

            // Тень (Shadow)
            var shadow = _scoreText.GetComponent<Shadow>();
            if (shadow == null)
                shadow = _scoreText.gameObject.AddComponent<Shadow>();
            shadow.effectColor = new Color32(0, 0, 0, 150);
            shadow.effectDistance = new Vector2(2f, -2f);

            // Градиент (пример — отключи если не надо)
            _scoreText.enableVertexGradient = true;
            _scoreText.colorGradient = new VertexGradient(
                new Color32(10, 50, 0, 150), // верхний левый
                new Color32(150, 0, 0, 255), // верхний правый
                new Color32(10, 50, 0, 150), // нижний левый
                new Color32(150, 0, 0, 255)  // нижний правый
            );

            _scoreText.text = $"Y | {(int)score} x {SwitchElement.ScoreMultiplier}";
        }
        
        private void UpdateUI()
        {
            if(_player.transform.position.y <= higherPlayerPosition) return;
            
            higherPlayerPosition = _player.transform.position.y;
            score += (int)(_player.transform.position.y * SwitchElement.ScoreMultiplier) / 100f;
            _scoreText.text = $"Y | {(int)score} x {SwitchElement.ScoreMultiplier}";
        }
        
        // Запуск: StartCoroutine(ScaleTo(targetScale, duration));
        public IEnumerator ScaleTo(Transform scaleObj, Vector3 targetScale, float duration)
        {
            Vector3 startScale = scaleObj.localScale;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                scaleObj.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
                yield return null;
            }
            scaleObj.localScale = targetScale;
        }
        
        // Твоя корутина плавного уменьшения размера
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
