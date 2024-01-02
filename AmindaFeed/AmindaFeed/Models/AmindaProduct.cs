using System.Text;
using System.Xml.Serialization;

[XmlRoot(ElementName = "Status")]
public class Status
{

    [XmlElement(ElementName = "Type")]
    public string Type { get; set; }

    [XmlElement(ElementName = "Value")]
    public int Value { get; set; }

    [XmlElement(ElementName = "Id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "Active")]
    public int Active { get; set; }

    [XmlElement(ElementName = "Empty")]
    public int Empty { get; set; }

    [XmlElement(ElementName = "Variant")]
    public int Variant { get; set; }
}

[XmlRoot(ElementName = "Statuses")]
public class Statuses
{

    [XmlElement(ElementName = "Status")]
    public Status Status { get; set; }
}

[XmlRoot(ElementName = "Category")]
public class Category
{

    [XmlElement(ElementName = "Id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "Name", DataType = "string", IsNullable = true, Type = typeof(string))]
    public string Name { get; set; }

    [XmlElement(ElementName = "Type")]
    public string Type { get; set; }
}

[XmlRoot(ElementName = "Categories")]
public class Categories
{

    [XmlElement(ElementName = "Category")]
    public Category Category { get; set; }
}

[XmlRoot(ElementName = "Price")]
public class Price
{

    [XmlElement(ElementName = "Type")]
    public string Type { get; set; }

    [XmlElement(ElementName = "Net")]
    public double Net { get; set; }

    [XmlElement(ElementName = "Gross")]
    public double Gross { get; set; }
}

[XmlRoot(ElementName = "Prices")]
public class Prices
{

    [XmlElement(ElementName = "Price")]
    public Price Price { get; set; }
}

[XmlRoot(ElementName = "Import")]
public class Import
{

    [XmlElement(ElementName = "Url")]
    public string Url { get; set; }
}

[XmlRoot(ElementName = "Export")]
public class Export
{

    [XmlElement(ElementName = "Status")]
    public int Status { get; set; }
}

[XmlRoot(ElementName = "Image")]
public class Image
{

    [XmlElement(ElementName = "Import")]
    public Import Import { get; set; }
    [XmlElement(ElementName = "Type")]
    public string Type { get; set; }
}

[XmlRoot(ElementName = "Images")]
public class Images
{

    [XmlElement(ElementName = "Image")]
    public List<Image> Image { get; set; }
}

public class Param
{

    [XmlElement(ElementName = "Id")]
    public int Id { get; set; }
    [XmlElement(ElementName = "Type")]
    public string Type { get; set; }
    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }
    [XmlElement(ElementName = "Value")]
    public string Value { get; set; }

}

[XmlRoot(ElementName = "Params")]
public class Parameters
{

    [XmlElement(ElementName = "Param")]
    public List<Param> Param { get; set; }
}

[XmlRoot(ElementName = "Value")]
public class Value
{

    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "Values")]
public class Values
{

    [XmlElement(ElementName = "Value")]
    public List<Value> Value { get; set; }
}

[XmlRoot(ElementName = "Variant")]
public class AmindaVariant
{

    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }
    [XmlElement(ElementName = "Values")]
    public Values? Values { get; set; }
}


[XmlRoot(ElementName = "Variants")]
public class AmindaVariants
{

    [XmlElement(ElementName = "Variant")]
    public List<AmindaVariant> AmindaVariant { get; set; }
}


[XmlRoot(ElementName = "Stocks")]
public class Stocks
{

    [XmlElement(ElementName = "Status")]
    public Status Status { get; set; }

    [XmlElement(ElementName = "Stock")]
    public List<Stock> Stock { get; set; }
}

[XmlRoot(ElementName = "Stock")]
public class Stock
{

    [XmlElement(ElementName = "Variants")]
    public AmindaStockVariants Variants { get; set; }

    [XmlElement(ElementName = "Qty")]
    public int Qty { get; set; }
}


[XmlRoot(ElementName = "Variants")]
public class AmindaStockVariants
{

    [XmlElement(ElementName = "Variant")]
    public string AmindaStockVariant { get; set; }
}

[XmlRoot(ElementName = "Description")]
public class Description
{

    [XmlElement(ElementName = "Short")]
    public string Short { get; set; }
}



[XmlRoot(ElementName = "Product")]
public class AmindaProduct
{

    [XmlElement(ElementName = "Action")]
    public string Action { get; set; }

    [XmlElement(ElementName = "Statuses")]
    public Statuses Statuses { get; set; }

    [XmlElement(ElementName = "Sku")]
    public string Sku { get; set; }

    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "Description")]
    public Description Description { get; set; }

    [XmlElement(ElementName = "Unit")]
    public string Unit { get; set; }

    [XmlElement(ElementName = "Categories")]
    public Categories Categories { get; set; }

    [XmlElement(ElementName = "Prices")]
    public Prices Prices { get; set; }

    [XmlElement(ElementName = "Images")]
    public Images Images { get; set; }

    [XmlElement(ElementName = "Export")]
    public Export Export { get; set; }

    [XmlElement(ElementName = "Params")]
    public Parameters Params { get; set; }

    [XmlElement(ElementName = "Variants")]
    public AmindaVariants AmindaVariants { get; set; }

    [XmlElement(ElementName = "Stocks")]
    public Stocks Stocks { get; set; }

}

[XmlRoot(ElementName = "Products")]
public class AmindaProducts
{

    [XmlElement(ElementName = "Product")]
    public List<AmindaProduct> Products { get; set; }
}
