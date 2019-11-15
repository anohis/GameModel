using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadSafe
{
	public class ThreadSafeValue<T>
    {
		private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
		private T _value;

		public T Value
		{
			get
			{
				using (new AutoReaderWriterLockSlim(_lock, AutoReaderWriterLockSlim.LockMode.Read))
				{
					return _value;
				}
			}
			set
			{
				using (new AutoReaderWriterLockSlim(_lock, AutoReaderWriterLockSlim.LockMode.Read))
				{
					_value = value;
				}
			}
		}

		public ThreadSafeValue()
		{

		}
		public ThreadSafeValue(T value)
		{
			_value = value;
		}
	}
}
