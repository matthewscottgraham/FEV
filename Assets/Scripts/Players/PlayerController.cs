using System;
using System.Collections.Generic;
using System.Linq;
using FEV;
using States;
using UnityEngine;
using Utils;

namespace Players
{
    public class PlayerController : MonoBehaviour, IDisposable
    {
        public Action OnScoreUpdated { get; set; }
        
        private InputController _inputController;
        private MatchConfiguration _matchConfiguration;
        private Player[] _players;
        private int _currentPlayerIndex = 0;
        private readonly Color[] _colors = new []
        {
            new Color(0.690f, 0.518f, 0.797f),
            new Color(0.941f, 0.598f, 0.332f),
            new Color(0.465f, 0.730f, 0.625f),
            new Color(0.699f, 0.223f, 0.315f)
        };
        
        private float _lastRotateTime = 0;
        private const float RotateDelay = 0.5f;
        
        public void Initialize(MatchConfiguration matchConfiguration, InputController inputController)
        {
            _matchConfiguration = matchConfiguration;
            _inputController = inputController;
            _players = new Player[_matchConfiguration.PlayerCount];
            
            for (int i = 0; i < _players.Length; ++i)
            {
                _players[i] = ScriptableObject.CreateInstance<Player>();
                _players[i].Initialize(i, _colors[i], IconUtility.GetPlayerSprite(i));
            }

            StateMachine.OnStateChanged += HandleStateChanged;
            _inputController.Zoomed += HandleZoom;
        }

        public Player GetCurrentPlayer()
        {
            return _players[_currentPlayerIndex];
        }

        public Player GetNextPlayer()
        {
            var index = _currentPlayerIndex + 1;
            index %= _players.Length;
            return _players[index];
        }

        public void UpdateScores(Dictionary<Player, int> scores)
        {
            foreach (var player in _players)
            {
                player.SetScore(scores.ContainsKey(player) ? scores[player] : 0);
            }
            
            OnScoreUpdated?.Invoke();
        }

        public void Dispose()
        {
            _players = null;
            StateMachine.OnStateChanged -= HandleStateChanged;
            _inputController.Zoomed -= HandleZoom;
            _inputController = null;
        }

        public Player GetPlayer(int playerIndex)
        {
            try { return _players[playerIndex]; }
            catch { return null; }
        }

        private void HandleStateChanged()
        {
            if (StateMachine.CurrentState.GetType() == typeof(EndTurnState))
            {
                NextPlayer();
            }
            else if (StateMachine.CurrentState.GetType() == typeof(EndGameState))
            {
                FindWinningPlayer();
            }
        }

        private void FindWinningPlayer()
        {
            var playerRankings = _players.ToList().OrderBy(x => x.Score);
            foreach (var player in playerRankings)
            {
                Debug.Log($"{player}: {player.Score}");    
            }
        }

        private void NextPlayer()
        {
            GetCurrentPlayer()?.EndTurn();
            _currentPlayerIndex++;
            _currentPlayerIndex %= _players.Length;
            StateMachine.StartTurnState();
        }

        private void HandleZoom(float delta)
        {
            if (Time.time - _lastRotateTime < RotateDelay) return;
            GetCurrentPlayer().SelectedTile.Rotate(delta >= 0);
            _lastRotateTime = Time.time;
        }
    }
}