using System;
using System.Collections;
using System.Collections.Generic;
using Core.Characters;
using DG.Tweening;

using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class  ShellsController : MonoBehaviour
{
    [SerializeField] private GameObject _shills;
    [SerializeField] private GameObject _instantiatePosition;
    [SerializeField] private float _step;


    private ShellsView _shellsView;
    private Vector3 _targetPosition;
    private ICharacter _targetCharacter;
    private GameObject _shill;

    private float _fixAngle;
    private float _progress;
    private CharacterView _characterView;
    private float _rotationShellY;

    public List<CharacterView> _CharacterTargerViews = new List<CharacterView>();
    public List<ShellsView> _ShellsViews = new List<ShellsView>();



    public void SetView(CharacterView characterView)
    {
        _characterView = characterView;
    }
    public void SetView(List<ICharacter> targetCharacters)
    {
        _CharacterTargerViews.Clear();
        foreach (var character in targetCharacters)
        {
            _CharacterTargerViews.Add(character.View);
        }
    }
    
    public void SetTargetPosition(Vector3 targetPosition, ICharacter targetCharacter)
    {
        targetPosition = new Vector3(targetPosition.x, targetPosition.y + 0.2f, targetPosition.z);
        _targetPosition = targetPosition;
        _targetCharacter = targetCharacter;
    }

    private void FixedUpdate()
    {
        CorrectRotation();
    }

    private void Update()
    {
        CorrectRotationShells();

        if (_ShellsViews.Count>=1 && _CharacterTargerViews.Count>=1)
        {
            for (int i = 0; i < _ShellsViews.Count; i++)
            {
                if (_ShellsViews.Count>=1 && _CharacterTargerViews.Count>=1)
                {
                    _ShellsViews[i].SetStartAndEndPosition(_CharacterTargerViews[i].transform.position,_CharacterTargerViews[i]._Character);
                }
            }
        }
        if (_shellsView == null) return;

        _shellsView.SetStartAndEndPositionTest(_targetPosition,_targetCharacter);
    }

    public void InstantiateShills()
    {
        if(_shill != null) return;
        _shill = Instantiate(_shills,_instantiatePosition.transform.position, Quaternion.identity);
        _shellsView = _shill.GetComponent<ShellsView>();

        _shellsView.OnShellsDestroyICharacters += _characterView.TakeDamageForShells;
        
        _shellsView.SetCharacterView(_characterView);
    }
    public void InstantiateShillsMass()
    {
        var shills= Instantiate(_shills,_instantiatePosition.transform.position, quaternion.identity);
        shills.GetComponent<ShellsView>().OnShellsDestroyICharacters += _characterView.TakeMassDamageForShells;
        _ShellsViews.Add(shills.GetComponent<ShellsView>());
        for (int i = 0; i < _ShellsViews.Count; i++)
        {
            _ShellsViews[i].OnShellsDestroyICharacters += _characterView.TakeMassDamageForShells;

            _ShellsViews[i].SetShellsController(this);
            _ShellsViews[i].SetCharacterView(_characterView);
        }
    }
    
    private void CorrectRotation()
    {
        if (_targetPosition.x >= gameObject.transform.position.x)
        {
            _rotationShellY = 0;
        }
        else
        {
            _rotationShellY = -180;
        }
        if(_shill == null || _characterView == null || _shellsView == null) return;
        _shellsView.CorrectRotationShill(_shill, _targetPosition, _rotationShellY);
    }
    private void CorrectRotationShells()
    {
        /*foreach (var shellsView in _ShellsViews)
        {
            shellsView.CorrectRotationShill(shellsView.gameObject, _targetPosition, _rotationShellY);
        }*/
        
        for (int i = 0; i < _ShellsViews.Count; i++)
        {
            if (_ShellsViews.Count>=1 && _CharacterTargerViews.Count>=1)
            {
                if (_targetPosition.x >= gameObject.transform.position.x)
                {
                    _rotationShellY = 180;
                }
                else
                {
                    _rotationShellY = -180;
                }
                
                _ShellsViews[i].CorrectRotationShill(_ShellsViews[i].gameObject,_CharacterTargerViews[i].transform.position,_rotationShellY);
                
            }
        }
    }
    
}