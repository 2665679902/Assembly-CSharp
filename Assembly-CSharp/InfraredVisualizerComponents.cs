using System;
using UnityEngine;

// Token: 0x0200048A RID: 1162
public class InfraredVisualizerComponents : KGameObjectComponentManager<InfraredVisualizerData>
{
	// Token: 0x060019F2 RID: 6642 RVA: 0x0008AD9C File Offset: 0x00088F9C
	public HandleVector<int>.Handle Add(GameObject go)
	{
		return base.Add(go, new InfraredVisualizerData(go));
	}

	// Token: 0x060019F3 RID: 6643 RVA: 0x0008ADAC File Offset: 0x00088FAC
	public void UpdateTemperature()
	{
		GridArea visibleArea = GridVisibleArea.GetVisibleArea();
		for (int i = 0; i < this.data.Count; i++)
		{
			KAnimControllerBase controller = this.data[i].controller;
			if (controller != null)
			{
				Vector3 position = controller.transform.GetPosition();
				if (visibleArea.Min <= position && position <= visibleArea.Max)
				{
					this.data[i].Update();
				}
			}
		}
	}

	// Token: 0x060019F4 RID: 6644 RVA: 0x0008AE3C File Offset: 0x0008903C
	public void ClearOverlayColour()
	{
		Color32 color = Color.black;
		for (int i = 0; i < this.data.Count; i++)
		{
			KAnimControllerBase controller = this.data[i].controller;
			if (controller != null)
			{
				controller.OverlayColour = color;
			}
		}
	}

	// Token: 0x060019F5 RID: 6645 RVA: 0x0008AE91 File Offset: 0x00089091
	public static void ClearOverlayColour(KBatchedAnimController controller)
	{
		if (controller != null)
		{
			controller.OverlayColour = Color.black;
		}
	}
}
