using System;
using TimeSystem;

namespace World
{
	public interface ITimeWorldEngine
	{
		float Time { get; }
		float DeltaTime { get; }

		event Action<float, float> OnUpdateEvent;
		void Close();
	}
}