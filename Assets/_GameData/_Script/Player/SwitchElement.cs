using UnityEditor.Animations;
using UnityEngine;

namespace Player
{
    internal sealed class SwitchElement : MonoBehaviour
    {
        public static AnimatorController MagmaSlime { get; private set; }
        public static AnimatorController IceSlime { get; private set; }
        private static Animator _animator;

        private void Start()
        {
            MagmaSlime = Resources.Load<AnimatorController>($"Animators/magmaSlime");
            IceSlime = Resources.Load<AnimatorController>($"Animators/iceSlime");
            _animator = GetComponent<Animator>();
        }

        public static void SetMagmaForm()
        {
            _animator.runtimeAnimatorController = MagmaSlime;
        }
        
        public static void SetIceForm()
        {
            _animator.runtimeAnimatorController = IceSlime;
        }
    }
}
