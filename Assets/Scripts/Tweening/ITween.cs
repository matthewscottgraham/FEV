namespace Tweening
{
    public interface ITween
    {
        TweenState State { get; }
        void Pause();
        void Resume();
        void Stop(StopBehavior stopBehavior);
        void Update(float elapsedTime);
    }
    
    public interface ITween<T> : ITween where T : struct
    {
        T CurrentValue { get; }
        void Start(T start, T end, float duration, ScaleFunction scaleFunction);
    }
}