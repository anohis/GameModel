using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrameSync.UnitTest
{
	[TestClass]
	public class RoomUnitTest
	{
		private class TickInfo { }

		[TestMethod]
		public void CompleteFixedUpdate_IsFixedUpdateCountEqualMax()
		{
			var room = new Room<TickInfo>(4);
			room.CompleteFixedUpdate();

			Assert.IsTrue(room.FixedUpdateCount == room.FixedUpdateCountMax);
		}
		[TestMethod]
		public void FixedUpdate_IsFixedUpdateCountEqualMax()
		{
			var room = new Room<TickInfo>(4);
			room.FixedUpdate();
			room.FixedUpdate();
			room.FixedUpdate();
			room.FixedUpdate();
			room.FixedUpdate();

			Assert.IsTrue(room.FixedUpdateCount == room.FixedUpdateCountMax);
		}
		[TestMethod]
		public void TickUpdate_IsFixedUpdateCountEqual()
		{
			long fixedUpdatePerTick = 4;

			var room = new Room<TickInfo>(fixedUpdatePerTick);
			room.TickUpdate();

			Assert.IsTrue(room.Tick * fixedUpdatePerTick == room.FixedUpdateCount);
		}
	}
}
