using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using System.Collections;

namespace Player
{
    internal sealed class SwitchElement : MonoBehaviour
    {
        internal static AnimatorController MagmaSlime { get; private set; }
        internal static AnimatorController IceSlime { get; private set; }
        private static Animator _animator;
        private static TMP_Text _scoreText;

        internal static float ScoreMultiplier { get; private set; }

        private void Start()
        {
            ScoreMultiplier = 1f;
            MagmaSlime = Resources.Load<AnimatorController>($"Animators/magmaSlime");
            IceSlime = Resources.Load<AnimatorController>($"Animators/iceSlime");
            _scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
            _animator = GetComponent<Animator>();
        }

        internal static void BoostScoreMultiplier()
        {
            if(ScoreMultiplier >= 10f) return;
            ScoreMultiplier++;
            //_scoreText.fontSize += ScoreMultiplier / 2;
        }

        internal static void SetMagmaForm()
        {
            ScoreMultiplier = 1f;
            _animator.runtimeAnimatorController = MagmaSlime;
        }
        
        internal static void SetIceForm()
        {
            ScoreMultiplier = 1f;
            _animator.runtimeAnimatorController = IceSlime;
        }
        
        // Твоя корутина плавного уменьшения размера
        internal static IEnumerator SmoothScoreFontSize(float targetSize, float duration)
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
