using UnityEngine;
using UIScript;
using UIScript.AllGUI;

namespace Player
{
    internal sealed class SwitchElement : MonoBehaviour
    {
        public static SwitchElement Instance { get; private set; }
        internal RuntimeAnimatorController MagmaSlime { get; private set; }
        internal RuntimeAnimatorController IceSlime { get; private set; }
        private Animator _animator;
        private UIHudCanvas _uiHudCanvas;

        internal float ScoreMultiplier { get; private set; }

        private void Awake()
        {
            Instance = this;
            ScoreMultiplier = 1f;
            MagmaSlime = Resources.Load<RuntimeAnimatorController>($"Animators/Player/magmaSlime");
            IceSlime = Resources.Load<RuntimeAnimatorController>($"Animators/Player/iceSlime");
            _animator = GetComponent<Animator>();
            _uiHudCanvas = GameObject.Find("Game{Canvas}").GetComponent<UIHudCanvas>();
            _animator.runtimeAnimatorController = IceSlime;
        }

        internal void BoostScoreMultiplier()
        {
            if (ScoreMultiplier >= 3f) return;
            
            ScoreMultiplier += 0.3f;
            if (ScoreMultiplier > 3)
            {
                ScoreMultiplier = 3f;
            }
        }

        internal void SetMagmaForm()
        {
            ScoreMultiplier = 1f;
            _animator.runtimeAnimatorController = MagmaSlime;
            _uiHudCanvas.IsIceForm = false;
        }
        
        internal void SetIceForm()
        {
            ScoreMultiplier = 1f;
            _animator.runtimeAnimatorController = IceSlime;
            _uiHudCanvas.IsIceForm = true;
        }
    }
}
