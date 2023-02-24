using System;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class KBatchedAnimTracker : MonoBehaviour
{
	// Token: 0x0600168D RID: 5773 RVA: 0x000745B4 File Offset: 0x000727B4
	private void Start()
	{
		if (this.controller == null)
		{
			Transform transform = base.transform.parent;
			while (transform != null)
			{
				this.controller = transform.GetComponent<KBatchedAnimController>();
				if (this.controller != null)
				{
					break;
				}
				transform = transform.parent;
			}
		}
		if (this.controller == null)
		{
			global::Debug.Log("Controller Null for tracker on " + base.gameObject.name, base.gameObject);
			base.enabled = false;
			return;
		}
		this.controller.onAnimEnter += this.OnAnimStart;
		this.controller.onAnimComplete += this.OnAnimStop;
		this.controller.onLayerChanged += this.OnLayerChanged;
		this.forceUpdate = true;
		if (this.myAnim != null)
		{
			return;
		}
		this.myAnim = base.GetComponent<KBatchedAnimController>();
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x000746A4 File Offset: 0x000728A4
	private void OnDestroy()
	{
		if (this.controller != null)
		{
			this.controller.onAnimEnter -= this.OnAnimStart;
			this.controller.onAnimComplete -= this.OnAnimStop;
			this.controller.onLayerChanged -= this.OnLayerChanged;
			this.controller = null;
		}
		this.myAnim = null;
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x00074714 File Offset: 0x00072914
	private void LateUpdate()
	{
		if (this.controller != null && (this.controller.IsVisible() || this.forceAlwaysVisible || this.forceUpdate))
		{
			this.UpdateFrame();
		}
		if (!this.alive)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x00074761 File Offset: 0x00072961
	public void SetAnimControllers(KBatchedAnimController controller, KBatchedAnimController parentController)
	{
		this.myAnim = controller;
		this.controller = parentController;
	}

	// Token: 0x06001691 RID: 5777 RVA: 0x00074774 File Offset: 0x00072974
	private void UpdateFrame()
	{
		this.forceUpdate = false;
		bool flag = false;
		if (this.controller.CurrentAnim != null)
		{
			Matrix2x3 symbolLocalTransform = this.controller.GetSymbolLocalTransform(this.symbol, out flag);
			Vector3 position = this.controller.transform.GetPosition();
			if (flag && (this.previousMatrix != symbolLocalTransform || position != this.previousPosition || (this.useTargetPoint && this.targetPoint != this.previousTargetPoint) || (this.matchParentOffset && this.myAnim.Offset != this.controller.Offset)))
			{
				this.previousMatrix = symbolLocalTransform;
				this.previousPosition = position;
				Matrix2x3 matrix2x = this.controller.GetTransformMatrix() * symbolLocalTransform;
				float z = base.transform.GetPosition().z;
				base.transform.SetPosition(matrix2x.MultiplyPoint(this.offset));
				if (this.useTargetPoint)
				{
					this.previousTargetPoint = this.targetPoint;
					Vector3 position2 = base.transform.GetPosition();
					position2.z = 0f;
					Vector3 vector = this.targetPoint - position2;
					float num = Vector3.Angle(vector, Vector3.right);
					if (vector.y < 0f)
					{
						num = 360f - num;
					}
					base.transform.localRotation = Quaternion.identity;
					base.transform.RotateAround(position2, new Vector3(0f, 0f, 1f), num);
					float sqrMagnitude = vector.sqrMagnitude;
					this.myAnim.GetBatchInstanceData().SetClipRadius(base.transform.GetPosition().x, base.transform.GetPosition().y, sqrMagnitude, true);
				}
				else
				{
					Vector3 vector2 = (this.controller.FlipX ? Vector3.left : Vector3.right);
					Vector3 vector3 = (this.controller.FlipY ? Vector3.down : Vector3.up);
					base.transform.up = matrix2x.MultiplyVector(vector3);
					base.transform.right = matrix2x.MultiplyVector(vector2);
					if (this.myAnim != null)
					{
						KBatchedAnimInstanceData batchInstanceData = this.myAnim.GetBatchInstanceData();
						if (batchInstanceData != null)
						{
							batchInstanceData.SetOverrideTransformMatrix(matrix2x);
						}
					}
				}
				base.transform.SetPosition(new Vector3(base.transform.GetPosition().x, base.transform.GetPosition().y, z));
				if (this.matchParentOffset)
				{
					this.myAnim.Offset = this.controller.Offset;
				}
				this.myAnim.SetDirty();
			}
		}
		if (this.myAnim != null && flag != this.myAnim.enabled)
		{
			this.myAnim.enabled = flag;
		}
	}

	// Token: 0x06001692 RID: 5778 RVA: 0x00074A54 File Offset: 0x00072C54
	[ContextMenu("ForceAlive")]
	private void OnAnimStart(HashedString name)
	{
		this.alive = true;
		base.enabled = true;
		this.forceUpdate = true;
	}

	// Token: 0x06001693 RID: 5779 RVA: 0x00074A6B File Offset: 0x00072C6B
	private void OnAnimStop(HashedString name)
	{
		if (!this.forceAlwaysAlive)
		{
			this.alive = false;
		}
	}

	// Token: 0x06001694 RID: 5780 RVA: 0x00074A7C File Offset: 0x00072C7C
	private void OnLayerChanged(int layer)
	{
		this.myAnim.SetLayer(layer);
	}

	// Token: 0x06001695 RID: 5781 RVA: 0x00074A8A File Offset: 0x00072C8A
	public void SetTarget(Vector3 target)
	{
		this.targetPoint = target;
		this.targetPoint.z = 0f;
	}

	// Token: 0x04000C7D RID: 3197
	public KBatchedAnimController controller;

	// Token: 0x04000C7E RID: 3198
	public Vector3 offset = Vector3.zero;

	// Token: 0x04000C7F RID: 3199
	public HashedString symbol;

	// Token: 0x04000C80 RID: 3200
	public Vector3 targetPoint = Vector3.zero;

	// Token: 0x04000C81 RID: 3201
	public Vector3 previousTargetPoint;

	// Token: 0x04000C82 RID: 3202
	public bool useTargetPoint;

	// Token: 0x04000C83 RID: 3203
	public bool fadeOut = true;

	// Token: 0x04000C84 RID: 3204
	public bool forceAlwaysVisible;

	// Token: 0x04000C85 RID: 3205
	public bool matchParentOffset;

	// Token: 0x04000C86 RID: 3206
	public bool forceAlwaysAlive;

	// Token: 0x04000C87 RID: 3207
	private bool alive = true;

	// Token: 0x04000C88 RID: 3208
	private bool forceUpdate;

	// Token: 0x04000C89 RID: 3209
	private Matrix2x3 previousMatrix;

	// Token: 0x04000C8A RID: 3210
	private Vector3 previousPosition;

	// Token: 0x04000C8B RID: 3211
	[SerializeField]
	private KBatchedAnimController myAnim;
}
