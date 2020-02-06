using System;
using System.Collections.Generic;
using System.Linq;
using Dynamo.Extensions;

namespace Dynamo.Reflection.Models
{
	public class DynamoClassInfo
	{
		public DynamoAccessModifier AccessModifier { get; set; }
		
		public string Name { get; set; }
		
		public IEnumerable<DynamoPropertyInfo> Properties { get; set; }

		public override string ToString()
		{
			string classDeclerator = $"{this.AccessModifier} class {this.Name}";
			return
				classDeclerator +
				Environment.NewLine +
				"{" +
				Environment.NewLine +
				string.Join($"{Environment.NewLine}{Environment.NewLine}", this.Properties.Select(x => x.ToString().IncreaseIndent())) +
				Environment.NewLine +
				"}";
		}
	}
}