namespace SimpleShop{
    public class InvoicePosition{
        public uint ItemIdentifier = 0;
        public string ItemName = "";
        public uint Orders = 0;
        public decimal SingleUnitPrice = 0.0m;
        public Customer Customer;

        public virtual decimal Price(){
            return this.Customer.CalculatePrice(this.SingleUnitPrice * Orders);
        }

        public static InvoicePosition CreateFromPairs(KeywordPair[] pairs){
            InvoicePosition invoice = new InvoicePosition();
            string Customer_name = "";
            string Customer_type = "";

            
            foreach(var tmp in pairs)
            {
                if (tmp.Key.GetString().Equals("ItemNumber"))
                {

                    invoice.ItemIdentifier = uint.Parse(System.Text.RegularExpressions.Regex.Replace(tmp.Value, @"[^0-9]+", ""));
                }
                if ( tmp.Key.GetString().Equals("ItemName"))
                {
                    invoice.ItemName = tmp.Value;
                }
                if (tmp.Key.GetString().Equals("AmountOrdered"))
                {
                    invoice.Orders = uint.Parse(System.Text.RegularExpressions.Regex.Replace(tmp.Value, @"[^0-9]+", ""));
                }
                if (tmp.Key.GetString().Equals("NetPrice"))
                {
                    var str = System.Text.RegularExpressions.Regex.Replace(tmp.Value, @"[^\d.\d]", "");
                    if (System.Text.RegularExpressions.Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                        invoice.SingleUnitPrice = decimal.Parse(str);
                }
                if (tmp.Key.GetString().Equals("CustomerName"))
                {
                    Customer_name = tmp.Value;
                }
                if (tmp.Key.GetString().Equals("CustomerType"))
                {
                    Customer_type = tmp.Value;
                }
            }
            invoice.Customer = Customer.CreateCustomer(Customer_name, Customer_type);

            return invoice;
        }
    }
}