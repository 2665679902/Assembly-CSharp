using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020008CD RID: 2253
[AddComponentMenu("KMonoBehaviour/scripts/Radiator")]
public class Radiator : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x060040BF RID: 16575 RVA: 0x0016A71C File Offset: 0x0016891C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.emitter = new RadiationGridEmitter(Grid.PosToCell(base.gameObject), this.intensity);
		this.emitter.projectionCount = this.projectionCount;
		this.emitter.direction = this.direction;
		this.emitter.angle = this.angle;
		if (base.GetComponent<Operational>() == null)
		{
			this.emitter.enabled = true;
		}
		else
		{
			base.Subscribe(824508782, new Action<object>(this.OnOperationalChanged));
		}
		RadiationGridManager.emitters.Add(this.emitter);
	}

	// Token: 0x060040C0 RID: 16576 RVA: 0x0016A7C2 File Offset: 0x001689C2
	protected override void OnCleanUp()
	{
		RadiationGridManager.emitters.Remove(this.emitter);
		base.OnCleanUp();
	}

	// Token: 0x060040C1 RID: 16577 RVA: 0x0016A7DC File Offset: 0x001689DC
	private void OnOperationalChanged(object data)
	{
		bool isActive = base.GetComponent<Operational>().IsActive;
		this.emitter.enabled = isActive;
	}

	// Token: 0x060040C2 RID: 16578 RVA: 0x0016A801 File Offset: 0x00168A01
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.EMITS_LIGHT, this.intensity), UI.GAMEOBJECTEFFECTS.TOOLTIPS.EMITS_LIGHT, Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x060040C3 RID: 16579 RVA: 0x0016A839 File Offset: 0x00168A39
	private void Update()
	{
		this.emitter.originCell = Grid.PosToCell(base.gameObject);
	}

	// Token: 0x04002B2A RID: 11050
	public RadiationGridEmitter emitter;

	// Token: 0x04002B2B RID: 11051
	public int intensity;

	// Token: 0x04002B2C RID: 11052
	public int projectionCount;

	// Token: 0x04002B2D RID: 11053
	public int direction;

	// Token: 0x04002B2E RID: 11054
	public int angle = 360;
}
