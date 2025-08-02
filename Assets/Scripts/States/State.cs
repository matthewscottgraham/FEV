namespace States
{
    public abstract class State
    {
        public virtual bool CanDrawTiles => false;
        public virtual bool CanEndTurn => false;

        public virtual void EnterState()
        {
            //noop
        }

        public virtual void ExitState()
        {
            // noop
        }
    }
}