using Player;
using UnityEngine;

namespace UIScript
{
    public class iceFireUI : MonoBehaviour
    {
        private const float maxScale = 3f;
        
        private UIManager _uiManager;
        private GameObject _player;
        
        private void Start()
        {
            _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            _player = GameObject.Find("Player");
        }

        private void Update()
        {
            if (_uiManager.IsIceForm)
            {
                float scaleMultiplier = SwitchElement.ScoreMultiplier / 10;
                Vector3 newScale = new(scaleMultiplier,scaleMultiplier,scaleMultiplier);
                if (SwitchElement.ScoreMultiplier > 1 && this.transform.localScale.magnitude < maxScale)
                {
                    StartCoroutine(_uiManager.ScaleTo(this.transform,this.transform.localScale + newScale, 0.5f));
                }
            }

            if (this.transform.localScale != Vector3.one && Mathf.Approximately(SwitchElement.ScoreMultiplier, 1f))
            {
                StartCoroutine(_uiManager.ScaleTo(this.transform, Vector3.one, 0.5f));
            }
        }
    }
}
