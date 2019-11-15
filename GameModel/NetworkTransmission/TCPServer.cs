using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ThreadSafe;

namespace NetworkTransmission
{
    public class TCPServer
    {
		public int ConnectionCount
		{
			get { return _clients.Count; }
		}

		private TcpListener _tcpListener;
		private ThreadSafeValue<bool> _isStart = new ThreadSafeValue<bool>();
		private ThreadSafeHashSet<Socket> _clients = new ThreadSafeHashSet<Socket>();

		public void Start(int port)
		{
			if (!_isStart.Value)
			{
				_tcpListener = new TcpListener(IPAddress.IPv6Any, port);
				_tcpListener.Server.NoDelay = true;
				_tcpListener.Server.Blocking = true;
				_tcpListener.Server.UseOnlyOverlappedIO = false;
				_tcpListener.Start();
				_tcpListener.BeginAcceptSocket(EndAcceptSocket, null);

				_isStart.Value = true;
			}
		}
		public void Close()
		{
			_isStart.Value = false;
			_clients.Clear();
			_tcpListener.Stop();
		}

		private void EndAcceptSocket(IAsyncResult result)
		{
			if (_isStart.Value)
			{
				if (result.IsCompleted)
				{
					var socket = _tcpListener.EndAcceptSocket(result);
					_clients.Add(socket);
					_tcpListener.BeginAcceptSocket(EndAcceptSocket, null);
				}
			}
		}
	}
}
