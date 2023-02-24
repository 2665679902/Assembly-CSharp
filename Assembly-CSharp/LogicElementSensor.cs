using System;
using KSerialization;

// Token: 0x020005E7 RID: 1511
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicElementSensor : Switch, ISaveLoadable, ISim200ms
{
	// Token: 0x06002633 RID: 9779 RVA: 0x000CDC8A File Offset: 0x000CBE8A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.GetComponent<Filterable>().onFilterChanged += this.OnElementSelected;
	}

	// Token: 0x06002634 RID: 9780 RVA: 0x000CDCAC File Offset: 0x000CBEAC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
		base.Subscribe<LogicElementSensor>(-592767678, LogicElementSensor.OnOperationalChangedDelegate);
	}

	// Token: 0x06002635 RID: 9781 RVA: 0x000CDCFC File Offset: 0x000CBEFC
	public void Sim200ms(float dt)
	{
		int num = Grid.PosToCell(this);
		if (this.sampleIdx < 8)
		{
			this.samples[this.sampleIdx] = Grid.ElementIdx[num] == this.desiredElementIdx;
			this.sampleIdx++;
			return;
		}
		this.sampleIdx = 0;
		bool flag = true;
		bool[] array = this.samples;
		for (int i = 0; i < array.Length; i++)
		{
			flag = array[i] && flag;
		}
		if (base.IsSwitchedOn != flag)
		{
			this.Toggle();
		}
	}

	// Token: 0x06002636 RID: 9782 RVA: 0x000CDD7B File Offset: 0x000CBF7B
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x06002637 RID: 9783 RVA: 0x000CDD8C File Offset: 0x000CBF8C
	private void UpdateLogicCircuit()
	{
		bool flag = this.switchedOn && base.GetComponent<Operational>().IsOperational;
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, flag ? 1 : 0);
	}

	// Token: 0x06002638 RID: 9784 RVA: 0x000CDDC8 File Offset: 0x000CBFC8
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x06002639 RID: 9785 RVA: 0x000CDE50 File Offset: 0x000CC050
	private void OnElementSelected(Tag element_tag)
	{
		if (!element_tag.IsValid)
		{
			return;
		}
		Element element = ElementLoader.GetElement(element_tag);
		bool flag = true;
		if (element != null)
		{
			this.desiredElementIdx = ElementLoader.GetElementIndex(element.id);
			flag = element.id == SimHashes.Void || element.id == SimHashes.Vacuum;
		}
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NoFilterElementSelected, flag, null);
	}

	// Token: 0x0600263A RID: 9786 RVA: 0x000CDEBF File Offset: 0x000CC0BF
	private void OnOperationalChanged(object data)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x0600263B RID: 9787 RVA: 0x000CDED0 File Offset: 0x000CC0D0
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x0400164E RID: 5710
	private bool wasOn;

	// Token: 0x0400164F RID: 5711
	public Element.State desiredState = Element.State.Gas;

	// Token: 0x04001650 RID: 5712
	private const int WINDOW_SIZE = 8;

	// Token: 0x04001651 RID: 5713
	private bool[] samples = new bool[8];

	// Token: 0x04001652 RID: 5714
	private int sampleIdx;

	// Token: 0x04001653 RID: 5715
	private ushort desiredElementIdx = ushort.MaxValue;

	// Token: 0x04001654 RID: 5716
	private static readonly EventSystem.IntraObjectHandler<LogicElementSensor> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<LogicElementSensor>(delegate(LogicElementSensor component, object data)
	{
		component.OnOperationalChanged(data);
	});
}
