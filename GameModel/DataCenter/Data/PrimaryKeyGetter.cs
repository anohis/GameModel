using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataCenter
{
	internal sealed class PrimaryKeyGetter
	{
		private static readonly ParameterExpression _dataParameter = Expression.Parameter(typeof(object));

		public Func<object, object> Get { get; private set; }

		public PrimaryKeyGetter(Type type, MemberInfo field)
		{
			var fieldExpression = Expression.MakeMemberAccess(Expression.Convert(_dataParameter, type), field);
			Get = Expression.Lambda<Func<object, object>>(Expression.Convert(fieldExpression, typeof(object)), _dataParameter).Compile();
		}
	}

	internal sealed class PrimaryKeyGetter<InstanceType, ReturnType>
	{
		private static readonly ParameterExpression _dataParameter = Expression.Parameter(typeof(InstanceType));

		public Func<InstanceType, ReturnType> Get { get; private set; }

		public PrimaryKeyGetter(MemberInfo field)
		{
			var fieldExpression = Expression.MakeMemberAccess(_dataParameter, field);
			Get = Expression.Lambda<Func<InstanceType, ReturnType>>(fieldExpression, _dataParameter).Compile();
		}
	}
}
