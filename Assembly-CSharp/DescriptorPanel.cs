using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A88 RID: 2696
[AddComponentMenu("KMonoBehaviour/scripts/DescriptorPanel")]
public class DescriptorPanel : KMonoBehaviour
{
	// Token: 0x06005283 RID: 21123 RVA: 0x001DD0FB File Offset: 0x001DB2FB
	public bool HasDescriptors()
	{
		return this.labels.Count > 0;
	}

	// Token: 0x06005284 RID: 21124 RVA: 0x001DD10C File Offset: 0x001DB30C
	public void SetDescriptors(IList<Descriptor> descriptors)
	{
		int i;
		for (i = 0; i < descriptors.Count; i++)
		{
			GameObject gameObject;
			if (i >= this.labels.Count)
			{
				gameObject = Util.KInstantiate((this.customLabelPrefab != null) ? this.customLabelPrefab : ScreenPrefabs.Instance.DescriptionLabel, base.gameObject, null);
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				this.labels.Add(gameObject);
			}
			else
			{
				gameObject = this.labels[i];
			}
			gameObject.GetComponent<LocText>().text = descriptors[i].IndentedText();
			gameObject.GetComponent<ToolTip>().toolTip = descriptors[i].tooltipText;
			gameObject.SetActive(true);
		}
		while (i < this.labels.Count)
		{
			this.labels[i].SetActive(false);
			i++;
		}
	}

	// Token: 0x040037C0 RID: 14272
	[SerializeField]
	private GameObject customLabelPrefab;

	// Token: 0x040037C1 RID: 14273
	private List<GameObject> labels = new List<GameObject>();
}
