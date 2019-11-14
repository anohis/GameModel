namespace TimeSystem
{
	public interface ITimeObject
	{
		float TimeScale { get; }
		void Update(float time, float deltaTime);
	}
}