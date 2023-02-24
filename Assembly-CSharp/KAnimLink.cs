using System;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public class KAnimLink
{
	// Token: 0x0600162C RID: 5676 RVA: 0x000724CF File Offset: 0x000706CF
	public KAnimLink(KAnimControllerBase master, KAnimControllerBase slave)
	{
		this.slave = slave;
		this.master = master;
		this.Register();
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x000724F4 File Offset: 0x000706F4
	private void Register()
	{
		this.master.OnOverlayColourChanged += this.OnOverlayColourChanged;
		KAnimControllerBase kanimControllerBase = this.master;
		kanimControllerBase.OnTintChanged = (Action<Color>)Delegate.Combine(kanimControllerBase.OnTintChanged, new Action<Color>(this.OnTintColourChanged));
		KAnimControllerBase kanimControllerBase2 = this.master;
		kanimControllerBase2.OnHighlightChanged = (Action<Color>)Delegate.Combine(kanimControllerBase2.OnHighlightChanged, new Action<Color>(this.OnHighlightColourChanged));
		this.master.onLayerChanged += this.slave.SetLayer;
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x00072584 File Offset: 0x00070784
	public void Unregister()
	{
		if (this.master != null)
		{
			this.master.OnOverlayColourChanged -= this.OnOverlayColourChanged;
			KAnimControllerBase kanimControllerBase = this.master;
			kanimControllerBase.OnTintChanged = (Action<Color>)Delegate.Remove(kanimControllerBase.OnTintChanged, new Action<Color>(this.OnTintColourChanged));
			KAnimControllerBase kanimControllerBase2 = this.master;
			kanimControllerBase2.OnHighlightChanged = (Action<Color>)Delegate.Remove(kanimControllerBase2.OnHighlightChanged, new Action<Color>(this.OnHighlightColourChanged));
			if (this.slave != null)
			{
				this.master.onLayerChanged -= this.slave.SetLayer;
			}
		}
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x00072632 File Offset: 0x00070832
	private void OnOverlayColourChanged(Color32 c)
	{
		if (this.slave != null)
		{
			this.slave.OverlayColour = c;
		}
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x00072653 File Offset: 0x00070853
	private void OnTintColourChanged(Color c)
	{
		if (this.syncTint && this.slave != null)
		{
			this.slave.TintColour = c;
		}
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x0007267C File Offset: 0x0007087C
	private void OnHighlightColourChanged(Color c)
	{
		if (this.slave != null)
		{
			this.slave.HighlightColour = c;
		}
	}

	// Token: 0x04000C54 RID: 3156
	public bool syncTint = true;

	// Token: 0x04000C55 RID: 3157
	private KAnimControllerBase master;

	// Token: 0x04000C56 RID: 3158
	private KAnimControllerBase slave;
}
