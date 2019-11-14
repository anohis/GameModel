using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageSystem;

namespace DataCenter
{
	public static class DataCenterUtility<T> where T : struct, IDataCenterData
	{
		public struct InsertRequest
		{
			public T Data;
		}
		public struct UpdateRequest
		{
			public Func<T, bool> Where;
			public Func<T, T> Update;
		}
		public struct DeleteRequest
		{
			public Func<T, bool> Where;
		}

		private static Action<IReadOnlyList<T>> _query = OnQuery;
		private static IReadOnlyList<T> _queryCache;

		public static void DoInsert(InsertRequest req)
		{
			MessageTransceiver<InsertMessage<T>>.Broadcast(new InsertMessage<T>
			{
				Data = req.Data
			});
			MessageTransceiver<OnInsertMessage<T>>.Broadcast(new OnInsertMessage<T>());
		}
		public static void DoQuery(out IReadOnlyList<T> results)
		{
			MessageTransceiver<QueryMessage<T>>.Broadcast(new QueryMessage<T>
			{
				Query = _query
			});
			results = _queryCache;
		}
		public static void DoUpdate(UpdateRequest req)
		{
			MessageTransceiver<UpdateMessage<T>>.Broadcast(new UpdateMessage<T>
			{
				Where = req.Where,
				Update = req.Update
			});
			MessageTransceiver<OnUpdateMessage<T>>.Broadcast(new OnUpdateMessage<T>());
		}
		public static void DoDelete(DeleteRequest req)
		{
			MessageTransceiver<DeleteMessage<T>>.Broadcast(new DeleteMessage<T>
			{
				Where = req.Where
			});
			MessageTransceiver<OnDeleteMessage<T>>.Broadcast(new OnDeleteMessage<T>());
		}

		private static void OnQuery(IReadOnlyList<T> results)
		{
			_queryCache = results;
		}
	}
}
