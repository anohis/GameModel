using System;
using System.Collections.Generic;

namespace TimeSystem
{
	public sealed class Timer
	{
		private sealed class Context : ITimer
		{
			public bool IsSingleUpdate { get; set; }
			public float Time { get; set; }
			public float TimeScale { get; set; }
			public float DeltaTime { get; set; }

			public List<ITimeObject> NotYetUpdate = new List<ITimeObject>();
			public List<ITimeObject> Updated = new List<ITimeObject>();

			public void Update()
			{
				while (NotYetUpdate.Count > 0)
				{
					var obj = NotYetUpdate[0];
					var timeScale = obj.TimeScale;
					obj.Update(Time * timeScale, DeltaTime * timeScale);

					NotYetUpdate.Remove(obj);
					Updated.Add(obj);
				}

				var temp = Updated;
				Updated = NotYetUpdate;
				NotYetUpdate = temp;
			}
		}

		public bool IsSingleUpdate
		{
			get
			{
				return _context.IsSingleUpdate;
			}
			set
			{
				_context.IsSingleUpdate = value;
			}
		}
		public float TimeScale
		{
			get
			{
				return _context.TimeScale;
			}
			set
			{
				_context.TimeScale = value;
			}
		}
		public float Time { get { return _context.Time; } }
		public float DeltaTime { get { return _context.DeltaTime; } }

		private Context _context;

		public Timer()
		{
			_context = new Context();
			TimeEngine.Register(_context);
		}
		public void Close()
		{
			TimeEngine.UnRegister(_context);
		}
		public void Register(ITimeObject obj)
		{
			if (!_context.NotYetUpdate.Contains(obj))
			{
				_context.NotYetUpdate.Add(obj);
			}
		}
		public void UnRegister(ITimeObject obj)
		{
			_context.NotYetUpdate.Remove(obj);
			_context.Updated.Remove(obj);
		}
	}
}

