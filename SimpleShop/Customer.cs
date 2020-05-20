namespace SimpleShop{
    
    public class Customer{
        public const decimal ValueAddedTax = 0.19m;
        public string Name = "";
        public virtual decimal CalculatePrice(decimal basePrice){
            return (1 + ValueAddedTax) * basePrice;
        }

        public static Customer CreateCustomer(string name, string customerType=""){
            Customer customer = new Customer();
            customer.Name = name;
            return customer;
        }
    }
}