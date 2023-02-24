using System;
using UnityEngine;

// Token: 0x0200087B RID: 2171
[AddComponentMenu("KMonoBehaviour/scripts/OneshotReactableHost")]
public class OneshotReactableHost : KMonoBehaviour
{
	// Token: 0x06003E56 RID: 15958 RVA: 0x0015C875 File Offset: 0x0015AA75
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("CleanupOneshotReactable", this.lifetime, new Action<object>(this.OnExpire), null, null);
	}

	// Token: 0x06003E57 RID: 15959 RVA: 0x0015C8A1 File Offset: 0x0015AAA1
	public void SetReactable(Reactable reactable)
	{
		this.reactable = reactable;
	}

	// Token: 0x06003E58 RID: 15960 RVA: 0x0015C8AC File Offset: 0x0015AAAC
	private void OnExpire(object obj)
	{
		if (!this.reactable.IsReacting)
		{
			this.reactable.Cleanup();
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		GameScheduler.Instance.Schedule("CleanupOneshotReactable", 0.5f, new Action<object>(this.OnExpire), null, null);
	}

	// Token: 0x040028CD RID: 10445
	private Reactable reactable;

	// Token: 0x040028CE RID: 10446
	public float lifetime = 1f;
}
