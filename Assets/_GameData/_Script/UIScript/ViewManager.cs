using System;
using UnityEngine;

namespace UIScript
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField] private Canvas[] _views;

        private void Start()
        {
            ActivateView(0);
        }

        public void ActivateView(int id)
        {
            if (id >= _views.Length)
            {
                return;
            }

            foreach (var view in _views)
            {
                view.enabled = false;
            }
            
            _views[id].enabled = true;
        }
    }
}
