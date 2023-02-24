using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000952 RID: 2386
internal class UpdateRocketSpeedParameter : LoopingSoundParameterUpdater
{
	// Token: 0x06004674 RID: 18036 RVA: 0x0018CBAC File Offset: 0x0018ADAC
	public UpdateRocketSpeedParameter()
		: base("rocketSpeed")
	{
	}

	// Token: 0x06004675 RID: 18037 RVA: 0x0018CBCC File Offset: 0x0018ADCC
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdateRocketSpeedParameter.Entry entry = new UpdateRocketSpeedParameter.Entry
		{
			rocketModule = sound.transform.GetComponent<RocketModule>(),
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x06004676 RID: 18038 RVA: 0x0018CC28 File Offset: 0x0018AE28
	public override void Update(float dt)
	{
		foreach (UpdateRocketSpeedParameter.Entry entry in this.entries)
		{
			if (!(entry.rocketModule == null))
			{
				LaunchConditionManager conditionManager = entry.rocketModule.conditionManager;
				if (!(conditionManager == null))
				{
					ILaunchableRocket component = conditionManager.GetComponent<ILaunchableRocket>();
					if (component != null)
					{
						EventInstance ev = entry.ev;
						ev.setParameterByID(entry.parameterId, component.rocketSpeed, false);
					}
				}
			}
		}
	}

	// Token: 0x06004677 RID: 18039 RVA: 0x0018CCC0 File Offset: 0x0018AEC0
	public override void Remove(LoopingSoundParameterUpdater.Sound sound)
	{
		for (int i = 0; i < this.entries.Count; i++)
		{
			if (this.entries[i].ev.handle == sound.ev.handle)
			{
				this.entries.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x04002E9E RID: 11934
	private List<UpdateRocketSpeedParameter.Entry> entries = new List<UpdateRocketSpeedParameter.Entry>();

	// Token: 0x02001748 RID: 5960
	private struct Entry
	{
		// Token: 0x04006CA0 RID: 27808
		public RocketModule rocketModule;

		// Token: 0x04006CA1 RID: 27809
		public EventInstance ev;

		// Token: 0x04006CA2 RID: 27810
		public PARAMETER_ID parameterId;
	}
}
