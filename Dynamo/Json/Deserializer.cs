using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json.Linq;

namespace Dynamo.Json
{
	public static class Deserializer
	{
		public static object Deserialize(string json)
		{
			var root = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
			if (root is JObject rootNode)
			{
				return Deserialize(rootNode);
			}

			return null;
		}
		
		private static object Deserialize(JToken jToken)
		{
			JTokenType jTokenType = jToken.Type;
			if (jTokenType == JTokenType.Property)
			{
				if (jToken is JProperty jProperty)
				{
					jTokenType = jProperty.Value.Type;
				}
			}
			
			if (IsPrimitiveType(jTokenType))
			{
				return Parse(jToken);
			}
			else
			{
				if (jToken.Type == JTokenType.None || 
					jToken.Type == JTokenType.Object || 
					jToken.Type == JTokenType.Constructor)
				{
					var dictionary = new Dictionary<string, object>();
					foreach (var child in jToken)
					{
						dictionary.Add(child.Path, Deserialize(child));
					}
					
					dynamic expando = new ExpandoObject();
					var expandoCollection = (ICollection<KeyValuePair<string, object>>)expando;
					foreach (var keyAndValue in dictionary)
					{
						expandoCollection.Add(keyAndValue);
					}

					return expando;
				}
				else if (jToken.Type == JTokenType.Array)
				{
					if (jToken is JArray jArray)
					{
						var list = new List<object>();
						foreach (var itemToken in jArray)
						{
							list.Add(Deserialize(itemToken));
						}

						return list.ToArray();
					}
				}
				
				return null;
			}
		}

		private static object Parse(JToken jToken)
		{
			switch (jToken.Type)
			{
				case JTokenType.Integer:
					return Parse<int>(jToken);
				case JTokenType.Float:
					return Parse<double>(jToken);
				case JTokenType.String:
					return Parse<string>(jToken);
				case JTokenType.Boolean:
					return Parse<bool>(jToken);
				case JTokenType.Date:
					return Parse<DateTime>(jToken);
				case JTokenType.Raw:
					return Parse<string>(jToken);
				case JTokenType.Bytes:
					return Parse<string>(jToken);
				case JTokenType.Guid:
					return Parse<string>(jToken);
				case JTokenType.Uri:
					return Parse<string>(jToken);
				case JTokenType.TimeSpan:
					return Parse<TimeSpan>(jToken);
				case JTokenType.Property:
					return Parse((jToken as JProperty).Value);
				case JTokenType.None:
				case JTokenType.Object:
				case JTokenType.Array:
				case JTokenType.Constructor:
				case JTokenType.Comment:
				case JTokenType.Null:
				case JTokenType.Undefined:
					throw new InvalidCastException("Object type values can not parsable!");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private static T Parse<T>(JToken jToken)
		{
			return jToken.Value<T>();
		}
		
		private static bool IsPrimitiveType(JToken jToken)
		{
			return IsPrimitiveType(jToken.Type);
		}

		private static bool IsPrimitiveType(JTokenType jTokenType)
		{
			switch (jTokenType)
			{
				case JTokenType.Integer:
				case JTokenType.Float:
				case JTokenType.String:
				case JTokenType.Boolean:
				case JTokenType.Date:
				case JTokenType.Raw:
				case JTokenType.Bytes:
				case JTokenType.Guid:
				case JTokenType.Uri:
				case JTokenType.TimeSpan:
					return true;
				case JTokenType.None:
				case JTokenType.Object:
				case JTokenType.Array:
				case JTokenType.Constructor:
				case JTokenType.Property:
				case JTokenType.Comment:
				case JTokenType.Null:
				case JTokenType.Undefined:
					return false;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}