using System;
using UnityEngine;

// Token: 0x0200087F RID: 2175
public class OreSizeVisualizerComponents : KGameObjectComponentManager<OreSizeVisualizerData>
{
	// Token: 0x06003E6B RID: 15979 RVA: 0x0015D2AC File Offset: 0x0015B4AC
	public HandleVector<int>.Handle Add(GameObject go)
	{
		HandleVector<int>.Handle handle = base.Add(go, new OreSizeVisualizerData(go));
		this.OnPrefabInit(handle);
		return handle;
	}

	// Token: 0x06003E6C RID: 15980 RVA: 0x0015D2D0 File Offset: 0x0015B4D0
	protected override void OnPrefabInit(HandleVector<int>.Handle handle)
	{
		Action<object> action = delegate(object ev_data)
		{
			OreSizeVisualizerComponents.OnMassChanged(handle, ev_data);
		};
		OreSizeVisualizerData data = base.GetData(handle);
		data.onMassChangedCB = action;
		data.primaryElement.Subscribe(-2064133523, action);
		data.primaryElement.Subscribe(1335436905, action);
		base.SetData(handle, data);
	}

	// Token: 0x06003E6D RID: 15981 RVA: 0x0015D340 File Offset: 0x0015B540
	protected override void OnSpawn(HandleVector<int>.Handle handle)
	{
		OreSizeVisualizerData data = base.GetData(handle);
		OreSizeVisualizerComponents.OnMassChanged(handle, data.primaryElement.GetComponent<Pickupable>());
	}

	// Token: 0x06003E6E RID: 15982 RVA: 0x0015D368 File Offset: 0x0015B568
	protected override void OnCleanUp(HandleVector<int>.Handle handle)
	{
		OreSizeVisualizerData data = base.GetData(handle);
		if (data.primaryElement != null)
		{
			Action<object> onMassChangedCB = data.onMassChangedCB;
			data.primaryElement.Unsubscribe(-2064133523, onMassChangedCB);
			data.primaryElement.Unsubscribe(1335436905, onMassChangedCB);
		}
	}

	// Token: 0x06003E6F RID: 15983 RVA: 0x0015D3B4 File Offset: 0x0015B5B4
	private static void OnMassChanged(HandleVector<int>.Handle handle, object other_data)
	{
		PrimaryElement primaryElement = GameComps.OreSizeVisualizers.GetData(handle).primaryElement;
		float num = primaryElement.Mass;
		if (other_data != null)
		{
			PrimaryElement component = ((Pickupable)other_data).GetComponent<PrimaryElement>();
			num += component.Mass;
		}
		OreSizeVisualizerComponents.MassTier massTier = default(OreSizeVisualizerComponents.MassTier);
		for (int i = 0; i < OreSizeVisualizerComponents.MassTiers.Length; i++)
		{
			if (num <= OreSizeVisualizerComponents.MassTiers[i].massRequired)
			{
				massTier = OreSizeVisualizerComponents.MassTiers[i];
				break;
			}
		}
		primaryElement.GetComponent<KBatchedAnimController>().Play(massTier.animName, KAnim.PlayMode.Once, 1f, 0f);
		KCircleCollider2D component2 = primaryElement.GetComponent<KCircleCollider2D>();
		if (component2 != null)
		{
			component2.radius = massTier.colliderRadius;
		}
		primaryElement.Trigger(1807976145, null);
	}

	// Token: 0x040028DC RID: 10460
	private static readonly OreSizeVisualizerComponents.MassTier[] MassTiers = new OreSizeVisualizerComponents.MassTier[]
	{
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle1",
			massRequired = 50f,
			colliderRadius = 0.15f
		},
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle2",
			massRequired = 600f,
			colliderRadius = 0.2f
		},
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle3",
			massRequired = float.MaxValue,
			colliderRadius = 0.25f
		}
	};

	// Token: 0x0200163C RID: 5692
	private struct MassTier
	{
		// Token: 0x0400693D RID: 26941
		public HashedString animName;

		// Token: 0x0400693E RID: 26942
		public float massRequired;

		// Token: 0x0400693F RID: 26943
		public float colliderRadius;
	}
}
