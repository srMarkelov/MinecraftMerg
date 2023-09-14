using System;
using Core.Characters;
using UnityEngine;

namespace Core.Field
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private CellView _view;
        [SerializeField] private float _width;
        [SerializeField] private float _height;
        public float Width => _width;
        public float Height => _height;

        public ICharacter Character;

        /*public bool InCell(Vector3 pos)
        {
            if (transform.position.x + _width / 5f < pos.x
                || transform.position.x - _width / 5f > pos.x)
            {
                return false;
            }
            if (transform.position.y + _height / 3f < pos.y
                || transform.position.y - _height / 3f > pos.y)
            {
                return false;
            }

            return true;
        }*/
        public bool OnFirstBound;

        public bool InCell(Vector3 pos)
        {
            if (transform.position.x + 2.5f / 5f < pos.x
                || transform.position.x - 2.5f / 5f > pos.x)
            {
                return false;
            }
            if (transform.position.y + 2.5f / 5f < pos.y
                || transform.position.y - 2.5f / 5f > pos.y)
            {
                return false;
            }
            /*SetColor();*/
            return true;
        }

        public bool IsBusy() => Character != null;

        public void ClearCharacter()
        {
            Character = null;
        }

        public void OffAndOnColorFinishButton()
        {
            if (InCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                _view.SetFirstColor();
            }
            else
            {
                _view.OffFirstColor();
            }
        }
        public void SetColor()
        {
            _view.SetColor();
        }
        public void OffColor()
        {
            _view.OffColor();
        }
    }
}
