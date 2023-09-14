using System;
using UnityEngine;

namespace Core.Field
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OffColor();
                OffFirstColor();
            }
        }

        public void SetFirstColor()
        {
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0.35f);

        }
        public void OffFirstColor()
        {
            _renderer.color = new Color(0.2f, 0.85f, _renderer.color.b, 0f);

        }
        
        public void SetColor()
        {
            _renderer.color = new Color(1, 0.7f, _renderer.color.b, 0.6f);
        }
        public void OffColor()
        {
            _renderer.color = new Color(0.2f, 0.85f, _renderer.color.b, 0f);
        }
    }
}
