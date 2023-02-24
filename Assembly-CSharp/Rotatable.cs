using System;
using KSerialization;
using UnityEngine;

// Token: 0x020004C3 RID: 1219
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Rotatable")]
public class Rotatable : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06001C2D RID: 7213 RVA: 0x00095CBA File Offset: 0x00093EBA
	public Orientation Orientation
	{
		get
		{
			return this.orientation;
		}
	}

	// Token: 0x06001C2E RID: 7214 RVA: 0x00095CC4 File Offset: 0x00093EC4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.building != null)
		{
			BuildingDef def = base.GetComponent<Building>().Def;
			this.SetSize(def.WidthInCells, def.HeightInCells);
		}
		this.OrientVisualizer(this.orientation);
		this.OrientCollider(this.orientation);
	}

	// Token: 0x06001C2F RID: 7215 RVA: 0x00095D1C File Offset: 0x00093F1C
	public void SetSize(int width, int height)
	{
		this.width = width;
		this.height = height;
		if (width % 2 == 0)
		{
			this.pivot = new Vector3(-0.5f, 0.5f, 0f);
			this.visualizerOffset = new Vector3(0.5f, 0f, 0f);
			return;
		}
		this.pivot = new Vector3(0f, 0.5f, 0f);
		this.visualizerOffset = Vector3.zero;
	}

	// Token: 0x06001C30 RID: 7216 RVA: 0x00095D9C File Offset: 0x00093F9C
	public Orientation Rotate()
	{
		switch (this.permittedRotations)
		{
		case PermittedRotations.R90:
			this.orientation = ((this.orientation == Orientation.Neutral) ? Orientation.R90 : Orientation.Neutral);
			break;
		case PermittedRotations.R360:
			this.orientation = (this.orientation + 1) % Orientation.NumRotations;
			break;
		case PermittedRotations.FlipH:
			this.orientation = ((this.orientation == Orientation.Neutral) ? Orientation.FlipH : Orientation.Neutral);
			break;
		case PermittedRotations.FlipV:
			this.orientation = ((this.orientation == Orientation.Neutral) ? Orientation.FlipV : Orientation.Neutral);
			break;
		}
		this.OrientVisualizer(this.orientation);
		return this.orientation;
	}

	// Token: 0x06001C31 RID: 7217 RVA: 0x00095E28 File Offset: 0x00094028
	public void SetOrientation(Orientation new_orientation)
	{
		this.orientation = new_orientation;
		this.OrientVisualizer(new_orientation);
		this.OrientCollider(new_orientation);
	}

	// Token: 0x06001C32 RID: 7218 RVA: 0x00095E40 File Offset: 0x00094040
	public void Match(Rotatable other)
	{
		this.pivot = other.pivot;
		this.visualizerOffset = other.visualizerOffset;
		this.permittedRotations = other.permittedRotations;
		this.orientation = other.orientation;
		this.OrientVisualizer(this.orientation);
		this.OrientCollider(this.orientation);
	}

	// Token: 0x06001C33 RID: 7219 RVA: 0x00095E98 File Offset: 0x00094098
	public float GetVisualizerRotation()
	{
		PermittedRotations permittedRotations = this.permittedRotations;
		if (permittedRotations - PermittedRotations.R90 <= 1)
		{
			return -90f * (float)this.orientation;
		}
		return 0f;
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x00095EC5 File Offset: 0x000940C5
	public bool GetVisualizerFlipX()
	{
		return this.orientation == Orientation.FlipH;
	}

	// Token: 0x06001C35 RID: 7221 RVA: 0x00095ED0 File Offset: 0x000940D0
	public bool GetVisualizerFlipY()
	{
		return this.orientation == Orientation.FlipV;
	}

	// Token: 0x06001C36 RID: 7222 RVA: 0x00095EDC File Offset: 0x000940DC
	public Vector3 GetVisualizerPivot()
	{
		Vector3 vector = this.pivot;
		Orientation orientation = this.orientation;
		if (orientation != Orientation.FlipH)
		{
			if (orientation != Orientation.FlipV)
			{
			}
		}
		else
		{
			vector.x = -this.pivot.x;
		}
		return vector;
	}

	// Token: 0x06001C37 RID: 7223 RVA: 0x00095F18 File Offset: 0x00094118
	private Vector3 GetVisualizerOffset()
	{
		Orientation orientation = this.orientation;
		Vector3 vector;
		if (orientation != Orientation.FlipH)
		{
			if (orientation != Orientation.FlipV)
			{
				vector = this.visualizerOffset;
			}
			else
			{
				vector = new Vector3(this.visualizerOffset.x, 1f, this.visualizerOffset.z);
			}
		}
		else
		{
			vector = new Vector3(-this.visualizerOffset.x, this.visualizerOffset.y, this.visualizerOffset.z);
		}
		return vector;
	}

	// Token: 0x06001C38 RID: 7224 RVA: 0x00095F8C File Offset: 0x0009418C
	private void OrientVisualizer(Orientation orientation)
	{
		float visualizerRotation = this.GetVisualizerRotation();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		component.Pivot = this.GetVisualizerPivot();
		component.Rotation = visualizerRotation;
		component.Offset = this.GetVisualizerOffset();
		component.FlipX = this.GetVisualizerFlipX();
		component.FlipY = this.GetVisualizerFlipY();
		base.Trigger(-1643076535, this);
	}

	// Token: 0x06001C39 RID: 7225 RVA: 0x00095FE8 File Offset: 0x000941E8
	private void OrientCollider(Orientation orientation)
	{
		KBoxCollider2D component = base.GetComponent<KBoxCollider2D>();
		if (component == null)
		{
			return;
		}
		float num = 0.5f * (float)((this.width + 1) % 2);
		float num2 = 0f;
		switch (orientation)
		{
		case Orientation.R90:
			num2 = -90f;
			goto IL_11B;
		case Orientation.R180:
			num2 = -180f;
			goto IL_11B;
		case Orientation.R270:
			num2 = -270f;
			goto IL_11B;
		case Orientation.FlipH:
			component.offset = new Vector2(num + (float)(this.width % 2) - 1f, 0.5f * (float)this.height);
			component.size = new Vector2((float)this.width, (float)this.height);
			goto IL_11B;
		case Orientation.FlipV:
			component.offset = new Vector2(num, -0.5f * (float)(this.height - 2));
			component.size = new Vector2((float)this.width, (float)this.height);
			goto IL_11B;
		}
		component.offset = new Vector2(num, 0.5f * (float)this.height);
		component.size = new Vector2((float)this.width, (float)this.height);
		IL_11B:
		if (num2 != 0f)
		{
			Matrix2x3 matrix2x = Matrix2x3.Translate(-this.pivot);
			Matrix2x3 matrix2x2 = Matrix2x3.Rotate(num2 * 0.017453292f);
			Matrix2x3 matrix2x3 = Matrix2x3.Translate(this.pivot + new Vector3(num, 0f, 0f)) * matrix2x2 * matrix2x;
			Vector2 vector = new Vector2(-0.5f * (float)this.width, 0f);
			Vector2 vector2 = new Vector2(0.5f * (float)this.width, (float)this.height);
			Vector2 vector3 = new Vector2(0f, 0.5f * (float)this.height);
			vector = matrix2x3.MultiplyPoint(vector);
			vector2 = matrix2x3.MultiplyPoint(vector2);
			vector3 = matrix2x3.MultiplyPoint(vector3);
			float num3 = Mathf.Min(vector.x, vector2.x);
			float num4 = Mathf.Max(vector.x, vector2.x);
			float num5 = Mathf.Min(vector.y, vector2.y);
			float num6 = Mathf.Max(vector.y, vector2.y);
			component.offset = vector3;
			component.size = new Vector2(num4 - num3, num6 - num5);
		}
	}

	// Token: 0x06001C3A RID: 7226 RVA: 0x00096270 File Offset: 0x00094470
	public CellOffset GetRotatedCellOffset(CellOffset offset)
	{
		return Rotatable.GetRotatedCellOffset(offset, this.orientation);
	}

	// Token: 0x06001C3B RID: 7227 RVA: 0x00096280 File Offset: 0x00094480
	public static CellOffset GetRotatedCellOffset(CellOffset offset, Orientation orientation)
	{
		switch (orientation)
		{
		default:
			return offset;
		case Orientation.R90:
			return new CellOffset(offset.y, -offset.x);
		case Orientation.R180:
			return new CellOffset(-offset.x, -offset.y);
		case Orientation.R270:
			return new CellOffset(-offset.y, offset.x);
		case Orientation.FlipH:
			return new CellOffset(-offset.x, offset.y);
		case Orientation.FlipV:
			return new CellOffset(offset.x, -offset.y);
		}
	}

	// Token: 0x06001C3C RID: 7228 RVA: 0x00096310 File Offset: 0x00094510
	public static CellOffset GetRotatedCellOffset(int x, int y, Orientation orientation)
	{
		return Rotatable.GetRotatedCellOffset(new CellOffset(x, y), orientation);
	}

	// Token: 0x06001C3D RID: 7229 RVA: 0x0009631F File Offset: 0x0009451F
	public Vector3 GetRotatedOffset(Vector3 offset)
	{
		return Rotatable.GetRotatedOffset(offset, this.orientation);
	}

	// Token: 0x06001C3E RID: 7230 RVA: 0x00096330 File Offset: 0x00094530
	public static Vector3 GetRotatedOffset(Vector3 offset, Orientation orientation)
	{
		switch (orientation)
		{
		default:
			return offset;
		case Orientation.R90:
			return new Vector3(offset.y, -offset.x);
		case Orientation.R180:
			return new Vector3(-offset.x, -offset.y);
		case Orientation.R270:
			return new Vector3(-offset.y, offset.x);
		case Orientation.FlipH:
			return new Vector3(-offset.x, offset.y);
		case Orientation.FlipV:
			return new Vector3(offset.x, -offset.y);
		}
	}

	// Token: 0x06001C3F RID: 7231 RVA: 0x000963C0 File Offset: 0x000945C0
	public Orientation GetOrientation()
	{
		return this.orientation;
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x06001C40 RID: 7232 RVA: 0x000963C8 File Offset: 0x000945C8
	public bool IsRotated
	{
		get
		{
			return this.orientation > Orientation.Neutral;
		}
	}

	// Token: 0x04000FCC RID: 4044
	[MyCmpReq]
	private KBatchedAnimController batchedAnimController;

	// Token: 0x04000FCD RID: 4045
	[MyCmpGet]
	private Building building;

	// Token: 0x04000FCE RID: 4046
	[Serialize]
	[SerializeField]
	private Orientation orientation;

	// Token: 0x04000FCF RID: 4047
	[SerializeField]
	private Vector3 pivot = Vector3.zero;

	// Token: 0x04000FD0 RID: 4048
	[SerializeField]
	private Vector3 visualizerOffset = Vector3.zero;

	// Token: 0x04000FD1 RID: 4049
	public PermittedRotations permittedRotations;

	// Token: 0x04000FD2 RID: 4050
	[SerializeField]
	private int width;

	// Token: 0x04000FD3 RID: 4051
	[SerializeField]
	private int height;
}
