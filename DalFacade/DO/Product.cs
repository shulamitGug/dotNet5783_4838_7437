namespace DO;

/// <summary>
/// Product description
/// </summary>
public struct Product
{
    /// <summary>
    /// Product specific id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Product Name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public int Price { get; set; }
    /// <summary>
    /// Belonging to a certain category
    /// </summary>
    public Category CategoryP { get; set; }
    /// <summary>
    /// The amount of products in stock
    /// </summary>
    public int InStock { get; set; }

    public override string ToString() => $@"
Product ID={ID}: {Name}, 
category - {CategoryP}
    	Price: {Price}
    	Amount in stock: {InStock}
";

}
