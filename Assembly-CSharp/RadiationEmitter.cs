using System;
using UnityEngine;

// Token: 0x020008B0 RID: 2224
public class RadiationEmitter : SimComponent
{
	// Token: 0x06003FEE RID: 16366 RVA: 0x00165405 File Offset: 0x00163605
	protected override void OnSpawn()
	{
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange), "RadiationEmitter.OnSpawn");
		base.OnSpawn();
	}

	// Token: 0x06003FEF RID: 16367 RVA: 0x0016542F File Offset: 0x0016362F
	protected override void OnCleanUp()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange));
		base.OnCleanUp();
	}

	// Token: 0x06003FF0 RID: 16368 RVA: 0x00165453 File Offset: 0x00163653
	public void SetEmitting(bool emitting)
	{
		base.SetSimActive(emitting);
	}

	// Token: 0x06003FF1 RID: 16369 RVA: 0x0016545C File Offset: 0x0016365C
	public int GetEmissionCell()
	{
		return Grid.PosToCell(base.transform.GetPosition() + this.emissionOffset);
	}

	// Token: 0x06003FF2 RID: 16370 RVA: 0x0016547C File Offset: 0x0016367C
	public void Refresh()
	{
		int emissionCell = this.GetEmissionCell();
		if (this.radiusProportionalToRads)
		{
			this.SetRadiusProportionalToRads();
		}
		SimMessages.ModifyRadiationEmitter(this.simHandle, emissionCell, this.emitRadiusX, this.emitRadiusY, this.emitRads, this.emitRate, this.emitSpeed, this.emitDirection, this.emitAngle, this.emitType);
	}

	// Token: 0x06003FF3 RID: 16371 RVA: 0x001654DA File Offset: 0x001636DA
	private void OnCellChange()
	{
		this.Refresh();
	}

	// Token: 0x06003FF4 RID: 16372 RVA: 0x001654E4 File Offset: 0x001636E4
	private void SetRadiusProportionalToRads()
	{
		this.emitRadiusX = (short)Mathf.Clamp(Mathf.RoundToInt(this.emitRads * 1f), 1, 128);
		this.emitRadiusY = (short)Mathf.Clamp(Mathf.RoundToInt(this.emitRads * 1f), 1, 128);
	}

	// Token: 0x06003FF5 RID: 16373 RVA: 0x00165538 File Offset: 0x00163738
	protected override void OnSimActivate()
	{
		int emissionCell = this.GetEmissionCell();
		if (this.radiusProportionalToRads)
		{
			this.SetRadiusProportionalToRads();
		}
		SimMessages.ModifyRadiationEmitter(this.simHandle, emissionCell, this.emitRadiusX, this.emitRadiusY, this.emitRads, this.emitRate, this.emitSpeed, this.emitDirection, this.emitAngle, this.emitType);
	}

	// Token: 0x06003FF6 RID: 16374 RVA: 0x00165598 File Offset: 0x00163798
	protected override void OnSimDeactivate()
	{
		int emissionCell = this.GetEmissionCell();
		SimMessages.ModifyRadiationEmitter(this.simHandle, emissionCell, 0, 0, 0f, 0f, 0f, 0f, 0f, this.emitType);
	}

	// Token: 0x06003FF7 RID: 16375 RVA: 0x001655DC File Offset: 0x001637DC
	protected override void OnSimRegister(HandleVector<Game.ComplexCallbackInfo<int>>.Handle cb_handle)
	{
		Game.Instance.simComponentCallbackManager.GetItem(cb_handle);
		int emissionCell = this.GetEmissionCell();
		SimMessages.AddRadiationEmitter(cb_handle.index, emissionCell, 0, 0, 0f, 0f, 0f, 0f, 0f, this.emitType);
	}

	// Token: 0x06003FF8 RID: 16376 RVA: 0x0016562F File Offset: 0x0016382F
	protected override void OnSimUnregister()
	{
		RadiationEmitter.StaticUnregister(this.simHandle);
	}

	// Token: 0x06003FF9 RID: 16377 RVA: 0x0016563C File Offset: 0x0016383C
	private static void StaticUnregister(int sim_handle)
	{
		global::Debug.Assert(Sim.IsValidHandle(sim_handle));
		SimMessages.RemoveRadiationEmitter(-1, sim_handle);
	}

	// Token: 0x06003FFA RID: 16378 RVA: 0x00165650 File Offset: 0x00163850
	private void OnDrawGizmosSelected()
	{
		int emissionCell = this.GetEmissionCell();
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(Grid.CellToPos(emissionCell) + Vector3.right / 2f + Vector3.up / 2f, 0.2f);
	}

	// Token: 0x06003FFB RID: 16379 RVA: 0x001656A4 File Offset: 0x001638A4
	protected override Action<int> GetStaticUnregister()
	{
		return new Action<int>(RadiationEmitter.StaticUnregister);
	}

	// Token: 0x040029E9 RID: 10729
	public bool radiusProportionalToRads;

	// Token: 0x040029EA RID: 10730
	[SerializeField]
	public short emitRadiusX = 4;

	// Token: 0x040029EB RID: 10731
	[SerializeField]
	public short emitRadiusY = 4;

	// Token: 0x040029EC RID: 10732
	[SerializeField]
	public float emitRads = 10f;

	// Token: 0x040029ED RID: 10733
	[SerializeField]
	public float emitRate = 1f;

	// Token: 0x040029EE RID: 10734
	[SerializeField]
	public float emitSpeed = 1f;

	// Token: 0x040029EF RID: 10735
	[SerializeField]
	public float emitDirection;

	// Token: 0x040029F0 RID: 10736
	[SerializeField]
	public float emitAngle = 360f;

	// Token: 0x040029F1 RID: 10737
	[SerializeField]
	public RadiationEmitter.RadiationEmitterType emitType;

	// Token: 0x040029F2 RID: 10738
	[SerializeField]
	public Vector3 emissionOffset = Vector3.zero;

	// Token: 0x02001683 RID: 5763
	public enum RadiationEmitterType
	{
		// Token: 0x04006A09 RID: 27145
		Constant,
		// Token: 0x04006A0A RID: 27146
		Pulsing,
		// Token: 0x04006A0B RID: 27147
		PulsingAveraged,
		// Token: 0x04006A0C RID: 27148
		SimplePulse,
		// Token: 0x04006A0D RID: 27149
		RadialBeams,
		// Token: 0x04006A0E RID: 27150
		Attractor
	}
}
