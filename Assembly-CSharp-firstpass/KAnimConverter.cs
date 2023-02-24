using System;

// Token: 0x02000010 RID: 16
public class KAnimConverter
{
	// Token: 0x02000961 RID: 2401
	public interface IAnimConverter
	{
		// Token: 0x060052C1 RID: 21185
		int GetMaxVisible();

		// Token: 0x060052C2 RID: 21186
		HashedString GetBatchGroupID(bool isEditorWindow = false);

		// Token: 0x060052C3 RID: 21187
		KAnimBatch GetBatch();

		// Token: 0x060052C4 RID: 21188
		void SetBatch(KAnimBatch id);

		// Token: 0x060052C5 RID: 21189
		Vector2I GetCellXY();

		// Token: 0x060052C6 RID: 21190
		float GetZ();

		// Token: 0x060052C7 RID: 21191
		int GetLayer();

		// Token: 0x060052C8 RID: 21192
		string GetName();

		// Token: 0x060052C9 RID: 21193
		bool IsActive();

		// Token: 0x060052CA RID: 21194
		bool IsVisible();

		// Token: 0x060052CB RID: 21195
		int GetCurrentNumFrames();

		// Token: 0x060052CC RID: 21196
		int GetFirstFrameIndex();

		// Token: 0x060052CD RID: 21197
		int GetCurrentFrameIndex();

		// Token: 0x060052CE RID: 21198
		Matrix2x3 GetTransformMatrix();

		// Token: 0x060052CF RID: 21199
		KBatchedAnimInstanceData GetBatchInstanceData();

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x060052D0 RID: 21200
		SymbolInstanceGpuData symbolInstanceGpuData { get; }

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x060052D1 RID: 21201
		SymbolOverrideInfoGpuData symbolOverrideInfoGpuData { get; }

		// Token: 0x060052D2 RID: 21202
		KAnimBatchGroup.MaterialType GetMaterialType();

		// Token: 0x060052D3 RID: 21203
		bool ApplySymbolOverrides();
	}
}
