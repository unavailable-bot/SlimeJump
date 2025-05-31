using Player;
using UnityEngine;

namespace UIScript
{
    public class PlayerFire : MonoBehaviour
    {
        private const float maxScale = 3f;
        
        private UIManager _uiManager;
        
        private Animator _animator;

        [SerializeField] private RuntimeAnimatorController _iceFire;
        [SerializeField] private RuntimeAnimatorController _magmaFire;
        
        private void Start()
        {
            _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_uiManager.IsIceForm && _animator.runtimeAnimatorController.name != _iceFire.name)
            {
                SetIceFire();
            }
            
            if (!_uiManager.IsIceForm && _animator.runtimeAnimatorController.name != _magmaFire.name)
            {
                SetMagmaFire();
            }
            
            float scaleMultiplier = SwitchElement.Instance.ScoreMultiplier / 10;
            Vector3 newScale = new(scaleMultiplier,scaleMultiplier,scaleMultiplier);
            if (SwitchElement.Instance.ScoreMultiplier > 1 && this.transform.localScale.magnitude < maxScale)
            {
                StartCoroutine(_uiManager.ScaleTo(this.transform,this.transform.localScale + newScale, 0.5f));
            }

            if (this.transform.localScale != Vector3.one && Mathf.Approximately(SwitchElement.Instance.ScoreMultiplier, 1f))
            {
                StartCoroutine(_uiManager.ScaleTo(this.transform, Vector3.one, 0.5f));
            }
        }

        private void SetIceFire()
        {
            _animator.runtimeAnimatorController = _iceFire;
        }

        private void SetMagmaFire()
        {
            _animator.runtimeAnimatorController = _magmaFire;
        }
    }
}
