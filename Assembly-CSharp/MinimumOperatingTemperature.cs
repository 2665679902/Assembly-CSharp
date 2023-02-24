using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020004A1 RID: 1185
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/MinimumOperatingTemperature")]
public class MinimumOperatingTemperature : KMonoBehaviour, ISim200ms, IGameObjectEffectDescriptor
{
	// Token: 0x06001AA0 RID: 6816 RVA: 0x0008E58B File Offset: 0x0008C78B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.TestTemperature(true);
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x0008E59A File Offset: 0x0008C79A
	public void Sim200ms(float dt)
	{
		this.TestTemperature(false);
	}

	// Token: 0x06001AA2 RID: 6818 RVA: 0x0008E5A4 File Offset: 0x0008C7A4
	private void TestTemperature(bool force)
	{
		bool flag;
		if (this.primaryElement.Temperature < this.minimumTemperature)
		{
			flag = false;
		}
		else
		{
			flag = true;
			for (int i = 0; i < this.building.PlacementCells.Length; i++)
			{
				int num = this.building.PlacementCells[i];
				float num2 = Grid.Temperature[num];
				float num3 = Grid.Mass[num];
				if ((num2 != 0f || num3 != 0f) && num2 < this.minimumTemperature)
				{
					flag = false;
					break;
				}
			}
		}
		if (!flag)
		{
			this.lastOffTime = Time.time;
		}
		if ((flag != this.isWarm && !flag) || (flag != this.isWarm && flag && Time.time > this.lastOffTime + 5f) || force)
		{
			this.isWarm = flag;
			this.operational.SetFlag(MinimumOperatingTemperature.warmEnoughFlag, this.isWarm);
			base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.TooCold, !this.isWarm, this);
		}
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x0008E6AE File Offset: 0x0008C8AE
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x0008E6C8 File Offset: 0x0008C8C8
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.MINIMUM_TEMP, GameUtil.GetFormattedTemperature(this.minimumTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.MINIMUM_TEMP, GameUtil.GetFormattedTemperature(this.minimumTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect, false);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x04000EBF RID: 3775
	[MyCmpReq]
	private Building building;

	// Token: 0x04000EC0 RID: 3776
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04000EC1 RID: 3777
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04000EC2 RID: 3778
	public float minimumTemperature = 275.15f;

	// Token: 0x04000EC3 RID: 3779
	private const float TURN_ON_DELAY = 5f;

	// Token: 0x04000EC4 RID: 3780
	private float lastOffTime;

	// Token: 0x04000EC5 RID: 3781
	public static Operational.Flag warmEnoughFlag = new Operational.Flag("warm_enough", Operational.Flag.Type.Functional);

	// Token: 0x04000EC6 RID: 3782
	private bool isWarm;

	// Token: 0x04000EC7 RID: 3783
	private HandleVector<int>.Handle partitionerEntry;
}
