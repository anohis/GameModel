using System;

namespace MessageSystem
{
	public static class MessageTransceiver<T> where T : struct, IMessage
	{
		private static Action<T> _action;

		public static void AddListener(Action<T> handler)
		{
			_action += handler;
		}
		public static void RemoveListener(Action<T> handler)
		{
			_action -= handler;
		}
		public static void Broadcast(T msg)
		{
			_action?.Invoke(msg);
		}
	}
}
