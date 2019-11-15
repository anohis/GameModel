using System;
using System.Diagnostics;

namespace Logger
{
    public abstract class LoggerBase
	{
		[Conditional("DEBUG")]
		public abstract void Debug(string log);
		public abstract void Info(string log);
		public abstract void Warn(string log);
		public abstract void Error(string log);
	}
}
