
namespace DO;
/// <summary>
/// Description of product order
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Product order specific id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Specific order id
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    /// Product specific id
    /// </summary>
    public int ProductId { get; set; }
    /// <summary>
    /// Quantity of products in the order
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// Total price of the order
    /// </summary>
    public double Price { get; set; }
    public override string ToString() => $@"
itemsInOrder id={ID}
 orderId: {OrderId}, 
 product Id - {ProductId}
    	 amount: {Amount}
    	 Price: {Price}
";
}
