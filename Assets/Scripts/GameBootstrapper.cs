using Commands;
using FEV;
using Pegs;
using Players;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private CommandView commandView;
    
    private PegController _pegController;
    private PlayerController _playerController;
    private CommandController _commandController;
    private InputController _inputController;
    private SelectionController _selectionController;
    private TileFactory _tileFactory;
    
    private void Start()
    {
        var matchConfiguration = new MatchConfiguration();
        
        _playerController = Create<PlayerController>() as PlayerController;
        _playerController?.Initialize(matchConfiguration);
        
        _pegController = Create<PegController>() as PegController;
        _pegController?.Initialize(matchConfiguration, _playerController);
        
        _tileFactory = new TileFactory();
        _commandController = new CommandController(matchConfiguration, commandView, _playerController, _tileFactory);
        
        _inputController = Create<InputController>() as InputController;
        _inputController?.Initialize();

        _selectionController = Create<SelectionController>() as SelectionController;
        _selectionController?.Initialize(matchConfiguration, _inputController, _pegController);
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
        
        _tileFactory?.Dispose();
        _tileFactory = null;
    }

    private Component Create<T>()
    {
        var go = new GameObject(typeof(T).Name);
        go.transform.SetParent(transform);
        return go.AddComponent(typeof(T));
    }
}