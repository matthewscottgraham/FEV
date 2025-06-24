using FEV;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private CommandView commandView;
    
    private PegController _pegController;
    private PlayerController _playerController;
    private CommandController _commandController;
    private InputController _inputController;
    private SelectionController _selectionController;

    private void Start()
    {
        var matchState = Resources.Load<MatchState>("MatchState");
        
        _pegController = Create<PegController>() as PegController;
        _pegController?.Initialize(matchState);
        
        _playerController = Create<PlayerController>() as PlayerController;
        _playerController?.Initialize();
        
        _commandController = new CommandController(commandView, _playerController);
        
        _inputController = Create<InputController>() as InputController;
        _inputController?.Initialize();

        _selectionController = Create<SelectionController>() as SelectionController;
        _selectionController?.Initialize(matchState, _inputController, _pegController);
    }

    private void OnDestroy()
    {
        _playerController?.Dispose();
        _playerController = null;
        
        _commandController?.Dispose();
        _commandController = null;

        _inputController?.Dispose();
        _inputController = null;
        
        _selectionController?.Dispose();
        _selectionController = null;
    }

    private Component Create<T>()
    {
        var go = new GameObject(typeof(T).Name);
        go.transform.SetParent(transform);
        return go.AddComponent(typeof(T));
    }
}