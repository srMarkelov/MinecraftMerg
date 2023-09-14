using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Core.Characters;
using Core.Field;
using UnityEngine;

public class SaveCloudController : MonoBehaviour
{
    public static SaveCloudController singleton { get; private set; }

    [SerializeField] private StorageVariable _storageVariable;
    [SerializeField] private FieldConstructor _fieldConstructor;
    [SerializeField] private FieldPreset _fieldPreset;
    [SerializeField] private MouseTracker _mouseTracker;
    [SerializeField] private LibraryController _libraryController;
    [SerializeField] private CharacterSeller _characterSeller;
    [SerializeField] private FinishLevelView _finishLevelView;
    
    public ICloud ICloud;
    

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject );
        }
        singleton = this;
        
#if UNITY_WEBGL
        ICloud = new SaveCloudYandex();
#endif
        if (ICloud != null)
        {
            ICloud.SetSaveCloudController(this);
            ICloud.OnEnable();
            ICloud.SetFieldConstructor(_fieldConstructor);
        }
    }

    public void LoadedSave()
    {
        _storageVariable.StartGame();
        _fieldPreset.StartGame();
        _mouseTracker.StartGame();
        _libraryController.StartGame();
        _characterSeller.StartGame();
        _finishLevelView.StartGame();
    }
    private void OnDisable()
    {
        ICloud.OnDisable();
    }
}
