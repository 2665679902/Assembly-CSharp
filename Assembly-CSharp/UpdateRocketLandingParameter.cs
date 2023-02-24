using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000951 RID: 2385
internal class UpdateRocketLandingParameter : LoopingSoundParameterUpdater
{
	// Token: 0x06004670 RID: 18032 RVA: 0x0018CA1F File Offset: 0x0018AC1F
	public UpdateRocketLandingParameter()
		: base("rocketLanding")
	{
	}

	// Token: 0x06004671 RID: 18033 RVA: 0x0018CA3C File Offset: 0x0018AC3C
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdateRocketLandingParameter.Entry entry = new UpdateRocketLandingParameter.Entry
		{
			rocketModule = sound.transform.GetComponent<RocketModule>(),
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x06004672 RID: 18034 RVA: 0x0018CA98 File Offset: 0x0018AC98
	public override void Update(float dt)
	{
		foreach (UpdateRocketLandingParameter.Entry entry in this.entries)
		{
			if (!(entry.rocketModule == null))
			{
				LaunchConditionManager conditionManager = entry.rocketModule.conditionManager;
				if (!(conditionManager == null))
				{
					ILaunchableRocket component = conditionManager.GetComponent<ILaunchableRocket>();
					if (component != null)
					{
						if (component.isLanding)
						{
							EventInstance eventInstance = entry.ev;
							eventInstance.setParameterByID(entry.parameterId, 1f, false);
						}
						else
						{
							EventInstance eventInstance = entry.ev;
							eventInstance.setParameterByID(entry.parameterId, 0f, false);
						}
					}
				}
			}
		}
	}

	// Token: 0x06004673 RID: 18035 RVA: 0x0018CB54 File Offset: 0x0018AD54
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

	// Token: 0x04002E9D RID: 11933
	private List<UpdateRocketLandingParameter.Entry> entries = new List<UpdateRocketLandingParameter.Entry>();

	// Token: 0x02001747 RID: 5959
	private struct Entry
	{
		// Token: 0x04006C9D RID: 27805
		public RocketModule rocketModule;

		// Token: 0x04006C9E RID: 27806
		public EventInstance ev;

		// Token: 0x04006C9F RID: 27807
		public PARAMETER_ID parameterId;
	}
}
