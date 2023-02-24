using System;
using UnityEngine;

// Token: 0x0200073C RID: 1852
public class ElementEmitter : SimComponent
{
	// Token: 0x170003AA RID: 938
	// (get) Token: 0x060032D6 RID: 13014 RVA: 0x00112C1D File Offset: 0x00110E1D
	// (set) Token: 0x060032D7 RID: 13015 RVA: 0x00112C25 File Offset: 0x00110E25
	public bool isEmitterBlocked { get; private set; }

	// Token: 0x060032D8 RID: 13016 RVA: 0x00112C30 File Offset: 0x00110E30
	protected override void OnSpawn()
	{
		this.onBlockedHandle = Game.Instance.callbackManager.Add(new Game.CallbackInfo(new System.Action(this.OnEmitterBlocked), true));
		this.onUnblockedHandle = Game.Instance.callbackManager.Add(new Game.CallbackInfo(new System.Action(this.OnEmitterUnblocked), true));
		base.OnSpawn();
	}

	// Token: 0x060032D9 RID: 13017 RVA: 0x00112C91 File Offset: 0x00110E91
	protected override void OnCleanUp()
	{
		Game.Instance.ManualReleaseHandle(this.onBlockedHandle);
		Game.Instance.ManualReleaseHandle(this.onUnblockedHandle);
		base.OnCleanUp();
	}

	// Token: 0x060032DA RID: 13018 RVA: 0x00112CB9 File Offset: 0x00110EB9
	public void SetEmitting(bool emitting)
	{
		base.SetSimActive(emitting);
	}

	// Token: 0x060032DB RID: 13019 RVA: 0x00112CC4 File Offset: 0x00110EC4
	protected override void OnSimActivate()
	{
		int num = Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), (int)this.outputElement.outputElementOffset.x, (int)this.outputElement.outputElementOffset.y);
		if (this.outputElement.elementHash != (SimHashes)0 && this.outputElement.massGenerationRate > 0f && this.emissionFrequency > 0f)
		{
			float num2 = ((this.outputElement.minOutputTemperature == 0f) ? base.GetComponent<PrimaryElement>().Temperature : this.outputElement.minOutputTemperature);
			SimMessages.ModifyElementEmitter(this.simHandle, num, (int)this.emitRange, this.outputElement.elementHash, this.emissionFrequency, this.outputElement.massGenerationRate, num2, this.maxPressure, this.outputElement.addedDiseaseIdx, this.outputElement.addedDiseaseCount);
		}
		if (this.showDescriptor)
		{
			this.statusHandle = base.GetComponent<KSelectable>().ReplaceStatusItem(this.statusHandle, Db.Get().BuildingStatusItems.ElementEmitterOutput, this);
		}
	}

	// Token: 0x060032DC RID: 13020 RVA: 0x00112DE0 File Offset: 0x00110FE0
	protected override void OnSimDeactivate()
	{
		int num = Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), (int)this.outputElement.outputElementOffset.x, (int)this.outputElement.outputElementOffset.y);
		SimMessages.ModifyElementEmitter(this.simHandle, num, (int)this.emitRange, SimHashes.Vacuum, 0f, 0f, 0f, 0f, byte.MaxValue, 0);
		if (this.showDescriptor)
		{
			this.statusHandle = base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, false);
		}
	}

	// Token: 0x060032DD RID: 13021 RVA: 0x00112E78 File Offset: 0x00111078
	public void ForceEmit(float mass, byte disease_idx, int disease_count, float temperature = -1f)
	{
		if (mass <= 0f)
		{
			return;
		}
		float num = ((temperature > 0f) ? temperature : this.outputElement.minOutputTemperature);
		Element element = ElementLoader.FindElementByHash(this.outputElement.elementHash);
		if (element.IsGas || element.IsLiquid)
		{
			SimMessages.AddRemoveSubstance(Grid.PosToCell(base.transform.GetPosition()), this.outputElement.elementHash, CellEventLogger.Instance.ElementConsumerSimUpdate, mass, num, disease_idx, disease_count, true, -1);
		}
		else if (element.IsSolid)
		{
			element.substance.SpawnResource(base.transform.GetPosition() + new Vector3(0f, 0.5f, 0f), mass, num, disease_idx, disease_count, false, true, false);
		}
		PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, ElementLoader.FindElementByHash(this.outputElement.elementHash).name, base.gameObject.transform, 1.5f, false);
	}

	// Token: 0x060032DE RID: 13022 RVA: 0x00112F74 File Offset: 0x00111174
	private void OnEmitterBlocked()
	{
		this.isEmitterBlocked = true;
		base.Trigger(1615168894, this);
	}

	// Token: 0x060032DF RID: 13023 RVA: 0x00112F89 File Offset: 0x00111189
	private void OnEmitterUnblocked()
	{
		this.isEmitterBlocked = false;
		base.Trigger(-657992955, this);
	}

	// Token: 0x060032E0 RID: 13024 RVA: 0x00112F9E File Offset: 0x0011119E
	protected override void OnSimRegister(HandleVector<Game.ComplexCallbackInfo<int>>.Handle cb_handle)
	{
		Game.Instance.simComponentCallbackManager.GetItem(cb_handle);
		SimMessages.AddElementEmitter(this.maxPressure, cb_handle.index, this.onBlockedHandle.index, this.onUnblockedHandle.index);
	}

	// Token: 0x060032E1 RID: 13025 RVA: 0x00112FD9 File Offset: 0x001111D9
	protected override void OnSimUnregister()
	{
		ElementEmitter.StaticUnregister(this.simHandle);
	}

	// Token: 0x060032E2 RID: 13026 RVA: 0x00112FE6 File Offset: 0x001111E6
	private static void StaticUnregister(int sim_handle)
	{
		global::Debug.Assert(Sim.IsValidHandle(sim_handle));
		SimMessages.RemoveElementEmitter(-1, sim_handle);
	}

	// Token: 0x060032E3 RID: 13027 RVA: 0x00112FFC File Offset: 0x001111FC
	private void OnDrawGizmosSelected()
	{
		int num = Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), (int)this.outputElement.outputElementOffset.x, (int)this.outputElement.outputElementOffset.y);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(Grid.CellToPos(num) + Vector3.right / 2f + Vector3.up / 2f, 0.2f);
	}

	// Token: 0x060032E4 RID: 13028 RVA: 0x00113081 File Offset: 0x00111281
	protected override Action<int> GetStaticUnregister()
	{
		return new Action<int>(ElementEmitter.StaticUnregister);
	}

	// Token: 0x04001F4A RID: 8010
	[SerializeField]
	public ElementConverter.OutputElement outputElement;

	// Token: 0x04001F4B RID: 8011
	[SerializeField]
	public float emissionFrequency = 1f;

	// Token: 0x04001F4C RID: 8012
	[SerializeField]
	public byte emitRange = 1;

	// Token: 0x04001F4D RID: 8013
	[SerializeField]
	public float maxPressure = 1f;

	// Token: 0x04001F4E RID: 8014
	private Guid statusHandle = Guid.Empty;

	// Token: 0x04001F4F RID: 8015
	public bool showDescriptor = true;

	// Token: 0x04001F50 RID: 8016
	private HandleVector<Game.CallbackInfo>.Handle onBlockedHandle = HandleVector<Game.CallbackInfo>.InvalidHandle;

	// Token: 0x04001F51 RID: 8017
	private HandleVector<Game.CallbackInfo>.Handle onUnblockedHandle = HandleVector<Game.CallbackInfo>.InvalidHandle;
}
