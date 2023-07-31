using System;
using System.IO;
using System.Collections.Generic;


namespace SimpleShop
{
    public static class SimpleShop
    {
        public static string[] ReadFileLineByLine(string path){
            var reader = new System.IO.StreamReader(path);
            var line_counter = 0;
            var needed_space = 0;
            // determine number of lines to create the correct sized of array
            for (var line = ""; line != null; line = reader.ReadLine(), ++line_counter){
                if (line.Length > 0 && line[0] != '#'){
                    ++needed_space;
                }
            }

            // Set Position to beginning of file
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            reader.DiscardBufferedData();
            
            // Read actual data
            var lines = new string[needed_space];
            
            for (var tag_lines=0; line_counter > 1; --line_counter){
                var tmp = reader.ReadLine();
                if (tmp[0] == '#'){continue;}
                lines[tag_lines++] = tmp;
            }
            return lines;
        }

        static void PrintWelcome(){
            var tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("#########################################\n" +
                          "#\t\t\t\t\t#\n" +
                          "#\tWelcome to the SimpleShop\t#\n" +
                          "#\t\t\t\t\t#\n#" +
                          "########################################\n\n");
            Console.ForegroundColor = tmp;
        }

        static void PrintInvoice(InvoicePosition ivp){
            Console.WriteLine(String.Join(", ",new string[]{
                ivp.Customer.Name, ivp.ItemName,ivp.Orders.ToString(), ivp.Price().ToString("0.##")
            }));
        }
        

        public static int Main(string[] args){
            /// Here to be correct
            args = new string[1] { "../../../../SimpleShop.Test/SampleOrder.tag" };
            
            if (args.Length != 1){
                Console.WriteLine("That is not how you use this shop!");
                return 1;
            }
            
            if (!File.Exists(args[0])){
                ReadFileLineByLine(args[0]);
                Console.WriteLine("Orders not found!");
                return 1;
            }
            
            PrintWelcome();

            var orders = ReadFileLineByLine(args[0]);

            Console.WriteLine("Invoices:");

            Keyword tag1 = new Keyword("ItemNumber",KeywordTypes.Int);
            Keyword tag2 = new Keyword("ItemName", KeywordTypes.String);
            Keyword tag3 = new Keyword("CustomerName", KeywordTypes.String);
            Keyword tag4 = new Keyword("AmountOrdered", KeywordTypes.Int);
            Keyword tag5 = new Keyword("NetPrice", KeywordTypes.Decimal);
            Keyword[] tags = { tag1, tag2, tag3, tag4, tag5 };

            ShopParser sp_test = new ShopParser();
            sp_test.SetKeywords(tags);
            

            List<KeywordPair[]> Key_4_Invoice = new List<KeywordPair[]> ();
            List<InvoicePosition> Invoices = new List<InvoicePosition>();
            foreach (var order in orders)
            {
                KeywordPair[] keyword4Invoice = ShopParser.ExtractFromTAG(sp_test, order);
                InvoicePosition invoice = InvoicePosition.CreateFromPairs(keyword4Invoice);
                PrintInvoice(invoice);

                Invoices.Add(invoice);
                Key_4_Invoice.Add(keyword4Invoice);
            }

            //#############################################################################
            //# Code to modify starts here:
            //# (2) Setup the ShopParser
            //# (2) Parse the "orders"
            //# (3) Create Invoices from "orders" (which should be in TAG format)
            //# (4) Output a the sum for each customer, you must use the PrintInvoice function
            //#############################################################################

            return 0;
        }
    }
}
