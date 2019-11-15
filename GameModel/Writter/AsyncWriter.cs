using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace Writer
{
	public class AsyncWriter : IWriter
	{
		private Thread _thread;
		private ConcurrentQueue<string> _queue;
		private bool _isClose;
		private StreamWriter _stream;

		public AsyncWriter(string file, bool isAppend)
		{
			_queue = new ConcurrentQueue<string>();
			_stream = new StreamWriter(file, isAppend);
			_thread = new Thread(Execute);
			_thread.Start();
		}

		public void WriteLine(string str)
		{
			_queue.Enqueue(str);
		}
		public void Close()
		{
			_isClose = true;
		}

		private void Execute()
		{
			while (!_isClose)
			{
				if (_queue.TryDequeue(out var str))
				{
					_stream.WriteLine(str);
				}
			}

			while (_queue.TryDequeue(out var str))
			{
				_stream.WriteLine(str);
			}

			_stream.Close();
		}
	}
}
