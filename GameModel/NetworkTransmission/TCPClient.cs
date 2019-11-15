using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetworkTransmission
{
	public class TCPClient
	{
		private Socket _socket;
		private Thread _thread;

		public void Start(string ip, int port)
		{
			var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.ReceiveTimeout = 3000;
			socket.ReceiveBufferSize = 65536;
			socket.BeginConnect(ip, port, OnConnect, socket);
		}

		private void OnConnect(IAsyncResult asyncResult)
		{
			var socket = (Socket)asyncResult.AsyncState;
			socket.EndConnect(asyncResult);
			if (socket.Connected)
			{
				_socket = socket;
			}
		}
	}
}
