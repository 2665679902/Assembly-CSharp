using System;
using System.Runtime.Serialization;
using Database;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x02000615 RID: 1557
[AddComponentMenu("KMonoBehaviour/scripts/MonumentPart")]
public class MonumentPart : KMonoBehaviour
{
	// Token: 0x060028A6 RID: 10406 RVA: 0x000D7AF8 File Offset: 0x000D5CF8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.MonumentParts.Add(this);
		if (!string.IsNullOrEmpty(this.chosenState))
		{
			this.SetState(this.chosenState);
		}
		this.UpdateMonumentDecor();
	}

	// Token: 0x060028A7 RID: 10407 RVA: 0x000D7B2C File Offset: 0x000D5D2C
	[OnDeserialized]
	private void OnDeserializedMethod()
	{
		if (Db.GetMonumentParts().TryGet(this.chosenState) == null)
		{
			string text = "";
			if (this.part == MonumentPartResource.Part.Bottom)
			{
				text = "bottom_" + this.chosenState;
			}
			else if (this.part == MonumentPartResource.Part.Middle)
			{
				text = "mid_" + this.chosenState;
			}
			else if (this.part == MonumentPartResource.Part.Top)
			{
				text = "top_" + this.chosenState;
			}
			if (Db.GetMonumentParts().TryGet(text) != null)
			{
				this.chosenState = text;
			}
		}
	}

	// Token: 0x060028A8 RID: 10408 RVA: 0x000D7BB6 File Offset: 0x000D5DB6
	protected override void OnCleanUp()
	{
		Components.MonumentParts.Remove(this);
		this.RemoveMonumentPiece();
		base.OnCleanUp();
	}

	// Token: 0x060028A9 RID: 10409 RVA: 0x000D7BD0 File Offset: 0x000D5DD0
	public void SetState(string state)
	{
		MonumentPartResource monumentPartResource = Db.GetMonumentParts().Get(state);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		component.SwapAnims(new KAnimFile[] { monumentPartResource.AnimFile });
		component.Play(monumentPartResource.State, KAnim.PlayMode.Once, 1f, 0f);
		this.chosenState = state;
	}

	// Token: 0x060028AA RID: 10410 RVA: 0x000D7C28 File Offset: 0x000D5E28
	public bool IsMonumentCompleted()
	{
		bool flag = this.GetMonumentPart(MonumentPartResource.Part.Top) != null;
		bool flag2 = this.GetMonumentPart(MonumentPartResource.Part.Middle) != null;
		bool flag3 = this.GetMonumentPart(MonumentPartResource.Part.Bottom) != null;
		return flag && flag3 && flag2;
	}

	// Token: 0x060028AB RID: 10411 RVA: 0x000D7C64 File Offset: 0x000D5E64
	public void UpdateMonumentDecor()
	{
		GameObject monumentPart = this.GetMonumentPart(MonumentPartResource.Part.Middle);
		if (this.IsMonumentCompleted())
		{
			monumentPart.GetComponent<DecorProvider>().SetValues(BUILDINGS.DECOR.BONUS.MONUMENT.COMPLETE);
			foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(base.GetComponent<AttachableBuilding>()))
			{
				if (gameObject != monumentPart)
				{
					gameObject.GetComponent<DecorProvider>().SetValues(BUILDINGS.DECOR.NONE);
				}
			}
		}
	}

	// Token: 0x060028AC RID: 10412 RVA: 0x000D7CF0 File Offset: 0x000D5EF0
	public void RemoveMonumentPiece()
	{
		if (this.IsMonumentCompleted())
		{
			foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(base.GetComponent<AttachableBuilding>()))
			{
				if (gameObject.GetComponent<MonumentPart>() != this)
				{
					gameObject.GetComponent<DecorProvider>().SetValues(BUILDINGS.DECOR.BONUS.MONUMENT.INCOMPLETE);
				}
			}
		}
	}

	// Token: 0x060028AD RID: 10413 RVA: 0x000D7D68 File Offset: 0x000D5F68
	private GameObject GetMonumentPart(MonumentPartResource.Part requestPart)
	{
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(base.GetComponent<AttachableBuilding>()))
		{
			MonumentPart component = gameObject.GetComponent<MonumentPart>();
			if (!(component == null) && component.part == requestPart)
			{
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x040017E7 RID: 6119
	public MonumentPartResource.Part part;

	// Token: 0x040017E8 RID: 6120
	public string stateUISymbol;

	// Token: 0x040017E9 RID: 6121
	[Serialize]
	private string chosenState;
}
