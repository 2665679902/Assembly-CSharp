using System;
using System.Diagnostics;

// Token: 0x02000012 RID: 18
[DebuggerDisplay("{name}")]
public class KAnimFileData
{
	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000103 RID: 259 RVA: 0x000064FB File Offset: 0x000046FB
	// (set) Token: 0x06000104 RID: 260 RVA: 0x00006503 File Offset: 0x00004703
	public string name { get; private set; }

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000105 RID: 261 RVA: 0x0000650C File Offset: 0x0000470C
	// (set) Token: 0x06000106 RID: 262 RVA: 0x00006514 File Offset: 0x00004714
	public KAnimHashedString hashName { get; private set; }

	// Token: 0x06000107 RID: 263 RVA: 0x00006520 File Offset: 0x00004720
	public KAnimFileData(string name)
	{
		this.name = name;
		this.firstAnimIndex = -1;
		this.buildIndex = -1;
		this.firstElementIndex = -1;
		this.animCount = 0;
		this.frameCount = 0;
		this.elementCount = 0;
		this.maxVisSymbolFrames = 0;
		this.hashName = new KAnimHashedString(name);
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000108 RID: 264 RVA: 0x00006578 File Offset: 0x00004778
	public KAnim.Build build
	{
		get
		{
			if (this.buildIndex == -1)
			{
				return null;
			}
			KBatchGroupData batchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(this.batchTag);
			if (batchGroupData == null)
			{
				global::Debug.LogErrorFormat("[{0}] No such batch group [{1}]", new object[]
				{
					this.name,
					this.batchTag.ToString()
				});
			}
			return batchGroupData.GetBuild(this.buildIndex);
		}
	}

	// Token: 0x06000109 RID: 265 RVA: 0x000065E0 File Offset: 0x000047E0
	public KAnim.Anim GetAnim(int index)
	{
		global::Debug.Assert(index >= 0 && index < this.animCount);
		KBatchGroupData batchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(this.animBatchTag);
		if (batchGroupData == null)
		{
			global::Debug.LogError(string.Format("[{0}] No such batch group [{1}]", this.name, this.animBatchTag.ToString()));
		}
		return batchGroupData.GetAnim(index + this.firstAnimIndex);
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00006648 File Offset: 0x00004848
	public KAnim.Anim.FrameElement GetAnimFrameElement(int index)
	{
		global::Debug.Assert(index >= 0 && index < this.elementCount);
		KBatchGroupData batchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(this.animBatchTag);
		if (batchGroupData == null)
		{
			global::Debug.LogErrorFormat("[{0}] No such batch group [{1}]", new object[]
			{
				this.name,
				this.animBatchTag.ToString()
			});
		}
		return batchGroupData.GetFrameElement(this.firstElementIndex + index);
	}

	// Token: 0x0600010B RID: 267 RVA: 0x000066BC File Offset: 0x000048BC
	public KAnim.Anim.FrameElement FindAnimFrameElement(KAnimHashedString symbolName)
	{
		return KAnimBatchManager.Instance().GetBatchGroupData(this.animBatchTag).frameElements.Find((KAnim.Anim.FrameElement match) => match.symbol == symbolName);
	}

	// Token: 0x0400005D RID: 93
	public const int NO_RECORD = -1;

	// Token: 0x0400005E RID: 94
	public int index;

	// Token: 0x04000061 RID: 97
	public HashedString batchTag;

	// Token: 0x04000062 RID: 98
	public int buildIndex;

	// Token: 0x04000063 RID: 99
	public HashedString animBatchTag;

	// Token: 0x04000064 RID: 100
	public int firstAnimIndex;

	// Token: 0x04000065 RID: 101
	public int animCount;

	// Token: 0x04000066 RID: 102
	public int frameCount;

	// Token: 0x04000067 RID: 103
	public int firstElementIndex;

	// Token: 0x04000068 RID: 104
	public int elementCount;

	// Token: 0x04000069 RID: 105
	public int maxVisSymbolFrames;
}
