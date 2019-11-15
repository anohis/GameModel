using System;
using System.Threading;

namespace ThreadSafe
{
	public struct AutoReaderWriterLockSlim : IDisposable
	{
		public enum LockMode
		{
			Read = 0,
			Write
		}

		private ReaderWriterLockSlim _lock;
		private LockMode _mode;

		public AutoReaderWriterLockSlim(ReaderWriterLockSlim rwLock, LockMode mode)
		{
			_lock = rwLock;
			_mode = mode;

			if (rwLock != null)
			{
				switch (mode)
				{
					case LockMode.Read:
						rwLock.EnterReadLock();
						break;
					case LockMode.Write:
						rwLock.EnterWriteLock();
						break;
				}
			}
		}
		public void Dispose()
		{
			if (_lock != null)
			{
				switch (_mode)
				{
					case LockMode.Read:
						_lock.ExitReadLock();
						break;
					case LockMode.Write:
						_lock.ExitWriteLock();
						break;
				}

				_lock = null;
			}
		}
	}
}