using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Core
{
    public sealed class UIManager : MonoBehaviour
    {
        private TMP_Text _scoreText;
        private Transform _player;
        
        private int score;
        private float higherPlayerPosition;
        private const int scoreMultiplier = 7;
        
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

            _scoreText.text = $"Y | {score}";
        }
        
        private void UpdateUI()
        {
            if(_player.transform.position.y <= higherPlayerPosition) return;
            
            higherPlayerPosition = _player.transform.position.y;
            score = (int)_player.transform.position.y * scoreMultiplier;
            _scoreText.text = $"Y | {score}";
        }
    }
}
