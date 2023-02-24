using System;
using YamlDotNet.Samples.Helpers;
using YamlDotNet.Serialization;

namespace YamlDotNet.Samples
{
	// Token: 0x020001F2 RID: 498
	public class SerializeObjectGraph
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x0003D630 File Offset: 0x0003B830
		public SerializeObjectGraph(ITestOutputHelper output)
		{
			this.output = output;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0003D640 File Offset: 0x0003B840
		[Sample(Title = "Serializing an object graph", Description = "Shows how to convert an object to its YAML representation.")]
		public void Main()
		{
			Address address = new Address
			{
				street = "123 Tornado Alley\nSuite 16",
				city = "East Westville",
				state = "KS"
			};
			Receipt receipt = new Receipt
			{
				receipt = "Oz-Ware Purchase Invoice",
				date = new DateTime(2007, 8, 6),
				customer = new Customer
				{
					given = "Dorothy",
					family = "Gale"
				},
				items = new Item[]
				{
					new Item
					{
						part_no = "A4786",
						descrip = "Water Bucket (Filled)",
						price = 1.47m,
						quantity = 4
					},
					new Item
					{
						part_no = "E1628",
						descrip = "High Heeled \"Ruby\" Slippers",
						price = 100.27m,
						quantity = 1
					}
				},
				bill_to = address,
				ship_to = address,
				specialDelivery = "Follow the Yellow Brick\nRoad to the Emerald City.\nPay no attention to the\nman behind the curtain."
			};
			string text = new SerializerBuilder().Build().Serialize(receipt);
			this.output.WriteLine(text);
		}

		// Token: 0x0400086A RID: 2154
		private readonly ITestOutputHelper output;
	}
}
