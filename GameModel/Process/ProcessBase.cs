using System;
using System.Collections;
using System.Collections.Generic;
using MessageSystem;

namespace ProcessSystem
{
	public abstract class ProcessBase
	{
		private Dictionary<Type, IWaitHandler> _waitHandlers = new Dictionary<Type, IWaitHandler>();
		private IEnumerator _process;
		private Action _run;

		public void Initialize()
		{
			OnInitialize();

			_run = RunNext;
			_process = Execute();
			RunNext();
		}
		public void Deinitialize()
		{
			foreach (var handler in _waitHandlers.Values)
			{
				handler.Close();
			}
			_waitHandlers.Clear();

			OnDeinitialize();
		}

		protected virtual void OnInitialize()
		{
		}
		protected virtual void OnDeinitialize()
		{
		}
		protected abstract IEnumerator Execute();
		protected bool Wait<T>(out WaitHandler<T> handler) where T : struct, IMessage
		{
			var type = typeof(T);
			handler = null;
			if (_waitHandlers.ContainsKey(type))
			{
				handler = _waitHandlers[type] as WaitHandler<T>;
			}
			else
			{
				handler = new WaitHandler<T>(_run);
				_waitHandlers.Add(type, handler);
			}

			handler.Wait();
			return true;
		}
		protected bool Wait<T>() where T : struct, IMessage
		{
			return Wait<T>(out var handler);
		}

		private void RunNext()
		{
			_process.MoveNext();
		}
	}
}
