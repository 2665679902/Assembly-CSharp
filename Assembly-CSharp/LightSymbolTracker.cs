using System;
using UnityEngine;

// Token: 0x02000092 RID: 146
[AddComponentMenu("KMonoBehaviour/scripts/LightSymbolTracker")]
public class LightSymbolTracker : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x06000297 RID: 663 RVA: 0x0001443C File Offset: 0x0001263C
	public void RenderEveryTick(float dt)
	{
		Vector3 vector = Vector3.zero;
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		bool flag;
		vector = (component.GetTransformMatrix() * component.GetSymbolLocalTransform(this.targetSymbol, out flag)).MultiplyPoint(Vector3.zero) - base.transform.GetPosition();
		base.GetComponent<Light2D>().Offset = vector;
	}

	// Token: 0x04000198 RID: 408
	public HashedString targetSymbol;
}
