using System;
using System.Collections.Generic;
using MessageSystem;

namespace DataCenter
{
	public abstract class DataCenterBase
	{
		public abstract void Initialize();
		public abstract void Deinitialize();
	}

	public abstract class DataCenterBase<T> : DataCenterBase where T : struct, IDataCenterData
	{
		private List<PrimaryKeyGetter> _primaryKeyGetterList = new List<PrimaryKeyGetter>();

		public override void Initialize()
		{
			MessageTransceiver<InsertMessage<T>>.AddListener(Insert);
			MessageTransceiver<QueryMessage<T>>.AddListener(Query);
			MessageTransceiver<UpdateMessage<T>>.AddListener(Update);
			MessageTransceiver<DeleteMessage<T>>.AddListener(Delete);
		}
		public override void Deinitialize()
		{
			MessageTransceiver<InsertMessage<T>>.RemoveListener(Insert);
			MessageTransceiver<QueryMessage<T>>.RemoveListener(Query);
			MessageTransceiver<UpdateMessage<T>>.RemoveListener(Update);
			MessageTransceiver<DeleteMessage<T>>.RemoveListener(Delete);
		}

		[Obsolete("Low Performance")]
		protected bool CheckPrimaryKey(T data1, T data2)
		{
			int count = _primaryKeyGetterList.Count;
			for (int i = 0; i < count; i++)
			{
				var getter = _primaryKeyGetterList[i];
				var key1 = getter.Get(data1);
				var key2 = getter.Get(data2);
				if (!key1.Equals(key2))
				{
					return true;
				}
			}

			throw new Exception(string.Format("{0} Has same PrimaryKey", typeof(T)));
		}
		[Obsolete("Low Performance")]
		protected bool CheckPrimaryKey(IReadOnlyList<T> list, T data)
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				var v = list[i];
				if (!CheckPrimaryKey(v, data))
				{
					return false;
				}
			}

			return true;
		}

		protected abstract void OnInsert(T data);
		protected abstract void OnQuery(Action<IReadOnlyList<T>> query);
		protected abstract void OnUpdate(Func<T, bool> where, Func<T, T> update);
		protected abstract void OnDelete(Func<T, bool> where);

		private void Insert(InsertMessage<T> msg)
		{
			OnInsert(msg.Data);
		}
		private void Query(QueryMessage<T> msg)
		{
			OnQuery(msg.Query);
		}
		private void Update(UpdateMessage<T> msg)
		{
			OnUpdate(msg.Where,msg.Update);
		}
		private void Delete(DeleteMessage<T> msg)
		{
			OnDelete(msg.Where);
		}
	}
}
