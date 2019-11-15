using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static ThreadSafe.AutoReaderWriterLockSlim;

namespace ThreadSafe
{
    public class ThreadSafeHashSet<T>
    {
		private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
		private HashSet<T> _hashSet = new HashSet<T>();

		public int Count
		{
			get
			{
				using (new AutoReaderWriterLockSlim(_lock, LockMode.Read))
				{
					return _hashSet.Count;
				}
			}
		}

		public bool Add(T item)
		{
			using (new AutoReaderWriterLockSlim(_lock, LockMode.Write))
			{
				return _hashSet.Add(item);
			}
		}
		public void Clear()
		{
			using (var locker = new AutoReaderWriterLockSlim(_lock, LockMode.Write))
			{
				_hashSet.Clear();
			}
		}
		public bool Remove(T item)
		{
			using (var locker = new AutoReaderWriterLockSlim(_lock, LockMode.Write))
			{
				return _hashSet.Remove(item);
			}
		}
	}
}
