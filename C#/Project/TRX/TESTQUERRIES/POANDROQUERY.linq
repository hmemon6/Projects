<Query Kind="Program">
  <Connection>
    <ID>47f4fd96-c03b-40e4-ada7-8c00d0f36ea7</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>.</Server>
    <Database>eTools</Database>
    <DisplayName>eTools-Entity</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	//ALL RECIEVE ORDERS
	//var receiveOrderItem = ReceiveOrderDetails.Select(x => x).Dump();
	//var existingReceive = ReceiveOrders
	//					.Where(x => x.PurchaseOrderID == 346)
	//					.ToList()
	//					.Dump();


	PurchaseOrders
		.Select(x => x).Dump();


	PurchaseOrders
			.Where(x => x.PurchaseOrderID == 346)
			.Select(x => x).Dump();


	PurchaseOrders
			.Where(x => x.PurchaseOrderID == 358)
			.Select(x => x).Dump();
	
	PurchaseOrderDetails
		.Where(x => x.PurchaseOrderID == 358)
		.Select(x => x).Dump();


	PurchaseOrderDetails
	.Where(x => x.PurchaseOrderDetailID == 10)
	.Select(x => x).Dump();


	var stockItem = PurchaseOrderDetails
					.Where(x => x.PurchaseOrderID == 358)
					.Select(x => x)
					.ToList()
					.Dump();

	var existingReceiveOrder = ReceiveOrders
							//.Where(x => x.PurchaseOrderID == 358)
							.Select(x => x).Dump();
	//	var stockItem = PurchaseOrderDetails
//					.Where(x => x.PurchaseOrderID == 358)
//					.Select(x => x)
//					.ToList()
//					.Dump();
//
	//var existingReceiveOrder = ReceiveOrders
	//						.Where(x => x.PurchaseOrderID == 358)
	//						.Select(x=>x).Dump();
//
//	var existingROrder = PurchaseOrderDetails
//							.Where(x => x.PurchaseOrderID == 358)
//							.Select(x=>x.StockItemID).Count().Dump();
}

// You can define other methods, fields, classes and namespaces here