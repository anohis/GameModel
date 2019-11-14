using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataCenter
{
	public static class DataCenterManager
	{
		private const BindingFlags _standardBindingFlag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

		private const string _getterListName = "_primaryKeyGetterList";

		private static List<DataCenterBase> _list = new List<DataCenterBase>();

		public static void Initialize()
		{
			var baseType = typeof(DataCenterBase);
			var list = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(p => baseType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

			foreach (var type in list)
			{
				var db = Activator.CreateInstance(type) as DataCenterBase;
				db.Initialize();
				_list.Add(db);

				if (type.BaseType.IsGenericType)
				{
					var usageType = type.BaseType.GenericTypeArguments[0];
					var members = GetPropertiesAndFields(usageType);
					var getterList = type.BaseType.GetField(_getterListName, _standardBindingFlag);

					var primaryKeyGetterList = new List<PrimaryKeyGetter>();
					foreach (var member in members)
					{
						var primaryKeyAttribute = member.GetCustomAttribute<PrimaryKeyAttribute>(false);
						if (primaryKeyAttribute != null)
						{
							primaryKeyGetterList.Add(new PrimaryKeyGetter(usageType, member));
						}
					}

					getterList.SetValue(db, primaryKeyGetterList);
				}
			}
		}
		public static void Deinitialize()
		{
			foreach (var db in _list)
			{
				db.Deinitialize();
			}
			_list.Clear();
		}

		private static MemberInfo[] GetPropertiesAndFields(Type type)
		{
			MemberInfo[] propInfo = type.GetProperties(_standardBindingFlag);
			MemberInfo[] fieldInfo = type.GetFields(_standardBindingFlag);
			var output = new MemberInfo[propInfo.Length + fieldInfo.Length];
			Array.Copy(propInfo, output, propInfo.Length);
			Array.Copy(fieldInfo, 0, output, propInfo.Length, fieldInfo.Length);
			return output;
		}
	}
}
