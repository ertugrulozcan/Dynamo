using System;

namespace Dynamo.Reflection.Models
{
	public enum DynamoPropertyMethodType
	{
		Getter,
		Setter
	}
	
	public class DynamoPropertyMethodInfo
	{
		public DynamoAccessModifier AccessModifier { get; set; }
		
		public DynamoPropertyMethodType PropertyMethodType { get; set; }

		private DynamoPropertyMethodInfo(DynamoPropertyMethodType propertyMethodType)
		{
			this.PropertyMethodType = propertyMethodType;
		}

		public static DynamoPropertyMethodInfo NewGetter()
		{
			return new DynamoPropertyMethodInfo(DynamoPropertyMethodType.Getter);
		}
		
		public static DynamoPropertyMethodInfo NewSetter()
		{
			return new DynamoPropertyMethodInfo(DynamoPropertyMethodType.Setter);
		}

		public override string ToString()
		{
			switch (this.PropertyMethodType)
			{
				case DynamoPropertyMethodType.Getter:
					return "get;";
				case DynamoPropertyMethodType.Setter:
					return "set;";
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}