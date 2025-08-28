using States;
using Players;

namespace Commands
{
    public class DrawTileCommand : ICommand
    {
        private readonly Player _player;
        public string Label => "+";
        
        public DrawTileCommand(Player player)
        {
            _player = player;
        }
        public void Execute()
        {
            _player.RemoveCommand(this);
            StateMachine.NextState();
        }

        public void Destroy()
        {
            // noop
        }
    }
}