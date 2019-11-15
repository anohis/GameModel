using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkTransmission;

namespace NetworkTransmission.UnitTest
{
	[TestClass]
	public class TCPServerTest
	{
		[TestMethod]
		public void Start_Twice()
		{
			var server = new TCPServer();
			server.Start(6022);
			server.Start(6022);
			server.Close();
		}
		[TestMethod]
		public void Start_Restart()
		{
			var server = new TCPServer();
			server.Start(6022);
			server.Close();
			server.Start(6022);
			server.Close();
		}
	}
}
