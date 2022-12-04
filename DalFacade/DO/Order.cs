

namespace DO;
/// <summary>
/// Order description
/// </summary>
public struct Order
{
    /// <summary>
    /// Specific order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The name of the ordering customer
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// The email address of the ordering customer
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// Residential address of the ordering customer
    /// </summary>
    public string CustomerAdress { get; set; }
    /// <summary>
    /// Order Date
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// Shipping departure date
    /// </summary>
    public DateTime ShipDate { get; set; }
    /// <summary>
    /// Arrival date of the shipment
    /// </summary>
    public DateTime DeliveryDate { get; set; }

   public override string ToString() => $@"
order ID={ID}
order date: {OrderDate}, 
    	Customer Name: {CustomerName}
    	CustomerEmail: {CustomerEmail}
Customer Adress: {CustomerAdress}
OrderDate: {OrderDate}
Ship Date: {ShipDate}
DeliveryDate: {DeliveryDate}
";


}
