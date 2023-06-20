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
	
	ReturnedOrderDetails.Select(x=>x).Dump();
//	var returnItem = ReturnedOrderDetails
//				.Where(x => x.PurchaseOrderID == 346)
//				.Select(x => x)
//				.ToList()
//				.Dump();
//
	var existingReceiveOrder = ReturnedOrderDetails
							.Where(x => x.ReceiveOrderID == 153)
							.Select(x => x).Dump();
//
//	var existingROrder = ReceiveOrders
//							.Where(x => x.PurchaseOrderID == 346)
//							.Count().Dump();


//var RO = ReceiveOrders.Select(x=>x.ReceiveOrderID).Dump();
//
//var ROTotal = ReceiveOrders.Select(x=>x.ReceiveOrderID).Max().Dump();
//ROTotal = ROTotal + 1;
//ROTotal.Dump();


}

