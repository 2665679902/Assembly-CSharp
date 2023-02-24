using System;
using System.IO;

// Token: 0x020000EF RID: 239
public interface ISaveLoadableDetails
{
	// Token: 0x0600087B RID: 2171
	void Serialize(BinaryWriter writer);

	// Token: 0x0600087C RID: 2172
	void Deserialize(IReader reader);
}
