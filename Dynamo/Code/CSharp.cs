using System;
using System.Collections.Generic;
using Dynamo.Reflection.Models;

namespace Dynamo.Code
{
	public static class CSharp
	{
		public static DynamoClassInfo ConvertToClassInfo(string className, string json, CodeGenerationOptions options, out IEnumerable<string> namespaces)
		{
			var usingNamespaces = new List<string>();
			
			dynamic expando = Json.Deserializer.Deserialize(json);
			var properties = new List<DynamoPropertyInfo>();

			foreach (var pair in (IDictionary<string, object>)expando)
			{
				var propertyName = pair.Key;
				var propertyValue = pair.Value;
				Type type = propertyValue.GetType();

				DynamoPropertyInfo propertyInfo = new DynamoPropertyInfo
				{
					Name = propertyName,
					PropertyType = type,
					Getter = DynamoPropertyMethodInfo.NewGetter(),
					Setter = DynamoPropertyMethodInfo.NewSetter(),
					AccessModifier = DynamoAccessModifier.Public,
					Attributes = new[] { new DynamoAnnotationInfo { Name = "JsonProperty", Value = propertyName } },
					IsNullable = options.FlagAsNullablePrimitiveTypes && (type.IsPrimitive || !type.IsClass)
				};
				
				properties.Add(propertyInfo);

				string namespacePath = type.Namespace;
				if (!usingNamespaces.Contains(namespacePath))
				{
					usingNamespaces.Add(namespacePath);
				}
			}

			namespaces = usingNamespaces;
			
			return new DynamoClassInfo
			{
				Name = className,
				AccessModifier = DynamoAccessModifier.Public,
				Properties = properties
			};
		}
	}
}