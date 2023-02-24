﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Klei
{
	// Token: 0x02000512 RID: 1298
	public class CSVReader
	{
		// Token: 0x06003751 RID: 14161 RVA: 0x0007CE24 File Offset: 0x0007B024
		public static void DebugOutputGrid(string[,] grid)
		{
			string text = "";
			for (int i = 0; i < grid.GetUpperBound(1); i++)
			{
				for (int j = 0; j < grid.GetUpperBound(0); j++)
				{
					text += grid[j, i];
					text += "|";
				}
				text += "\n";
			}
			global::Debug.Log(text);
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x0007CE88 File Offset: 0x0007B088
		public static string[,] SplitCsvGrid(string csvText, string csv_name)
		{
			string[] array = csvText.Split(new char[] { '\n', '\r' });
			List<string> list = new List<string>();
			foreach (string text in array)
			{
				if (text.Length != 0 && !text.StartsWith("#"))
				{
					list.Add(text);
				}
			}
			List<string> list2 = new List<string>();
			for (int j = 0; j < list.Count; j++)
			{
				string text2 = list[j];
				int num = 0;
				for (int k = 0; k < text2.Length; k++)
				{
					if (text2[k] == '"')
					{
						num++;
					}
				}
				if (num % 2 == 1)
				{
					text2 = list[j] + "\n" + list[j + 1];
					list[j + 1] = text2;
				}
				else
				{
					list2.Add(text2);
				}
			}
			list2.RemoveAll((string x) => x.StartsWith("#"));
			string[][] array3 = new string[list2.Count][];
			for (int l = 0; l < list2.Count; l++)
			{
				array3[l] = CSVReader.SplitCsvLine(list2[l]);
			}
			int num2 = 0;
			for (int m = 0; m < array3.Length; m++)
			{
				num2 = Mathf.Max(num2, array3[m].Length);
			}
			string[,] array4 = new string[num2 + 1, array3.Length + 1];
			for (int n = 0; n < array3.Length; n++)
			{
				string[] array5 = array3[n];
				for (int num3 = 0; num3 < array5.Length; num3++)
				{
					array4[num3, n] = array5[num3];
					array4[num3, n] = array4[num3, n].Replace("\"\"", "\"");
				}
			}
			return array4;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x0007D068 File Offset: 0x0007B268
		public static string[] SplitCsvLine(string line)
		{
			line = line.Replace("\n\n", "\n");
			return (from Match m in CSVReader.regex.Matches(line)
				select m.Groups[1].Value).ToArray<string>();
		}

		// Token: 0x04001402 RID: 5122
		private static Regex regex = new Regex("(((?<x>(?=[,\\r\\n]+))|\"(?<x>([^\"]|\"\")+)\"|(?<x>[^,\\r\\n]+)),?)", RegexOptions.ExplicitCapture);

		// Token: 0x02000B20 RID: 2848
		private struct ParseWorkItem : IWorkItem<object>
		{
			// Token: 0x0600585F RID: 22623 RVA: 0x000A46E9 File Offset: 0x000A28E9
			public void Run(object shared_data)
			{
				this.row = CSVReader.SplitCsvLine(this.line);
			}

			// Token: 0x04002639 RID: 9785
			public string line;

			// Token: 0x0400263A RID: 9786
			public string[] row;
		}
	}
}
