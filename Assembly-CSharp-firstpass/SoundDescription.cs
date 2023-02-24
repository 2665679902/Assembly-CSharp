using System;
using FMOD.Studio;

// Token: 0x0200001E RID: 30
public struct SoundDescription
{
	// Token: 0x060001C3 RID: 451 RVA: 0x0000A230 File Offset: 0x00008430
	public PARAMETER_ID GetParameterId(HashedString name)
	{
		foreach (SoundDescription.Parameter parameter in this.parameters)
		{
			if (parameter.name == name)
			{
				return parameter.id;
			}
		}
		return SoundDescription.Parameter.INVALID_ID;
	}

	// Token: 0x040000B3 RID: 179
	public string path;

	// Token: 0x040000B4 RID: 180
	public float falloffDistanceSq;

	// Token: 0x040000B5 RID: 181
	public SoundDescription.Parameter[] parameters;

	// Token: 0x040000B6 RID: 182
	public OneShotSoundParameterUpdater[] oneShotParameterUpdaters;

	// Token: 0x02000975 RID: 2421
	public struct Parameter
	{
		// Token: 0x040020C9 RID: 8393
		public HashedString name;

		// Token: 0x040020CA RID: 8394
		public PARAMETER_ID id;

		// Token: 0x040020CB RID: 8395
		public static readonly PARAMETER_ID INVALID_ID;
	}
}
