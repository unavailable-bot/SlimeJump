using UnityEditor.Animations;
using UnityEngine;
using UIScript;

namespace Player
{
    internal sealed class SwitchElement : MonoBehaviour
    {
        internal static AnimatorController MagmaSlime { get; private set; }
        internal static AnimatorController IceSlime { get; private set; }
        private static Animator _animator;
        private static UIManager _uiManager;

        internal static float ScoreMultiplier { get; private set; }

        private void Start()
        {
            ScoreMultiplier = 1f;
            MagmaSlime = Resources.Load<AnimatorController>($"Animators/magmaSlime");
            IceSlime = Resources.Load<AnimatorController>($"Animators/iceSlime");
            _animator = GetComponent<Animator>();
            _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            _animator.runtimeAnimatorController = IceSlime;
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
            _uiManager.IsIceForm = false;
        }
        
        internal static void SetIceForm()
        {
            ScoreMultiplier = 1f;
            _animator.runtimeAnimatorController = IceSlime;
            _uiManager.IsIceForm = true;
        }
    }
}
