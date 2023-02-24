using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041B RID: 1051
[AddComponentMenu("KMonoBehaviour/scripts/KBatchedAnimEventToggler")]
public class KBatchedAnimEventToggler : KMonoBehaviour
{
	// Token: 0x06001687 RID: 5767 RVA: 0x00074394 File Offset: 0x00072594
	protected override void OnPrefabInit()
	{
		Vector3 position = this.eventSource.transform.GetPosition();
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Front);
		int num = LayerMask.NameToLayer("Default");
		foreach (KBatchedAnimEventToggler.Entry entry in this.entries)
		{
			entry.controller.transform.SetPosition(position);
			entry.controller.SetLayer(num);
			entry.controller.gameObject.SetActive(false);
		}
		int num2 = Hash.SDBMLower(this.enableEvent);
		int num3 = Hash.SDBMLower(this.disableEvent);
		base.Subscribe(this.eventSource, num2, new Action<object>(this.Enable));
		base.Subscribe(this.eventSource, num3, new Action<object>(this.Disable));
	}

	// Token: 0x06001688 RID: 5768 RVA: 0x00074484 File Offset: 0x00072684
	protected override void OnSpawn()
	{
		this.animEventHandler = base.GetComponentInParent<AnimEventHandler>();
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x00074494 File Offset: 0x00072694
	private void Enable(object data)
	{
		this.StopAll();
		HashedString context = this.animEventHandler.GetContext();
		if (!context.IsValid)
		{
			return;
		}
		foreach (KBatchedAnimEventToggler.Entry entry in this.entries)
		{
			if (entry.context == context)
			{
				entry.controller.gameObject.SetActive(true);
				entry.controller.Play(entry.anim, KAnim.PlayMode.Loop, 1f, 0f);
			}
		}
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x0007453C File Offset: 0x0007273C
	private void Disable(object data)
	{
		this.StopAll();
	}

	// Token: 0x0600168B RID: 5771 RVA: 0x00074544 File Offset: 0x00072744
	private void StopAll()
	{
		foreach (KBatchedAnimEventToggler.Entry entry in this.entries)
		{
			entry.controller.StopAndClear();
			entry.controller.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000C78 RID: 3192
	[SerializeField]
	public GameObject eventSource;

	// Token: 0x04000C79 RID: 3193
	[SerializeField]
	public string enableEvent;

	// Token: 0x04000C7A RID: 3194
	[SerializeField]
	public string disableEvent;

	// Token: 0x04000C7B RID: 3195
	[SerializeField]
	public List<KBatchedAnimEventToggler.Entry> entries;

	// Token: 0x04000C7C RID: 3196
	private AnimEventHandler animEventHandler;

	// Token: 0x02001043 RID: 4163
	[Serializable]
	public struct Entry
	{
		// Token: 0x040056D1 RID: 22225
		public string anim;

		// Token: 0x040056D2 RID: 22226
		public HashedString context;

		// Token: 0x040056D3 RID: 22227
		public KBatchedAnimController controller;
	}
}
