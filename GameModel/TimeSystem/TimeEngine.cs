using System.Collections.Generic;

namespace TimeSystem
{
	public static class TimeEngine 
	{
		private static List<ITimer> _notYetUpdate = new List<ITimer>();
		private static List<ITimer> _Updated = new List<ITimer>();

		public static void Register(ITimer timer)
		{
			if (!_notYetUpdate.Contains(timer))
			{
				_notYetUpdate.Add(timer);
			}
		}
		public static void UnRegister(ITimer timer)
		{
			_Updated.Remove(timer);
			_Updated.Remove(timer);
		}
		public static void Update(float deltaTime)
		{
			while (_notYetUpdate.Count > 0)
			{
				var timer = _notYetUpdate[0];

				if (timer.IsSingleUpdate)
				{
					SingleUpdate(timer,deltaTime);
				}
				else
				{

				}

				_notYetUpdate.Remove(timer);
				_Updated.Add(timer);
			}

			Switch();
		}
		public static void Clear()
		{
			_notYetUpdate.Clear();
			_Updated.Clear();
		}

		private static void Switch()
		{
			var temp = _Updated;
			_Updated = _notYetUpdate;
			_notYetUpdate = temp;
		}
		private static void Update(ITimer timer, float deltaTime)
		{
			timer.DeltaTime = deltaTime;
			timer.Time += deltaTime;
			timer.Update();
		}
		private static void SingleUpdate(ITimer timer, float deltaTime)
		{
			deltaTime *= timer.TimeScale;
			Update(timer, deltaTime);
		}
		private static void MultipleUpdate(ITimer timer, float deltaTime)
		{
			var quotient = (int)(timer.TimeScale / 1);
			var remainder = timer.TimeScale % 1;
			for (int i = 0; i < quotient; i++)
			{
				Update(timer, deltaTime);
			}
			if (remainder > 0)
			{
				deltaTime *= remainder;
				Update(timer, deltaTime);
			}
		}
	}
}