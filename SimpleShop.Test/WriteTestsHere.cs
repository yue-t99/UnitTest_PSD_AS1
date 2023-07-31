using NUnit.Framework;
using SimpleShop;
// Remember [UnitOfWork_StateUnderTest_ExpectedBehaviour]

namespace SimpleShop.Test
{
    public class Tests
    {
        private string str1;
        private string initagfile;
        private Keyword tag1; 
        private KeywordTypes typ1;
        private Keyword[] tags;
        private ShopParser sp;
        private Customer cust;
        private InvoicePosition invoicePosition;
        private KeywordPair[] pairs;

        [SetUp]
        public void Setup(){
            str1 = "CustomerName";

            initagfile = "<ItemNumber>0</ItemNumber><ItemName>SpringRoll</ItemName><CustomerName>Test man</CustomerName><AmountOrdered>2</AmountOrdered><NetPrice>3.50</NetPrice>";

            tag1 = new Keyword("CustomerName", KeywordTypes.String);

            typ1 = KeywordTypes.String;

            tags = new Keyword[]{ 
                new Keyword("ItemNumber", KeywordTypes.Int),
                new Keyword("ItemName", KeywordTypes.String),
                new Keyword("CustomerName", KeywordTypes.String),
                new Keyword("AmountOrdered", KeywordTypes.Int),
                new Keyword("NetPrice", KeywordTypes.Decimal)
            };

            cust = Customer.CreateCustomer("Test man");

            invoicePosition = new InvoicePosition
            {
                Customer = new Customer(),
                ItemIdentifier = 0,
                ItemName = "SpringRoll",
                Orders = 2,
                SingleUnitPrice = 3.50m
            };

            sp = new ShopParser();

            pairs = new KeywordPair[]{
                new KeywordPair(new Keyword("ItemNumber",KeywordTypes.Int), "0"),
                new KeywordPair(new Keyword("ItemName",KeywordTypes.String), "SpringRoll"),
                new KeywordPair(new Keyword("CustomerName",KeywordTypes.String), "Test man"),
                new KeywordPair(new Keyword("AmountOrdered",KeywordTypes.Int), "2"),
                new KeywordPair(new Keyword("NetPrice",KeywordTypes.Decimal), "3.50")
            };

        }
        
        /// <summary>
        /// Check if the Keyword opening is modified with added <Keyword> 
        /// Rating 0
        /// </summary>
        [Test]
        [Category("Keyword")]
        public void Parsing_KeywordStartTag_AddedBraces(){
            string tmp = tag1.GetStart();
            int[] test = { 9, 2, 3, 4, 5, 8 };
            CodeSnippets.CodeSnippets.function2(test);
            Assert.AreEqual(tmp, "<CustomerName>");
        }
        
        /// <summary>
        /// Check if the Keyword closing is modified with added </Keyword>
        /// Rating 0
        /// </summary>
        [Test]
        [Category("Keyword")]
        public void Parsing_KeywordEndTag_AddedSlashAndBraces(){
            string tmp = tag1.GetEnd();
            Assert.AreEqual(tmp, "</CustomerName>");
        }
        
        /// <summary>
        /// Set the Keywords and check if they are valid.
        /// Rating 1
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_SetKeywords_OrderOfKeywordsIsCorrect(){
            sp.SetKeywords(tags);
            Assert.AreEqual(sp.GetKeywords(), tags);
            // Order Meaning?
        }
        
        /// <summary>
        /// Set the Keyword types and check if they are valid.
        /// Rating 0
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void ShopParser_SetKeyword_Typ()
        {
            sp.SetKeywords(new Keyword[]{ new Keyword (str1,typ1) });
            var tmp = sp.GetKeywords()[0];
            Assert.AreEqual(tmp.WhichType(), typ1);
        }
        
        
        /// <summary>
        /// Check if the parser works correctly. Make examples and see if you can find problems with the code.
        /// Literals represent KeywordPairs with different Keywords
        /// A B C D
        /// Rating 2
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_ValidFindings_True(){
            sp.SetKeywords(tags);
            var tmp = ShopParser.ExtractFromTAG(sp, initagfile);
            Assert.AreEqual(tmp, pairs);
        }
        
        /// <summary>
        /// Check if the parser works correctly. This time you should check if repetition invalidates the findings.
        /// A A B B C C
        /// Rating 2
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_InvalidatedFindingsWithRepeatedEntry_False(){
            Keyword[] tags1 = new Keyword[]
            {
                new Keyword("ItemNumber", KeywordTypes.Int),
                new Keyword("ItemNumber", KeywordTypes.Int),
                new Keyword("ItemName", KeywordTypes.String),
                new Keyword("ItemName", KeywordTypes.String),
                new Keyword("CustomerName", KeywordTypes.String),
                new Keyword("CustomerName", KeywordTypes.String),
                new Keyword("AmountOrdered", KeywordTypes.Int),
                new Keyword("AmountOrdered", KeywordTypes.Int),
                new Keyword("NetPrice", KeywordTypes.Decimal),
                new Keyword("NetPrice", KeywordTypes.Decimal)
            };
            sp.SetKeywords(tags1);
            var tmp = ShopParser.ExtractFromTAG(sp, initagfile);
            var tmp_TF = ShopParser.ValidateFindings(tmp);
            Assert.AreEqual(tmp_TF, false);
        }
        
        /// <summary>
        ///  Check if the parser works correctly. This time with circular keywords.
        /// A B C A
        /// Rating 2
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_InvalidatedFindingsCircular_False(){
            Keyword[] tags2 = new Keyword[]{
                new Keyword("ItemNumber", KeywordTypes.Int),
                new Keyword("ItemName", KeywordTypes.String),
                new Keyword("CustomerName", KeywordTypes.String),
                new Keyword("AmountOrdered", KeywordTypes.Int),
                new Keyword("NetPrice", KeywordTypes.Decimal),
                new Keyword("ItemNumber", KeywordTypes.Int)
            };
            sp.SetKeywords(tags2);
            var tmp = ShopParser.ExtractFromTAG(sp, initagfile);
            var tmp_TF = ShopParser.ValidateFindings(tmp);
            Assert.AreEqual(tmp_TF, false);
        }
        
        /// <summary>
        /// See Tagfile (SampleOrder.tag) for more information. Are the correct number of keywords recognized ? 
        /// Rating 1
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_KeywordsSetTagString_CorrectNumberOfEntries(){
            int len = System.Text.RegularExpressions.Regex.Matches(initagfile, "><").Count;
            len = len + 1;
            sp.SetKeywords(tags);
            var tmp = ShopParser.ExtractFromTAG(sp, initagfile);
            int i = 0;
            foreach( var t in tmp)
            {
                i = i + 1;
            }
            Assert.AreEqual(len, i);
        }
        
        /// <summary>
        /// Again consult the Tagfile for more information. The parsing should follow the order of the keywords you provided.
        /// Make sure to make it adaptable to different configurations.
        /// Rating 2
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_KeywordsSetTagString_ListOfProvidedTagsInOrder(){
            sp.SetKeywords(tags);
            var tmp = ShopParser.ExtractFromTAG(sp, initagfile);
            Assert.AreEqual(tmp[0].Key, pairs[0].Key);
            Assert.AreEqual(tmp[1].Key, pairs[1].Key);
            Assert.AreEqual(tmp[2].Key, pairs[2].Key);
            Assert.AreEqual(tmp[3].Key, pairs[3].Key);
            Assert.AreEqual(tmp[4].Key, pairs[4].Key);
        }

        /// <summary>
        /// Test if the VAT is calculated correctly for the Customer.CalculatePrice
        /// Rating 1
        /// </summary>
        [Test]
        [Category("Customer")]
        public void Invoice_CalculateNormalCustomer_AddValueAddedTax(){
            var tmp1 = cust.CalculatePrice(invoicePosition.SingleUnitPrice);
            var tmp2 = invoicePosition.SingleUnitPrice * decimal.Parse("1.19");
            Assert.AreEqual(tmp1, tmp2);
        }
        
        /// <summary>
        /// Test if the function CreateCustomer returns a customer
        /// Rating 0
        /// </summary>
        [Test]
        [Category("Customer")]
        public void Invoice_CreateCustomer_ReturnsCustomer(){
            var customer_test = Customer.CreateCustomer("TestMan");
            Customer tmp = new Customer();
            tmp.Name = "TestMan";
            Assert.AreEqual(customer_test.Name, tmp.Name);
        }
        
        /// <summary>
        /// Test if the InvoicePosition.Price calculates correctly:
        /// Provided Orders, NetPrice is set.
        /// Rating 1
        /// </summary>
        [Test]
        [Category("Invoice")]
        public void Invoice_OrdersAndNetPriceValid_CalculateCorrectPrice(){
            int orders = 2;
            decimal NetPrice = 3.50m;
            var tmp1 = orders * NetPrice * (1 + 0.19m);
            var tmp2 = invoicePosition.Price();
            Assert.AreEqual(tmp1, tmp2);
        }
    }
}