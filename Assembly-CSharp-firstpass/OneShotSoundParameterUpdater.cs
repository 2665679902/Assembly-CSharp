using System;
using FMOD.Studio;

// Token: 0x02000020 RID: 32
public abstract class OneShotSoundParameterUpdater
{
	// Token: 0x1700005D RID: 93
	// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000A890 File Offset: 0x00008A90
	// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000A898 File Offset: 0x00008A98
	public HashedString parameter { get; private set; }

	// Token: 0x060001D8 RID: 472 RVA: 0x0000A8A1 File Offset: 0x00008AA1
	public OneShotSoundParameterUpdater(HashedString parameter)
	{
		this.parameter = parameter;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000A8B0 File Offset: 0x00008AB0
	public virtual void Update(float dt)
	{
	}

	// Token: 0x060001DA RID: 474
	public abstract void Play(OneShotSoundParameterUpdater.Sound sound);

	// Token: 0x02000979 RID: 2425
	public struct Sound
	{
		// Token: 0x040020D8 RID: 8408
		public EventInstance ev;

		// Token: 0x040020D9 RID: 8409
		public SoundDescription description;

		// Token: 0x040020DA RID: 8410
		public HashedString path;
	}
}
