using System;
using TimeSystem;

namespace World
{
	public class TimeWorldEngine : ITimeWorldEngine, ITimeObject
	{
		public event Action<float, float> OnUpdateEvent;
		public float TimeScale { get; set; }
		public float Time { get { return _time.Time; } }
		public float DeltaTime { get { return _time.DeltaTime; } }

		private Timer _time;

		public TimeWorldEngine()
		{
			_time = CreateTimer();
			_time.Register(this);
		}
		public void Close()
		{
			_time.UnRegister(this);
			_time.Close();
			OnClose();
		}
		public virtual void Update(float time, float deltaTime)
		{
			OnUpdate(time, deltaTime);
		}

		protected void OnUpdate(float time, float deltaTime)
		{
			OnUpdateEvent?.Invoke(time, deltaTime);
		}
		protected virtual void OnClose()
		{

		}
		protected virtual Timer CreateTimer()
		{
			return new Timer();
		}
	}
}