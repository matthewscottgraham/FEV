using Commands;
using Commands.View;
using DG.Tweening;
using FEV;
using Pegs;
using Players;
using States;
using UnityEngine;

namespace Utils
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private CommandView commandView;
    
        private PegController _pegController;
        private PlayerController _playerController;
        private CommandController _commandController;
        private InputController _inputController;
        private SelectionController _selectionController;
        private StateMachine _stateMachine;
        
        private void Start()
        {
            DOTween.SetTweensCapacity(400, 50);
            var matchConfiguration = new MatchConfiguration();
        
            _stateMachine = new StateMachine();
        
            _inputController = Create<InputController>() as InputController;
            _inputController?.Initialize();
            
            _playerController = Create<PlayerController>() as PlayerController;
            _playerController?.Initialize(matchConfiguration, _inputController);
        
            _pegController = Create<PegController>() as PegController;
            _pegController?.Initialize(matchConfiguration, _playerController);
            
            _commandController = new CommandController(matchConfiguration, commandView, _playerController);

            _selectionController = Create<SelectionController>() as SelectionController;
            _selectionController?.Initialize(_playerController, _inputController, _pegController);
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

            _stateMachine?.Dispose();
            _stateMachine = null;
        }

        private Component Create<T>()
        {
            var go = new GameObject(typeof(T).Name);
            go.transform.SetParent(transform);
            return go.AddComponent(typeof(T));
        }
    }
}