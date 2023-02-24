using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000243 RID: 579
	internal static class SetPropertyUtility
	{
		// Token: 0x06001162 RID: 4450 RVA: 0x00045020 File Offset: 0x00043220
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0004506F File Offset: 0x0004326F
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00045090 File Offset: 0x00043290
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
