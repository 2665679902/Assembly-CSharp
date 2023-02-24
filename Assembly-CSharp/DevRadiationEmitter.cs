using System;
using STRINGS;

// Token: 0x020005A9 RID: 1449
public class DevRadiationEmitter : KMonoBehaviour, ISingleSliderControl, ISliderControl
{
	// Token: 0x060023B7 RID: 9143 RVA: 0x000C129D File Offset: 0x000BF49D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.radiationEmitter != null)
		{
			this.radiationEmitter.SetEmitting(true);
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x060023B8 RID: 9144 RVA: 0x000C12BF File Offset: 0x000BF4BF
	public string SliderTitleKey
	{
		get
		{
			return BUILDINGS.PREFABS.DEVRADIATIONGENERATOR.NAME;
		}
	}

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x060023B9 RID: 9145 RVA: 0x000C12CB File Offset: 0x000BF4CB
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.RADIATION.RADS;
		}
	}

	// Token: 0x060023BA RID: 9146 RVA: 0x000C12D7 File Offset: 0x000BF4D7
	public float GetSliderMax(int index)
	{
		return 5000f;
	}

	// Token: 0x060023BB RID: 9147 RVA: 0x000C12DE File Offset: 0x000BF4DE
	public float GetSliderMin(int index)
	{
		return 0f;
	}

	// Token: 0x060023BC RID: 9148 RVA: 0x000C12E5 File Offset: 0x000BF4E5
	public string GetSliderTooltip()
	{
		return "";
	}

	// Token: 0x060023BD RID: 9149 RVA: 0x000C12EC File Offset: 0x000BF4EC
	public string GetSliderTooltipKey(int index)
	{
		return "";
	}

	// Token: 0x060023BE RID: 9150 RVA: 0x000C12F3 File Offset: 0x000BF4F3
	public float GetSliderValue(int index)
	{
		return this.radiationEmitter.emitRads;
	}

	// Token: 0x060023BF RID: 9151 RVA: 0x000C1300 File Offset: 0x000BF500
	public void SetSliderValue(float value, int index)
	{
		this.radiationEmitter.emitRads = value;
		this.radiationEmitter.Refresh();
	}

	// Token: 0x060023C0 RID: 9152 RVA: 0x000C1319 File Offset: 0x000BF519
	public int SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x0400147B RID: 5243
	[MyCmpReq]
	private RadiationEmitter radiationEmitter;
}
