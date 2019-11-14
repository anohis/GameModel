using System;
using System.Collections.Generic;
using MessageSystem;

namespace DataCenter
{
	internal struct QueryMessage<T> : IMessage where T : struct, IDataCenterData
	{
		public Action<IReadOnlyList<T>> Query;
	}
}
