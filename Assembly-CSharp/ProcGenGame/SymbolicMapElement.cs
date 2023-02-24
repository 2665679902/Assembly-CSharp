using System;

namespace ProcGenGame
{
	// Token: 0x02000C40 RID: 3136
	public interface SymbolicMapElement
	{
		// Token: 0x06006343 RID: 25411
		void ConvertToMap(Chunk world, TerrainCell.SetValuesFunction SetValues, float temperatureMin, float temperatureRange, SeededRandom rnd);
	}
}
