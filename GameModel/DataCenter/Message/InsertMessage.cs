using MessageSystem;

namespace DataCenter
{
	internal struct InsertMessage<T> : IMessage where T : struct, IDataCenterData
	{
		public T Data;
	}

	public struct OnInsertMessage<T> : IMessage where T : struct, IDataCenterData
	{

	}
}
