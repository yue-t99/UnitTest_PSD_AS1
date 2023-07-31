namespace SimpleShop{
    
    public class Customer{
        public const decimal ValueAddedTax = 0.19m;
        public string Name = "";

        public virtual decimal CalculatePrice(decimal basePrice){
            return (1 + ValueAddedTax) * basePrice;
        }

        public static Customer CreateCustomer(string name, string customerType=""){
            if (customerType.Equals("Company")|| customerType.Equals("SimpleShop.Company"))
            {
                Company company = new Company ();
                company.Name = name;
                return company;
            }
            else if (customerType.Equals("Student")||customerType.Equals("SimpleShop.Student"))
            {
                Student student = new Student ();
                student.Name = name;
                return student;
            }
            else
            {
                Customer customer = new Customer();
                customer.Name = name;
                return customer;
            }

        }





    }

    public class Company : Customer {
        public new const decimal ValueAddedTax = 0.00m;
        public override decimal CalculatePrice(decimal basePrice)
        {
            return (1 + ValueAddedTax) * basePrice;
        }
    }

    public class Student : Customer {
        public new const decimal ValueAddedTax = 0.19m;
        public const decimal Discount = 0.20m;
        public override decimal CalculatePrice(decimal basePrice)
        {
            return (1 - Discount) * (1 + ValueAddedTax) * basePrice;
        }
    }
}