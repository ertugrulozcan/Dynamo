namespace Dynamo.Reflection.Models
{
	public class DynamoAnnotationInfo
	{
		public string Name { get; set; }
		
		public string Value { get; set; }

		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.Value))
			{
				return $"[{this.Name}]";	
			}
			
			return $"[{this.Name}(\"{this.Value}\")]";
		}
	}
}