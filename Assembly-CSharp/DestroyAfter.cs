using System;
using UnityEngine;

// Token: 0x02000473 RID: 1139
[AddComponentMenu("KMonoBehaviour/scripts/DestroyAfter")]
public class DestroyAfter : KMonoBehaviour
{
	// Token: 0x06001948 RID: 6472 RVA: 0x00087713 File Offset: 0x00085913
	protected override void OnSpawn()
	{
		this.particleSystems = base.gameObject.GetComponentsInChildren<ParticleSystem>(true);
	}

	// Token: 0x06001949 RID: 6473 RVA: 0x00087728 File Offset: 0x00085928
	private bool IsAlive()
	{
		for (int i = 0; i < this.particleSystems.Length; i++)
		{
			if (this.particleSystems[i].IsAlive(false))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600194A RID: 6474 RVA: 0x0008775B File Offset: 0x0008595B
	private void Update()
	{
		if (this.particleSystems != null && !this.IsAlive())
		{
			this.DeleteObject();
		}
	}

	// Token: 0x04000E23 RID: 3619
	private ParticleSystem[] particleSystems;
}
