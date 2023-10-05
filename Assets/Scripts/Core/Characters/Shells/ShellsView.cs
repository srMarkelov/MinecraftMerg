using System;
using Core.Characters;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;


public class ShellsView : MonoBehaviour
{
    private const float ToleranceShellsFly = 0.05f;
    [SerializeField] private float _speedMoveShill;
    [SerializeField] private float _speedRotationShill;
    [SerializeField] private GameObject _shillRotation;
    [SerializeField] private GameObject _chicken;
    [SerializeField] private bool _itsChicken;


    private float _fixAngle;
    private float _progress;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool startMove;
    private bool _newTarget;
    private int correctRotation;
    private CharacterView _characterView;
    private ShellsController _shellsController;
    private ICharacter _targetCharacter;
    public Action<ICharacter> OnShellsDestroyICharacters;
    public Action OnShellsDestroy;
    private ICharacter _targetCharacterTest;
    private int randomNumb;

    private void Start()
    {
        Random random = new Random();
        randomNumb = random.Next(1, 6);
    }

    private void FixedUpdate()
    {
        /*startPos = transform.position;*/
        MoveShill();
        RotationShill();
    }

    public void SetCharacterView(CharacterView characterView)
    {
        _characterView = characterView;
    }
    
    
    public void SetStartAndEndPositionTest(Vector3 targetPosition,ICharacter targetCharacter)
    {
        if (_targetCharacterTest!=null)
        {
            if (_targetCharacterTest != targetCharacter)
            {
                _newTarget = true;
                startMove = true;
                return;
            }
        }
        if (startMove == false)
        {
            startPos = _characterView.SpawnShellsPosition;
        }

        endPos = targetPosition;
        startMove = true;
        _targetCharacterTest = targetCharacter;
    }
    public void SetStartAndEndPosition(Vector3 targetPosition,ICharacter targetCharacter)
    {
        if (startMove == false)
        {
            startPos = _characterView.SpawnShellsPosition;
        }

        _targetCharacter = targetCharacter;
        endPos = targetPosition;
        startMove = true;
    }

    
    private void MoveShill()
    {
        if(startMove != true) return;

        if (Vector3.Distance(transform.position, endPos) < ToleranceShellsFly)  // Maybe add tolerance  
        {
            if (_targetCharacterTest != null)
            {
                OnShellsDestroyICharacters.Invoke(_targetCharacterTest);
            }
            else if(_targetCharacter != null)
            {
                OnShellsDestroyICharacters.Invoke(_targetCharacter);
            }
            if (_shellsController != null)
            {
                _shellsController._ShellsViews.Clear();
            }
            Destroy(gameObject);
        }
        transform.position = Vector3.Lerp(startPos, endPos, _progress / Vector3.Distance(startPos,endPos));
        _progress += _speedMoveShill;
    }

    public void CorrectRotationShill(GameObject _shill, Vector3 _targetPosition, float Y)
    {
        if(_shill==null) return;
        if (_newTarget) return;

        Vector3 direction = _targetPosition - _shill.transform.position;
        Vector3 forward = Vector3.up;
        float angle = Vector3.SignedAngle(direction, forward, Vector3.down);

        var _rotationShellY = 180;
        if (_targetPosition.x > _shill.transform.position.x)
        {
            _fixAngle = angle;
            _rotationShellY = 180;
        }
        else
        {
            _fixAngle = angle;
            _rotationShellY = 0;
        }

        _shill.transform.rotation = Quaternion.Euler(_shill.transform.rotation.x,
            _rotationShellY, _fixAngle);

    }

    private void RotationShill()
    {
        if (_shillRotation == null) return;
            
        _shillRotation.transform.Rotate(0,0,_speedRotationShill/* * correctRotation*/);
    }

    public void SetShellsController(ShellsController controller)
    {
        _shellsController = controller;
        //aaa
    }

    private void OnDisable()
    {
        
        if (_itsChicken && randomNumb == 3)
        {
            Instantiate(_chicken, new Vector3(transform.position.x, transform.position.y - 1.15f, transform.position.z),quaternion.identity);
        }
    }
}
