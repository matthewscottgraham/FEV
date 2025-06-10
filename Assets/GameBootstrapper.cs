using System;
using FEV;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private GridVisualizer gridVisualizer;
    [SerializeField] private CommandView commandView;
    
    private PlayerController _playerController;
    private CommandController _commandController;
    
    private void Start()
    {
        gridVisualizer.Initialize();
        
        _playerController = Create<PlayerController>() as PlayerController;
        _playerController?.Initialize();
        
        _commandController = new CommandController(commandView, _playerController);
    }

    private void OnDestroy()
    {
        _playerController?.Dispose();
        _playerController = null;
        
        _commandController?.Dispose();
        _commandController = null;
    }

    private Component Create<T>()
    {
        var go = new GameObject(typeof(T).Name);
        go.transform.SetParent(transform);
        return go.AddComponent(typeof(T));
    }
}
