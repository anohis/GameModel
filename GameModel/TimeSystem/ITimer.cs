namespace TimeSystem
{
	public interface ITimer
	{
		bool IsSingleUpdate { get; }
		float TimeScale { get; }
		float Time { get; set; }
		float DeltaTime { get; set; }

		void Update();
	}
}