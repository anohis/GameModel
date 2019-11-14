using System;
using MessageSystem;

namespace ProcessSystem
{
	public sealed class WaitHandler<T> : IWaitHandler where T : struct, IMessage
	{
		public bool IsWaiting { get { return _isWaiting; } }
		public T Msg { get { return _msg; } }

		private bool _isWaiting;
		private T _msg;
		private Action<T> _onReceive;
		private Action _onComplete;

		public WaitHandler(Action onComplete)
		{
			_onReceive = OnReceive;
			_onComplete = onComplete;
		}
		public void Wait()
		{
			if (_isWaiting)
			{
				MessageTransceiver<T>.RemoveListener(_onReceive);
			}

			_isWaiting = true;
			MessageTransceiver<T>.AddListener(_onReceive);
		}
		public void Close()
		{
			MessageTransceiver<T>.RemoveListener(_onReceive);
			_isWaiting = false;
		}

		private void OnReceive(T msg)
		{
			if (_isWaiting)
			{
				MessageTransceiver<T>.RemoveListener(_onReceive);
				_isWaiting = false;
				_msg = msg;

				_onComplete?.Invoke();
			}
		}
	}
}
