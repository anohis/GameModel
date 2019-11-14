using System;
using MessageSystem;

namespace DataCenter
{
	internal struct UpdateMessage<T> : IMessage where T : struct, IDataCenterData
	{
		public Func<T, bool> Where;
		public Func<T, T> Update;
	}

	public struct OnUpdateMessage<T> : IMessage where T : struct, IDataCenterData
	{

	}
}
