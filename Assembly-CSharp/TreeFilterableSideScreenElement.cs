using System;
using UnityEngine;

// Token: 0x02000BEF RID: 3055
[AddComponentMenu("KMonoBehaviour/scripts/TreeFilterableSideScreenElement")]
public class TreeFilterableSideScreenElement : KMonoBehaviour
{
	// Token: 0x06006065 RID: 24677 RVA: 0x00234617 File Offset: 0x00232817
	public Tag GetElementTag()
	{
		return this.elementTag;
	}

	// Token: 0x170006AF RID: 1711
	// (get) Token: 0x06006066 RID: 24678 RVA: 0x0023461F File Offset: 0x0023281F
	public bool IsSelected
	{
		get
		{
			return this.checkBox.CurrentState == 1;
		}
	}

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x06006067 RID: 24679 RVA: 0x00234630 File Offset: 0x00232830
	// (remove) Token: 0x06006068 RID: 24680 RVA: 0x00234668 File Offset: 0x00232868
	public event Action<Tag, bool> OnSelectionChanged;

	// Token: 0x06006069 RID: 24681 RVA: 0x0023469D File Offset: 0x0023289D
	public MultiToggle GetCheckboxToggle()
	{
		return this.checkBox;
	}

	// Token: 0x170006B0 RID: 1712
	// (get) Token: 0x0600606A RID: 24682 RVA: 0x002346A5 File Offset: 0x002328A5
	// (set) Token: 0x0600606B RID: 24683 RVA: 0x002346AD File Offset: 0x002328AD
	public TreeFilterableSideScreen Parent
	{
		get
		{
			return this.parent;
		}
		set
		{
			this.parent = value;
		}
	}

	// Token: 0x0600606C RID: 24684 RVA: 0x002346B6 File Offset: 0x002328B6
	private void Initialize()
	{
		if (this.initialized)
		{
			return;
		}
		this.checkBoxImg = this.checkBox.gameObject.GetComponentInChildrenOnly<KImage>();
		this.checkBox.onClick = new System.Action(this.CheckBoxClicked);
		this.initialized = true;
	}

	// Token: 0x0600606D RID: 24685 RVA: 0x002346F5 File Offset: 0x002328F5
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Initialize();
	}

	// Token: 0x0600606E RID: 24686 RVA: 0x00234704 File Offset: 0x00232904
	public Sprite GetStorageObjectSprite(Tag t)
	{
		Sprite sprite = null;
		GameObject prefab = Assets.GetPrefab(t);
		if (prefab != null)
		{
			KBatchedAnimController component = prefab.GetComponent<KBatchedAnimController>();
			if (component != null)
			{
				sprite = Def.GetUISpriteFromMultiObjectAnim(component.AnimFiles[0], "ui", false, "");
			}
		}
		return sprite;
	}

	// Token: 0x0600606F RID: 24687 RVA: 0x00234750 File Offset: 0x00232950
	public void SetSprite(Tag t)
	{
		global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(t, "ui", false);
		this.elementImg.sprite = uisprite.first;
		this.elementImg.color = uisprite.second;
		this.elementImg.gameObject.SetActive(true);
	}

	// Token: 0x06006070 RID: 24688 RVA: 0x002347A4 File Offset: 0x002329A4
	public void SetTag(Tag newTag)
	{
		this.Initialize();
		this.elementTag = newTag;
		this.SetSprite(this.elementTag);
		string text = this.elementTag.ProperName();
		if (this.parent.IsStorage)
		{
			float amountInStorage = this.parent.GetAmountInStorage(this.elementTag);
			text = text + ": " + GameUtil.GetFormattedMass(amountInStorage, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
		}
		this.elementName.text = text;
	}

	// Token: 0x06006071 RID: 24689 RVA: 0x0023481B File Offset: 0x00232A1B
	private void CheckBoxClicked()
	{
		this.SetCheckBox(!this.parent.IsTagAllowed(this.GetElementTag()));
	}

	// Token: 0x06006072 RID: 24690 RVA: 0x00234837 File Offset: 0x00232A37
	public void SetCheckBox(bool checkBoxState)
	{
		this.checkBox.ChangeState(checkBoxState ? 1 : 0);
		this.checkBoxImg.enabled = checkBoxState;
		if (this.OnSelectionChanged != null)
		{
			this.OnSelectionChanged(this.GetElementTag(), checkBoxState);
		}
	}

	// Token: 0x04004213 RID: 16915
	[SerializeField]
	private LocText elementName;

	// Token: 0x04004214 RID: 16916
	[SerializeField]
	private MultiToggle checkBox;

	// Token: 0x04004215 RID: 16917
	[SerializeField]
	private KImage elementImg;

	// Token: 0x04004216 RID: 16918
	private KImage checkBoxImg;

	// Token: 0x04004217 RID: 16919
	private Tag elementTag;

	// Token: 0x04004219 RID: 16921
	private TreeFilterableSideScreen parent;

	// Token: 0x0400421A RID: 16922
	private bool initialized;
}
