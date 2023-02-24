using System;
using UnityEngine;

// Token: 0x0200035D RID: 861
public class GameObjectPool : ObjectPool<GameObject>
{
	// Token: 0x06001159 RID: 4441 RVA: 0x0005D3AF File Offset: 0x0005B5AF
	public GameObjectPool(Func<GameObject> instantiator, int initial_count = 0)
		: base(instantiator, initial_count)
	{
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x0005D3B9 File Offset: 0x0005B5B9
	public override GameObject GetInstance()
	{
		return base.GetInstance();
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x0005D3C4 File Offset: 0x0005B5C4
	public void Destroy()
	{
		for (int i = this.unused.Count - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(this.unused.Pop());
		}
	}
}
