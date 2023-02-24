using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D96 RID: 3478
	public class ModifierInstance<ModifierType> : IStateMachineTarget
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060069E6 RID: 27110 RVA: 0x00292D10 File Offset: 0x00290F10
		// (set) Token: 0x060069E7 RID: 27111 RVA: 0x00292D18 File Offset: 0x00290F18
		public GameObject gameObject { get; private set; }

		// Token: 0x060069E8 RID: 27112 RVA: 0x00292D21 File Offset: 0x00290F21
		public ModifierInstance(GameObject game_object, ModifierType modifier)
		{
			this.gameObject = game_object;
			this.modifier = modifier;
		}

		// Token: 0x060069E9 RID: 27113 RVA: 0x00292D37 File Offset: 0x00290F37
		public ComponentType GetComponent<ComponentType>()
		{
			return this.gameObject.GetComponent<ComponentType>();
		}

		// Token: 0x060069EA RID: 27114 RVA: 0x00292D44 File Offset: 0x00290F44
		public int Subscribe(int hash, Action<object> handler)
		{
			return this.gameObject.GetComponent<KMonoBehaviour>().Subscribe(hash, handler);
		}

		// Token: 0x060069EB RID: 27115 RVA: 0x00292D58 File Offset: 0x00290F58
		public void Unsubscribe(int hash, Action<object> handler)
		{
			this.gameObject.GetComponent<KMonoBehaviour>().Unsubscribe(hash, handler);
		}

		// Token: 0x060069EC RID: 27116 RVA: 0x00292D6C File Offset: 0x00290F6C
		public void Unsubscribe(int id)
		{
			this.gameObject.GetComponent<KMonoBehaviour>().Unsubscribe(id);
		}

		// Token: 0x060069ED RID: 27117 RVA: 0x00292D7F File Offset: 0x00290F7F
		public void Trigger(int hash, object data = null)
		{
			this.gameObject.GetComponent<KPrefabID>().Trigger(hash, data);
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060069EE RID: 27118 RVA: 0x00292D93 File Offset: 0x00290F93
		public Transform transform
		{
			get
			{
				return this.gameObject.transform;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060069EF RID: 27119 RVA: 0x00292DA0 File Offset: 0x00290FA0
		public bool isNull
		{
			get
			{
				return this.gameObject == null;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060069F0 RID: 27120 RVA: 0x00292DAE File Offset: 0x00290FAE
		public string name
		{
			get
			{
				return this.gameObject.name;
			}
		}

		// Token: 0x060069F1 RID: 27121 RVA: 0x00292DBB File Offset: 0x00290FBB
		public virtual void OnCleanUp()
		{
		}

		// Token: 0x04004FA7 RID: 20391
		public ModifierType modifier;
	}
}
