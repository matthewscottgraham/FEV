using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using States;
using UnityEngine;
using Utils;

namespace Players
{
    public class PlayerController : MonoBehaviour, IDisposable
    {
        public Action OnScoreUpdated { get; set; }
        
        private MatchConfiguration _matchConfiguration;
        private Player[] _players;
        private int _currentPlayerIndex = 0;
        private readonly Color[] _colors = new []{Color.blue, Color.red, Color.green, Color.yellow};
        
        public void Initialize(MatchConfiguration matchConfiguration)
        {
            _matchConfiguration = matchConfiguration;
            _players = new Player[_matchConfiguration.PlayerCount];
            
            for (int i = 0; i < _players.Length; ++i)
            {
                _players[i] = ScriptableObject.CreateInstance<Player>();
                _players[i].Initialize(i, _colors[i], IconUtility.GetPlayerSprite(i));
            }

            StateMachine.OnStateChanged += HandleStateChanged;
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

        public async void RemoveTile(Commands.ICommand command, Player player)
        {
            player.RemoveCommand(command);

            if (GetCurrentPlayer().AvailableCommands.Count > 0) return;
            
            await Task.Delay(TimeSpan.FromSeconds(2f));
        }

        public void Dispose()
        {
            _players = null;
            StateMachine.OnStateChanged -= HandleStateChanged;
        }

        public Player GetPlayer(int playerIndex)
        {
            try { return _players[playerIndex]; }
            catch { return null; }
        }

        private void HandleStateChanged()
        {
            if (StateMachine.CurrentState.GetType() == typeof(EndTurnPhase))
            {
                NextPlayer();
            }
            else if (StateMachine.CurrentState.GetType() == typeof(EndGamePhase))
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
        }
    }
}