using System;

namespace Tweening
{
    public delegate float ScaleFunction(float progress);
    public delegate T LerpFunction<T>(T start, T end, float progress);
    
    public class Tween<T> : ITween<T> where T : struct
    {
        private readonly LerpFunction<T> _lerpFunction;

        private float _currentTime;
        private float _duration;
        private ScaleFunction _scaleFunction;
        private TweenState _state;

        private T _start;
        private T _end;
        private T _value;
        
        public float CurrentTime => _currentTime;
        public float Duration => _duration;
        public TweenState State => _state;
        public T StartValue => _start;
        public T EndValue => _end;
        public T CurrentValue => _value;

        public Tween(LerpFunction<T> lerpFunction)
        {
            this._lerpFunction = lerpFunction;
            _state = TweenState.Stopped;
        }
        
        public void Start(T start, T end, float duration, ScaleFunction scaleFunction)
        {
            if (duration <= 0)
            {
                throw new ArgumentException("duration must be greater than 0");
            }
            if (scaleFunction == null)
            {
                throw new ArgumentNullException("scaleFunction");
            }

            _currentTime = 0;
            _duration = duration;
            _scaleFunction = scaleFunction;
            _state = TweenState.Running;

            _start = start;
            _end = end;

            UpdateValue();
        }
        
        public void Pause()
        {
            if (_state == TweenState.Running)
            {
                _state = TweenState.Paused;
            }
        }
        
        public void Resume()
        {
            if (_state == TweenState.Paused)
            {
                _state = TweenState.Running;
            }
        }
        
        public void Stop(StopBehavior stopBehavior)
        {
            _state = TweenState.Stopped;

            if (stopBehavior != StopBehavior.ForceComplete) return;
            _currentTime = _duration;
            UpdateValue();
        }
        
        public void Update(float elapsedTime)
        {
            if (_state != TweenState.Running) return;

            _currentTime += elapsedTime;
            if (_currentTime >= _duration)
            {
                _currentTime = _duration;
                _state = TweenState.Stopped;
            }

            UpdateValue();
        }
        
        private void UpdateValue()
        {
            _value = _lerpFunction(_start, _end, _scaleFunction(_currentTime / _duration));
        }
    }
}
