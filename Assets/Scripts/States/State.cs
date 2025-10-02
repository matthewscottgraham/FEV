using System.Threading.Tasks;

namespace States
{
    public interface IState
    {
        public async Task EnterState() { await Task.Delay(500); }
        public async Task ExitState() { await Task.Delay(500); }
    }
}