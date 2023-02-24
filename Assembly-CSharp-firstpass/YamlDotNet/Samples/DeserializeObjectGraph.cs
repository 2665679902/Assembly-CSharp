using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Samples.Helpers;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace YamlDotNet.Samples
{
	// Token: 0x020001EF RID: 495
	public class DeserializeObjectGraph
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x0003D203 File Offset: 0x0003B403
		public DeserializeObjectGraph(ITestOutputHelper output)
		{
			this.output = output;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0003D214 File Offset: 0x0003B414
		[Sample(Title = "Deserializing an object graph", Description = "Shows how to convert a YAML document to an object graph.")]
		public void Main()
		{
			StringReader stringReader = new StringReader("---\n            receipt:    Oz-Ware Purchase Invoice\n            date:        2007-08-06\n            customer:\n                given:   Dorothy\n                family:  Gale\n\n            items:\n                - part_no:   A4786\n                  descrip:   Water Bucket (Filled)\n                  price:     1.47\n                  quantity:  4\n\n                - part_no:   E1628\n                  descrip:   High Heeled \"Ruby\" Slippers\n                  price:     100.27\n                  quantity:  1\n\n            bill-to:  &id001\n                street: |-\n                        123 Tornado Alley\n                        Suite 16\n                city:   East Westville\n                state:  KS\n\n            ship-to:  *id001\n\n            specialDelivery: >\n                Follow the Yellow Brick\n                Road to the Emerald City.\n                Pay no attention to the\n                man behind the curtain.\n...");
			DeserializeObjectGraph.Order order = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build().Deserialize<DeserializeObjectGraph.Order>(stringReader);
			this.output.WriteLine("Order");
			this.output.WriteLine("-----");
			this.output.WriteLine();
			foreach (DeserializeObjectGraph.OrderItem orderItem in order.Items)
			{
				this.output.WriteLine("{0}\t{1}\t{2}\t{3}", new object[] { orderItem.PartNo, orderItem.Quantity, orderItem.Price, orderItem.Descrip });
			}
			this.output.WriteLine();
			this.output.WriteLine("Shipping");
			this.output.WriteLine("--------");
			this.output.WriteLine();
			this.output.WriteLine(order.ShipTo.Street);
			this.output.WriteLine(order.ShipTo.City);
			this.output.WriteLine(order.ShipTo.State);
			this.output.WriteLine();
			this.output.WriteLine("Billing");
			this.output.WriteLine("-------");
			this.output.WriteLine();
			if (order.BillTo == order.ShipTo)
			{
				this.output.WriteLine("*same as shipping address*");
			}
			else
			{
				this.output.WriteLine(order.ShipTo.Street);
				this.output.WriteLine(order.ShipTo.City);
				this.output.WriteLine(order.ShipTo.State);
			}
			this.output.WriteLine();
			this.output.WriteLine("Delivery instructions");
			this.output.WriteLine("---------------------");
			this.output.WriteLine();
			this.output.WriteLine(order.SpecialDelivery);
		}

		// Token: 0x04000864 RID: 2148
		private readonly ITestOutputHelper output;

		// Token: 0x04000865 RID: 2149
		private const string Document = "---\n            receipt:    Oz-Ware Purchase Invoice\n            date:        2007-08-06\n            customer:\n                given:   Dorothy\n                family:  Gale\n\n            items:\n                - part_no:   A4786\n                  descrip:   Water Bucket (Filled)\n                  price:     1.47\n                  quantity:  4\n\n                - part_no:   E1628\n                  descrip:   High Heeled \"Ruby\" Slippers\n                  price:     100.27\n                  quantity:  1\n\n            bill-to:  &id001\n                street: |-\n                        123 Tornado Alley\n                        Suite 16\n                city:   East Westville\n                state:  KS\n\n            ship-to:  *id001\n\n            specialDelivery: >\n                Follow the Yellow Brick\n                Road to the Emerald City.\n                Pay no attention to the\n                man behind the curtain.\n...";

		// Token: 0x02000A5F RID: 2655
		public class Order
		{
			// Token: 0x17000E78 RID: 3704
			// (get) Token: 0x06005582 RID: 21890 RVA: 0x0009F1D8 File Offset: 0x0009D3D8
			// (set) Token: 0x06005583 RID: 21891 RVA: 0x0009F1E0 File Offset: 0x0009D3E0
			public string Receipt { get; set; }

			// Token: 0x17000E79 RID: 3705
			// (get) Token: 0x06005584 RID: 21892 RVA: 0x0009F1E9 File Offset: 0x0009D3E9
			// (set) Token: 0x06005585 RID: 21893 RVA: 0x0009F1F1 File Offset: 0x0009D3F1
			public DateTime Date { get; set; }

			// Token: 0x17000E7A RID: 3706
			// (get) Token: 0x06005586 RID: 21894 RVA: 0x0009F1FA File Offset: 0x0009D3FA
			// (set) Token: 0x06005587 RID: 21895 RVA: 0x0009F202 File Offset: 0x0009D402
			public DeserializeObjectGraph.Customer Customer { get; set; }

			// Token: 0x17000E7B RID: 3707
			// (get) Token: 0x06005588 RID: 21896 RVA: 0x0009F20B File Offset: 0x0009D40B
			// (set) Token: 0x06005589 RID: 21897 RVA: 0x0009F213 File Offset: 0x0009D413
			public List<DeserializeObjectGraph.OrderItem> Items { get; set; }

			// Token: 0x17000E7C RID: 3708
			// (get) Token: 0x0600558A RID: 21898 RVA: 0x0009F21C File Offset: 0x0009D41C
			// (set) Token: 0x0600558B RID: 21899 RVA: 0x0009F224 File Offset: 0x0009D424
			[YamlMember(Alias = "bill-to", ApplyNamingConventions = false)]
			public DeserializeObjectGraph.Address BillTo { get; set; }

			// Token: 0x17000E7D RID: 3709
			// (get) Token: 0x0600558C RID: 21900 RVA: 0x0009F22D File Offset: 0x0009D42D
			// (set) Token: 0x0600558D RID: 21901 RVA: 0x0009F235 File Offset: 0x0009D435
			[YamlMember(Alias = "ship-to", ApplyNamingConventions = false)]
			public DeserializeObjectGraph.Address ShipTo { get; set; }

			// Token: 0x17000E7E RID: 3710
			// (get) Token: 0x0600558E RID: 21902 RVA: 0x0009F23E File Offset: 0x0009D43E
			// (set) Token: 0x0600558F RID: 21903 RVA: 0x0009F246 File Offset: 0x0009D446
			public string SpecialDelivery { get; set; }
		}

		// Token: 0x02000A60 RID: 2656
		public class Customer
		{
			// Token: 0x17000E7F RID: 3711
			// (get) Token: 0x06005591 RID: 21905 RVA: 0x0009F257 File Offset: 0x0009D457
			// (set) Token: 0x06005592 RID: 21906 RVA: 0x0009F25F File Offset: 0x0009D45F
			public string Given { get; set; }

			// Token: 0x17000E80 RID: 3712
			// (get) Token: 0x06005593 RID: 21907 RVA: 0x0009F268 File Offset: 0x0009D468
			// (set) Token: 0x06005594 RID: 21908 RVA: 0x0009F270 File Offset: 0x0009D470
			public string Family { get; set; }
		}

		// Token: 0x02000A61 RID: 2657
		public class OrderItem
		{
			// Token: 0x17000E81 RID: 3713
			// (get) Token: 0x06005596 RID: 21910 RVA: 0x0009F281 File Offset: 0x0009D481
			// (set) Token: 0x06005597 RID: 21911 RVA: 0x0009F289 File Offset: 0x0009D489
			[YamlMember(Alias = "part_no", ApplyNamingConventions = false)]
			public string PartNo { get; set; }

			// Token: 0x17000E82 RID: 3714
			// (get) Token: 0x06005598 RID: 21912 RVA: 0x0009F292 File Offset: 0x0009D492
			// (set) Token: 0x06005599 RID: 21913 RVA: 0x0009F29A File Offset: 0x0009D49A
			public string Descrip { get; set; }

			// Token: 0x17000E83 RID: 3715
			// (get) Token: 0x0600559A RID: 21914 RVA: 0x0009F2A3 File Offset: 0x0009D4A3
			// (set) Token: 0x0600559B RID: 21915 RVA: 0x0009F2AB File Offset: 0x0009D4AB
			public decimal Price { get; set; }

			// Token: 0x17000E84 RID: 3716
			// (get) Token: 0x0600559C RID: 21916 RVA: 0x0009F2B4 File Offset: 0x0009D4B4
			// (set) Token: 0x0600559D RID: 21917 RVA: 0x0009F2BC File Offset: 0x0009D4BC
			public int Quantity { get; set; }
		}

		// Token: 0x02000A62 RID: 2658
		public class Address
		{
			// Token: 0x17000E85 RID: 3717
			// (get) Token: 0x0600559F RID: 21919 RVA: 0x0009F2CD File Offset: 0x0009D4CD
			// (set) Token: 0x060055A0 RID: 21920 RVA: 0x0009F2D5 File Offset: 0x0009D4D5
			public string Street { get; set; }

			// Token: 0x17000E86 RID: 3718
			// (get) Token: 0x060055A1 RID: 21921 RVA: 0x0009F2DE File Offset: 0x0009D4DE
			// (set) Token: 0x060055A2 RID: 21922 RVA: 0x0009F2E6 File Offset: 0x0009D4E6
			public string City { get; set; }

			// Token: 0x17000E87 RID: 3719
			// (get) Token: 0x060055A3 RID: 21923 RVA: 0x0009F2EF File Offset: 0x0009D4EF
			// (set) Token: 0x060055A4 RID: 21924 RVA: 0x0009F2F7 File Offset: 0x0009D4F7
			public string State { get; set; }
		}
	}
}
