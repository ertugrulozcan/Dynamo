using System;

namespace Dynamo.Helpers
{
	public static class TypeHelper
	{
		public static string ConvertBuiltInDotNetTypeToPrimitiveTypeName<T>()
		{
			Type type = typeof(T);
			return ConvertBuiltInDotNetTypeToPrimitiveTypeName(type);
		}
		
		public static string ConvertBuiltInDotNetTypeToPrimitiveTypeName(Type type)
		{
			if (type.FullName == "System.Boolean")
				return "bool";
			if (type.FullName == "System.Byte")
				return "byte";
			if (type.FullName == "System.SByte")
				return "sbyte";
			if (type.FullName == "System.Char")
				return "char";
			if (type.FullName == "System.Decimal")
				return "decimal";
			if (type.FullName == "System.Double")
				return "double";
			if (type.FullName == "System.Single")
				return "float";
			if (type.FullName == "System.Int32")
				return "int";
			if (type.FullName == "System.UInt32")
				return "uint";
			if (type.FullName == "System.Int64")
				return "long";
			if (type.FullName == "System.UInt64")
				return "ulong";
			if (type.FullName == "System.Object")
				return "object";
			if (type.FullName == "System.Int16")
				return "short";
			if (type.FullName == "System.UInt16")
				return "ushort";
			if (type.FullName == "System.String")
				return "string";

			return type.Name;
		}
	}
}