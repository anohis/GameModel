using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameSync
{
	public class Room<TickInfo> where TickInfo : class, new()
	{
		public long Tick
		{
			get { return _tick; }
			set
			{
				_tick = value;
				FixedUpdateCount = _tick * _fixedUpdateCountPerTick;
				FixedUpdateCountMax = (_tick + 1) * _fixedUpdateCountPerTick;
			}
		}
		public long FixedUpdateCount { get; private set; }
		public long FixedUpdateCountMax { get; private set; }

		private readonly long _fixedUpdateCountPerTick;

		private Dictionary<long, TickInfo> _tickInfos;
		private long _tick;

		public Room(long fixedUpdateCountPerTick)
		{
			_fixedUpdateCountPerTick = fixedUpdateCountPerTick;

			_tickInfos = new Dictionary<long, TickInfo>();
			Tick = 0;
		}

		public void CompleteFixedUpdate()
		{
			while (FixedUpdateCount < FixedUpdateCountMax)
			{
				FixedUpdate();
			}
		}
		public void FixedUpdate()
		{
			if (FixedUpdateCount < FixedUpdateCountMax)
			{
				OnFixedUpdate();
				FixedUpdateCount++;
			}
		}
		public void TickUpdate()
		{
			OnTickUpdate(_tick, GetTick(_tick, false));
			CompleteFixedUpdate();
			Tick++;
		}

		protected TickInfo GetTick(long tick, bool autoNew)
		{
			if (!_tickInfos.TryGetValue(tick, out var info))
			{
				info = autoNew ? CreateTickInfo() : default(TickInfo);
				_tickInfos.Add(tick, info);
			}

			return info;
		}
		protected virtual void OnFixedUpdate()
		{

		}
		protected virtual void OnTickUpdate(long tick, TickInfo info)
		{

		}

		private TickInfo CreateTickInfo()
		{
			return new TickInfo();
		}
	}
}
