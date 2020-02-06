using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Reflection.Models
{
	public class DynamoPropertyInfo
	{
		public DynamoAccessModifier AccessModifier { get; set; }
		
		public Type PropertyType { get; set; }
		
		public string Name { get; set; }
		
		public bool IsNullable { get; set; }
		
		public DynamoPropertyMethodInfo Getter { get; set; }
		
		public DynamoPropertyMethodInfo Setter { get; set; }
		
		public IEnumerable<DynamoAnnotationInfo> Attributes { get; set; }

		public override string ToString()
		{
			string getterAndSetter;
			if (this.Getter != null && this.Setter != null)
				getterAndSetter = $"{this.Getter} {this.Setter}";
			else if (this.Getter == null)
				getterAndSetter = $"{this.Setter}";
			else if (this.Setter == null)
				getterAndSetter = $"{this.Getter}";
			else
				throw new Exception("A property must have getter or setter!");
			
			string propertyTypeName = Helpers.TypeHelper.ConvertBuiltInDotNetTypeToPrimitiveTypeName(this.PropertyType);
			propertyTypeName = (this.IsNullable ? propertyTypeName + "?" : propertyTypeName);
			
			string propertyString = $"{this.AccessModifier} {propertyTypeName} {FixPropertyNameByCamelNotation(this.Name)} " + "{ " + getterAndSetter + " }";
			
			if (this.Attributes != null)
			{
				return string.Join(Environment.NewLine, this.Attributes.Select(x => x.ToString())) +
					Environment.NewLine +
					propertyString;
			}

			return propertyString;
		}
		
		private static string FixPropertyNameByCamelNotation(string propertyName)
		{
			return propertyName.First().ToString().ToUpper() + propertyName.Substring(1);
		}
	}
}