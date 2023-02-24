using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Satsuma
{
	// Token: 0x02000285 RID: 645
	internal static class Utils
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x0004C6BC File Offset: 0x0004A8BC
		public static double LargestPowerOfTwo(double d)
		{
			long num = BitConverter.DoubleToInt64Bits(d);
			num &= 9218868437227405312L;
			if (num == 9218868437227405312L)
			{
				num = 9214364837600034816L;
			}
			return BitConverter.Int64BitsToDouble(num);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0004C6F8 File Offset: 0x0004A8F8
		public static V MakeEntry<K, V>(Dictionary<K, V> dict, K key) where V : new()
		{
			V v;
			if (dict.TryGetValue(key, out v))
			{
				return v;
			}
			return dict[key] = new V();
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0004C724 File Offset: 0x0004A924
		public static void RemoveAll<T>(HashSet<T> set, Func<T, bool> condition)
		{
			foreach (T t in set.Where(condition).ToList<T>())
			{
				set.Remove(t);
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004C780 File Offset: 0x0004A980
		public static void RemoveAll<K, V>(Dictionary<K, V> dict, Func<K, bool> condition)
		{
			foreach (K k in dict.Keys.Where(condition).ToList<K>())
			{
				dict.Remove(k);
			}
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0004C7E0 File Offset: 0x0004A9E0
		public static void RemoveLast<T>(List<T> list, T element) where T : IEquatable<T>
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (element.Equals(list[i]))
				{
					list.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0004C820 File Offset: 0x0004AA20
		public static IEnumerable<XElement> ElementsLocal(XElement xParent, string localName)
		{
			return from x in xParent.Elements()
				where x.Name.LocalName == localName
				select x;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004C851 File Offset: 0x0004AA51
		public static XElement ElementLocal(XElement xParent, string localName)
		{
			return Utils.ElementsLocal(xParent, localName).FirstOrDefault<XElement>();
		}
	}
}
