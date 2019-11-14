namespace World
{
	public abstract class TimeWorld
	{
		protected ITimeWorldEngine _engine;

		public TimeWorld() : this(new TimeWorldEngine())
		{

		}
		public TimeWorld(ITimeWorldEngine engine)
		{
			_engine = engine;
			_engine.OnUpdateEvent += Update;
		}
		public void Close()
		{
			_engine.Close();
			OnClose();
		}

		protected abstract void Update(float time, float deltaTime);
		protected abstract void OnClose();
	}
}
