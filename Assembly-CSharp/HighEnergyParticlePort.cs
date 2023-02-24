using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020007AB RID: 1963
public class HighEnergyParticlePort : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06003785 RID: 14213 RVA: 0x00134CEB File Offset: 0x00132EEB
	public int GetHighEnergyParticleInputPortPosition()
	{
		return this.m_building.GetHighEnergyParticleInputCell();
	}

	// Token: 0x06003786 RID: 14214 RVA: 0x00134CF8 File Offset: 0x00132EF8
	public int GetHighEnergyParticleOutputPortPosition()
	{
		return this.m_building.GetHighEnergyParticleOutputCell();
	}

	// Token: 0x06003787 RID: 14215 RVA: 0x00134D05 File Offset: 0x00132F05
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06003788 RID: 14216 RVA: 0x00134D0D File Offset: 0x00132F0D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.HighEnergyParticlePorts.Add(this);
	}

	// Token: 0x06003789 RID: 14217 RVA: 0x00134D20 File Offset: 0x00132F20
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.HighEnergyParticlePorts.Remove(this);
	}

	// Token: 0x0600378A RID: 14218 RVA: 0x00134D34 File Offset: 0x00132F34
	public bool InputActive()
	{
		Operational component = base.GetComponent<Operational>();
		return this.particleInputEnabled && component != null && component.IsFunctional && (!this.requireOperational || component.IsOperational);
	}

	// Token: 0x0600378B RID: 14219 RVA: 0x00134D73 File Offset: 0x00132F73
	public bool AllowCapture(HighEnergyParticle particle)
	{
		return this.onParticleCaptureAllowed == null || this.onParticleCaptureAllowed(particle);
	}

	// Token: 0x0600378C RID: 14220 RVA: 0x00134D8B File Offset: 0x00132F8B
	public void Capture(HighEnergyParticle particle)
	{
		this.currentParticle = particle;
		if (this.onParticleCapture != null)
		{
			this.onParticleCapture(particle);
		}
	}

	// Token: 0x0600378D RID: 14221 RVA: 0x00134DA8 File Offset: 0x00132FA8
	public void Uncapture(HighEnergyParticle particle)
	{
		if (this.onParticleUncapture != null)
		{
			this.onParticleUncapture(particle);
		}
		this.currentParticle = null;
	}

	// Token: 0x0600378E RID: 14222 RVA: 0x00134DC8 File Offset: 0x00132FC8
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.particleInputEnabled)
		{
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.PARTICLE_PORT_INPUT, UI.BUILDINGEFFECTS.TOOLTIPS.PARTICLE_PORT_INPUT, Descriptor.DescriptorType.Requirement, false));
		}
		if (this.particleOutputEnabled)
		{
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.PARTICLE_PORT_OUTPUT, UI.BUILDINGEFFECTS.TOOLTIPS.PARTICLE_PORT_OUTPUT, Descriptor.DescriptorType.Effect, false));
		}
		return list;
	}

	// Token: 0x04002533 RID: 9523
	[MyCmpGet]
	private Building m_building;

	// Token: 0x04002534 RID: 9524
	public HighEnergyParticlePort.OnParticleCapture onParticleCapture;

	// Token: 0x04002535 RID: 9525
	public HighEnergyParticlePort.OnParticleCaptureAllowed onParticleCaptureAllowed;

	// Token: 0x04002536 RID: 9526
	public HighEnergyParticlePort.OnParticleCapture onParticleUncapture;

	// Token: 0x04002537 RID: 9527
	public HighEnergyParticle currentParticle;

	// Token: 0x04002538 RID: 9528
	public bool requireOperational = true;

	// Token: 0x04002539 RID: 9529
	public bool particleInputEnabled;

	// Token: 0x0400253A RID: 9530
	public bool particleOutputEnabled;

	// Token: 0x0400253B RID: 9531
	public CellOffset particleInputOffset;

	// Token: 0x0400253C RID: 9532
	public CellOffset particleOutputOffset;

	// Token: 0x0200150F RID: 5391
	// (Invoke) Token: 0x06008289 RID: 33417
	public delegate void OnParticleCapture(HighEnergyParticle particle);

	// Token: 0x02001510 RID: 5392
	// (Invoke) Token: 0x0600828D RID: 33421
	public delegate bool OnParticleCaptureAllowed(HighEnergyParticle particle);
}
