using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x020008A1 RID: 2209
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/PreventFOWRevealTracker")]
public class PreventFOWRevealTracker : KMonoBehaviour
{
	// Token: 0x06003F64 RID: 16228 RVA: 0x00161EE8 File Offset: 0x001600E8
	[OnSerializing]
	private void OnSerialize()
	{
		this.preventFOWRevealCells.Clear();
		for (int i = 0; i < Grid.VisMasks.Length; i++)
		{
			if (Grid.PreventFogOfWarReveal[i])
			{
				this.preventFOWRevealCells.Add(i);
			}
		}
	}

	// Token: 0x06003F65 RID: 16229 RVA: 0x00161F2C File Offset: 0x0016012C
	[OnDeserialized]
	private void OnDeserialized()
	{
		foreach (int num in this.preventFOWRevealCells)
		{
			Grid.PreventFogOfWarReveal[num] = true;
		}
	}

	// Token: 0x0400299F RID: 10655
	[Serialize]
	public List<int> preventFOWRevealCells;
}
