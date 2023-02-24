using System;
using System.Collections.Generic;

// Token: 0x020000D7 RID: 215
public class MarkovNameGenerator
{
	// Token: 0x060007FC RID: 2044 RVA: 0x00020190 File Offset: 0x0001E390
	public MarkovNameGenerator(IEnumerable<string> sampleNames, int order, int minLength)
	{
		if (order < 1)
		{
			order = 1;
		}
		if (minLength < 1)
		{
			minLength = 1;
		}
		this._order = order;
		this._minLength = minLength;
		foreach (string text in sampleNames)
		{
			string[] array = text.Split(new char[] { ',' });
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i].Trim().ToUpper();
				if (text2.Length >= order + 1)
				{
					this._samples.Add(text2);
				}
			}
		}
		foreach (string text3 in this._samples)
		{
			for (int j = 0; j < text3.Length - order; j++)
			{
				string text4 = text3.Substring(j, order);
				List<char> list;
				if (this._chains.ContainsKey(text4))
				{
					list = this._chains[text4];
				}
				else
				{
					list = new List<char>();
					this._chains[text4] = list;
				}
				list.Add(text3[j + order]);
			}
		}
	}

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x060007FD RID: 2045 RVA: 0x00020314 File Offset: 0x0001E514
	public string NextName
	{
		get
		{
			string text;
			do
			{
				int num = this._rnd.Next(this._samples.Count);
				int length = this._samples[num].Length;
				text = this._samples[num].Substring(this._rnd.Next(0, this._samples[num].Length - this._order), this._order);
				while (text.Length < length)
				{
					string text2 = text.Substring(text.Length - this._order, this._order);
					if (this.GetLetter(text2) == '?')
					{
						break;
					}
					text += this.GetLetter(text2).ToString();
				}
				if (text.Contains(" "))
				{
					string[] array = text.Split(new char[] { ' ' });
					text = "";
					for (int i = 0; i < array.Length; i++)
					{
						if (!(array[i] == ""))
						{
							if (array[i].Length == 1)
							{
								array[i] = array[i].ToUpper();
							}
							else
							{
								array[i] = array[i].Substring(0, 1) + array[i].Substring(1).ToLower();
							}
							if (text != "")
							{
								text += " ";
							}
							text += array[i];
						}
					}
				}
				else
				{
					text = text.Substring(0, 1) + text.Substring(1).ToLower();
				}
			}
			while (this._used.Contains(text) || text.Length < this._minLength);
			this._used.Add(text);
			return text;
		}
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x000204DA File Offset: 0x0001E6DA
	public void Reset()
	{
		this._used.Clear();
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x000204E8 File Offset: 0x0001E6E8
	private char GetLetter(string token)
	{
		if (!this._chains.ContainsKey(token))
		{
			return '?';
		}
		List<char> list = this._chains[token];
		int num = this._rnd.Next(list.Count);
		return list[num];
	}

	// Token: 0x04000619 RID: 1561
	private Dictionary<string, List<char>> _chains = new Dictionary<string, List<char>>();

	// Token: 0x0400061A RID: 1562
	private List<string> _samples = new List<string>();

	// Token: 0x0400061B RID: 1563
	private List<string> _used = new List<string>();

	// Token: 0x0400061C RID: 1564
	private Random _rnd = new Random();

	// Token: 0x0400061D RID: 1565
	private int _order;

	// Token: 0x0400061E RID: 1566
	private int _minLength;
}
