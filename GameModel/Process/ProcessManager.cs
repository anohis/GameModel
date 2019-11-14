using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessSystem
{
	public static class ProcessManager
	{
		private static List<ProcessBase> _list = new List<ProcessBase>();

		public static void Initialize()
		{
			var baseType = typeof(ProcessBase);
			var list = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(p => baseType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

			foreach (var type in list)
			{
				var process = Activator.CreateInstance(type) as ProcessBase;
				process.Initialize();
				_list.Add(process);
			}
		}
		public static void Deinitialize()
		{
			foreach (var process in _list)
			{
				process.Deinitialize();
			}
			_list.Clear();
		}
	}
}
