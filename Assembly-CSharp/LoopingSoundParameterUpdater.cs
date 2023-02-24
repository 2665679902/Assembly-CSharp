using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000498 RID: 1176
public abstract class LoopingSoundParameterUpdater
{
	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0008CE40 File Offset: 0x0008B040
	// (set) Token: 0x06001A69 RID: 6761 RVA: 0x0008CE48 File Offset: 0x0008B048
	public HashedString parameter { get; private set; }

	// Token: 0x06001A6A RID: 6762 RVA: 0x0008CE51 File Offset: 0x0008B051
	public LoopingSoundParameterUpdater(HashedString parameter)
	{
		this.parameter = parameter;
	}

	// Token: 0x06001A6B RID: 6763
	public abstract void Add(LoopingSoundParameterUpdater.Sound sound);

	// Token: 0x06001A6C RID: 6764
	public abstract void Update(float dt);

	// Token: 0x06001A6D RID: 6765
	public abstract void Remove(LoopingSoundParameterUpdater.Sound sound);

	// Token: 0x020010DA RID: 4314
	public struct Sound
	{
		// Token: 0x040058E3 RID: 22755
		public EventInstance ev;

		// Token: 0x040058E4 RID: 22756
		public HashedString path;

		// Token: 0x040058E5 RID: 22757
		public Transform transform;

		// Token: 0x040058E6 RID: 22758
		public SoundDescription description;

		// Token: 0x040058E7 RID: 22759
		public bool objectIsSelectedAndVisible;
	}
}
