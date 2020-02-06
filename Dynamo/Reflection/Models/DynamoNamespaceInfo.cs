using System;
using System.Collections.Generic;
using System.Linq;
using Dynamo.Extensions;

namespace Dynamo.Reflection.Models
{
	public class DynamoNamespaceInfo
	{
		public IEnumerable<string> References { get; set; }
		
		public string Path { get; set; }
		
		public IEnumerable<DynamoClassInfo> Classes { get; set; }

		public override string ToString()
		{
			string usingPart = string.Join(Environment.NewLine, this.References.Select(x => $"using {x};"));
			return usingPart + 
				Environment.NewLine +
				Environment.NewLine +
				$"namespace {this.Path}" +
				Environment.NewLine +
				"{" +
				Environment.NewLine +
				string.Join(Environment.NewLine + Environment.NewLine, this.Classes.Select(x => x.ToString().IncreaseIndent(1))) +
				Environment.NewLine +
				"}";
		}
	}
}