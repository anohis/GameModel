using System;
using MessageSystem;

namespace DataCenter
{
	internal struct DeleteMessage<T> : IMessage where T : struct, IDataCenterData
	{
		public Func<T, bool> Where;
	}

	public struct OnDeleteMessage<T> : IMessage where T : struct, IDataCenterData
	{

	}
}
