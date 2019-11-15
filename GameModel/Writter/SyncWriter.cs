using System.IO;

namespace Writer
{
	public class SyncWriter : IWriter
	{
		private StreamWriter _stream;

		public SyncWriter(string file, bool isAppend)
		{
			_stream = new StreamWriter(file, isAppend);
		}

		public void WriteLine(string str)
		{
			_stream.WriteLine(str);
		}
		public void Close()
		{
			_stream.Close();
		}
	}
}
