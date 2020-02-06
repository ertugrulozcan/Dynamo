namespace Dynamo.Reflection.Models
{
	public class DynamoAccessModifier
	{
		private string Modifier { get; }
		
		private DynamoAccessModifier(string modifier)
		{
			this.Modifier = modifier;
		}
		
		public static DynamoAccessModifier Public = new DynamoAccessModifier("public");
		public static DynamoAccessModifier Private = new DynamoAccessModifier("private");
		public static DynamoAccessModifier Protected = new DynamoAccessModifier("protected");
		public static DynamoAccessModifier Internal = new DynamoAccessModifier("internal");
		public static DynamoAccessModifier ProtectedInternal = new DynamoAccessModifier("protected internal");
		public static DynamoAccessModifier PrivateProtected = new DynamoAccessModifier("private protected");

		public override string ToString()
		{
			return this.Modifier;
		}
	}
}