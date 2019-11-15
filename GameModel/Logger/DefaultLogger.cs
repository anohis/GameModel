using System;
using System.Collections.Generic;
using System.Text;
using Writer;

namespace Logger
{
	public class DefaultLogger : LoggerBase
	{
		private IWriter _writer;

		public DefaultLogger(string file, bool isAppend)
		{
			_writer = new AsyncWriter(file, isAppend);
		}

		public override void Debug(string log)
		{
			_writer.WriteLine(log);
		}
		public override void Error(string log)
		{
			_writer.WriteLine(log);
		}
		public override void Info(string log)
		{
			_writer.WriteLine(log);
		}
		public override void Warn(string log)
		{
			_writer.WriteLine(log);
		}
	}
}
