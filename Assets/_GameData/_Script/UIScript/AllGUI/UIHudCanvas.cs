using System.Collections;
using Core.EventBas;
using Player;
using TMPro;
using UIScript.Model;
using UnityEngine;
using UnityEngine.UI;

namespace UIScript.AllGUI
{
    public sealed class UIHudCanvas : MonoBehaviour
    {
        public static UIHudCanvas I { get; private set; }
        private float deltaTime;
        private float higherPlayerPosition;
        public float Score { get; private set; }

        [SerializeField] ViewManager _viewManager;
        
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _fpsCounter;
        [SerializeField] private Transform _player;
        [SerializeField] private TMP_Text _burgerCounter;
        [SerializeField] private Button _menuButton;
        
        internal bool IsIceForm { get; set; } = true;
        
        internal void Initialize()
        {
            I = this;
            
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
            _scoreText.text = $"Y | {(int)Score} x {SwitchElement.Instance.ScoreMultiplier}";
        }
        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnSubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            GameEventBas.OnBurgerTook += TakeBurger;
            _menuButton.onClick.AddListener(() =>
            {
                _viewManager.ActivateView(1);
            });
        }

        private void UnSubscribeFromEvents()
        {
            GameEventBas.OnBurgerTook -= TakeBurger;
            _menuButton.onClick.RemoveAllListeners();
        }

        private void TakeBurger()
        {
            _burgerCounter.text = (++CharacterModel.Burger).ToString();
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
            Score += (int)(_player.transform.position.y * SwitchElement.Instance.ScoreMultiplier) / 100f;
            _scoreText.text = $"Y | {(int)Score} x {SwitchElement.Instance.ScoreMultiplier}";
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
