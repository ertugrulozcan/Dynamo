using System.Collections.Generic;
using System.Linq;
using Dynamo.Code;
using Dynamo.Reflection.Models;
using NUnit.Framework;

namespace Dynamo.Test
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void DeserializeTest()
		{
			var options = new CodeGenerationOptions
			{
				HasGetter = true,
				HasSetter = true,
				FlagAsNullablePrimitiveTypes = true
			};
		
			string json1 = "{\"id\": 1, \"name\": \"blablabla\"}";
			var class1 = CSharp.ConvertToClassInfo("TestClass1", json1, options, out IEnumerable<string> namespaces1);
			
			string json2 = "{\"key\": 1, \"date\": \"2012-04-23T18:25:43.511Z\"}";
			var class2 = CSharp.ConvertToClassInfo("TestClass2", json2, options, out IEnumerable<string> namespaces2);
			
			List<string> namespaces = new List<string> { "Newtonsoft.Json" };
			namespaces.AddRange(namespaces1);
			namespaces.AddRange(namespaces2);
			namespaces = namespaces.Distinct().ToList();

			DynamoNamespaceInfo namespaceInfo = new DynamoNamespaceInfo
			{
				Path = "Dynamo.Test",
				References = namespaces,
				Classes = new[] { class1, class2 }
			};

			string code = namespaceInfo.ToString();
			Assert.IsNotNull(code);
		}
	}
}