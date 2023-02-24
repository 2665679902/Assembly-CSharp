using System;
using UnityEngine;

// Token: 0x02000703 RID: 1795
[AddComponentMenu("KMonoBehaviour/scripts/DeserializeWarnings")]
public class DeserializeWarnings : KMonoBehaviour
{
	// Token: 0x0600315F RID: 12639 RVA: 0x001074CB File Offset: 0x001056CB
	public static void DestroyInstance()
	{
		DeserializeWarnings.Instance = null;
	}

	// Token: 0x06003160 RID: 12640 RVA: 0x001074D3 File Offset: 0x001056D3
	protected override void OnPrefabInit()
	{
		DeserializeWarnings.Instance = this;
	}

	// Token: 0x04001E28 RID: 7720
	public DeserializeWarnings.Warning BuildingTemeperatureIsZeroKelvin;

	// Token: 0x04001E29 RID: 7721
	public DeserializeWarnings.Warning PipeContentsTemperatureIsNan;

	// Token: 0x04001E2A RID: 7722
	public DeserializeWarnings.Warning PrimaryElementTemperatureIsNan;

	// Token: 0x04001E2B RID: 7723
	public DeserializeWarnings.Warning PrimaryElementHasNoElement;

	// Token: 0x04001E2C RID: 7724
	public static DeserializeWarnings Instance;

	// Token: 0x0200141E RID: 5150
	public struct Warning
	{
		// Token: 0x06008021 RID: 32801 RVA: 0x002DE795 File Offset: 0x002DC995
		public void Warn(string message, GameObject obj = null)
		{
			if (!this.isSet)
			{
				global::Debug.LogWarning(message, obj);
				this.isSet = true;
			}
		}

		// Token: 0x04006296 RID: 25238
		private bool isSet;
	}
}
