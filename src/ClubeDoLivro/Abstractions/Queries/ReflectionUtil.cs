using System;
using System.Collections;
using System.Linq;

namespace ClubeDoLivro.Abstractions.Queries
{
	public static class ReflectionUtil
	{
		public static bool IsSubclassOf<Class>(this Type self) => self.IsSubclassOf(typeof(Class));

		public static bool Implements<Interface>(this Type self) => self.Implements(typeof(Interface));

		public static bool Implements(this Type self, Type @interface) => self.GetInterfaces().Any(i => i == @interface);

		public static bool IsGenericList(this Type self) =>
			self.IsGenericType && self.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IList));
	}
}