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
	PurchaseOrders
			.Where(x => x.Closed == true && x.OrderDate != null)
			.Select(x =>x).Dump();
	
	StockItems.Where(x=>x.StockItemID == 34)
	.Select(x=>x.QuantityOnHand).Dump()
	;
}

// You can define other methods, fields, classes and namespaces here