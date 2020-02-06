using System;
using System.Linq;

namespace Dynamo.Extensions
{
	public static class CodeExtensions
	{
		public static string IncreaseIndent(this string code, int tabCount)
		{
			if (string.IsNullOrEmpty(code))
				return code;

			string tabSpace = "";
			for (int i = 0; i < tabCount; i++)
				tabSpace += "\t";

			var rows = code.Split(Environment.NewLine);
			return string.Join(Environment.NewLine, rows.Select(x => tabSpace + x));
		}
		
		public static string IncreaseIndent(this string code)
		{
			if (string.IsNullOrEmpty(code))
				return code;
			
			return code.IncreaseIndent(1);
		}
	}
}