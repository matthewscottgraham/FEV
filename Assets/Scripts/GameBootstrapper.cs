using System;
using FEV;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private CommandView commandView;
    
    private GridVisualizer _gridVisualizer;
    private PegController _pegController;
    private PlayerController _playerController;
    private CommandController _commandController;
    private InputController _inputController;
    
    private void Start()
    {
        _gridVisualizer = Create<GridVisualizer>() as GridVisualizer;
        _gridVisualizer?.Initialize();
        
        _pegController = Create<PegController>() as PegController;
        _pegController?.Initialize();
        
        _playerController = Create<PlayerController>() as PlayerController;
        _playerController?.Initialize();
        
        _commandController = new CommandController(commandView, _playerController);
        
        _inputController = Create<InputController>() as InputController;
        _inputController?.Initialize();
    }

    private void OnDestroy()
    {
        _playerController?.Dispose();
        _playerController = null;
        
        _commandController?.Dispose();
        _commandController = null;

        _inputController?.Dispose();
        _inputController = null;
    }

    private Component Create<T>()
    {
        var go = new GameObject(typeof(T).Name);
        go.transform.SetParent(transform);
        return go.AddComponent(typeof(T));
    }
}
