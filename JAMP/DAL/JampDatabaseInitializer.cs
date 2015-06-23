//Added Namespaces
using System;
using System.Data.Entity;
using JAMP.Models;
using System.Collections.Generic;

namespace JAMP.DAL
{
    public class JampDatabaseInitializer :
         DropCreateDatabaseAlways<JampContext>               //re-creates every time the server starts
   // DropCreateDatabaseIfModelChanges<JampContext>       //re-creates every when model changes
    {
        //Date creation variables
        private static DateTime _baseCreatedAtDate;

        protected override void Seed(JampContext context)
        {
            SeedDatabase(context);
        }

        #region Construction Methods

        //Add data to the db context
        public static void SeedDatabase(JampContext context)
        {
            //                              YYYY, MM, DD HH mm ss
            _baseCreatedAtDate = new DateTime(2013, 9, 01, 9, 0, 0);

            #region Add Employee
            new List<Employee>
             {
                //              name        surname     tel     email                       role            password                                    image location                                  busID
                // EmployeeID No 1
                CreateEmployee( "John",     "Doe",      "021 737 4596",  "john@mail.com",            "Owner",    "5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8", "../../Images/ProfilePictures/photo.jpg",   1),
                CreateEmployee( "Jane",     "Doe",      "021 737 4596",  "jane@mail.com",            "Manager",  "5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8", "../../Images/ProfilePictures/photo.jpg",   1), // 2
                CreateEmployee( "Jeremy",   "Wilkinson","092 663 5371",  "jeremyw@mail.com",         "Cashier",  "5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8", "../../Images/ProfilePictures/photo.jpg",   1), // 3
                CreateEmployee( "Mandla",   "Mxolisi",  "093 452 7485",  "m.mandla@mail.com",        "Cashier",  "5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8", "../../Images/ProfilePictures/photo.jpg",   1)  // 4
                
             }.ForEach(a => context.Employees.Add(a));

            #endregion

            #region Add Business
            new List<Business>
             {
                 CreateBusiness("Doe's Retail Store", "21 Upper King Road", "Cape Town", "7700",  "mybus@email.com", "021 737 4596")         
       
             }.ForEach(a => context.Businesses.Add(a));

            context.SaveChanges(); //Save Employees and there business
            #endregion

            #region User Settings
            new List<SettingUser>
             {
                //          empID, autoUpdate, descp
                // EmpNo 1
                CreateSettingUser(1, "On",  "On"),     
                CreateSettingUser(2, "On",  "On"),     // 2 
                CreateSettingUser(3, "On",  "On"),     // 3 
                CreateSettingUser(4, "On",  "On"),     // 4    

             }.ForEach(s => context.SettingUsers.Add(s));

            #endregion

            #region Add Roles
            new List<Role>
             {
                 new Role {RoleName = "Owner"},
                 new Role {RoleName = "Manager"},
                 new Role {RoleName = "Cashier"}  
              
             }.ForEach(a => context.Roles.Add(a));
            #endregion

            #region Add Categories
            new List<Category>
            {
                                // name,        busID
                //CategoryID 1
                CreateCategory("Beverages", "2013-08-01, 10:00 am",        1),                
                CreateCategory("Bakery",  "2013-08-01, 10:00 am",          1), //2                
                CreateCategory("Fruit & Veg", "2013-08-01, 10:00 am",      1), //3                
                CreateCategory("Frozen Foods",  "2013-08-01, 10:00 am",    1), //4               
                CreateCategory("Dairy",     "2013-08-01, 10:00 am"    ,    1), //5                
                CreateCategory("Chips",      "2013-08-01, 10:00 am",       1), //6                
                CreateCategory("Candy & Biscuits", "2013-08-01, 10:00 am", 1), //7              
                CreateCategory("Toiletries",   "2013-08-01, 10:00 am",     1), //8                
                CreateCategory("Meat",       "2013-09-01, 10:00 am",       1), //9               
                CreateCategory("Canned Food",   "2013-10-01, 10:00 am",    1), //10               
                CreateCategory("Dry Grocery",   "2013-09-01, 10:00 am" ,   1), //11
                CreateCategory("Household",  "2013-08-10, 10:00 am",       1)  //12

            }.ForEach(c => context.Categories.Add(c));
            #endregion

            #region Add Products
            new List<Product> 
            {
                //                   brand,              name,                          size, quantity, sellingPrice, costPrice, reorderLvl,    creation                       description,       category  bus  emp    archived,         dateArchived    special
                //ProductNo 1
                CreateProduct("Fanta",            "Orange",                      "350ml",    50,     7.95M,       5M,        10,    "2013-09-09, 12:55 pm",      "Orange Flavoured soft drink",      1,      1,     1,  false,     null,                    "No"),
                CreateProduct("Fanta",            "Grape",                       "350ml",    60,     7.95M,       5M,        10,    "2013-10-09, 1:05 pm",       "Grape Flavoured soft drink",       1,      1,     1,  false,     null,                    "No"),    //2 
                CreateProduct("CocaCola",         "Coke",                        "350ml",    100,    7.95M,       5M,        20,    "2013-08-09, 1:00 pm",       "CocaCola soft drink",              1,      1,     1,  false,     null,                    "No"),    //3  
                CreateProduct("CocaCola",         "Coke",                        "2l",       43,     15.95M,      10M,       20,    "2013-09-09, 12:30 pm",      "CocaCola soft drink",              1,      1,     1,  false,     null,                    "No"),    //4     
                CreateProduct("Sprite",           "Sprite",                      "2l",       35,     15.95M,      10M,       10,    "2013-10-09, 12:35 pm",      "Sprite soft drink",                1,      1,     1,  false,     null,                    "No"),    //5 
                CreateProduct("Stoney",           "Ginger Beer",                 "2l",       30,     15.95M,      10M,       10,    "2013-08-09, 12:40 pm",      "Stoney Ginger beer soft drink",    1,      1,     1,  false,     null,                    "Yes"),   //6 
                CreateProduct("Spar-Letta",       "Creme Soda",                  "500ml",    45,     10.95M,      7M,        20,    "2013-09-09, 12:45 pm",      "Creme Soda soft drink",            1,      1,     1,  false,     null,                    "No"),    //7   
                CreateProduct("Albany",           "Brown Bread",                 "500g",     75,     10.95M,      8M,        30,    "2013-10-09, 12:00 pm",      "Albany brown loaf",                2,      1,     1,  false,     null,                    "No"),    //8 
                CreateProduct("Albany",           "White Bread",                 "500g",     63,     12.95M,      9M,        30,    "2013-08-09, 12:05 pm",      "Albany white loaf",                2,      1,     1,  false,     null,                    "No"),    //9  
                CreateProduct("Albany",           "Wholewheat",                  "500g",     47,     12.95M,      9M,        30,    "2013-09-09, 12:08 pm",      "Albany wholewheat loaf",           2,      1,     1,  false,     null,                    "No"),    //10  
                CreateProduct("Clover",           "Full Cream",                  "2l",       31,     20.95M,      12M,       20,    "2013-10-10, 2:18 pm",       "Fresh milk full cream",            5,      1,     1,  false,     null,                    "No"),    //11  
                CreateProduct("Clover",           "Low Fat",                     "2l",       24,     22.95M,      14M,       10,    "2013-08-11, 3:53 pm",       "Fresh low fat milk",               5,      1,     1,  false,     null,                    "No"),    //12
                CreateProduct("Farm Fresh",       "Apples",                      "Big",      0,      15.95M,      10M,       10,    "2013-09-10, 4:45 pm",       "Fresh Apples",                     3,      1,     1,  true,     "2013/10/01, 12:55 pm",   "No"),    //13 
                CreateProduct("WilderKlawer",     "Onions",                      "Medium",   15,     15.95M,      10M,       10,    "2013-10-10, 4:50 pm",       "Fresh onions",                     3,      1,     1,  false,     null,                    "No"),    //14
                CreateProduct("Farm Fresh",       "Potatoes",                    "Large",    20,     20.95M,      15M,       20,    "2013-08-10, 4:55 pm",       "Fresh potatoes",                   3,      1,     1,  false,     null,                    "No"),    //15
                CreateProduct("McCain",           "Mixed Veg",                   "1kg",      34,     20.95M,      15M,       10,    "2013-09-10, 5:00 pm",       "Mixed vegetables",                 4,      1,     1,  false,     null,                    "No"),    //16 
                CreateProduct("Simba",            "Salt & Vinegar",              "500g",     40,     12.95M,      10M,       20,    "2013-10-10, 5:05 pm",       "Bag of air with some chips",       6,      1,     1,  false,     null,                    "No"),    //17 
                CreateProduct("Simba",            "Smoked Beef",                 "500g",     40,     12.95M,      10M,       20,    "2013-08-10, 5:06 pm",       "Bag of air with some chips",       6,      1,     1,  false,     null,                    "No"),    //18
                CreateProduct("Lays",             "Cheese & Onion",              "500g",     50,     13.95M,      10M,       20,    "2013-09-10, 5:08 pm",       "Bag of air with some chips",       6,      1,     1,  false,     null,                    "No"),    //19 
                CreateProduct("Nestle",           "KitKat",                      "100g",     40,     7.95M,       5M,        20,    "2013-10-10, 5:10 pm",       "Have a break",                     7,      1,     1,  false,     null,                    "No"),    //20
                CreateProduct("Astros",           "Chocolate",                   "100g",     50,     7.95M,       5M,        20,    "2013-08-10, 5:11 pm",       "Space chocolate",                  7,      1,     1,  false,     null,                    "No"),    //21 
                CreateProduct("Chomp",            "Chocolate Bar",               "Small",    43,     1.95M,       0.50M,     20,    "2013-09-10, 5:15 pm",       "Original for kids",                7,      1,     1,  false,     null,                    "No"),    //22   
                CreateProduct("Fizzer",           "Sweet",                       "Small",    55,     1.95M,       1.00M,     20,    "2013-10-10, 10:00 am",      "Sticky sweets",                    7,      1,     1,  false,     null,                    "No"),    //23  
                CreateProduct("Smarties",         "Sweets",                      "100g",     60,     6.95M,       4M,        20,    "2013-08-10, 10:05 am",      "Chocolate sweets",                 7,      1,     1,  false,     null,                    "No"),    //24  
                CreateProduct("Tennis",           "Biscuits",                    "Medium",   50,     12.95M,      10M,       20,    "2013-09-10, 10:06 am",      "Tennis bisuits",                   7,      1,     1,  false,     null,                    "No"),    //25
                CreateProduct("Tea-Lovers",       "Bisuits",                     "Medium",   50,     11.50M,      9M,        20,    "2013-10-10, 10:09 am",      "Tea time biscuits",                7,      1,     1,  false,     null,                    "No"),    //26 
                CreateProduct("Marie",            "Bisuits",                     "Medium",   42,     10.00M,      8M,        20,    "2013-08-10, 10:10 am",      "Biscuits for old ladies",          7,      1,     1,  false,     null,                    "No"),    //27 
                CreateProduct("NikNaks",          "Chips",                       "500g",     56,     11.95M,      9M,        20,    "2013-09-10, 10:11 am",      "Cheese flavoured chips",           6,      1,     1,  false,     null,                    "No"),    //28   
                CreateProduct("Doritos",          "Original",                    "250g",     70,     5.50M,       3M,        30,    "2013-10-10, 10:12 am",      "Original flavour doritos",         6,      1,     1,  false,     null,                    "No"),    //29  
                CreateProduct("Doritos",          "Tangy Cheese",                "250g",     67,     5.50M,       3M,        30,    "2013-08-10, 10:15 am",      "Cheese flavour doritos",           6,      1,     1,  false,     null,                    "No"),    //30
                CreateProduct("Doritos",          "Chilli Heatwave",             "250g",     74,     5.50M,       3M,        30,    "2013-09-10, 10:17 am",      "Chilli flavour doritos",           6,      1,     1,  false,     null,                    "No"),    //31  
                CreateProduct("Lays",             "Sour Cream & Onion",          "250g",     25,     5.50M,       3M,        30,    "2013-10-10, 10:19 am",      "Lays sour cream and onion",        6,      1,     1,  false,     null,                    "No"),    //32 
                CreateProduct("I&J",              "Fish Fingers",                "5005",     30,     22.95M,      18M,       20,    "2013-08-10, 10:20 am",      "Frozen fish fingers",              4,      1,     1,  false,     null,                    "No"),    //33 
                CreateProduct("I&J",              "Light & Crispy",              "750g",     40,     55.95M,      45M,       20,    "2013-09-10, 10:23 am",      "Frozen fish portions",             4,      1,     1,  false,     null,                    "No"),    //34 
                CreateProduct("Eskort",           "Breakfast Sausages",          "375g",     35,     34.95M,      25M,       30,    "2013-10-10, 10:25 am",      "Quality breakfast sausage pack",   4,      1,     1,  false,     null,                    "No"),    //35 
                CreateProduct("Fry's",            "Beef Pattis",                 "320g",     20,     29.95M,      20M,       17,    "2013-08-10, 10:26 am",      "Good beef burger pattis",          4,      1,     1,  false,     null,                    "No"),    //36  
                CreateProduct("No Name",          "Frozen Chicken",              "1.5kg",    38,     41.95M,      35M,       30,    "2013-09-10, 10:28 am",      "Frozen chicken portions",          4,      1,     1,  false,     null,                    "No"),    //37 
                CreateProduct("No Name",          "Chicken Liver",               "500g",     20,     13.95M,      10M,       30,    "2013-10-10, 10:30 am",      "Frozen chicken liver",             4,      1,     1,  false,     null,                    "No"),    //38  
                CreateProduct("Eskort",           "Chicken Viennas",             "500g",     45,     27.95M,      20M,       30,    "2013-08-10, 10:32 am",      "Vienna packs",                     9,      1,     1,  false,     null,                    "No"),    //39 
                CreateProduct("Bokkie",           "Polony",                      "750g",     14,     19.95M,      15M,       20,    "2013-09-10, 10:33 am",      "Delicious polony",                 9,      1,     1,  false,     null,                    "No"),    //40 
                CreateProduct("Enterprise",       "Bacon",                       "250g",     22,     27.95M,      20M,       20,    "2013-10-10, 10:35 am",      "Tasty bacon",                      9,      1,     1,  false,     null,                    "No"),    //41 
                CreateProduct("Deli",             "Roast Beef",                  "1Kg",      15,     35.95M,      20M,       20,    "2013-08-10, 10:37 am",      "Sliced roast beef",                9,      1,     1,  false,     null,                    "No"),    //42
                CreateProduct("Championship",     "Boerewors",                   "750g",     30,     19.95M,      12M,       20,    "2013-09-10, 10:39 am",      "SA's number 1",                    9,      1,     1,  false,     null,                    "No"),    //43
                CreateProduct("Karoo",            "Wors",                        "750g",     13,     15.95M,      10M,       20,    "2013-09-10, 10:41 am",      "Good wors",                        9,      1,     1,  false,     null,                    "No"),    //44
                CreateProduct("Sunlight",         "Bar Soap",                    "Small",    24,     4.95M,       2M,        20,    "2013-08-10, 10:43 am",      "Quality Soap",                     8,      1,     1,  false,     null,                    "No"),    //45 
                CreateProduct("Shield",           "Deodorant",                   "Small",    32,     9.95M,       6M,        25,    "2013-09-10, 10:45 am",      "Good deodorant roll on",           8,      1,     1,  false,     null,                    "No"),    //46  
                CreateProduct("Radox",            "Body Wash",                   "250ml",    20,     28.95M,      20M,       20,    "2013-10-10, 10:47 am",      "Body wash soap",                   8,      1,     1,  false,     null,                    "No"),    //47  
                CreateProduct("Twinsaver",        "2 Ply Toilet Paper",          "9 pack",   17,     47.95M,      35M,       20,    "2013-08-10, 10:50 am",      "Toilet paper",                     8,      1,     1,  false,     null,                    "No"),    //48
                CreateProduct("Nivea",            "Body Lotion",                 "400ml",    29,     39.95M,      25M,       20,    "2013-09-10, 10:52 am",      "Good lotion",                      8,      1,     1,  false,     null,                    "No"),    //49  
                CreateProduct("Vaseline",         "Body Cream",                  "400ml",    15,     35.95M,      20M,       20,    "2013-10-10, 10:54 am",      "Classic body cream",               8,      1,     1,  false,     null,                    "No"),    //50 
                CreateProduct("Clere",            "Petroleum Jelly",             "250ml",    21,     18.95M,      12M,       20,    "2013-08-10, 10:55 am",      "Good lotion",                      8,      1,     1,  false,     null,                    "No"),    //51
                CreateProduct("Aquafresh",        "Toothpaste",                  "100ml",    15,     12.95M,      8M,        20,    "2013-09-10, 10:57 am",      "Quality oral care",                8,      1,     1,  false,     null,                    "No"),    //52 
                CreateProduct("Colgate",          "Total Advance",               "75ml",     25,     14.95M,      8M,        20,    "2013-09-10, 10:59 am",      "Good toothpaste",                  8,      1,     1,  false,     null,                    "No"),    //53 
                CreateProduct("Luck Star",        "Pilchards",                   "200g",     29,     8.95M,       6M,        20,    "2013-08-10, 11:00 am",      "Canned fish",                      10,     1,     1,  false,     null,                    "No"),    //54 
                CreateProduct("Koo",              "Baked Beans",                 "300g",     30,     9.95M,       6M,        20,    "2013-09-10, 11:03 am",      "Canned beans",                     10,     1,     1,  false,     null,                    "No"),    //55 
                CreateProduct("All Gold",         "Chunky Tomato",               "350g",     19,     9.95M,       7M,        20,    "2013-09-10, 11:05 am",      "Canned tomatoes",                  10,     1,     1,  false,     null,                    "No"),    //56 
                CreateProduct("Goldcrest",        "Pease & Carrots",             "400g",     23,     21.95M,      15M,       20,    "2013-08-10, 11:06 am",      "Canned Veges",                     10,     1,     1,  false,     null,                    "No"),    //57 
                CreateProduct("White Star",       "Super Maize Meal",            "1kg",      40,     9.95M,       6M,        30,    "2013-09-10, 11:09 am",      "Maize meal",                       11,     1,     1,  false,     null,                    "No"),    //58 
                CreateProduct("Iwisa",            "Maize Meal",                  "1kg",      40,     6.95M,       4M,        30,    "2013-09-10, 11:10 am",      "Maize meal",                       11,     1,     1,  false,     null,                    "No"),    //59 
                CreateProduct("Ace",              "Maize Meal",                  "1kg",      36,     7.95M,       4M,        30,    "2013-08-10, 11:11 am",      "Maize meal",                       11,     1,     1,  false,     null,                    "No"),    //60
                CreateProduct("Impala",           "Super Maize",                 "1kg",      13,     5.50M,       3M,        30,    "2013-09-10, 11:13 am",      "cheap maize meal",                 11,     1,     1,  false,     null,                    "No"),    //61
                CreateProduct("Clover",           "Condensed Milk",              "355ml",    12,     18.95M,      15M,       20,    "2013-10-10, 11:15 am",      "Condensed Milk",                   5,      1,     1,  false,     null,                    "No"),    //62 
                CreateProduct("Five Roses",       "Tea",                         "100 pack", 25,     24.95M,      18M,       20,    "2013-08-10, 11:17 am",      "Best tea",                         11,     1,     1,  false,     null,                    "No"),    //63 
                CreateProduct("Freshpak",         "Rooibos Tea",                 "80 pack",  30,     16.95M,      10M,       20,    "2013-09-10, 11:18 am",      "Rooibos tea",                      11,     1,     1,  false,     null,                    "No"),    //64  
                CreateProduct("Nestle",           "Nescafe Original Cappucino",  "185g",     27,     42.95M,      35M,       20,    "2013-10-10, 11:20 am",      "Nescafe capuccino",                11,     1,     1,  false,     null,                    "No"),    //65 
                CreateProduct("Nestle",           "Ricoffy",                     "250g",     32,     27.95M,      22M,       20,    "2013-08-10, 11:21 am",      "Ricoffy coffee",                   11,     1,     1,  false,     null,                    "No"),    //66 
                CreateProduct("Nestle",           "Ricoffy",                     "750g",     20,     61.95M,      55M,       20,    "2013-09-10, 11:22 am",      "Ricoffy coffee",                   11,     1,     1,  false,     null,                    "No"),    //67 
                CreateProduct("Nescafe",          "Classic Decaff",              "200g",     34,     83.95M,      75M,       20,    "2013-10-10, 11:25 am",      "decaff coffee",                    11,     1,     1,  false,     null,                    "No"),    //68
                CreateProduct("Canola",           "Cooking Oil",                 "2l",       15,     42.95M,      35M,       20,    "2013-08-10, 11:26 am",      "Cooking oil",                      11,     1,     1,  false,     null,                    "No"),    //69  
                CreateProduct("Canola",           "Cooking Oil",                 "750ml",    30,     17.95M,      12M,       20,    "2013-09-10, 11:28 am",      "Cooking oil",                      11,     1,     1,  false,     null,                    "No"),    //70  
                CreateProduct("Tastic",           "Rice",                        "1kg",      45,     13.95M,      8M,        20,    "2013-10-10, 11:28 am",      "Good rice",                        11,     1,     1,  false,     null,                    "No"),    //71 
                CreateProduct("Knorr",            "Brown Onion Soup",            "45g",      50,     3.55M,       1.50M,     20,    "2013-08-10, 11:30 am",      "Great soup",                       11,     1,     1,  false,     null,                    "No"),    //72 
                CreateProduct("Knorr",            "Beef & Onion Soup",           "67g",      36,     3.55M,       1.50M,     20,    "2013-09-10, 11:32 am",      "Great soup",                       11,     1,     1,  false,     null,                    "No"),    //73 
                CreateProduct("Knorr",            "Beef & Tomato Soup",          "51g",      33,     3.55M,       1.50M,     20,    "2013-10-10, 11:34 am",      "Great soup",                       11,     1,     1,  false,     null,                    "No"),    //74 
                CreateProduct("Knorr",            "Chakalaka Soup",              "58g",      41,     3.55M,       1.50M,     20,    "2013-08-10, 11:35 am",      "Great soup",                       11,     1,     1,  false,     null,                    "No"),    //75   
                CreateProduct("Knorr",            "Chicken Noodle Soup",         "50g",      25,     3.55M,       1.50M,     20,    "2013-09-10, 11:37 am",      "Great soup",                       11,     1,     1,  false,     null,                    "No"),    //76    
                CreateProduct("Knorr",            "Cream of Chicken",            "50g",      25,     3.55M,       1.50M,     20,    "2013-10-10, 11:38 am",      "Great soup",                       11,     1,     1,  false,     null,                    "No"),    //77 
                CreateProduct("Knorr",            "Beef & Vegetable Soup",       "50g",      27,     3.55M,       1.50M,     20,    "2013-08-10, 11:39 am",      "Great soup",                       11,     1,     1,  false,     null,                    "No"),    //78
                CreateProduct("Knorr",            "Mutton & Vegetable Soup",     "62g",      22,     3.55M,       1.50M,     20,    "2013-09-10, 11:40 am",      "Great soup",                       11,     1,     1,  false,     null,                    "No"),    //79 
                CreateProduct("Knorr",            "Minstrone Soup",              "62g",      26,     3.55M,       1.50M,     20,    "2013-10-10, 11:42 am",      "Great soup",                       11,     1,     1,  false,     null,                    "No"),    //80   
                CreateProduct("Knorr",            "Hearty Beef Soup",            "64g",      20,     3.55M,       1.50M,     20,    "2013-08-10, 11:44 am",      "Great soup",                       11,     1,     1,  false,     null,                    "No"),    //81 
                CreateProduct("All Gold",         "Tomato Sauce",                "350ml",    19,     13.95M,      8M,        15,    "2013-09-10, 11:46 am",      "Tomato sauce",                     11,     1,     1,  false,     null,                    "No"),     //82 
                CreateProduct("All Gold",         "Tomato Sauce",                "700ml",    20,     15.95M,      10M,       15,    "2013-10-10, 11:47 am",      "Tomato sauce",                     11,     1,     1,  false,     null,                    "No"),     //83
                CreateProduct("Maggi",            "Worcestershire Sauce",        "250ml",    22,     15.95M,      10M,       15,    "2013-08-10, 11:49 am",      "General sauce",                    11,     1,     1,  false,     null,                    "No"),     //84
                CreateProduct("Mrs Balls",        "Original Chutney",            "470g",     17,     15.95M,      10M,       15,    "2013-09-10, 11:50 am",      "Chutney sauce",                    11,     1,     1,  false,     null,                    "No"),     //85
                CreateProduct("Fatti's & Moni's", "Macaroni",                    "500g",     10,     10.25M,      7M,        15,    "2013-10-10, 11:52 am",      "Good macaroni",                    11,     1,     1,  false,     null,                    "No"),     //86
                CreateProduct("Fatti's & Moni's", "Spaghetti",                   "500g",     17,     10.25M,      7M,        15,    "2013-08-10, 11:53 am",      "Good spaghetti",                   11,     1,     1,  false,     null,                    "No"),     //87 
                CreateProduct("Maggi",            "2 Min Noodles Beef",          "73g",      23,     2.95M,       1M,        15,    "2013-09-10, 11:55 am",      "For tough times",                  11,     1,     1,  false,     null,                    "No"),     //88 
                CreateProduct("Maggi",            "2 Min Noodles Chicken",       "73g",      30,     2.95M,       1M,        15,    "2013-10-10, 11:57 am",      "Great noodles",                    11,     1,     1,  false,     null,                    "No"),     //89 
                CreateProduct("Maggi",            "2 Min Noodles Cheese",        "73g",      30,     2.95M,       1M,        15,    "2013-08-10, 11:58 am",      "Great noodles",                    11,     1,     1,  false,     null,                    "No"),     //90  
                CreateProduct("Maggi",            "2 Min Noodles Pizza",         "73g",      30,     2.95M,       1M,        15,    "2013-09-10, 12:00 pm",      "Great noodles",                    11,     1,     1,  false,     null,                    "No"),     //91  
                CreateProduct("Maggi",            "2 Min Noodles Boerewors",     "73g",      30,     2.95M,       1M,        15,    "2013-10-10, 12:02 pm",      "Great noodles",                    11,     1,     1,  false,     null,                    "No"),     //92
                CreateProduct("First Choice",     "Custard",                     "1l",       29,     19.95M,      15M,       20,    "2013-08-10, 12:04 pm",      "Good custard",                     11,     1,     1,  false,     null,                    "No"),     //93 
                CreateProduct("Ultramel",        "Vanilla Custard",              "500ml",    24,     18.95M,      15M,       20,    "2013-09-10, 12:05 pm",      "Great custard",                    11,     1,     1,  false,     null,                    "No"),     //94 
                CreateProduct("Greens",           "Quick Jelly Red",             "70g",      32,     13.25M,      9M,        15,    "2013-10-10, 12:08 pm",      "Good jelly",                       11,     1,     1,  false,     null,                    "No"),     //95 
                CreateProduct("Greens",          "Quick Jelly Green",            "70g",      28,     13.25M,      9M,        15,    "2013-08-10, 12:09 pm",      "Good jelly",                       11,     1,     1,  false,     null,                    "No"),     //96  
                CreateProduct("All Gold",        "Apricot Jam",                  "450g",     32,     13.50M,      9M,        15,    "2013-09-10, 12:10 pm",      "Jam for bread",                    11,     1,     1,  false,     null,                    "No"),     //97 
                CreateProduct("Black Cat",       "Smooth Peanut Butter",         "400g",     19,     23.95M,      15M,       15,    "2013-10-10, 12:11 pm",      "Peanut butter",                    11,     1,     1,  false,     null,                    "No"),     //98 
                CreateProduct("Black Cat",       "Crunchy Peanut Butter",        "400g",     21,     23.95M,      15M,       15,    "2013-08-10, 12:12 pm",      "Peanut butter",                    11,     1,     1,  false,     null,                    "No"),     //99  
                CreateProduct("Knorr Aromat",    "Original Aromat Cannister",    "75g",      18,     11.25M,      8M,        15,    "2013-09-10, 12:15 pm",      "Aromat seasoning in cannister",    11,     1,     1,  false,     null,                    "No"),     //100 
                CreateProduct("Knorr Aromat",    "Sachet Pack",                  "400g",     20,     24.95M,      18M,       15,    "2013-10-10, 12:17 pm",      "Aromat seasoning in sachets",      11,     1,     1,  false,     null,                    "No"),     //101
                CreateProduct("Knorrox",         "Beef Cubes",                   "12",       14,     6.95M,       3M,        10,    "2013-08-10, 12:19 pm",      "Knorrox beef cubes",               11,     1,     1,  false,     null,                    "No"),     //102             
                CreateProduct("Knorrox",         "Chicken Stock Cubes",          "12",       22,     6.95M,       3M,        10,    "2013-09-10, 12:21 pm",      "Knorrox chicken stock",            11,     1,     1,  false,     null,                    "No"),     //103               
                CreateProduct("Robertsons",      "Barbeque Spice",               "35g",      37,     7.65M,       5M,        20,    "2013-10-10, 12:22 pm",      "Spice for braais and stuff",       11,     1,     1,  false,     null,                    "No"),     //104               
                CreateProduct("Roberstons",      "Chicken Spice",                "35g",      30,     7.65M,       5M,        20,    "2013-08-10, 12:24 pm",      "Spice for chicken",                11,     1,     1,  false,     null,                    "No"),     //105                
                CreateProduct("Rajah",           "Hot Curry Powder",             "100g",     0,      14.35M,      7M,        20,    "2013-09-10, 12:26 pm",      "Curry powder",                     11,     1,     1,  false,     null,                    "No"),     //106               
                CreateProduct("Rajah",           "Medium Curry Powder",          "100g",     0,      14.35M,      7M,        20,    "2013-10-10, 12:27 pm",      "Curry powder",                     11,     1,     1,  false,     null,                    "No"),     //108                
                CreateProduct("Rajah",           "All-In-One Curry Powder",      "100g",     1,      14.35M,      7M,        20,    "2013-08-10, 12:29 pm",      "Curry powder",                     11,     1,     1,  false,     null,                    "No"),     //109                
                CreateProduct("Rajah",           "Mild & Spicy Curry Powder",    "100g",     2,      14.35M,      7M,        20,    "2013-09-10, 12:31 pm",      "Curry powder",                     11,     1,     1,  false,     null,                    "No"),     //110               
                CreateProduct("Cerebos",         "Iodated Sea Salt",             "1kg",      19,     9.95M,       5M,        15,    "2013-10-10, 12:32 pm",      "Iodated salt",                     11,     1,     1,  false,     null,                    "No"),     //110               
                CreateProduct("Cerebos",         "Iodated Table Salt",           "1kg",      23,     9.95M,       5M,        15,    "2013-08-10, 12:34 pm",      "Iodated salt",                     11,     1,     1,  false,     null,                    "No"),     //111
                CreateProduct("Huletts",         "Brown Sugar",                  "500g",     14,     5.55M,       3M,        15,    "2013-09-10, 12:36 pm",      "Brown sugar",                      11,     1,     1,  false,     null,                    "No"),     //112
                CreateProduct("Huletts",         "Brown Sugar",                  "1kg",      17,     10.75M,      6M,        15,    "2013-10-10, 12:38 pm",      "Brown sugar",                      11,     1,     1,  false,     null,                    "No"),     //113
                CreateProduct("Huletts",         "White Sugar",                  "500g",     12,     6.50M,       3.5M,      15,    "2013-08-10, 12:40 pm",      "White sugar",                      11,     1,     1,  false,     null,                    "No"),     //114               
                CreateProduct("Huletts",         "White Sugar",                  "1kg",      16,     12.50M,      7M,        15,    "2013-09-10, 12:43 pm",      "White sugar",                      11,     1,     1,  false,     null,                    "No"),     //113                
                CreateProduct("Bokomo",          "Weet Bix",                     "900g",     17,     32.95M,      20M,       10,    "2013-10-10, 12:45 pm",      "Fine cereal",                      11,     1,     1,  false,     null,                    "No"),     //116                
                CreateProduct("Jungle Oats",     "Oatmeal",                      "500g",     14,     12.95M,      7M,        10,    "2013-08-10, 12:47 pm",      "Energy breakfast",                 11,     1,     1,  false,     null,                    "No"),     //117               
                CreateProduct("Kellogg's",       "All Bran Flakes",              "300g",     13,     22.95M,      15M,       10,    "2013-09-10, 12:49 pm",      "Get it all",                       11,     1,     1,  false,     null,                    "No"),     //118               
                CreateProduct("Kellogg's",       "Corn Flakes",                  "500g",     17,     30.50M,      20M,       10,    "2013-10-10, 12:51 pm",      "Good cereal",                      11,     1,     1,  false,     null,                    "No"),     //119             
                CreateProduct("Kellogg's",       "Rice Krispies",                "400g",     15,     33.95M,      20M,       15,    "2013-08-10, 12:53 pm",      "Snap, crackle pop",                11,     1,     1,  false,     null,                    "No"),     //120               
                CreateProduct("Bull Brand",      "Corned Meat",                  "300g",     29,     17.95M,      12M,       15,    "2013-09-10, 12:55 pm",      "Canned meat",                      10,     1,     1,  false,     null,                    "No"),     //121               
                CreateProduct("Bull Brand",      "Meatballs",                    "400g",     30,     15.95M,      10M,       15,    "2013-10-10, 12:57 pm",      "Canned meatballs",                 10,     1,     1,  false,     null,                    "No"),     //122                
                CreateProduct("Lucky Star",      "Tuna Chunks",                  "170g",     0,      14.95M,      10M,       15,    "2013-08-10, 12:59 pm",      "Canned tuna",                      10,     1,     1,  false,     null,                    "No"),     //123                
                CreateProduct("Appletiser",      "Sparkling Apple Juice",        "350ml",    27,     9.95M,       6M,        20,    "2013-09-10, 13:00 pm",      "Appletiser juice",                 1,      1,     1,  false,     null,                    "No"),     //124             
                CreateProduct("Fanta",           "Pinapple",                     "350ml",    30,     7.95M,       5M,        10,    "2013-10-10, 13:01 pm",      "Soft drink",                       1,      1,     1,  false,     null,                    "No"),     //125                
                CreateProduct("Appletiser",      "Sparkling Grape Juice",        "350ml",    27,     9.95M,       6M,        20,    "2013-08-10, 13:03 pm",      "Appletiser juice",                 1,      1,     1,  false,     null,                    "No"),     //126               
                CreateProduct("Fanta",           "Pinapple",                     "2l",       30,     15.95M,      10M,       10,    "2013-09-10, 13:04 pm",      "Soft drink",                       1,      1,     1,  false,     null,                    "No"),     //127
                CreateProduct("CocaCola",        "Coke",                         "1l",       37,     11.95M,      7M,        20,    "2013-10-10, 13:05 pm",      "CocaCola soft drink",              1,      1,     1,  false,     null,                    "No"),     //128

            }.ForEach(p => context.Products.Add(p));
            #endregion

            #region Add Supplier
            new List<Supplier> 
            {
                //              Name,                   address,                email,                      contact,           desc                             location            postal    busID, empID, 
                // SupplierNo 1
                CreateSupplier("Boxer Store",           "21 Barrow Road",       "boxerstorect@boxer.co.za",    "021-123-4567",  "Large Retailer",                  "Cape Town",  "7500",    "2013-08-09, 10:00 am",       1,   1),
                CreateSupplier("Checkers Hyper",        "34 Frans Conradie Dr", "checkersct@live.co.za",       "021-777-3457",  "Large Retailer",                  "Cape Town",    "7460", "2013-09-01, 10:00 am",        1,   1),    // 2
                CreateSupplier("Sonny's Store",         "59 Tonner North",      "sonny@gmail.com",             "092-124-4558",  "Local supplier",                  "Cape Town",     "7700", "2013-10-01, 10:00 am",       1,   1),    // 3
                CreateSupplier("FMCG Wholesalers",       "10 Woolsack Drive",    "fmcg@gmail.co.za",           "021-664-4596",  "Local wholesales",                "Cape Town",    "7700", "2013-08-01, 10:00 am",         1,   1)     // 4
            
            }.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges(); //Save suppliers

            new List<SupplierAccount>
            {
                //                Supplier   name    balance  limit comments  payment   
                // AccountNo 1
                CreateSupplierAccount(1, "BO-1110",  1500,    2500,   "",    "2012-01-05"),  
                CreateSupplierAccount(2, "CH-1113",  1000,    2500,   "",    "2012-01-05"),  // 2
                CreateSupplierAccount(3, "SO-1112",   1850,    2500,   "",    "2012-01-05"),  // 3
                CreateSupplierAccount(4, "FM-1115",    1100,    2500,   "",    "2012-01-05")   // 4
            }.ForEach(s => context.SupplierAccounts.Add(s));
            context.SaveChanges(); //Save suppliersAccounts

            new List<SupplierPayment>
            {
                //                   created            amount  ref         account, emp
                // Payment 1
                CreateSupplierPayment("2012-08-05, 2:45 pm",     1500,   "RFZ10X5",   1,      1),
                CreateSupplierPayment("2012-09-05, 2:45 pm",     900,   "RFZ10X5",   2,      1),
                CreateSupplierPayment("2012-10-05, 2:45 pm",     1500,   "RFZ10X5",   3,      1)
            }.ForEach(s => context.SupplierPayments.Add(s));

            #endregion

            #region Add Orders
            new List<Order> 
            {
                //              Created,        DateDue,    TCost,       completed,      busID Supplier  empID
                // OrderNo 1
                CreateOrder("2013-04-02, 2:45 pm",   "2013-04-10",    200,     "Completed",       1,       1,      1),
                CreateOrder("2013-04-28, 10:43 pm",  "2013-05-02",    300,     "Partial",         1,       1,      2),    // 2
                CreateOrder("2013-05-01, 8:45 pm",   "2013-05-09",    300,     "Completed",       1,       2,      1),    // 3
                CreateOrder("2013-05-22, 9:42 pm",   "2013-05-29",    300,     "Outstanding",     1,       3,      1),    // 4
                CreateOrder("2013-06-02, 11:47 pm",  "2013-06-11",    300,     "Outstanding",     1,       2,      2),    // 5
                CreateOrder("2013-08-02, 12:35 pm",  "2011-08-09",    300,     "Completed",       1,       1,      1),    // 6
                CreateOrder("2013-09-02, 3:55 pm",   "2013-09-12",    300,     "Completed",       1,       3,      1),    // 7
                CreateOrder("2013-09-13, 3:55 pm",   "2013-10-09",    400,     "Outstanding",       1,       3,      1)   // 8

            }.ForEach(o => context.Orders.Add(o));

            context.SaveChanges(); //Save orders

            new List<ProductOrder> 
            {
                //              order, product, quantity, quantity delivered, costPrice
                // OrderID 1
                CreateProductOrder(1,    1,      15,    15,  5),                               
                CreateProductOrder(2,    2,      15,    15,  5),    // 2 [Can deliver]         
                CreateProductOrder(2,    3,      15,    10,  5),    //Partial                  
                CreateProductOrder(2,    4,      20,    0,   5),                               
                CreateProductOrder(2,    5,      5,     0,   5),                               
                CreateProductOrder(3,    1,      10,    10,  5),    // 3                       
                CreateProductOrder(4,    1,      15,    0,   5),    // 4 [Can deliver]         
                CreateProductOrder(4,    2,      18,    0,   5),                               
                CreateProductOrder(5,    4,      30,    0,   5),    // 5 [Can deliver]         
                CreateProductOrder(5,    5,      25,    0,   5),                               
                CreateProductOrder(6,    23,     30,    0,   5),    // 6 [Can deliver]         
                CreateProductOrder(6,    8,      25,    0,   5),                               
                CreateProductOrder(7,    10,     30,    0,   5),    // 7 [Can deliver]         
                CreateProductOrder(7,    12,     25,    0,   5),    
                CreateProductOrder(8,    11,     30,    0,   5),    // 8 [Can deliver]         
                CreateProductOrder(8,    20,     25,    0,   5), 
                CreateProductOrder(8,    45,     25,    0,   5), 
            
            }.ForEach(po => context.ProductOrders.Add(po));
            #endregion

            #region Add Delivery
            new List<Delivery>
            {
                //                  created,          EmpID, OrderID
                // DeliveryNO 1
                CreateDelivery("2013-04-11, 4:23 pm",   1 ,    1),                
                CreateDelivery("2013-05-02, 10:23 am",  1 ,    2),    // 2               
                CreateDelivery("2013-05-09, 1:23 pm",   1 ,    2),    // 3                
                CreateDelivery("2013-05-09, 3:23 pm",   1 ,    3),    // 4
                CreateDelivery("2013-05-18, 2:23 pm",   1 ,    3),    // 5

            }.ForEach(d => context.Deliveries.Add(d));

            context.SaveChanges(); //Save Deliveries

            new List<ProductDelivery> 
            {
                //               delivery, product, quantity 
                // DeliveryNo 1
                CreateProductDelivery(1,     1,      10),               
                CreateProductDelivery(2,     1,      5),    // 2
                CreateProductDelivery(3,     1,      10),   // 3                
                CreateProductDelivery(4,     2,      15),   // 4               
                CreateProductDelivery(5,     4,      5)     // 5
            
            }.ForEach(pd => context.ProductDeliveries.Add(pd));
            #endregion

            #region Add Customers
            new List<Customer>
            {
                //              Name,       Surname,        Contact,            Address,        PostCode,       Email,              Business,     Account,   emp
                // Customer No 1
                CreateCustomer("Themba",    "Ndlovu",   "092-659-8945",     "21 Barrow Road",       "7700",     "themz@mail.com",       "2013-08-01, 10:00 am"    ,1,             1),    
                CreateCustomer("Esetu",     "Jozi",     "092-700-9054",     "59 Barrow North",      "7700",     "joziace@mail.com",     "2013-08-02, 10:00 am"    ,1,             1),    // 2
                CreateCustomer("Papa",      "Cheba",    "082-895-8930",     "6 Liesbeek Road",      "7700",     "chebz@mail.com",       "2013-08-02, 11:00 am"    ,1,             1),    // 3
                CreateCustomer("Tsepo",     "Koena",    "094-421-8035",     "65 Main Road",         "7701",     "supharisk@mail.com",   "2013-08-02, 13:00 am"    ,1,             1),    // 4
                CreateCustomer("Charles",   "Rathbone", "081-659-8945",     "35 Barrow Road",       "7700",     "charlsea@mail.com",    "2013-08-02, 14:40 am"    ,1,             1),    // 5
                CreateCustomer("James",     "Small",    "086-650-7048",     "3 Constania Drive",    "7362",     "jamy@mail.com",        "2013-09-01, 10:00 am"    ,1,             1),    // 6
                CreateCustomer("Lisa",      "Kruger",   "082-789-9139",     "10 Table Drive",       "7210",     "lisaS@mail.com",       "2013-09-02, 10:00 am"    ,1,             1),    // 7
                CreateCustomer("Mike",      "Botha",    "081-700-7745",     "961 Durram Square",    "7321",     "miketheman@mail.com",  "2013-09-02, 11:00 am" ,1,             1)     // 8
            }.ForEach(s => context.Customers.Add(s));

            context.SaveChanges(); //Save Customers

            new List<CustomerAccount>
            {
                //                    Customer       name,          owing,     limit           comments                                         Payment
                // Customer No 1
                CreateCustomerAccount(1,        "THND-011",    120.50,      500.00,     "Long serving citizen who always pays on time.",     "2013-09-05"),
                CreateCustomerAccount(2,        "ESJO-021",    150.00,      500.00,     "Long serving citizen who always pays on time.",     "2013-09-05"),   // 2
                CreateCustomerAccount(3,        "PACH-031",    500.00,      600.00,     "Long serving citizen who always pays on time.",     "2013-09-05"),   // 3
                CreateCustomerAccount(4,        "TSKO-041",    250.00,      500.00,     "Long serving citizen who always pays on time.",     "2013-10-05"),   // 4
                CreateCustomerAccount(5,        "CHRA-051",    360.00,      500.00,     "Long serving citizen who always pays on time.",     "2013-10-05"),   // 5
                CreateCustomerAccount(6,        "JASM-061",    520.00,      600.00,     "Long serving citizen who always pays on time.",     "2013-10-05"),   // 6
                CreateCustomerAccount(7,        "LIKR-081",    300.00,      500.00,     "Long serving citizen who always pays on time.",     "2013-10-05"),   // 7
                CreateCustomerAccount(8,        "MIBO-091",    695.33,      700.00,     "Long serving citizen who always pays on time.",     "2013-10-05")    // 8
            }.ForEach(s => context.CustomerAccounts.Add(s));

            new List<CustomerPayment>
            {
                //                      created                  amount  ref       account   empID
                // Customer Payment No 1
                CreateAccountPayment("2013-09-04, 3:34 pm",   110,   "GSTGS",     1,      1),    // 2
                CreateAccountPayment("2013-09-12, 4:23 pm",   150,   "GSTGS",     2,      1),    // 3
                CreateAccountPayment("2013-09-05, 5:05 pm",   400,   "GSTGS",     3,      1)   // 4


            }.ForEach(a => context.CustomerPayments.Add(a));
            #endregion

            #region Add Incident
            new List<Incident> 
            {
                //              creation date,           description,       type,  employeeId, BusinessId
                // IncidentID 1
                CreateIncident("2013-08-19, 5:34 pm", "Many of our Ricoffy products spilt",           "Damage",   1 ,     1),
                CreateIncident("2013-08-19, 7:34 am", "10 Nescafe's went missing",                    "Theft",    1 ,     1), // 2
            
            }.ForEach(i => context.Incidents.Add(i));

            new List<ProductIncident> 
            {
                //            incident, product, quantity, removed
                // IncidentID 1
                CreateProductIncident(1,    1,      10,     true),
                CreateProductIncident(1,    2,      15,     true),
                CreateProductIncident(2,    4,      5,      true)  // 2
            
            }.ForEach(i => context.ProductIncidents.Add(i));
            #endregion

            #region Add Sale
            new List<Sale>
            {
                //              created,        charged, received, change,       customer, bus, emp, credit?
                #region July Sales
                            // Sales For July..........................................***********

                             CreateSale("2013-08-01, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 1
                             CreateSale("2013-08-01, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //2
                             CreateSale("2013-08-01, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //3
                             CreateSale("2013-08-01, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //4
                             CreateSale("2013-08-01, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //5
                             CreateSale("2013-08-01, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //6
                             CreateSale("2013-08-01, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //7
                             CreateSale("2013-08-01, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //8
                             CreateSale("2013-08-01, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //9
                             CreateSale("2013-08-01, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //10
                             CreateSale("2013-08-01, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //11
                             CreateSale("2013-08-01, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //12
                             CreateSale("2013-08-01, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //13
                             CreateSale("2013-08-01, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //14
                             CreateSale("2013-08-01, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //15
                             CreateSale("2013-08-01, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //16
                             CreateSale("2013-08-01, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //17
                             CreateSale("2013-08-01, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //18
                             CreateSale("2013-08-01, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //19
                             CreateSale("2013-08-01, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //20
                             CreateSale("2013-08-01, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //21
                             CreateSale("2013-08-01, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //22
                             CreateSale("2013-08-01, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //23
                             CreateSale("2013-08-01, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //24
                             CreateSale("2013-08-01, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //25
                             CreateSale("2013-08-01, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //26
                             CreateSale("2013-08-01, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //27
                             CreateSale("2013-08-01, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //28
                             CreateSale("2013-08-01, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //29
                             CreateSale("2013-08-01, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //30
                             CreateSale("2013-08-01, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //31
                             CreateSale("2013-08-01, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //32
                             CreateSale("2013-08-01, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //33
                             CreateSale("2013-08-01, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //34
                             CreateSale("2013-08-01, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //35
                             CreateSale("2013-08-01, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //36
                             CreateSale("2013-08-01, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //37
                             CreateSale("2013-08-01, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //38
                             CreateSale("2013-08-01, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //39
                             CreateSale("2013-08-01, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //40
                             CreateSale("2013-08-01, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //41
                             CreateSale("2013-08-01, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //42
                             CreateSale("2013-08-01, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //43
                             CreateSale("2013-08-01, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //44
                             CreateSale("2013-08-01, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //45
                             CreateSale("2013-08-01, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //46
                             CreateSale("2013-08-02, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //47
                             CreateSale("2013-08-02, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //48
                             CreateSale("2013-08-02, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //49
                             CreateSale("2013-08-02, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //50
                             CreateSale("2013-08-02, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //51
                             CreateSale("2013-08-02, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //52
                             CreateSale("2013-08-02, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //53
                             CreateSale("2013-08-02, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //54
                             CreateSale("2013-08-02, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //55
                             CreateSale("2013-08-02, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //56
                             CreateSale("2013-08-02, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //57
                             CreateSale("2013-08-02, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //58
                             CreateSale("2013-08-02, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //59
                             CreateSale("2013-08-02, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //60
                             CreateSale("2013-08-02, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //61
                             CreateSale("2013-08-02, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //62
                             CreateSale("2013-08-02, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //63
                             CreateSale("2013-08-02, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //64
                             CreateSale("2013-08-02, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //65
                             CreateSale("2013-08-02, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //66
                             CreateSale("2013-08-02, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //67
                             CreateSale("2013-08-02, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //68
                             CreateSale("2013-08-02, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //69
                             CreateSale("2013-08-02, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //70
                             CreateSale("2013-08-02, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //71
                             CreateSale("2013-08-02, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //72
                             CreateSale("2013-08-03, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //73
                             CreateSale("2013-08-03, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //74
                             CreateSale("2013-08-03, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //75
                             CreateSale("2013-08-03, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //76
                             CreateSale("2013-08-03, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //77
                             CreateSale("2013-08-03, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //78
                             CreateSale("2013-08-03, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //79
                             CreateSale("2013-08-03, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //80
                             CreateSale("2013-08-03, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //81
                             CreateSale("2013-08-03, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //82
                             CreateSale("2013-08-03, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //83
                             CreateSale("2013-08-03, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //84
                             CreateSale("2013-08-03, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //85
                             CreateSale("2013-08-03, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //86
                             CreateSale("2013-08-03, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //87
                             CreateSale("2013-08-03, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //88
                             CreateSale("2013-08-03, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //89
                             CreateSale("2013-08-03, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //90
                             CreateSale("2013-08-03, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //91
                             CreateSale("2013-08-03, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //92
                             CreateSale("2013-08-03, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //93
                             CreateSale("2013-08-03, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //94
                             CreateSale("2013-08-03, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //95
                             CreateSale("2013-08-03, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //96
                             CreateSale("2013-08-03, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //97
                             CreateSale("2013-08-03, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //98
                             CreateSale("2013-08-03, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //99
                             CreateSale("2013-08-03, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //100

                             CreateSale("2013-08-04, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 101
                             CreateSale("2013-08-04, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //102
                             CreateSale("2013-08-04, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //103
                             CreateSale("2013-08-04, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //104
                             CreateSale("2013-08-04, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //105
                             CreateSale("2013-08-04, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //106
                             CreateSale("2013-08-04, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //108
                             CreateSale("2013-08-04, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //109
                             CreateSale("2013-08-04, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //110
                             CreateSale("2013-08-04, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //110
                             CreateSale("2013-08-04, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //111
                             CreateSale("2013-08-04, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //112
                             CreateSale("2013-08-04, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //113
                             CreateSale("2013-08-04, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //114
                             CreateSale("2013-08-04, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //115
                             CreateSale("2013-08-04, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //116
                             CreateSale("2013-08-04, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //117
                             CreateSale("2013-08-04, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //118
                             CreateSale("2013-08-04, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //119
                             CreateSale("2013-08-04, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //120
                             CreateSale("2013-08-04, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //121
                             CreateSale("2013-08-04, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //122
                             CreateSale("2013-08-04, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //123
                             CreateSale("2013-08-04, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //124
                             CreateSale("2013-08-04, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //125
                             CreateSale("2013-08-04, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //126
                             CreateSale("2013-08-04, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //127
                             CreateSale("2013-08-04, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //128
                             CreateSale("2013-08-04, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //129
                             CreateSale("2013-08-04, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //130
                             CreateSale("2013-08-04, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //131
                             CreateSale("2013-08-04, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //132
                             CreateSale("2013-08-04, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //133
                             CreateSale("2013-08-04, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //134
                             CreateSale("2013-08-04, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //135
                             CreateSale("2013-08-04, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //136
                             CreateSale("2013-08-04, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //137
                             CreateSale("2013-08-04, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //138
                             CreateSale("2013-08-04, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //139
                             CreateSale("2013-08-04, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //140
                             CreateSale("2013-08-04, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //141
                             CreateSale("2013-08-04, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //142
                             CreateSale("2013-08-04, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //143
                             CreateSale("2013-08-04, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //144
                             CreateSale("2013-08-04, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //145
                             CreateSale("2013-08-04, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //146
                             CreateSale("2013-08-05, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //147
                             CreateSale("2013-08-05, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //148
                             CreateSale("2013-08-05, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //149
                             CreateSale("2013-08-05, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //150
                             CreateSale("2013-08-05, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //151
                             CreateSale("2013-08-05, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //152
                             CreateSale("2013-08-05, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //153
                             CreateSale("2013-08-05, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //154
                             CreateSale("2013-08-05, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //155
                             CreateSale("2013-08-05, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //156
                             CreateSale("2013-08-05, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //157
                             CreateSale("2013-08-05, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //158
                             CreateSale("2013-08-05, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //159
                             CreateSale("2013-08-05, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //160
                             CreateSale("2013-08-05, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //161
                             CreateSale("2013-08-05, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //162
                             CreateSale("2013-08-05, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //163
                             CreateSale("2013-08-05, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //164
                             CreateSale("2013-08-05, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //165
                             CreateSale("2013-08-05, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //166
                             CreateSale("2013-08-05, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //167
                             CreateSale("2013-08-05, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //168
                             CreateSale("2013-08-05, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //169
                             CreateSale("2013-08-05, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //170
                             CreateSale("2013-08-05, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //171
                             CreateSale("2013-08-05, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //172
                             CreateSale("2013-08-09, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //173
                             CreateSale("2013-08-09, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //174
                             CreateSale("2013-08-09, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //175
                             CreateSale("2013-08-09, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //176
                             CreateSale("2013-08-09, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //177
                             CreateSale("2013-08-09, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //178
                             CreateSale("2013-08-09, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //179
                             CreateSale("2013-08-09, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //180
                             CreateSale("2013-08-09, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //181
                             CreateSale("2013-08-09, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //182
                             CreateSale("2013-08-09, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //183
                             CreateSale("2013-08-09, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //184
                             CreateSale("2013-08-09, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //185
                             CreateSale("2013-08-09, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //186
                             CreateSale("2013-08-09, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //187
                             CreateSale("2013-08-09, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //188
                             CreateSale("2013-08-09, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //189
                             CreateSale("2013-08-09, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //190
                             CreateSale("2013-08-09, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //191
                             CreateSale("2013-08-09, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //192
                             CreateSale("2013-08-09, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //193
                             CreateSale("2013-08-09, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //194
                             CreateSale("2013-08-09, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //195
                             CreateSale("2013-08-09, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //196
                             CreateSale("2013-08-09, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //197
                             CreateSale("2013-08-09, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //198
                             CreateSale("2013-08-09, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //199
                             CreateSale("2013-08-09, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //200

                             CreateSale("2013-08-10, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 201
                             CreateSale("2013-08-10, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //202
                             CreateSale("2013-08-10, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //203
                             CreateSale("2013-08-10, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //204
                             CreateSale("2013-08-10, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //205
                             CreateSale("2013-08-10, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //206
                             CreateSale("2013-08-10, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //208
                             CreateSale("2013-08-10, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //209
                             CreateSale("2013-08-10, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //210
                             CreateSale("2013-08-10, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //210
                             CreateSale("2013-08-10, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //211
                             CreateSale("2013-08-10, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //212
                             CreateSale("2013-08-10, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //213
                             CreateSale("2013-08-10, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //214
                             CreateSale("2013-08-10, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //215
                             CreateSale("2013-08-10, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //216
                             CreateSale("2013-08-10, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //217
                             CreateSale("2013-08-10, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //218
                             CreateSale("2013-08-10, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //219
                             CreateSale("2013-08-10, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //220
                             CreateSale("2013-08-10, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //221
                             CreateSale("2013-08-10, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //222
                             CreateSale("2013-08-10, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //223
                             CreateSale("2013-08-10, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //224
                             CreateSale("2013-08-10, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //225
                             CreateSale("2013-08-10, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //226
                             CreateSale("2013-08-10, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //227
                             CreateSale("2013-08-10, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //228
                             CreateSale("2013-08-10, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //229
                             CreateSale("2013-08-10, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //230
                             CreateSale("2013-08-10, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //231
                             CreateSale("2013-08-10, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //232
                             CreateSale("2013-08-10, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //233
                             CreateSale("2013-08-10, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //234
                             CreateSale("2013-08-10, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //235
                             CreateSale("2013-08-10, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //236
                             CreateSale("2013-08-10, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //237
                             CreateSale("2013-08-10, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //238
                             CreateSale("2013-08-10, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //239
                             CreateSale("2013-08-10, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //240
                             CreateSale("2013-08-10, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //241
                             CreateSale("2013-08-10, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //242
                             CreateSale("2013-08-10, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //243
                             CreateSale("2013-08-10, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //244
                             CreateSale("2013-08-10, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //245
                             CreateSale("2013-08-10, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //246
                             CreateSale("2013-08-10, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //247
                             CreateSale("2013-08-10, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //248
                             CreateSale("2013-08-10, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //249
                             CreateSale("2013-08-10, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //250
                             CreateSale("2013-08-10, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //251
                             CreateSale("2013-08-10, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //252
                             CreateSale("2013-08-10, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //253
                             CreateSale("2013-08-10, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //254
                             CreateSale("2013-08-10, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //255
                             CreateSale("2013-08-10, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //256
                             CreateSale("2013-08-10, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //257
                             CreateSale("2013-08-10, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //258
                             CreateSale("2013-08-10, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //259
                             CreateSale("2013-08-10, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //260
                             CreateSale("2013-08-10, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //261
                             CreateSale("2013-08-10, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //262
                             CreateSale("2013-08-10, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //263
                             CreateSale("2013-08-10, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //264
                             CreateSale("2013-08-10, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //265
                             CreateSale("2013-08-10, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //266
                             CreateSale("2013-08-10, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //267
                             CreateSale("2013-08-10, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //268
                             CreateSale("2013-08-10, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //269
                             CreateSale("2013-08-10, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //270
                             CreateSale("2013-08-10, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //271
                             CreateSale("2013-08-10, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //272
                             CreateSale("2013-08-11, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //273
                             CreateSale("2013-08-11, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //274
                             CreateSale("2013-08-11, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //275
                             CreateSale("2013-08-11, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //276
                             CreateSale("2013-08-11, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //277
                             CreateSale("2013-08-11, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //278
                             CreateSale("2013-08-11, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //279
                             CreateSale("2013-08-11, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //280
                             CreateSale("2013-08-11, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //281
                             CreateSale("2013-08-11, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //282
                             CreateSale("2013-08-11, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //283
                             CreateSale("2013-08-11, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //284
                             CreateSale("2013-08-11, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //285
                             CreateSale("2013-08-11, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //286
                             CreateSale("2013-08-11, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //287
                             CreateSale("2013-08-11, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //288
                             CreateSale("2013-08-11, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //289
                             CreateSale("2013-08-11, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //290
                             CreateSale("2013-08-11, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //291
                             CreateSale("2013-08-11, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //292
                             CreateSale("2013-08-11, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //293
                             CreateSale("2013-08-11, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //294
                             CreateSale("2013-08-11, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //295
                             CreateSale("2013-08-11, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //296
                             CreateSale("2013-08-11, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //297
                             CreateSale("2013-08-11, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //298
                             CreateSale("2013-08-11, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //299
                             CreateSale("2013-08-11, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //300

                             CreateSale("2013-08-12, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 301
                             CreateSale("2013-08-12, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //302
                             CreateSale("2013-08-12, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //303
                             CreateSale("2013-08-12, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //304
                             CreateSale("2013-08-12, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //305
                             CreateSale("2013-08-12, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //306
                             CreateSale("2013-08-12, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //308
                             CreateSale("2013-08-12, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //309
                             CreateSale("2013-08-12, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //310
                             CreateSale("2013-08-12, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //310
                             CreateSale("2013-08-12, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //311
                             CreateSale("2013-08-12, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //312
                             CreateSale("2013-08-12, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //313
                             CreateSale("2013-08-12, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //314
                             CreateSale("2013-08-12, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //315
                             CreateSale("2013-08-12, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //316
                             CreateSale("2013-08-12, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //317
                             CreateSale("2013-08-12, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //318
                             CreateSale("2013-08-12, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //319
                             CreateSale("2013-08-12, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //320
                             CreateSale("2013-08-12, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //321
                             CreateSale("2013-08-12, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //322
                             CreateSale("2013-08-12, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //323
                             CreateSale("2013-08-12, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //324
                             CreateSale("2013-08-12, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //325
                             CreateSale("2013-08-12, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //326
                             CreateSale("2013-08-12, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //327
                             CreateSale("2013-08-12, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //328
                             CreateSale("2013-08-12, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //329
                             CreateSale("2013-08-12, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //330
                             CreateSale("2013-08-12, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //331
                             CreateSale("2013-08-12, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //332
                             CreateSale("2013-08-12, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //333
                             CreateSale("2013-08-12, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //334
                             CreateSale("2013-08-12, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //335
                             CreateSale("2013-08-12, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //336
                             CreateSale("2013-08-12, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //337
                             CreateSale("2013-08-12, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //338
                             CreateSale("2013-08-12, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //339
                             CreateSale("2013-08-12, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //340
                             CreateSale("2013-08-12, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //341
                             CreateSale("2013-08-12, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //342
                             CreateSale("2013-08-12, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //343
                             CreateSale("2013-08-12, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //344
                             CreateSale("2013-08-12, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //345
                             CreateSale("2013-08-12, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //346
                             CreateSale("2013-08-15, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //347
                             CreateSale("2013-08-15, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //348
                             CreateSale("2013-08-15, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //349
                             CreateSale("2013-08-15, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //350
                             CreateSale("2013-08-15, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //351
                             CreateSale("2013-08-15, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //352
                             CreateSale("2013-08-15, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //353
                             CreateSale("2013-08-15, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //354
                             CreateSale("2013-08-15, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //355
                             CreateSale("2013-08-15, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //356
                             CreateSale("2013-08-15, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //357
                             CreateSale("2013-08-15, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //358
                             CreateSale("2013-08-15, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //359
                             CreateSale("2013-08-15, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //360
                             CreateSale("2013-08-15, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //361
                             CreateSale("2013-08-15, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //362
                             CreateSale("2013-08-15, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //363
                             CreateSale("2013-08-15, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //364
                             CreateSale("2013-08-15, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //365
                             CreateSale("2013-08-15, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //366
                             CreateSale("2013-08-15, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //367
                             CreateSale("2013-08-15, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //368
                             CreateSale("2013-08-15, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //369
                             CreateSale("2013-08-15, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //370
                             CreateSale("2013-08-15, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //371
                             CreateSale("2013-08-15, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //372
                             CreateSale("2013-08-16, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //373
                             CreateSale("2013-08-16, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //374
                             CreateSale("2013-08-16, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //375
                             CreateSale("2013-08-16, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //376
                             CreateSale("2013-08-16, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //377
                             CreateSale("2013-08-16, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //378
                             CreateSale("2013-08-16, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //379
                             CreateSale("2013-08-16, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //380
                             CreateSale("2013-08-16, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //381
                             CreateSale("2013-08-16, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //382
                             CreateSale("2013-08-16, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //383
                             CreateSale("2013-08-16, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //384
                             CreateSale("2013-08-16, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //385
                             CreateSale("2013-08-16, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //386
                             CreateSale("2013-08-16, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //387
                             CreateSale("2013-08-16, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //388
                             CreateSale("2013-08-16, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //389
                             CreateSale("2013-08-16, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //390
                             CreateSale("2013-08-16, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //391
                             CreateSale("2013-08-16, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //392
                             CreateSale("2013-08-16, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //393
                             CreateSale("2013-08-16, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //394
                             CreateSale("2013-08-16, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //395
                             CreateSale("2013-08-16, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //396
                             CreateSale("2013-08-16, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //397
                             CreateSale("2013-08-16, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //398
                             CreateSale("2013-08-16, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //399
                             CreateSale("2013-08-16, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //400

                             CreateSale("2013-08-17, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 401
                             CreateSale("2013-08-17, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //402
                             CreateSale("2013-08-17, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //403
                             CreateSale("2013-08-17, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //404
                             CreateSale("2013-08-17, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //405
                             CreateSale("2013-08-17, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //406
                             CreateSale("2013-08-17, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //408
                             CreateSale("2013-08-17, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //409
                             CreateSale("2013-08-17, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //410
                             CreateSale("2013-08-17, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //410
                             CreateSale("2013-08-17, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //411
                             CreateSale("2013-08-17, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //412
                             CreateSale("2013-08-17, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //413
                             CreateSale("2013-08-17, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //414
                             CreateSale("2013-08-17, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //415
                             CreateSale("2013-08-17, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //416
                             CreateSale("2013-08-17, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //417
                             CreateSale("2013-08-17, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //418
                             CreateSale("2013-08-17, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //419
                             CreateSale("2013-08-17, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //420
                             CreateSale("2013-08-17, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //421
                             CreateSale("2013-08-17, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //422
                             CreateSale("2013-08-17, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //423
                             CreateSale("2013-08-17, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //424
                             CreateSale("2013-08-17, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //425
                             CreateSale("2013-08-17, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //426
                             CreateSale("2013-08-17, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //427
                             CreateSale("2013-08-17, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //428
                             CreateSale("2013-08-17, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //429
                             CreateSale("2013-08-17, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //430
                             CreateSale("2013-08-17, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //431
                             CreateSale("2013-08-17, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //432
                             CreateSale("2013-08-17, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //433
                             CreateSale("2013-08-17, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //434
                             CreateSale("2013-08-17, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //435
                             CreateSale("2013-08-17, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //436
                             CreateSale("2013-08-17, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //437
                             CreateSale("2013-08-17, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //438
                             CreateSale("2013-08-17, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //439
                             CreateSale("2013-08-17, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //440
                             CreateSale("2013-08-17, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //441
                             CreateSale("2013-08-17, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //442
                             CreateSale("2013-08-17, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //443
                             CreateSale("2013-08-17, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //444
                             CreateSale("2013-08-17, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //445
                             CreateSale("2013-08-17, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //446
                             CreateSale("2013-08-18, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //447
                             CreateSale("2013-08-18, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //448
                             CreateSale("2013-08-18, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //449
                             CreateSale("2013-08-18, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //450
                             CreateSale("2013-08-18, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //451
                             CreateSale("2013-08-18, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //452
                             CreateSale("2013-08-18, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //453
                             CreateSale("2013-08-18, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //454
                             CreateSale("2013-08-18, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //455
                             CreateSale("2013-08-18, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //456
                             CreateSale("2013-08-18, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //457
                             CreateSale("2013-08-18, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //458
                             CreateSale("2013-08-18, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //459
                             CreateSale("2013-08-18, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //460
                             CreateSale("2013-08-18, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //461
                             CreateSale("2013-08-18, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //462
                             CreateSale("2013-08-18, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //463
                             CreateSale("2013-08-18, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //464
                             CreateSale("2013-08-18, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //465
                             CreateSale("2013-08-18, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //466
                             CreateSale("2013-08-18, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //467
                             CreateSale("2013-08-18, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //468
                             CreateSale("2013-08-18, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //469
                             CreateSale("2013-08-18, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //470
                             CreateSale("2013-08-18, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //471
                             CreateSale("2013-08-18, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //472
                             CreateSale("2013-08-19, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //473
                             CreateSale("2013-08-19, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //474
                             CreateSale("2013-08-19, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //475
                             CreateSale("2013-08-19, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //476
                             CreateSale("2013-08-19, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //477
                             CreateSale("2013-08-19, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //478
                             CreateSale("2013-08-19, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //479
                             CreateSale("2013-08-19, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //480
                             CreateSale("2013-08-19, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //481
                             CreateSale("2013-08-19, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //482
                             CreateSale("2013-08-19, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //483
                             CreateSale("2013-08-19, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //484
                             CreateSale("2013-08-19, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //485
                             CreateSale("2013-08-19, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //486
                             CreateSale("2013-08-19, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //487
                             CreateSale("2013-08-19, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //488
                             CreateSale("2013-08-19, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //489
                             CreateSale("2013-08-19, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //490
                             CreateSale("2013-08-19, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //491
                             CreateSale("2013-08-19, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //492
                             CreateSale("2013-08-19, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //493
                             CreateSale("2013-08-19, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //494
                             CreateSale("2013-08-19, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //495
                             CreateSale("2013-08-19, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //496
                             CreateSale("2013-08-19, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //497
                             CreateSale("2013-08-19, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //498
                             CreateSale("2013-08-19, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //499
                             CreateSale("2013-08-19, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //500

            #endregion

                #region August Sales
                                                                  // Sales For August..........................................***********

                             CreateSale("2013-09-01, 10:00 am",     50.00M,      60M,       10M,         null,   1, 2, false), //SaleID 1001
                             CreateSale("2013-09-01, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //2
                             CreateSale("2013-09-01, 10:24 am",     20.00M,      20M,       0.00M,         null,   1, 2, false), //3
                             CreateSale("2013-09-01, 10:30 am",     50.00M,      50M,       00.00M,        4,      1, 2, false), //4
                             CreateSale("2013-09-01, 10:36 am",     60.00M,      70M,       10.00M,         null,   1, 2, false), //5
                             CreateSale("2013-09-01, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //6
                             CreateSale("2013-09-01, 10:45 am",     25.00M,      30M,       5.00M,         null,   1, 2, false), //7
                             CreateSale("2013-09-01, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //8
                             CreateSale("2013-09-01, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //9
                             CreateSale("2013-09-01, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //10
                             CreateSale("2013-09-01, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //11
                             CreateSale("2013-09-01, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //12
                             CreateSale("2013-09-01, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //13
                             CreateSale("2013-09-01, 11:36 am",     30.00M,      50M,       20.00M,        null,   1, 2, false), //14
                             CreateSale("2013-09-01, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //15
                             CreateSale("2013-09-01, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //16
                             CreateSale("2013-09-01, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //17
                             CreateSale("2013-09-01, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //18
                             CreateSale("2013-09-01, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //19
                             CreateSale("2013-09-01, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //20
                             CreateSale("2013-09-01, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //21
                             CreateSale("2013-09-01, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //22
                             CreateSale("2013-09-01, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //23
                             CreateSale("2013-09-01, 1:04 pm",      40.00M,      50M,       10.00M,         null,   1, 3, false), //24
                             CreateSale("2013-09-01, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //25
                             CreateSale("2013-09-01, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //26
                             CreateSale("2013-09-01, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //27
                             CreateSale("2013-09-01, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //28
                             CreateSale("2013-09-01, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //29
                             CreateSale("2013-09-01, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //30
                             CreateSale("2013-09-01, 1:42 pm",      30.00M,      50M,       20.00M,         null,   1, 3, false), //31
                             CreateSale("2013-09-01, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //32
                             CreateSale("2013-09-01, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //33
                             CreateSale("2013-09-01, 2:01 pm",      100.00M,     170M,      70.00M,         null,   1, 2, false), //34
                             CreateSale("2013-09-01, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //35
                             CreateSale("2013-09-01, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //36
                             CreateSale("2013-09-01, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //37
                             CreateSale("2013-09-01, 2:44 pm",      40.00M,      50M,       10.00M,        null,   1, 2, false), //38
                             CreateSale("2013-09-01, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //39
                             CreateSale("2013-09-01, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //40
                             CreateSale("2013-09-01, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //41
                             CreateSale("2013-09-01, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //42
                             CreateSale("2013-09-01, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //43
                             CreateSale("2013-09-01, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //44
                             CreateSale("2013-09-01, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //45
                             CreateSale("2013-09-01, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //46
                             CreateSale("2013-09-02, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //47
                             CreateSale("2013-09-02, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //48
                             CreateSale("2013-09-02, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //49
                             CreateSale("2013-09-02, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //50
                             CreateSale("2013-09-02, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //51
                             CreateSale("2013-09-02, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //52
                             CreateSale("2013-09-02, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //53
                             CreateSale("2013-09-02, 11:15 am",     150.00M,     200M,      50.00M,         4,      1, 4, true),  //54
                             CreateSale("2013-09-02, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //55
                             CreateSale("2013-09-02, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //56
                             CreateSale("2013-09-02, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //57
                             CreateSale("2013-09-02, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //58
                             CreateSale("2013-09-02, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //59
                             CreateSale("2013-09-02, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //60
                             CreateSale("2013-09-02, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //61
                             CreateSale("2013-09-02, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //62
                             CreateSale("2013-09-02, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //63
                             CreateSale("2013-09-02, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //64
                             CreateSale("2013-09-02, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //65
                             CreateSale("2013-09-02, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //66
                             CreateSale("2013-09-02, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //67
                             CreateSale("2013-09-02, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //68
                             CreateSale("2013-09-02, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //69
                             CreateSale("2013-09-02, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //70
                             CreateSale("2013-09-02, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //71
                             CreateSale("2013-09-02, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //72
                             CreateSale("2013-09-05, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //73
                             CreateSale("2013-09-05, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //74
                             CreateSale("2013-09-05, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //75
                             CreateSale("2013-09-05, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //76
                             CreateSale("2013-09-05, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //77
                             CreateSale("2013-09-05, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //78
                             CreateSale("2013-09-05, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //79
                             CreateSale("2013-09-05, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //80
                             CreateSale("2013-09-05, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //81
                             CreateSale("2013-09-05, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //82
                             CreateSale("2013-09-05, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //83
                             CreateSale("2013-09-05, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //84
                             CreateSale("2013-09-05, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //85
                             CreateSale("2013-09-05, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //86
                             CreateSale("2013-09-05, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //87
                             CreateSale("2013-09-05, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //88
                             CreateSale("2013-09-05, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //89
                             CreateSale("2013-09-05, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //90
                             CreateSale("2013-09-05, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //91
                             CreateSale("2013-09-05, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //92
                             CreateSale("2013-09-05, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //93
                             CreateSale("2013-09-05, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //94
                             CreateSale("2013-09-05, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //95
                             CreateSale("2013-09-05, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //96
                             CreateSale("2013-09-05, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //97
                             CreateSale("2013-09-05, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //98
                             CreateSale("2013-09-05, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //99
                             CreateSale("2013-09-05, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //100

                             CreateSale("2013-09-06, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 101
                             CreateSale("2013-09-06, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //102
                             CreateSale("2013-09-06, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //103
                             CreateSale("2013-09-06, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //104
                             CreateSale("2013-09-06, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //105
                             CreateSale("2013-09-06, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //106
                             CreateSale("2013-09-06, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //108
                             CreateSale("2013-09-06, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //109
                             CreateSale("2013-09-06, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //110
                             CreateSale("2013-09-06, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //110
                             CreateSale("2013-09-06, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //111
                             CreateSale("2013-09-06, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //112
                             CreateSale("2013-09-06, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //113
                             CreateSale("2013-09-06, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //114
                             CreateSale("2013-09-06, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //115
                             CreateSale("2013-09-06, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //116
                             CreateSale("2013-09-06, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //117
                             CreateSale("2013-09-06, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //118
                             CreateSale("2013-09-06, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //119
                             CreateSale("2013-09-06, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //120
                             CreateSale("2013-09-06, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //121
                             CreateSale("2013-09-06, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //122
                             CreateSale("2013-09-06, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //123
                             CreateSale("2013-09-06, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //124
                             CreateSale("2013-09-06, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //125
                             CreateSale("2013-09-06, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //126
                             CreateSale("2013-09-06, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //127
                             CreateSale("2013-09-06, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //128
                             CreateSale("2013-09-06, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //129
                             CreateSale("2013-09-06, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //130
                             CreateSale("2013-09-06, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //131
                             CreateSale("2013-09-06, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //132
                             CreateSale("2013-09-06, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //133
                             CreateSale("2013-09-06, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //134
                             CreateSale("2013-09-06, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //135
                             CreateSale("2013-09-06, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //136
                             CreateSale("2013-09-06, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //137
                             CreateSale("2013-09-06, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //138
                             CreateSale("2013-09-06, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //139
                             CreateSale("2013-09-06, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //140
                             CreateSale("2013-09-06, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //141
                             CreateSale("2013-09-06, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //142
                             CreateSale("2013-09-06, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //143
                             CreateSale("2013-09-06, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //144
                             CreateSale("2013-09-06, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //145
                             CreateSale("2013-09-06, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //146
                             CreateSale("2013-09-08, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //147
                             CreateSale("2013-09-08, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //148
                             CreateSale("2013-09-08, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //149
                             CreateSale("2013-09-08, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //150
                             CreateSale("2013-09-08, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //151
                             CreateSale("2013-09-08, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //152
                             CreateSale("2013-09-08, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //153
                             CreateSale("2013-09-08, 11:15 am",     150.00M,     200.00M,   50.00M,         4,      1, 4, true),  //154
                             CreateSale("2013-09-08, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //155
                             CreateSale("2013-09-08, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //156
                             CreateSale("2013-09-08, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //157
                             CreateSale("2013-09-08, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //158
                             CreateSale("2013-09-08, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //159
                             CreateSale("2013-09-08, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //160
                             CreateSale("2013-09-08, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //161
                             CreateSale("2013-09-08, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //162
                             CreateSale("2013-09-08, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //163
                             CreateSale("2013-09-08, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //164
                             CreateSale("2013-09-08, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //165
                             CreateSale("2013-09-08, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //166
                             CreateSale("2013-09-08, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //167
                             CreateSale("2013-09-08, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //168
                             CreateSale("2013-09-08, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //169
                             CreateSale("2013-09-08, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //170
                             CreateSale("2013-09-08, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //171
                             CreateSale("2013-09-08, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //172
                             CreateSale("2013-09-09, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //173
                             CreateSale("2013-09-09, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //174
                             CreateSale("2013-09-09, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //175
                             CreateSale("2013-09-09, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //176
                             CreateSale("2013-09-09, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //177
                             CreateSale("2013-09-09, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //178
                             CreateSale("2013-09-09, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //179
                             CreateSale("2013-09-09, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //180
                             CreateSale("2013-09-09, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //181
                             CreateSale("2013-09-09, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //182
                             CreateSale("2013-09-09, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //183
                             CreateSale("2013-09-09, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //184
                             CreateSale("2013-09-09, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //185
                             CreateSale("2013-09-09, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //186
                             CreateSale("2013-09-09, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //187
                             CreateSale("2013-09-09, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //188
                             CreateSale("2013-09-09, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //189
                             CreateSale("2013-09-09, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //190
                             CreateSale("2013-09-09, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //191
                             CreateSale("2013-09-09, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //192
                             CreateSale("2013-09-09, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //193
                             CreateSale("2013-09-09, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //194
                             CreateSale("2013-09-09, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //195
                             CreateSale("2013-09-09, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //196
                             CreateSale("2013-09-09, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //197
                             CreateSale("2013-09-09, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //198
                             CreateSale("2013-09-09, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //199
                             CreateSale("2013-09-09, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //200

                             CreateSale("2013-09-10, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 201
                             CreateSale("2013-09-10, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //202
                             CreateSale("2013-09-10, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //203
                             CreateSale("2013-09-10, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //204
                             CreateSale("2013-09-10, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //205
                             CreateSale("2013-09-10, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //206
                             CreateSale("2013-09-10, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //208
                             CreateSale("2013-09-10, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //209
                             CreateSale("2013-09-10, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //210
                             CreateSale("2013-09-10, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //210
                             CreateSale("2013-09-10, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //211
                             CreateSale("2013-09-10, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //212
                             CreateSale("2013-09-10, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //213
                             CreateSale("2013-09-10, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //214
                             CreateSale("2013-09-10, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //215
                             CreateSale("2013-09-10, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //216
                             CreateSale("2013-09-10, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //217
                             CreateSale("2013-09-10, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //218
                             CreateSale("2013-09-10, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //219
                             CreateSale("2013-09-10, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //220
                             CreateSale("2013-09-10, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //221
                             CreateSale("2013-09-10, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //222
                             CreateSale("2013-09-10, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //223
                             CreateSale("2013-09-10, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //224
                             CreateSale("2013-09-10, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //225
                             CreateSale("2013-09-10, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //226
                             CreateSale("2013-09-10, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //227
                             CreateSale("2013-09-10, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //228
                             CreateSale("2013-09-10, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //229
                             CreateSale("2013-09-10, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //230
                             CreateSale("2013-09-10, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //231
                             CreateSale("2013-09-10, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //232
                             CreateSale("2013-09-10, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //233
                             CreateSale("2013-09-10, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //234
                             CreateSale("2013-09-10, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //235
                             CreateSale("2013-09-10, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //236
                             CreateSale("2013-09-10, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //237
                             CreateSale("2013-09-10, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //238
                             CreateSale("2013-09-10, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //239
                             CreateSale("2013-09-10, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //240
                             CreateSale("2013-09-10, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //241
                             CreateSale("2013-09-10, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //242
                             CreateSale("2013-09-10, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //243
                             CreateSale("2013-09-10, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //244
                             CreateSale("2013-09-10, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //245
                             CreateSale("2013-09-10, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //246
                             CreateSale("2013-09-12, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //247
                             CreateSale("2013-09-12, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //248
                             CreateSale("2013-09-12, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //249
                             CreateSale("2013-09-12, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //250
                             CreateSale("2013-09-12, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //251
                             CreateSale("2013-09-12, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //252
                             CreateSale("2013-09-12, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //253
                             CreateSale("2013-09-12, 11:15 am",     200.00M,     200.00M,   0.00M,         4,      1, 4, true),  //254
                             CreateSale("2013-09-12, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //255
                             CreateSale("2013-09-12, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //256
                             CreateSale("2013-09-12, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //257
                             CreateSale("2013-09-12, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //258
                             CreateSale("2013-09-12, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //259
                             CreateSale("2013-09-12, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //260
                             CreateSale("2013-09-12, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //261
                             CreateSale("2013-09-12, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //262
                             CreateSale("2013-09-12, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //263
                             CreateSale("2013-09-12, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //264
                             CreateSale("2013-09-12, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //265
                             CreateSale("2013-09-12, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //266
                             CreateSale("2013-09-12, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //267
                             CreateSale("2013-09-12, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //268
                             CreateSale("2013-09-12, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //269
                             CreateSale("2013-09-12, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //270
                             CreateSale("2013-09-12, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //271
                             CreateSale("2013-09-12, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //272
                             CreateSale("2013-09-13, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //273
                             CreateSale("2013-09-13, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //274
                             CreateSale("2013-09-13, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //275
                             CreateSale("2013-09-13, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //276
                             CreateSale("2013-09-13, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //277
                             CreateSale("2013-09-13, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //278
                             CreateSale("2013-09-13, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //279
                             CreateSale("2013-09-13, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //280
                             CreateSale("2013-09-13, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //281
                             CreateSale("2013-09-13, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //282
                             CreateSale("2013-09-13, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //283
                             CreateSale("2013-09-13, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //284
                             CreateSale("2013-09-13, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //285
                             CreateSale("2013-09-13, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //286
                             CreateSale("2013-09-13, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //287
                             CreateSale("2013-09-13, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //288
                             CreateSale("2013-09-13, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //289
                             CreateSale("2013-09-13, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //290
                             CreateSale("2013-09-13, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //291
                             CreateSale("2013-09-13, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //292
                             CreateSale("2013-09-13, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //293
                             CreateSale("2013-09-13, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //294
                             CreateSale("2013-09-13, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //295
                             CreateSale("2013-09-13, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //296
                             CreateSale("2013-09-13, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //297
                             CreateSale("2013-09-13, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //298
                             CreateSale("2013-09-13, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //299
                             CreateSale("2013-09-13, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //300

                             CreateSale("2013-09-14, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 301
                             CreateSale("2013-09-14, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //302
                             CreateSale("2013-09-14, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //303
                             CreateSale("2013-09-14, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //304
                             CreateSale("2013-09-14, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //305
                             CreateSale("2013-09-14, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //306
                             CreateSale("2013-09-14, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //308
                             CreateSale("2013-09-14, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //309
                             CreateSale("2013-09-14, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //310
                             CreateSale("2013-09-14, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //310
                             CreateSale("2013-09-14, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //311
                             CreateSale("2013-09-14, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //312
                             CreateSale("2013-09-14, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //313
                             CreateSale("2013-09-14, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //314
                             CreateSale("2013-09-14, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //315
                             CreateSale("2013-09-14, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //316
                             CreateSale("2013-09-14, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //317
                             CreateSale("2013-09-14, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //318
                             CreateSale("2013-09-14, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //319
                             CreateSale("2013-09-14, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //320
                             CreateSale("2013-09-14, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //321
                             CreateSale("2013-09-14, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //322
                             CreateSale("2013-09-14, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //323
                             CreateSale("2013-09-14, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //324
                             CreateSale("2013-09-14, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //325
                             CreateSale("2013-09-14, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //326
                             CreateSale("2013-09-14, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //327
                             CreateSale("2013-09-14, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //328
                             CreateSale("2013-09-14, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //329
                             CreateSale("2013-09-14, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //330
                             CreateSale("2013-09-14, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //331
                             CreateSale("2013-09-14, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //332
                             CreateSale("2013-09-14, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //333
                             CreateSale("2013-09-14, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //334
                             CreateSale("2013-09-14, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //335
                             CreateSale("2013-09-14, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //336
                             CreateSale("2013-09-14, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //337
                             CreateSale("2013-09-14, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //338
                             CreateSale("2013-09-14, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //339
                             CreateSale("2013-09-14, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //340
                             CreateSale("2013-09-14, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //341
                             CreateSale("2013-09-14, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //342
                             CreateSale("2013-09-14, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //343
                             CreateSale("2013-09-14, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //344
                             CreateSale("2013-09-14, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //345
                             CreateSale("2013-09-14, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //346
                             CreateSale("2013-09-15, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //347
                             CreateSale("2013-09-15, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //348
                             CreateSale("2013-09-15, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //349
                             CreateSale("2013-09-15, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //350
                             CreateSale("2013-09-15, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //351
                             CreateSale("2013-09-15, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //352
                             CreateSale("2013-09-15, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //353
                             CreateSale("2013-09-15, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //354
                             CreateSale("2013-09-15 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //355
                             CreateSale("2013-09-15, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //356
                             CreateSale("2013-09-15, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //357
                             CreateSale("2013-09-15, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //358
                             CreateSale("2013-09-15, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //359
                             CreateSale("2013-09-15, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //360
                             CreateSale("2013-09-15, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //361
                             CreateSale("2013-09-15, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //362
                             CreateSale("2013-09-15, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //363
                             CreateSale("2013-09-15, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //364
                             CreateSale("2013-09-15, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //365
                             CreateSale("2013-09-15, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //366
                             CreateSale("2013-09-15, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //367
                             CreateSale("2013-09-15, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //368
                             CreateSale("2013-09-15, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //369
                             CreateSale("2013-09-15, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //370
                             CreateSale("2013-09-15, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //371
                             CreateSale("2013-09-15, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //372
                             CreateSale("2013-09-16, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //373
                             CreateSale("2013-09-16, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //374
                             CreateSale("2013-09-16, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //375
                             CreateSale("2013-09-16, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //376
                             CreateSale("2013-09-16, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //377
                             CreateSale("2013-09-16, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //378
                             CreateSale("2013-09-16, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //379
                             CreateSale("2013-09-16, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //380
                             CreateSale("2013-09-16, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //381
                             CreateSale("2013-09-16, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //382
                             CreateSale("2013-09-16, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //383
                             CreateSale("2013-09-16, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //384
                             CreateSale("2013-09-16, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //385
                             CreateSale("2013-09-16, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //386
                             CreateSale("2013-09-16, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //387
                             CreateSale("2013-09-16, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //388
                             CreateSale("2013-09-16, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //389
                             CreateSale("2013-09-16, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //390
                             CreateSale("2013-09-16, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //391
                             CreateSale("2013-09-16, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //392
                             CreateSale("2013-09-16, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //393
                             CreateSale("2013-09-16, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //394
                             CreateSale("2013-09-16, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //395
                             CreateSale("2013-09-16, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //396
                             CreateSale("2013-09-16, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //397
                             CreateSale("2013-09-16, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //398
                             CreateSale("2013-09-16, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //399
                             CreateSale("2013-09-16, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //400

                             CreateSale("2013-09-19, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 401
                             CreateSale("2013-09-19, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //402
                             CreateSale("2013-09-19, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //403
                             CreateSale("2013-09-19, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //404
                             CreateSale("2013-09-19, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //405
                             CreateSale("2013-09-19, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //406
                             CreateSale("2013-09-19, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //408
                             CreateSale("2013-09-19, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //409
                             CreateSale("2013-09-19, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //410
                             CreateSale("2013-09-19, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //410
                             CreateSale("2013-09-19, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //411
                             CreateSale("2013-09-19, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //412
                             CreateSale("2013-09-19, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //413
                             CreateSale("2013-09-19, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //414
                             CreateSale("2013-09-19, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //415
                             CreateSale("2013-09-19, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //416
                             CreateSale("2013-09-19, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //417
                             CreateSale("2013-09-19, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //418
                             CreateSale("2013-09-19, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //419
                             CreateSale("2013-09-19, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //420
                             CreateSale("2013-09-19, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //421
                             CreateSale("2013-09-19, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //422
                             CreateSale("2013-09-19, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //423
                             CreateSale("2013-09-19, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //424
                             CreateSale("2013-09-19, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //425
                             CreateSale("2013-09-19, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //426
                             CreateSale("2013-09-19, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //427
                             CreateSale("2013-09-19, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //428
                             CreateSale("2013-09-19, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //429
                             CreateSale("2013-09-19, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //430
                             CreateSale("2013-09-19, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //431
                             CreateSale("2013-09-19, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //432
                             CreateSale("2013-09-19, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //433
                             CreateSale("2013-09-19, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //434
                             CreateSale("2013-09-19, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //435
                             CreateSale("2013-09-19, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //436
                             CreateSale("2013-09-19, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //437
                             CreateSale("2013-09-19, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //438
                             CreateSale("2013-09-19, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //439
                             CreateSale("2013-09-19, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //440
                             CreateSale("2013-09-19, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //441
                             CreateSale("2013-09-19, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //442
                             CreateSale("2013-09-19, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //443
                             CreateSale("2013-09-19, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //444
                             CreateSale("2013-09-19, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //445
                             CreateSale("2013-09-19, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //446
                             CreateSale("2013-09-19, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //447
                             CreateSale("2013-09-20, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //448
                             CreateSale("2013-09-20, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //449
                             CreateSale("2013-09-20, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //450
                             CreateSale("2013-09-20, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //451
                             CreateSale("2013-09-20, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //452
                             CreateSale("2013-09-20, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //453
                             CreateSale("2013-09-20, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //454
                             CreateSale("2013-09-20, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //455
                             CreateSale("2013-09-20, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //456
                             CreateSale("2013-09-20, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //457
                             CreateSale("2013-09-20, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //458
                             CreateSale("2013-09-20, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //459
                             CreateSale("2013-09-20, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //460
                             CreateSale("2013-09-20, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //461
                             CreateSale("2013-09-20, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //462
                             CreateSale("2013-09-20, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //463
                             CreateSale("2013-09-20, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //464
                             CreateSale("2013-09-20, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //465
                             CreateSale("2013-09-20, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //466
                             CreateSale("2013-09-20, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //467
                             CreateSale("2013-09-20, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //468
                             CreateSale("2013-09-20, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //469
                             CreateSale("2013-09-20, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //470
                             CreateSale("2013-09-20, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //471
                             CreateSale("2013-09-20, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //472
                             CreateSale("2013-09-21, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //473
                             CreateSale("2013-09-21, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //474
                             CreateSale("2013-09-21, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //475
                             CreateSale("2013-09-21, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //476
                             CreateSale("2013-09-21, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //477
                             CreateSale("2013-09-21, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //478
                             CreateSale("2013-09-21, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //479
                             CreateSale("2013-09-21, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //480
                             CreateSale("2013-09-21, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //481
                             CreateSale("2013-09-21, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //482
                             CreateSale("2013-09-21, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //483
                             CreateSale("2013-09-21, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //484
                             CreateSale("2013-09-21, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //485
                             CreateSale("2013-09-21, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //486
                             CreateSale("2013-09-21, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //487
                             CreateSale("2013-09-21, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //488
                             CreateSale("2013-09-21, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //489
                             CreateSale("2013-09-21, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //490
                             CreateSale("2013-09-21, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //491
                             CreateSale("2013-09-21, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //492
                             CreateSale("2013-09-21, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //493
                             CreateSale("2013-09-21, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //494
                             CreateSale("2013-09-21, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //495
                             CreateSale("2013-09-21, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //496
                             CreateSale("2013-09-21, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //497
                             CreateSale("2013-09-21, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //498
                             CreateSale("2013-09-21, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //499
                             CreateSale("2013-09-21, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //500


            #endregion

                #region September Sales

                             CreateSale("2013-10-01, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 1
                             CreateSale("2013-10-01, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //2
                             CreateSale("2013-10-01, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //3
                             CreateSale("2013-10-01, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //4
                             CreateSale("2013-10-01, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //5
                             CreateSale("2013-10-01, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //6
                             CreateSale("2013-10-01, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //7
                             CreateSale("2013-10-01, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //8
                             CreateSale("2013-10-01, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //9
                             CreateSale("2013-10-01, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //10
                             CreateSale("2013-10-01, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //11
                             CreateSale("2013-10-01, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //12
                             CreateSale("2013-10-01, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //13
                             CreateSale("2013-10-01, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //14
                             CreateSale("2013-10-01, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //15
                             CreateSale("2013-10-01, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //16
                             CreateSale("2013-10-01, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //17
                             CreateSale("2013-10-01, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //18
                             CreateSale("2013-10-01, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //19
                             CreateSale("2013-10-01, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //20
                             CreateSale("2013-10-01, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //21
                             CreateSale("2013-10-01, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //22
                             CreateSale("2013-10-01, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //23
                             CreateSale("2013-10-01, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //24
                             CreateSale("2013-10-01, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //25
                             CreateSale("2013-10-01, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //26
                             CreateSale("2013-10-01, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //27
                             CreateSale("2013-10-01, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //28
                             CreateSale("2013-10-01, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //29
                             CreateSale("2013-10-01, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //30
                             CreateSale("2013-10-01, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //31
                             CreateSale("2013-10-01, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //32
                             CreateSale("2013-10-01, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //33
                             CreateSale("2013-10-01, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //34
                             CreateSale("2013-10-01, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //35
                             CreateSale("2013-10-01, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //36
                             CreateSale("2013-10-01, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //37
                             CreateSale("2013-10-01, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //38
                             CreateSale("2013-10-01, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //39
                             CreateSale("2013-10-01, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //40
                             CreateSale("2013-10-01, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //41
                             CreateSale("2013-10-01, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //42
                             CreateSale("2013-10-01, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //43
                             CreateSale("2013-10-01, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //44
                             CreateSale("2013-10-01, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //45
                             CreateSale("2013-10-01, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //46
                             CreateSale("2013-10-02, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //47
                             CreateSale("2013-10-02, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //48
                             CreateSale("2013-10-02, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //49
                             CreateSale("2013-10-02, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //50
                             CreateSale("2013-10-02, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //51
                             CreateSale("2013-10-02, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //52
                             CreateSale("2013-10-02, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //53
                             CreateSale("2013-10-02, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //54
                             CreateSale("2013-10-02, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, true), //55
                             CreateSale("2013-10-02, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //56
                             CreateSale("2013-10-02, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //57
                             CreateSale("2013-10-02, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //58
                             CreateSale("2013-10-02, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //59
                             CreateSale("2013-10-02, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //60
                             CreateSale("2013-10-02, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //61
                             CreateSale("2013-10-02, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //62
                             CreateSale("2013-10-02, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //63
                             CreateSale("2013-10-02, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, true), //64
                             CreateSale("2013-10-02, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //65
                             CreateSale("2013-10-02, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //66
                             CreateSale("2013-10-02, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //67
                             CreateSale("2013-10-02, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //68
                             CreateSale("2013-10-02, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //69
                             CreateSale("2013-10-02, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //70
                             CreateSale("2013-10-02, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //71
                             CreateSale("2013-10-02, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //72
                             CreateSale("2013-10-03, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //73
                             CreateSale("2013-10-03, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //74
                             CreateSale("2013-10-03, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //75
                             CreateSale("2013-10-03, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //76
                             CreateSale("2013-10-03, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //77
                             CreateSale("2013-10-03, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //78
                             CreateSale("2013-10-03, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, true), //79
                             CreateSale("2013-10-03, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //80
                             CreateSale("2013-10-03, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //81
                             CreateSale("2013-10-03, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //82
                             CreateSale("2013-10-03, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //83
                             CreateSale("2013-10-03, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //84
                             CreateSale("2013-10-03, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //85
                             CreateSale("2013-10-03, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //86
                             CreateSale("2013-10-03, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //87
                             CreateSale("2013-10-03, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //88
                             CreateSale("2013-10-03, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //89
                             CreateSale("2013-10-03, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //90
                             CreateSale("2013-10-03, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //91
                             CreateSale("2013-10-03, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //92
                             CreateSale("2013-10-03, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //93
                             CreateSale("2013-10-03, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //94
                             CreateSale("2013-10-03, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //95
                             CreateSale("2013-10-03, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //96
                             CreateSale("2013-10-03, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //97
                             CreateSale("2013-10-03, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //98
                             CreateSale("2013-10-03, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //99
                             CreateSale("2013-10-03, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //100

                             CreateSale("2013-10-04, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 101
                             CreateSale("2013-10-04, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //102
                             CreateSale("2013-10-04, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //103
                             CreateSale("2013-10-04, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //104
                             CreateSale("2013-10-04, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //105
                             CreateSale("2013-10-04, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //106
                             CreateSale("2013-10-04, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //108
                             CreateSale("2013-10-04, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //109
                             CreateSale("2013-10-04, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //110
                             CreateSale("2013-10-04, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //110
                             CreateSale("2013-10-04, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //111
                             CreateSale("2013-10-04, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //112
                             CreateSale("2013-10-04, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //113
                             CreateSale("2013-10-04, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //114
                             CreateSale("2013-10-04, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //115
                             CreateSale("2013-10-04, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //116
                             CreateSale("2013-10-04, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //117
                             CreateSale("2013-10-04, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //118
                             CreateSale("2013-10-04, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //119
                             CreateSale("2013-10-04, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //120
                             CreateSale("2013-10-04, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //121
                             CreateSale("2013-10-04, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //122
                             CreateSale("2013-10-04, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //123
                             CreateSale("2013-10-04, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //124
                             CreateSale("2013-10-04, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //125
                             CreateSale("2013-10-04, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //126
                             CreateSale("2013-10-04, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //127
                             CreateSale("2013-10-04, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //128
                             CreateSale("2013-10-04, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //129
                             CreateSale("2013-10-04, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //130
                             CreateSale("2013-10-04, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //131
                             CreateSale("2013-10-04, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //132
                             CreateSale("2013-10-04, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //133
                             CreateSale("2013-10-04, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //134
                             CreateSale("2013-10-04, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //135
                             CreateSale("2013-10-04, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //136
                             CreateSale("2013-10-04, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //137
                             CreateSale("2013-10-04, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //138
                             CreateSale("2013-10-04, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //139
                             CreateSale("2013-10-04, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //140
                             CreateSale("2013-10-04, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //141
                             CreateSale("2013-10-04, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //142
                             CreateSale("2013-10-04, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //143
                             CreateSale("2013-10-04, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //144
                             CreateSale("2013-10-04, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //145
                             CreateSale("2013-10-04, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //146
                             CreateSale("2013-10-05, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //147
                             CreateSale("2013-10-05, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //148
                             CreateSale("2013-10-05, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //149
                             CreateSale("2013-10-05, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //150
                             CreateSale("2013-10-05, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //151
                             CreateSale("2013-10-05, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //152
                             CreateSale("2013-10-05, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //153
                             CreateSale("2013-10-05, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //154
                             CreateSale("2013-10-05, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //155
                             CreateSale("2013-10-05, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //156
                             CreateSale("2013-10-05, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //157
                             CreateSale("2013-10-05, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //158
                             CreateSale("2013-10-05, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //159
                             CreateSale("2013-10-05, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //160
                             CreateSale("2013-10-05, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //161
                             CreateSale("2013-10-05, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //162
                             CreateSale("2013-10-05, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //163
                             CreateSale("2013-10-05, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //164
                             CreateSale("2013-10-05, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //165
                             CreateSale("2013-10-05, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //166
                             CreateSale("2013-10-05, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //167
                             CreateSale("2013-10-05, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, true), //168
                             CreateSale("2013-10-05, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //169
                             CreateSale("2013-10-05, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //170
                             CreateSale("2013-10-05, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //171
                             CreateSale("2013-10-05, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //172
                             CreateSale("2013-10-06, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //173
                             CreateSale("2013-10-06, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //174
                             CreateSale("2013-10-06, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //175
                             CreateSale("2013-10-06, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //176
                             CreateSale("2013-10-06, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //177
                             CreateSale("2013-10-06, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //178
                             CreateSale("2013-10-06, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, true), //179
                             CreateSale("2013-10-06, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //180
                             CreateSale("2013-10-06, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //181
                             CreateSale("2013-10-06, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //182
                             CreateSale("2013-10-06, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //183
                             CreateSale("2013-10-06, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //184
                             CreateSale("2013-10-06, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //185
                             CreateSale("2013-10-06, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //186
                             CreateSale("2013-10-06, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //187
                             CreateSale("2013-10-06, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //188
                             CreateSale("2013-10-06, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //189
                             CreateSale("2013-10-06, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //190
                             CreateSale("2013-10-06, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //191
                             CreateSale("2013-10-06, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //192
                             CreateSale("2013-10-06, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //193
                             CreateSale("2013-10-06, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //194
                             CreateSale("2013-10-06, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //195
                             CreateSale("2013-10-06, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //196
                             CreateSale("2013-10-06, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //197
                             CreateSale("2013-10-06, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //198
                             CreateSale("2013-10-06, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //199
                             CreateSale("2013-10-06, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //200

                             CreateSale("2013-10-08, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 201
                             CreateSale("2013-10-08, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //202
                             CreateSale("2013-10-08, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //203
                             CreateSale("2013-10-08, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //204
                             CreateSale("2013-10-08, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //205
                             CreateSale("2013-10-08, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //206
                             CreateSale("2013-10-08, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //208
                             CreateSale("2013-10-08, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //209
                             CreateSale("2013-10-08, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //210
                             CreateSale("2013-10-08, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //210
                             CreateSale("2013-10-08, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //211
                             CreateSale("2013-10-08, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //212
                             CreateSale("2013-10-08, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //213
                             CreateSale("2013-10-08, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //214
                             CreateSale("2013-10-08, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //215
                             CreateSale("2013-10-08, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //216
                             CreateSale("2013-10-08, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //217
                             CreateSale("2013-10-08, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //218
                             CreateSale("2013-10-08, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //219
                             CreateSale("2013-10-08, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //220
                             CreateSale("2013-10-08, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //221
                             CreateSale("2013-10-08, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //222
                             CreateSale("2013-10-08, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //223
                             CreateSale("2013-10-08, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //224
                             CreateSale("2013-10-08, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //225
                             CreateSale("2013-10-08, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //226
                             CreateSale("2013-10-08, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //227
                             CreateSale("2013-10-08, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //228
                             CreateSale("2013-10-08, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //229
                             CreateSale("2013-10-08, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //230
                             CreateSale("2013-10-08, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //231
                             CreateSale("2013-10-08, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //232
                             CreateSale("2013-10-08, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //233
                             CreateSale("2013-10-08, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //234
                             CreateSale("2013-10-08, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //235
                             CreateSale("2013-10-08, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //236
                             CreateSale("2013-10-08, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //237
                             CreateSale("2013-10-08, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //238
                             CreateSale("2013-10-08, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //239
                             CreateSale("2013-10-08, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //240
                             CreateSale("2013-10-08, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //241
                             CreateSale("2013-10-08, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //242
                             CreateSale("2013-10-08, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //243
                             CreateSale("2013-10-08, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //244
                             CreateSale("2013-10-08, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //245
                             CreateSale("2013-10-08, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //246
                             CreateSale("2013-10-12, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //247
                             CreateSale("2013-10-12, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //248
                             CreateSale("2013-10-12, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //249
                             CreateSale("2013-10-12, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //250
                             CreateSale("2013-10-12, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //251
                             CreateSale("2013-10-12, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //252
                             CreateSale("2013-10-12, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //253
                             CreateSale("2013-10-12, 11:15 am",     200.00M,     200.00M,   0.00M,         4,      1, 4, true),  //254
                             CreateSale("2013-10-12, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //255
                             CreateSale("2013-10-12, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //256
                             CreateSale("2013-10-12, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //257
                             CreateSale("2013-10-12, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //258
                             CreateSale("2013-10-12, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //259
                             CreateSale("2013-10-12, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //260
                             CreateSale("2013-10-12, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //261
                             CreateSale("2013-10-12, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //262
                             CreateSale("2013-10-12, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //263
                             CreateSale("2013-10-12, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //264
                             CreateSale("2013-10-12, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //265
                             CreateSale("2013-10-12, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //266
                             CreateSale("2013-10-12, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //267
                             CreateSale("2013-10-12, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //268
                             CreateSale("2013-10-12, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //269
                             CreateSale("2013-10-12, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //270
                             CreateSale("2013-10-12, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //271
                             CreateSale("2013-10-12, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //272
                             CreateSale("2013-10-13, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //273
                             CreateSale("2013-10-13, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //274
                             CreateSale("2013-10-13, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //275
                             CreateSale("2013-10-13, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //276
                             CreateSale("2013-10-13, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //277
                             CreateSale("2013-10-13, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //278
                             CreateSale("2013-10-13, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //279
                             CreateSale("2013-10-13, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //280
                             CreateSale("2013-10-13, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //281
                             CreateSale("2013-10-13, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //282
                             CreateSale("2013-10-13, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //283
                             CreateSale("2013-10-13, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //284
                             CreateSale("2013-10-13, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //285
                             CreateSale("2013-10-13, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //286
                             CreateSale("2013-10-13, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //287
                             CreateSale("2013-10-13, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //288
                             CreateSale("2013-10-13, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //289
                             CreateSale("2013-10-13, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //290
                             CreateSale("2013-10-13, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //291
                             CreateSale("2013-10-13, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //292
                             CreateSale("2013-10-13, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //293
                             CreateSale("2013-10-13, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //294
                             CreateSale("2013-10-13, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //295
                             CreateSale("2013-10-13, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //296
                             CreateSale("2013-10-13, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //297
                             CreateSale("2013-10-13, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //298
                             CreateSale("2013-10-13, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //299
                             CreateSale("2013-10-13, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //300
                            
                             CreateSale("2013-10-14, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 301
                             CreateSale("2013-10-14, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //302
                             CreateSale("2013-10-14, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //303
                             CreateSale("2013-10-14, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //304
                             CreateSale("2013-10-14, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //305
                             CreateSale("2013-10-14, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //306
                             CreateSale("2013-10-14, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //308
                             CreateSale("2013-10-14, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //309
                             CreateSale("2013-10-14, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //310
                             CreateSale("2013-10-14, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //310
                             CreateSale("2013-10-14, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //311
                             CreateSale("2013-10-14, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //312
                             CreateSale("2013-10-14, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //313
                             CreateSale("2013-10-14, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //314
                             CreateSale("2013-10-14, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //315
                             CreateSale("2013-10-14, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //316
                             CreateSale("2013-10-14, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //317
                             CreateSale("2013-10-14, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //318
                             CreateSale("2013-10-14, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //319
                             CreateSale("2013-10-14, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //320
                             CreateSale("2013-10-14, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //321
                             CreateSale("2013-10-14, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //322
                             CreateSale("2013-10-14, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //323
                             CreateSale("2013-10-14, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //324
                             CreateSale("2013-10-14, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //325
                             CreateSale("2013-10-14, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //326
                             CreateSale("2013-10-14, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //327
                             CreateSale("2013-10-14, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //328
                             CreateSale("2013-10-14, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //329
                             CreateSale("2013-10-14, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //330
                             CreateSale("2013-10-14, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //331
                             CreateSale("2013-10-14, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //332
                             CreateSale("2013-10-14, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //333
                             CreateSale("2013-10-14, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //334
                             CreateSale("2013-10-14, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //335
                             CreateSale("2013-10-14, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //336
                             CreateSale("2013-10-14, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //337
                             CreateSale("2013-10-14, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //338
                             CreateSale("2013-10-14, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //339
                             CreateSale("2013-10-14, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //340
                             CreateSale("2013-10-14, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //341
                             CreateSale("2013-10-14, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //342
                             CreateSale("2013-10-14, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //343
                             CreateSale("2013-10-14, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //344
                             CreateSale("2013-10-14, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //345
                             CreateSale("2013-10-14, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //346
                             CreateSale("2013-10-15, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //347
                             CreateSale("2013-10-15, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //348
                             CreateSale("2013-10-15, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //349
                             CreateSale("2013-10-15, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //350
                             CreateSale("2013-10-15, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //351
                             CreateSale("2013-10-15, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //352
                             CreateSale("2013-10-15, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //353
                             CreateSale("2013-10-15, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //354
                             CreateSale("2013-10-15 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //355
                             CreateSale("2013-10-15, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //356
                             CreateSale("2013-10-15, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //357
                             CreateSale("2013-10-15, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //358
                             CreateSale("2013-10-15, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //359
                             CreateSale("2013-10-15, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //360
                             CreateSale("2013-10-15, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //361
                             CreateSale("2013-10-15, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //362
                             CreateSale("2013-10-15, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //363
                             CreateSale("2013-10-15, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //364
                             CreateSale("2013-10-15, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //365
                             CreateSale("2013-10-15, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //366
                             CreateSale("2013-10-15, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //367
                             CreateSale("2013-10-15, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //368
                             CreateSale("2013-10-15, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //369
                             CreateSale("2013-10-15, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //370
                             CreateSale("2013-10-15, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //371
                             CreateSale("2013-10-15, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //372
                             CreateSale("2013-10-16, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //373
                             CreateSale("2013-10-16, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //374
                             CreateSale("2013-10-16, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //375
                             CreateSale("2013-10-16, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //376
                             CreateSale("2013-10-16, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //377
                             CreateSale("2013-10-16, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //378
                             CreateSale("2013-10-16, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //379
                             CreateSale("2013-10-16, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //380
                             CreateSale("2013-10-16, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //381
                             CreateSale("2013-10-16, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //382
                             CreateSale("2013-10-16, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //383
                             CreateSale("2013-10-16, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //384
                             CreateSale("2013-10-16, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //385
                             CreateSale("2013-10-16, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //386
                             CreateSale("2013-10-16, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //387
                             CreateSale("2013-10-16, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //388
                             CreateSale("2013-10-16, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //389
                             CreateSale("2013-10-16, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //390
                             CreateSale("2013-10-16, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //391
                             CreateSale("2013-10-16, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //392
                             CreateSale("2013-10-16, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //393
                             CreateSale("2013-10-16, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //394
                             CreateSale("2013-10-16, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //395
                             CreateSale("2013-10-16, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //396
                             CreateSale("2013-10-16, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //397
                             CreateSale("2013-10-16, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //398
                             CreateSale("2013-10-16, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //399
                             CreateSale("2013-10-16, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //400
                            
                             CreateSale("2013-10-19, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 401
                             CreateSale("2013-10-19, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //402
                             CreateSale("2013-10-19, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //403
                             CreateSale("2013-10-19, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //404
                             CreateSale("2013-10-19, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //405
                             CreateSale("2013-10-19, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //406
                             CreateSale("2013-10-19, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //408
                             CreateSale("2013-10-19, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //409
                             CreateSale("2013-10-19, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //410
                             CreateSale("2013-10-19, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //410
                             CreateSale("2013-10-19, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //411
                             CreateSale("2013-10-19, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //412
                             CreateSale("2013-10-19, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //413
                             CreateSale("2013-10-19, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //414
                             CreateSale("2013-10-19, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //415
                             CreateSale("2013-10-19, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //416
                             CreateSale("2013-10-19, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //417
                             CreateSale("2013-10-19, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //418
                             CreateSale("2013-10-19, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //419
                             CreateSale("2013-10-19, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //420
                             CreateSale("2013-10-19, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //421
                             CreateSale("2013-10-19, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //422
                             CreateSale("2013-10-19, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //423
                             CreateSale("2013-10-19, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //424
                             CreateSale("2013-10-19, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //425
                             CreateSale("2013-10-19, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //426
                             CreateSale("2013-10-19, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //427
                             CreateSale("2013-10-19, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //428
                             CreateSale("2013-10-19, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //429
                             CreateSale("2013-10-19, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //430
                             CreateSale("2013-10-19, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //431
                             CreateSale("2013-10-19, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //432
                             CreateSale("2013-10-19, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //433
                             CreateSale("2013-10-19, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //434
                             CreateSale("2013-10-19, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //435
                             CreateSale("2013-10-19, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //436
                             CreateSale("2013-10-19, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //437
                             CreateSale("2013-10-19, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //438
                             CreateSale("2013-10-19, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //439
                             CreateSale("2013-10-19, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //440
                             CreateSale("2013-10-19, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //441
                             CreateSale("2013-10-19, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //442
                             CreateSale("2013-10-19, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //443
                             CreateSale("2013-10-19, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //444
                             CreateSale("2013-10-19, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //445
                             CreateSale("2013-10-19, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //446
                             CreateSale("2013-10-19, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //447
                             CreateSale("2013-10-20, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //448
                             CreateSale("2013-10-20, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //449
                             CreateSale("2013-10-20, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //450
                             CreateSale("2013-10-20, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //451
                             CreateSale("2013-10-20, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //452
                             CreateSale("2013-10-20, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //453
                             CreateSale("2013-10-20, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //454
                             CreateSale("2013-10-20, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //455
                             CreateSale("2013-10-20, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //456
                             CreateSale("2013-10-20, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //457
                             CreateSale("2013-10-20, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //458
                             CreateSale("2013-10-20, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //459
                             CreateSale("2013-10-20, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //460
                             CreateSale("2013-10-20, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //461
                             CreateSale("2013-10-20, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //462
                             CreateSale("2013-10-20, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //463
                             CreateSale("2013-10-20, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //464
                             CreateSale("2013-10-20, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //465
                             CreateSale("2013-10-20, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //466
                             CreateSale("2013-10-20, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //467
                             CreateSale("2013-10-20, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //468
                             CreateSale("2013-10-20, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //469
                             CreateSale("2013-10-20, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //470
                             CreateSale("2013-10-20, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //471
                             CreateSale("2013-10-20, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //472
                             CreateSale("2013-10-21, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //473
                             CreateSale("2013-10-21, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //474
                             CreateSale("2013-10-21, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //475
                             CreateSale("2013-10-21, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //476
                             CreateSale("2013-10-21, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //477
                             CreateSale("2013-10-21, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //478
                             CreateSale("2013-10-21, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, false), //479
                             CreateSale("2013-10-21, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //480
                             CreateSale("2013-10-21, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //481
                             CreateSale("2013-10-21, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //482
                             CreateSale("2013-10-21, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //483
                             CreateSale("2013-10-21, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //484
                             CreateSale("2013-10-21, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //485
                             CreateSale("2013-10-21, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //486
                             CreateSale("2013-10-21, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //487
                             CreateSale("2013-10-21, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //488
                             CreateSale("2013-10-21, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //489
                             CreateSale("2013-10-21, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //490
                             CreateSale("2013-10-21, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //491
                             CreateSale("2013-10-21, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //492
                             CreateSale("2013-10-21, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //493
                             CreateSale("2013-10-21, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //494
                             CreateSale("2013-10-21, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //495
                             CreateSale("2013-10-21, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //496
                             CreateSale("2013-10-21, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //497
                             CreateSale("2013-10-21, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //498
                             CreateSale("2013-10-21, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //499
                             CreateSale("2013-10-21, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //500

                            #endregion

                #region October Sales
                             CreateSale("2013-10-01, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 1
                             CreateSale("2013-10-01, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //2
                             CreateSale("2013-10-01, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //3
                             CreateSale("2013-10-01, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //4
                             CreateSale("2013-10-01, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //5
                             CreateSale("2013-10-01, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //6
                             CreateSale("2013-10-01, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //7
                             CreateSale("2013-10-01, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //8
                             CreateSale("2013-10-01, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //9
                             CreateSale("2013-10-01, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //10
                             CreateSale("2013-10-01, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //11
                             CreateSale("2013-10-01, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //12
                             CreateSale("2013-10-01, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //13
                             CreateSale("2013-10-01, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //14
                             CreateSale("2013-10-01, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //15
                             CreateSale("2013-10-01, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //16
                             CreateSale("2013-10-01, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //17
                             CreateSale("2013-10-01, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //18
                             CreateSale("2013-10-01, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //19
                             CreateSale("2013-10-01, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //20
                             CreateSale("2013-10-01, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //21
                             CreateSale("2013-10-01, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //22
                             CreateSale("2013-10-01, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //23
                             CreateSale("2013-10-01, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //24
                             CreateSale("2013-10-01, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //25
                             CreateSale("2013-10-01, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //26
                             CreateSale("2013-10-01, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //27
                             CreateSale("2013-10-01, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //28
                             CreateSale("2013-10-01, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //29
                             CreateSale("2013-10-01, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //30
                             CreateSale("2013-10-01, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //31
                             CreateSale("2013-10-01, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //32
                             CreateSale("2013-10-01, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //33
                             CreateSale("2013-10-01, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //34
                             CreateSale("2013-10-01, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //35
                             CreateSale("2013-10-01, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //36
                             CreateSale("2013-10-01, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //37
                             CreateSale("2013-10-01, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //38
                             CreateSale("2013-10-01, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //39
                             CreateSale("2013-10-01, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //40
                             CreateSale("2013-10-01, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //41
                             CreateSale("2013-10-01, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //42
                             CreateSale("2013-10-01, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //43
                             CreateSale("2013-10-01, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //44
                             CreateSale("2013-10-01, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //45
                             CreateSale("2013-10-01, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //46
                             CreateSale("2013-10-02, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //47
                             CreateSale("2013-10-02, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //48
                             CreateSale("2013-10-02, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //49
                             CreateSale("2013-10-02, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //50
                             CreateSale("2013-10-02, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //51
                             CreateSale("2013-10-02, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //52
                             CreateSale("2013-10-02, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //53
                             CreateSale("2013-10-02, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //54
                             CreateSale("2013-10-02, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, true), //55
                             CreateSale("2013-10-02, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //56
                             CreateSale("2013-10-02, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //57
                             CreateSale("2013-10-02, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //58
                             CreateSale("2013-10-02, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //59
                             CreateSale("2013-10-02, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //60
                             CreateSale("2013-10-02, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //61
                             CreateSale("2013-10-02, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //62
                             CreateSale("2013-10-02, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //63
                             CreateSale("2013-10-02, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, true), //64
                             CreateSale("2013-10-02, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //65
                             CreateSale("2013-10-02, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //66
                             CreateSale("2013-10-02, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //67
                             CreateSale("2013-10-02, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, false), //68
                             CreateSale("2013-10-02, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //69
                             CreateSale("2013-10-02, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //70
                             CreateSale("2013-10-02, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //71
                             CreateSale("2013-10-02, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //72
                             CreateSale("2013-10-03, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //73
                             CreateSale("2013-10-03, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //74
                             CreateSale("2013-10-03, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //75
                             CreateSale("2013-10-03, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //76
                             CreateSale("2013-10-03, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //77
                             CreateSale("2013-10-03, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //78
                             CreateSale("2013-10-03, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, true), //79
                             CreateSale("2013-10-03, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //80
                             CreateSale("2013-10-03, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //81
                             CreateSale("2013-10-03, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //82
                             CreateSale("2013-10-03, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //83
                             CreateSale("2013-10-03, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //84
                             CreateSale("2013-10-03, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //85
                             CreateSale("2013-10-03, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //86
                             CreateSale("2013-10-03, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //87
                             CreateSale("2013-10-03, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //88
                             CreateSale("2013-10-03, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //89
                             CreateSale("2013-10-03, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //90
                             CreateSale("2013-10-03, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //91
                             CreateSale("2013-10-03, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //92
                             CreateSale("2013-10-03, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //93
                             CreateSale("2013-10-03, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //94
                             CreateSale("2013-10-03, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //95
                             CreateSale("2013-10-03, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //96
                             CreateSale("2013-10-03, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //97
                             CreateSale("2013-10-03, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //98
                             CreateSale("2013-10-03, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //99
                             CreateSale("2013-10-03, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //100

                             CreateSale("2013-10-04, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 101
                             CreateSale("2013-10-04, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //102
                             CreateSale("2013-10-04, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //103
                             CreateSale("2013-10-04, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //104
                             CreateSale("2013-10-04, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //105
                             CreateSale("2013-10-04, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //106
                             CreateSale("2013-10-04, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //108
                             CreateSale("2013-10-04, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //109
                             CreateSale("2013-10-04, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //110
                             CreateSale("2013-10-04, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //110
                             CreateSale("2013-10-04, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //111
                             CreateSale("2013-10-04, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //112
                             CreateSale("2013-10-04, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //113
                             CreateSale("2013-10-04, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //114
                             CreateSale("2013-10-04, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //115
                             CreateSale("2013-10-04, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //116
                             CreateSale("2013-10-04, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //117
                             CreateSale("2013-10-04, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //118
                             CreateSale("2013-10-04, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //119
                             CreateSale("2013-10-04, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //120
                             CreateSale("2013-10-04, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //121
                             CreateSale("2013-10-04, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //122
                             CreateSale("2013-10-04, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //123
                             CreateSale("2013-10-04, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //124
                             CreateSale("2013-10-04, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //125
                             CreateSale("2013-10-04, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //126
                             CreateSale("2013-10-04, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //127
                             CreateSale("2013-10-04, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //128
                             CreateSale("2013-10-04, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //129
                             CreateSale("2013-10-04, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //130
                             CreateSale("2013-10-04, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //131
                             CreateSale("2013-10-04, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //132
                             CreateSale("2013-10-04, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //133
                             CreateSale("2013-10-04, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //134
                             CreateSale("2013-10-04, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //135
                             CreateSale("2013-10-04, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //136
                             CreateSale("2013-10-04, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //137
                             CreateSale("2013-10-04, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //138
                             CreateSale("2013-10-04, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //139
                             CreateSale("2013-10-04, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //140
                             CreateSale("2013-10-04, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //141
                             CreateSale("2013-10-04, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //142
                             CreateSale("2013-10-04, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //143
                             CreateSale("2013-10-04, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //144
                             CreateSale("2013-10-04, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //145
                             CreateSale("2013-10-04, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //146
                             CreateSale("2013-10-05, 10:04 am",     33.95M,      40M,       6.05M,         null,   1, 4, false), //147
                             CreateSale("2013-10-05, 10:10 am",     20.95M,      25M,       4.05M,         null,   1, 4, false), //148
                             CreateSale("2013-10-05, 10:15 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //149
                             CreateSale("2013-10-05, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 4, false), //150
                             CreateSale("2013-10-05, 10:27 am",     9.95M,       10M,       0.05M,         null,   1, 4, false), //151
                             CreateSale("2013-10-05, 10:35 am",     28.95M,      30M,       1.05M,         null,   1, 4, false), //152
                             CreateSale("2013-10-05, 11:00 am",     11.95M,      15M,       3.05M,         null,   1, 4, false), //153
                             CreateSale("2013-10-05, 11:15 am",     126.90M,     126.90M,   0.00M,         4,      1, 4, true),  //154
                             CreateSale("2013-10-05, 11:24 am",     7.95M,       10M,       2.05M,         1,      1, 4, false), //155
                             CreateSale("2013-10-05, 11:50 am",     10.95M,      11M,       0.05M,         null,   1, 4, false), //156
                             CreateSale("2013-10-05, 12:05 pm",     12.95M,      13M,       0.05M,         null,   1, 4, false), //157
                             CreateSale("2013-10-05, 12:30 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //158
                             CreateSale("2013-10-05, 12:45 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //159
                             CreateSale("2013-10-05, 12:58 pm",     13.95M,      20M,       6.05M,         null,   1, 4, false), //160
                             CreateSale("2013-10-05, 1:10 pm",      27.95M,      30M,       2.05M,         null,   1, 2, false), //161
                             CreateSale("2013-10-05, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 2, false), //162
                             CreateSale("2013-10-05, 1:21 pm",      6.50M,       7M,        0.50M,         null,   1, 2, false), //163
                             CreateSale("2013-10-05, 1:30 pm",      27.95M,      30M,       2.05M,         1,      1, 2, false), //164
                             CreateSale("2013-10-05, 1:44 pm",      39.95M,      40M,       0.05M,         null,   1, 2, false), //165
                             CreateSale("2013-10-05, 2:05 pm",      20.90M,      25M,       4.10M,         null,   1, 2, false), //166
                             CreateSale("2013-10-05, 2:19 pm",      12.95M,      14M,       1.05M,         null,   1, 2, false), //167
                             CreateSale("2013-10-05, 2:35 pm",      7.95M,       10M,       2.05M,          3,      1, 2, true), //168
                             CreateSale("2013-10-05, 2:46 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //169
                             CreateSale("2013-10-05, 3:15 pm",      6.95M,       7M,        0.05M,         null,   1, 2, false), //170
                             CreateSale("2013-10-05, 3:24 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //171
                             CreateSale("2013-10-05, 3:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //172
                             CreateSale("2013-10-06, 10:10 am",     20.95M,      21M,       0.05M,         null,   1, 2, false), //173
                             CreateSale("2013-10-06, 10:21 am",     17.95M,      20M,       2.05M,         2,      1, 2, false), //174
                             CreateSale("2013-10-06, 10:27 am",     32.95M,      40M,       7.05M,         null,   1, 2, false), //175
                             CreateSale("2013-10-06, 10:35 am",     24.95M,      25M,       0.05M,         null,   1, 2, false), //176
                             CreateSale("2013-10-06, 10:46 am",     23.95M,      25M,       1.05M,         null,   1, 2, false), //177
                             CreateSale("2013-10-06, 11:05 am",     13.95M,      14M,       0.05M,         null,   1, 2, false), //178
                             CreateSale("2013-10-06, 11:17 am",     10.95M,      20M,       9.05M,         4,      1, 2, true), //179
                             CreateSale("2013-10-06, 11:50 am",     14.95M,      15M,       0.05M,         null,   1, 2, false), //180
                             CreateSale("2013-10-06, 12:10 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //181
                             CreateSale("2013-10-06, 12:15 pm",     7.95M,       10,        2.05M,         null,   1, 2, false), //182
                             CreateSale("2013-10-06, 12:30 pm",     11.50M,      12M,       0.50M,         null,   1, 2, false), //183
                             CreateSale("2013-10-06, 12:33 pm",     13.95M,      20M,       6.05M,         null,   1, 2, false), //184
                             CreateSale("2013-10-06, 1:05 pm",      29.95M,      30M,       0.05M,         null,   1, 4, false), //185
                             CreateSale("2013-10-06, 1:22 pm",      15.95M,      20M,       4.05M,         null,   1, 4, false), //186
                             CreateSale("2013-10-06, 1:34 pm",      27.95M,      30M,       2.05M,         null,   1, 4, false), //187
                             CreateSale("2013-10-06, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //188
                             CreateSale("2013-10-06, 2:05 pm",      7.95M,       10M,       2.05M,         null,   1, 4, false), //189
                             CreateSale("2013-10-06, 2:10 pm",      9.95M,       10M,       0.05M,         null,   1, 4, false), //190
                             CreateSale("2013-10-06, 2:26 pm",      14.95M,      15M,       0.05M,         null,   1, 4, false), //191
                             CreateSale("2013-10-06, 2:30 pm",      12.95M,      15M,       2.05M,         null,   1, 4, false), //192
                             CreateSale("2013-10-06, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 4, false), //193
                             CreateSale("2013-10-06, 3:00 pm",     23.90M,      30M,       6.10M,         null,   1, 2, false), //194
                             CreateSale("2013-10-06, 3:35 pm",     7.95M,       10M,       2.05M,         null,   1, 2, false), //195
                             CreateSale("2013-10-06, 3:40 pm",     10.95M,      11M,       0.05M,         null,   1, 4, false), //196
                             CreateSale("2013-10-06, 3:45 pm",     9.95M,       10M,       0.05M,         null,   1, 2, false), //197
                             CreateSale("2013-10-06, 3:50 pm",      2.95M,       3M,        0.05M,         null,   1, 2, false), //198
                             CreateSale("2013-10-06, 3:53 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //199
                             CreateSale("2013-10-06, 3:55 pm",     7.95M,       10M,       2.05M,         null,   1, 4, false), //200

                             CreateSale("2013-10-08, 10:00 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //SaleID 201
                             CreateSale("2013-10-08, 10:20 am",     7.95M,       10M,       2.05M,         null,   1, 2, false), //202
                             CreateSale("2013-10-08, 10:24 am",     15.95M,      20M,       4.05M,         null,   1, 2, false), //203
                             CreateSale("2013-10-08, 10:30 am",     32.95M,      50M,       17.05M,        4,      1, 2, false), //204
                             CreateSale("2013-10-08, 10:36 am",     22.95M,      25M,       2.05M,         null,   1, 2, false), //205
                             CreateSale("2013-10-08, 10:40 am",     11.80M,      20M,       8.20M,         null,   1, 2, false), //206
                             CreateSale("2013-10-08, 10:45 am",     24.90M,      30M,       5.10M,         null,   1, 2, false), //208
                             CreateSale("2013-10-08, 10:55 am",     23.90M,      30M,       6.10M,         null,   1, 2, false), //209
                             CreateSale("2013-10-08, 11:08 am",     10.95M,      20M,       9.05M,         null,   1, 2, false), //210
                             CreateSale("2013-10-08, 11:10 am",     23.95M,      30M,       6.05M,         null,   1, 2, false), //210
                             CreateSale("2013-10-08, 11:19 am",     11.95M,      15M,       3.05M,         null,   1, 2, false), //211
                             CreateSale("2013-10-08, 11:27 am",     10.95M,      15M,       4.05M,         null,   1, 2, false), //212
                             CreateSale("2013-10-08, 11:30 am",     11.95M,      12M,       0.05M,         null,   1, 2, false), //213
                             CreateSale("2013-10-08, 11:36 am",     32.20M,      50M,       17.80M,        null,   1, 2, false), //214
                             CreateSale("2013-10-08, 11:44 am",     10.75M,      20M,       9.25M,         null,   1, 2, false), //215
                             CreateSale("2013-10-08, 11:48 am",     15.90M,      20M,       4.10M,         null,   1, 2, false), //216
                             CreateSale("2013-10-08, 11:55 am",     17.75M,      20M,       2.25M,         null,   1, 2, false), //217
                             CreateSale("2013-10-08, 12:05 pm",     15.45M,      20M,       4.55M,         null,   1, 2, false), //218
                             CreateSale("2013-10-08, 12:15 pm",     31.90M,      35M,       3.10M,         null,   1, 2, false), //219
                             CreateSale("2013-10-08, 12:20 pm",     5.50M,       6M,        0.50M,         null,   1, 2, false), //220
                             CreateSale("2013-10-08, 12:30 pm",     24.95M,      25M,       0.05M,         null,   1, 2, false), //221
                             CreateSale("2013-10-08, 12:45 pm",     14.95M,      20M,       5.05M,         null,   1, 2, false), //222
                             CreateSale("2013-10-08, 1:01 pm",      12.95M,      15M,       2.05M,         null,   1, 3, false), //223
                             CreateSale("2013-10-08, 1:04 pm",      29.85M,      30M,       0.15M,         null,   1, 3, false), //224
                             CreateSale("2013-10-08, 1:10 pm",      3.90M,       5M,        1.10M,         null,   1, 3, false), //225
                             CreateSale("2013-10-08, 1:15 pm",      11.95M,      12M,       0.05M,         null,   1, 3, false), //226
                             CreateSale("2013-10-08, 1:17 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //227
                             CreateSale("2013-10-08, 1:21 pm",      10.00M,      10M,       0.00M,         null,   1, 3, false), //228
                             CreateSale("2013-10-08, 1:29 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //229
                             CreateSale("2013-10-08, 1:35 pm",      5.50M,       10M,       4.50M,         null,   1, 3, false), //230
                             CreateSale("2013-10-08, 1:42 pm",      19.90M,      20M,       0.10M,         null,   1, 3, false), //231
                             CreateSale("2013-10-08, 1:45 pm",      7.95M,       10M,       2.05M,         null,   1, 3, false), //232
                             CreateSale("2013-10-08, 1:53 pm",      39.95M,      40M,       0.05M,         null,   1, 3, false), //233
                             CreateSale("2013-10-08, 2:01 pm",      61.95M,      70M,       8.05M,         null,   1, 2, false), //234
                             CreateSale("2013-10-08, 2:10 pm",      13.95M,      13.95M,    0.00M,         null,   1, 2, false), //235
                             CreateSale("2013-10-08, 2:22 pm",      9.95M,       10M,       0.05M,         null,   1, 2, false), //236
                             CreateSale("2013-10-08, 2:40 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //237
                             CreateSale("2013-10-08, 2:44 pm",      34.95M,      50M,       15.05M,        null,   1, 2, false), //238
                             CreateSale("2013-10-08, 2:59 pm",      10.95M,      11M,       0.05M,         null,   1, 2, false), //239
                             CreateSale("2013-10-08, 3:06 pm",      13.50M,      15M,       1.50M,         null,   1, 2, false), //240
                             CreateSale("2013-10-08, 3:17 pm",      4.95M,       5M,        0.05M,         null,   1, 2, false), //241
                             CreateSale("2013-10-08, 3:25 pm",      7.95M,       10M,       2.05M,         null,   1, 2, false), //242
                             CreateSale("2013-10-08, 3:38 pm",      5.50M,       6M,        0.50M,         null,   1, 1, false), //243
                             CreateSale("2013-10-08, 3:47 pm",      30.90M,      50M,       19.10M,        null,   1, 2, false), //244
                             CreateSale("2013-10-08, 4:05 pm",      23.95M,      30M,       6.05M,         null,   1, 2, false), //245
                             CreateSale("2013-10-08, 4:25 pm",      15.95M,      20M,       4.05M,         null,   1, 2, false), //246
                            #endregion
            }.ForEach(i => context.Sales.Add(i));

            new List<SalesItem>
            {
                            // saleId, productId, quantity, price, cost
                #region July Sale Items
                            // Sale No.1
                            CreateSalesItem(1,      8,      1,      10.95M,     8M),
                            CreateSalesItem(1,      9,      1,      12.95M,     9M),                
                            CreateSalesItem(2,      20,     1,      7.95M,      5M),     // 2                
                            CreateSalesItem(3,      127,    1,      15.95M,     10M),    // 3               
                            CreateSalesItem(4,      116,    1,      32.95M,     20M),    // 4                
                            CreateSalesItem(5,      118,    1,      22.95M,     15M),    // 5                
                            CreateSalesItem(6,      89,     4,      2.95M,      1M),     // 6               
                            CreateSalesItem(7,      128,    1,      11.95M,     7M),    // 7
                            CreateSalesItem(7,      25,     1,      12.95M,     10M),                
                            CreateSalesItem(8,      8,      1,      10.95M,     8M),    // 8
                            CreateSalesItem(8,      9,      1,      12.95M,     9M),               
                            CreateSalesItem(9,      8,      1,      10.95M,     8M),    // 9                
                            CreateSalesItem(10,     98,     1,      23.95M,     15M),    // 10                
                            CreateSalesItem(11,     128,    1,      11.95M,     7M),    // 11               
                            CreateSalesItem(12,     8,      1,      11.95M,     8M),    // 12                
                            CreateSalesItem(13,     28,     1,      11.95M,     9M),    // 13               
                            CreateSalesItem(14,     94,     1,      18.95M,     15M),    // 14
                            CreateSalesItem(14,     95,     1,      13.25M,     9M),               
                            CreateSalesItem(15,     113,    1,      10.75M,     7M),    // 15               
                            CreateSalesItem(16,     1,      1,      7.95M,      5M),     // 16
                            CreateSalesItem(16,     3,      1,      7.95M,      5M),
                            CreateSalesItem(17,     74,     2,      3.55M,      1.50M),     // 17
                            CreateSalesItem(17,     75,     1,      3.55M,      1.50M),
                            CreateSalesItem(17,     76,     1,      3.55M,      1.50M),
                            CreateSalesItem(17,     77,     1,      3.55M,      1.50M),                
                            CreateSalesItem(18,     126,    1,      9.95M,      6M),     // 18
                            CreateSalesItem(18,     29,     1,      5.50M,      3M),                
                            CreateSalesItem(19,     5,      2,      15.95M,     10M),    // 19               
                            CreateSalesItem(20,     61,     1,      5.50M,      3M),     // 20               
                            CreateSalesItem(21,     63,     1,      24.95M,     18M),    // 21               
                            CreateSalesItem(22,     123,    1,      14.95M,     10M),    // 22               
                            CreateSalesItem(23,     117,    1,      12.95M,     7M),    // 23                
                            CreateSalesItem(24,     20,     1,      7.95M,      5M),     // 24
                            CreateSalesItem(24,     19,     1,      13.95M,     10M),
                            CreateSalesItem(24,     1,      1,      7.95M,      5M),
                            CreateSalesItem(25,     23,     1,      1.95M,      1M),     // 25
                            CreateSalesItem(26,     128,    1,      11.95M,     7M),    // 26
                            CreateSalesItem(27,     21,     1,      7.95M,      5M),     // 27                
                            CreateSalesItem(28,     27,     1,      10.00M,     8M),    // 28                
                            CreateSalesItem(29,     125,    1,      7.95M,      5M),     // 29              
                            CreateSalesItem(30,     30,     1,      5.50M,      3M),     // 30               
                            CreateSalesItem(31,     3,      1,      7.95M,      5M),     // 31
                            CreateSalesItem(31,     28,     1,      11.95M,     9M),               
                            CreateSalesItem(32,     3,      1,      7.95M,      5M),     // 32                
                            CreateSalesItem(33,     49,     1,      39.95M,     25M),    // 33                
                            CreateSalesItem(34,     67,     1,      61.95M,     55M),    // 34                
                            CreateSalesItem(35,     71,     1,      13.95M,     8M),    // 35                
                            CreateSalesItem(36,     46,     1,      9.95M,      6M),     // 36                
                            CreateSalesItem(37,     8,      1,      10.95M,     8M),    // 37
                            CreateSalesItem(38,     35,     1,      34.95M,     25M),    // 38           
                            CreateSalesItem(39,     7,      1,      10.95M,     7M),    // 39               
                            CreateSalesItem(40,     97,     1,      13.50M,     9M),    // 40              
                            CreateSalesItem(41,     45,     1,      4.95M,      2M),     // 41               
                            CreateSalesItem(42,     3,      1,      7.95M,      5M),     // 42
                            CreateSalesItem(43,     32,     1,      5.50M,      3M),     // 43
                            CreateSalesItem(44,     40,     1,      19.95M,     15M),    // 44
                            CreateSalesItem(44,     8,      1,      10.95M,     8M),
                            CreateSalesItem(45,     99,     1,      23.95M,     15M),    // 45
                            CreateSalesItem(46,     83,     1,      15.95M,     10M),     // 46
                            CreateSalesItem(47,     120,    1,      33.95M,     20M),     //47
                            CreateSalesItem(48,     11,     1,      20.95M,     12M),    //48
                            CreateSalesItem(49,     2,      1,       7.95M,     5M),    //49
                            CreateSalesItem(50,     1,      1,       7.95M,     5M),    //50
                            CreateSalesItem(51,     111,    1,       9.95M,     5M),    //51
                            CreateSalesItem(52,     47,     1,       28.95M,    20M),   //52
                            CreateSalesItem(53,     128,    1,       11.95M,    7M),    //53
                            CreateSalesItem(54,     68,     1,      83.95M,     75M),    //54
                            CreateSalesItem(54,     65,     1,      42.95M,     35M), 
                            CreateSalesItem(55,     105,    1,      7.95M,      5M),     //55
                            CreateSalesItem(56,     8,      1,      10.95M,     8M),    //56
                            CreateSalesItem(57,     9,      1,      12.95M,     9M),    //57
                            CreateSalesItem(58,     8,      1,      10.95M,     8M),    //58
                            CreateSalesItem(59,     3,      1,      7.95M,      5M),     //59
                            CreateSalesItem(60,     19,     1,      13.95M,     10M),    //60
                            CreateSalesItem(61,     39,     1,      27.95M,     20M),    //61
                            CreateSalesItem(62,     128,    1,      11.95M,     7M),    //62
                            CreateSalesItem(63,     114,    1,      6.50M,      3.5M),     //63
                            CreateSalesItem(64,     41,     1,      27.95M,     20M),    //64
                            CreateSalesItem(65,     49,     1,      39.95M,     25M),    //65
                            CreateSalesItem(66,     3,      1,      7.95M,      5M),     //66
                            CreateSalesItem(66,     17,     1,      12.95M,     10M),   
                            CreateSalesItem(67,     9,      1,      12.95M,     9M),    //67
                            CreateSalesItem(68,     3,      1,      7.95M,      5M),     //68
                            CreateSalesItem(69,     127,    1,      15.95M,     10M),    //69
                            CreateSalesItem(70,     102,    1,      6.95M,      3M),     //70
                            CreateSalesItem(71,     91,     1,      2.95M,      1M),     //71
                            CreateSalesItem(72,     8,      1,      10.95M,     8M),    //72
                            CreateSalesItem(73,     11,     1,      20.95M,     12M),    //73
                            CreateSalesItem(74,     70,     1,      17.95M,     12M),    //74
                            CreateSalesItem(75,     116,    1,      32.95M,     20M),    //75
                            CreateSalesItem(76,     101,    1,      24.95M,     18M),    //76
                            CreateSalesItem(77,     99,     1,      23.95M,     15M),    //77
                            CreateSalesItem(78,     71,     1,      13.95M,     8M),    //78
                            CreateSalesItem(79,     8,      1,      10.95M,     8M),    //79
                            CreateSalesItem(80,     53,     1,      14.95M,     8M),    //80
                            CreateSalesItem(81,     58,     1,      9.95M,      6M),     //81
                            CreateSalesItem(82,     3,      1,      7.95M,      5M),     //82
                            CreateSalesItem(83,     26,     1,      11.50M,     9M),    //83
                            CreateSalesItem(84,     38,     1,      13.95M,     10M),    //84
                            CreateSalesItem(85,     36,     1,      29.95M,     20M),    //85
                            CreateSalesItem(86,     122,    1,      15.95M,     10M),    //86
                            CreateSalesItem(87,     66,     1,      27.95M,     22M),    //87
                            CreateSalesItem(88,     2,      1,      7.95M,      5M),     //88
                            CreateSalesItem(89,     3,      1,      7.95M,      5M),     //89
                            CreateSalesItem(90,     124,    1,      9.95M,      6M),     //90
                            CreateSalesItem(91,     123,    1,      14.95M,     10M),    //91
                            CreateSalesItem(92,     9,      1,      12.95M,     9M),    //92
                            CreateSalesItem(93,     8,      1,      10.95M,     8M),    //93
                            CreateSalesItem(94,    8,       1,      10.95M,     8M),     //94
                            CreateSalesItem(94,     9,      1,      12.95M,     9M),
                            CreateSalesItem(95,      20,    1,      7.95M,      5M),     //95
                            CreateSalesItem(96,     8,      1,      10.95M,     8M),   //96
                            CreateSalesItem(97,     58,     1,      9.95M,      6M),     //97
                            CreateSalesItem(98,     91,     1,      2.95M,      1M),     //98
                            CreateSalesItem(99,     40,     1,      19.95M,     15M),    // 99
                            CreateSalesItem(99,     8,      1,      10.95M,     8M),
                            CreateSalesItem(100,     3,     1,      7.95M,      5M),     //100

                             // Sale No.101
                            CreateSalesItem(101,      8,      1,      10.95M     ,8M),
                            CreateSalesItem(101,      9,      1,      12.95M     ,9M),                   
                            CreateSalesItem(102,      20,     1,      7.95M      ,5M),        // 102                
                            CreateSalesItem(103,      127,    1,      15.95M     ,10M),      // 103               
                            CreateSalesItem(104,      116,    1,      32.95M     ,20M),      // 104                
                            CreateSalesItem(105,      118,    1,      22.95M     ,15M),      // 105                
                            CreateSalesItem(106,      89,     4,      2.95M      ,1M),        // 106               
                            CreateSalesItem(108,      128,    1,      11.95M     ,7M),       // 108
                            CreateSalesItem(108,      25,     1,      12.95M     ,10M),                  
                            CreateSalesItem(109,      8,      1,      10.95M     ,8M),       // 109
                            CreateSalesItem(109,      9,      1,      12.95M     ,9M),                  
                            CreateSalesItem(110,      8,      1,      10.95M     ,8M),       // 110                
                            CreateSalesItem(110,     98,     1,      23.95M      ,15M),      // 110                
                            CreateSalesItem(111,     128,    1,      11.95M      ,7M),       // 111               
                            CreateSalesItem(112,     8,      1,      11.95M      ,8M),       // 112                
                            CreateSalesItem(113,     28,     1,      11.95M      ,9M),       // 113               
                            CreateSalesItem(114,     94,     1,      18.95M      ,15M),      // 114
                            CreateSalesItem(114,     95,     1,      13.25M      ,9M),                  
                            CreateSalesItem(115,     113,    1,      10.75M      ,7M),       // 115               
                            CreateSalesItem(116,     1,      1,      7.95M       ,5M),        // 116
                            CreateSalesItem(116,     3,      1,      7.95M       ,5M),
                            CreateSalesItem(117,     74,     2,      3.55M       ,1.50M),     // 117
                            CreateSalesItem(117,     75,     1,      3.55M       ,1.50M),
                            CreateSalesItem(117,     76,     1,      3.55M       ,1.50M),
                            CreateSalesItem(117,     77,     1,      3.55M       ,1.50M),                
                            CreateSalesItem(118,     126,    1,      9.95M      ,6M),        // 118
                            CreateSalesItem(118,     29,     1,      5.50M       ,3M),                   
                            CreateSalesItem(119,     5,      2,      15.95M      ,10M),      // 119               
                            CreateSalesItem(120,     61,     1,      5.50M       ,3M),        // 120               
                            CreateSalesItem(121,     63,     1,      24.95M      ,18M),      // 121               
                            CreateSalesItem(122,     123,    1,      14.95M      ,10M),      // 122               
                            CreateSalesItem(123,     117,    1,      12.95M      ,7M),       // 123                
                            CreateSalesItem(124,     20,     1,      7.95M       ,5M),        // 124
                            CreateSalesItem(124,     19,     1,      13.95M      ,10M),
                            CreateSalesItem(124,     1,      1,      7.95M       ,5M),
                            CreateSalesItem(125,     23,     1,      1.95M       ,1M),        // 125
                            CreateSalesItem(126,     128,    1,      11.95M      ,7M),       // 126
                            CreateSalesItem(127,     21,     1,      7.95M       ,5M),        // 127                
                            CreateSalesItem(128,     27,     1,      10.00M      ,8M),       // 128                
                            CreateSalesItem(129,     125,    1,      7.95M       ,5M),        // 129              
                            CreateSalesItem(130,     30,     1,      5.50M       ,3M),        // 130               
                            CreateSalesItem(131,     3,      1,      7.95M       ,5M),        // 131
                            CreateSalesItem(131,     28,     1,      11.95M      ,9M),                  
                            CreateSalesItem(132,     3,      1,      7.95M       ,5M),        // 132                
                            CreateSalesItem(133,     49,     1,      39.95M      ,25M),      // 133                
                            CreateSalesItem(134,     67,     1,      61.95M      ,55M),      // 134                
                            CreateSalesItem(135,     71,     1,      13.95M      ,8M),       // 135                
                            CreateSalesItem(136,     46,     1,      9.95M       ,6M),        // 136                
                            CreateSalesItem(137,     8,      1,      10.95M      ,8M),       // 137
                            CreateSalesItem(138,     35,     1,      34.95M      ,25M),      // 138           
                            CreateSalesItem(139,     7,      1,      10.95M      ,7M),       // 139               
                            CreateSalesItem(140,     97,     1,      13.50M      ,9M),       // 140              
                            CreateSalesItem(141,     45,     1,      4.95M       ,2M),        // 141               
                            CreateSalesItem(142,     3,      1,      7.95M       ,5M),        // 142
                            CreateSalesItem(143,     32,     1,      5.50M       ,3M),        // 143
                            CreateSalesItem(144,     40,     1,      19.95M      ,15M),      // 144
                            CreateSalesItem(144,     8,      1,      10.95M      ,8M),
                            CreateSalesItem(145,     99,     1,      23.95M      ,15M),      // 145
                            CreateSalesItem(146,     83,     1,      15.95M      ,10M),       // 146
                            CreateSalesItem(147,     120,    1,      33.95M      ,20M),       //147
                            CreateSalesItem(148,     11,     1,      20.95M      ,12M),      //148
                            CreateSalesItem(149,     2,      1,       7.95M      ,5M),       //149
                            CreateSalesItem(150,     1,      1,       7.95M      ,5M),       //150
                            CreateSalesItem(151,     111,    1,       9.95M      ,5M),       //151
                            CreateSalesItem(152,     47,     1,       28.95M     ,20M),     //152
                            CreateSalesItem(153,     128,    1,       11.95M     ,7M),       //153
                            CreateSalesItem(154,     68,     1,      83.95M      ,75M),      //154
                            CreateSalesItem(154,     65,     1,      42.95M      ,35M),  
                            CreateSalesItem(155,     105,    1,      7.95M       ,5M),        //155
                            CreateSalesItem(156,     8,      1,      10.95M      ,8M),       //156
                            CreateSalesItem(157,     9,      1,      12.95M      ,9M),       //157
                            CreateSalesItem(158,     8,      1,      10.95M      ,8M),       //158
                            CreateSalesItem(159,     3,      1,      7.95M       ,5M),        //159
                            CreateSalesItem(160,     19,     1,      13.95M      ,10M),      //160
                            CreateSalesItem(161,     39,     1,      27.95M      ,20M),      //161
                            CreateSalesItem(162,     128,    1,      11.95M      ,7M),       //162
                            CreateSalesItem(163,     114,    1,      6.50M       ,3.5M),      //163
                            CreateSalesItem(164,     41,     1,      27.95M      ,20M),      //164
                            CreateSalesItem(165,     49,     1,      39.95M      ,25M),      //165
                            CreateSalesItem(166,     3,      1,      7.95M       ,5M),        //166
                            CreateSalesItem(166,     17,     1,      12.95M      ,10M),     
                            CreateSalesItem(167,     9,      1,      12.95M      ,9M),       //167
                            CreateSalesItem(168,     3,      1,      7.95M       ,5M),        //168
                            CreateSalesItem(169,     127,    1,      15.95M      ,10M),      //169
                            CreateSalesItem(170,     102,    1,      6.95M       ,3M),        //170
                            CreateSalesItem(171,     91,     1,      2.95M       ,1M),        //171
                            CreateSalesItem(172,     8,      1,      10.95M      ,8M),       //172
                            CreateSalesItem(173,     11,     1,      20.95M      ,12M),      //173
                            CreateSalesItem(174,     70,     1,      17.95M      ,12M),      //174
                            CreateSalesItem(175,     116,    1,      32.95M      ,20M),      //175
                            CreateSalesItem(176,     101,    1,      24.95M      ,18M),      //176
                            CreateSalesItem(177,     99,     1,      23.95M      ,15M),      //177
                            CreateSalesItem(178,     71,     1,      13.95M      ,8M),       //178
                            CreateSalesItem(179,     8,      1,      10.95M      ,8M),       //179
                            CreateSalesItem(180,     53,     1,      14.95M      ,8M),       //180
                            CreateSalesItem(181,     58,     1,      9.95M       ,6M),        //181
                            CreateSalesItem(182,     3,      1,      7.95M       ,5M),        //182
                            CreateSalesItem(183,     26,     1,      11.50M      ,9M),       //183
                            CreateSalesItem(184,     38,     1,      13.95M      ,10M),      //184
                            CreateSalesItem(185,     36,     1,      29.95M      ,20M),      //185
                            CreateSalesItem(186,     122,    1,      15.95M      ,10M),      //186
                            CreateSalesItem(187,     66,     1,      27.95M      ,22M),      //187
                            CreateSalesItem(188,     2,      1,      7.95M       ,5M),        //188
                            CreateSalesItem(189,     3,      1,      7.95M       ,5M),        //189
                            CreateSalesItem(190,     124,    1,      9.95M       ,6M),        //190
                            CreateSalesItem(191,     123,    1,      14.95M      ,10M),      //191
                            CreateSalesItem(192,     9,      1,      12.95M      ,9M),       //192
                            CreateSalesItem(193,     8,      1,      10.95M      ,8M),       //193
                            CreateSalesItem(194,     8,      1,      10.95M      ,8M),        //194
                            CreateSalesItem(194,     9,      1,      12.95M      ,9M),
                            CreateSalesItem(195,     20,    1,      7.95M        ,5M),        //195
                            CreateSalesItem(196,     8,      1,      10.95M      ,8M),      //196
                            CreateSalesItem(197,     58,     1,      9.95M       ,6M),        //197
                            CreateSalesItem(198,     91,     1,      2.95M       ,1M),        //198
                            CreateSalesItem(199,     40,     1,      19.95M      ,15M),      // 199
                            CreateSalesItem(199,     8,      1,      10.95M      ,8M),
                            CreateSalesItem(200,    3,     1,      7.95M         ,5M),         //200

                               // Sale No.201
                            CreateSalesItem(201,      8,      1,      10.95M    ,8M),
                            CreateSalesItem(201,      9,      1,      12.95M    ,9M),                      
                            CreateSalesItem(202,      20,     1,      7.95M     ,5M),           // 202                
                            CreateSalesItem(203,      127,    1,      15.95M    ,10M),         // 203               
                            CreateSalesItem(204,      116,    1,      32.95M    ,20M),         // 204                
                            CreateSalesItem(205,      118,    1,      22.95M    ,15M),         // 205                
                            CreateSalesItem(206,      89,     4,      2.95M     ,1M),           // 206               
                            CreateSalesItem(208,      128,    1,      11.95M    ,7M),          // 208
                            CreateSalesItem(208,      25,     1,      12.95M    ,10M),                     
                            CreateSalesItem(209,      8,      1,      10.95M    ,8M),          // 209
                            CreateSalesItem(209,      9,      1,      12.95M    ,9M),                     
                            CreateSalesItem(210,      8,      1,      10.95M    ,8M),          // 210                
                            CreateSalesItem(210,     98,     1,      23.95M     ,15M),         // 210                
                            CreateSalesItem(211,     128,    1,      11.95M     ,7M),          // 211               
                            CreateSalesItem(212,     8,      1,      11.95M     ,8M),          // 212                
                            CreateSalesItem(213,     28,     1,      11.95M     ,9M),          // 213               
                            CreateSalesItem(214,     94,     1,      18.95M     ,15M),         // 214
                            CreateSalesItem(214,     95,     1,      13.25M     ,9M),                     
                            CreateSalesItem(215,     113,    1,      10.75M     ,7M),          // 215               
                            CreateSalesItem(216,     1,      1,      7.95M      ,5M),           // 216
                            CreateSalesItem(216,     3,      1,      7.95M      ,5M),
                            CreateSalesItem(217,     74,     2,      3.55M      ,1.50M),        // 217
                            CreateSalesItem(217,     75,     1,      3.55M      ,1.50M),
                            CreateSalesItem(217,     76,     1,      3.55M      ,1.50M),
                            CreateSalesItem(217,     77,     1,      3.55M      ,1.50M),                   
                            CreateSalesItem(218,     126,    1,      9.95M      ,6M),           // 218
                            CreateSalesItem(218,     29,     1,      5.50M      ,3M),                      
                            CreateSalesItem(219,     5,      2,      15.95M     ,10M),         // 219               
                            CreateSalesItem(220,     61,     1,      5.50M      ,3M),           // 220               
                            CreateSalesItem(221,     63,     1,      24.95M     ,18M),         // 221               
                            CreateSalesItem(222,     123,    1,      14.95M     ,10M),         // 222               
                            CreateSalesItem(223,     117,    1,      12.95M     ,7M),          // 223                
                            CreateSalesItem(224,     20,     1,      7.95M      ,5M),           // 224
                            CreateSalesItem(224,     19,     1,      13.95M     ,10M),
                            CreateSalesItem(224,     1,      1,      7.95M      ,5M),
                            CreateSalesItem(225,     23,     1,      1.95M      ,1M),           // 225
                            CreateSalesItem(226,     128,    1,      11.95M     ,7M),          // 226
                            CreateSalesItem(227,     21,     1,      7.95M      ,5M),           // 227                
                            CreateSalesItem(228,     27,     1,      10.00M     ,8M),          // 228                
                            CreateSalesItem(229,     125,    1,      7.95M      ,5M),           // 229              
                            CreateSalesItem(230,     30,     1,      5.50M      ,3M),           // 230               
                            CreateSalesItem(231,     3,      1,      7.95M      ,5M),           // 231
                            CreateSalesItem(231,     28,     1,      11.95M     ,9M),                     
                            CreateSalesItem(232,     3,      1,      7.95M      ,5M),           // 232                
                            CreateSalesItem(233,     49,     1,      39.95M     ,25M),         // 233                
                            CreateSalesItem(234,     67,     1,      61.95M     ,55M),         // 234                
                            CreateSalesItem(235,     71,     1,      13.95M     ,8M),          // 235                
                            CreateSalesItem(236,     46,     1,      9.95M      ,6M),           // 236                
                            CreateSalesItem(237,     8,      1,      10.95M     ,8M),          // 237
                            CreateSalesItem(238,     35,     1,      34.95M     ,25M),         // 238           
                            CreateSalesItem(239,     7,      1,      10.95M     ,7M),          // 239               
                            CreateSalesItem(240,     97,     1,      13.50M     ,9M),          // 240              
                            CreateSalesItem(241,     45,     1,      4.95M      ,2M),           // 241               
                            CreateSalesItem(242,     3,      1,      7.95M      ,5M),           // 242
                            CreateSalesItem(243,     32,     1,      5.50M      ,3M),           // 243
                            CreateSalesItem(244,     40,     1,      19.95M     ,15M),         // 244
                            CreateSalesItem(244,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(245,     99,     1,      23.95M     ,15M),         // 245
                            CreateSalesItem(246,     83,     1,      15.95M     ,10M),          // 246
                            CreateSalesItem(247,     120,    1,      33.95M     ,20M),          //247
                            CreateSalesItem(248,     11,     1,      20.95M     ,12M),         //248
                            CreateSalesItem(249,     2,      1,       7.95M     ,5M),          //249
                            CreateSalesItem(250,     1,      1,       7.95M     ,5M),          //250
                            CreateSalesItem(251,     111,    1,       9.95M     ,5M),          //251
                            CreateSalesItem(252,     47,     1,       28.95M    ,20M),        //252
                            CreateSalesItem(253,     128,    1,       11.95M    ,7M),          //253
                            CreateSalesItem(254,     68,     1,      83.95M     ,75M),         //254
                            CreateSalesItem(254,     65,     1,      42.95M     ,35M),   
                            CreateSalesItem(255,     105,    1,      7.95M      ,5M),           //255
                            CreateSalesItem(256,     8,      1,      10.95M     ,8M),          //256
                            CreateSalesItem(257,     9,      1,      12.95M     ,9M),          //257
                            CreateSalesItem(258,     8,      1,      10.95M     ,8M),          //258
                            CreateSalesItem(259,     3,      1,      7.95M      ,5M),           //259
                            CreateSalesItem(260,     19,     1,      13.95M     ,10M),         //260
                            CreateSalesItem(261,     39,     1,      27.95M     ,20M),         //261
                            CreateSalesItem(262,     128,    1,      11.95M     ,7M),          //262
                            CreateSalesItem(263,     114,    1,      6.50M      ,3.5M),         //263
                            CreateSalesItem(264,     41,     1,      27.95M     ,20M),         //264
                            CreateSalesItem(265,     49,     1,      39.95M     ,25M),         //265
                            CreateSalesItem(266,     3,      1,      7.95M      ,5M),           //266
                            CreateSalesItem(266,     17,     1,      12.95M     ,10M),        
                            CreateSalesItem(267,     9,      1,      12.95M     ,9M),          //267
                            CreateSalesItem(268,     3,      1,      7.95M      ,5M),           //268
                            CreateSalesItem(269,     127,    1,      15.95M     ,10M),         //269
                            CreateSalesItem(270,     102,    1,      6.95M      ,3M),           //270
                            CreateSalesItem(271,     91,     1,      2.95M      ,1M),           //271
                            CreateSalesItem(272,     8,      1,      10.95M     ,8M),          //272
                            CreateSalesItem(273,     11,     1,      20.95M     ,12M),         //273
                            CreateSalesItem(274,     70,     1,      17.95M     ,12M),         //274
                            CreateSalesItem(275,     116,    1,      32.95M     ,20M),         //275
                            CreateSalesItem(276,     101,    1,      24.95M     ,18M),         //276
                            CreateSalesItem(277,     99,     1,      23.95M     ,15M),         //277
                            CreateSalesItem(278,     71,     1,      13.95M     ,8M),          //278
                            CreateSalesItem(279,     8,      1,      10.95M     ,8M),          //279
                            CreateSalesItem(280,     53,     1,      14.95M     ,8M),          //280
                            CreateSalesItem(281,     58,     1,      9.95M      ,6M),           //281
                            CreateSalesItem(282,     3,      1,      7.95M      ,5M),           //282
                            CreateSalesItem(283,     26,     1,      11.50M     ,9M),          //283
                            CreateSalesItem(284,     38,     1,      13.95M     ,10M),         //284
                            CreateSalesItem(285,     36,     1,      29.95M     ,20M),         //285
                            CreateSalesItem(286,     122,    1,      15.95M     ,10M),         //286
                            CreateSalesItem(287,     66,     1,      27.95M     ,22M),         //287
                            CreateSalesItem(288,     2,      1,      7.95M      ,5M),           //288
                            CreateSalesItem(289,     3,      1,      7.95M      ,5M),           //289
                            CreateSalesItem(290,     124,    1,      9.95M      ,6M),           //290
                            CreateSalesItem(291,     123,    1,      14.95M     ,10M),         //291
                            CreateSalesItem(292,     9,      1,      12.95M     ,9M),          //292
                            CreateSalesItem(293,     8,      1,      10.95M     ,8M),          //293
                            CreateSalesItem(294,    8,       1,      10.95M     ,8M),           //294
                            CreateSalesItem(294,     9,      1,      12.95M     ,9M),
                            CreateSalesItem(295,      20,    1,      7.95M      ,5M),           //295
                            CreateSalesItem(296,     8,      1,      10.95M     ,8M),         //296
                            CreateSalesItem(297,     58,     1,      9.95M      ,6M),           //297
                            CreateSalesItem(298,     91,     1,      2.95M      ,1M),           //298
                            CreateSalesItem(299,     40,     1,      19.95M     ,15M),         // 299
                            CreateSalesItem(299,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(300,     3,     1,      7.95M       ,5M),           //300

                                    // Sale No.301
                            CreateSalesItem(301,      8,      1,      10.95M     ,8M),
                            CreateSalesItem(301,      9,      1,      12.95M     ,9M),                     
                            CreateSalesItem(302,      20,     1,      7.95M      ,5M),          // 302                
                            CreateSalesItem(303,      127,    1,      15.95M     ,10M),        // 303               
                            CreateSalesItem(304,      116,    1,      32.95M     ,20M),        // 304                
                            CreateSalesItem(305,      118,    1,      22.95M     ,15M),        // 305                
                            CreateSalesItem(306,      89,     4,      2.95M      ,1M),          // 306               
                            CreateSalesItem(308,      128,    1,      11.95M     ,7M),         // 308
                            CreateSalesItem(308,      25,     1,      12.95M     ,10M),                    
                            CreateSalesItem(309,      8,      1,      10.95M     ,8M),         // 309
                            CreateSalesItem(309,      9,      1,      12.95M     ,9M),                    
                            CreateSalesItem(310,      8,      1,      10.95M     ,8M),         // 310                
                            CreateSalesItem(310,     98,     1,      23.95M      ,15M),        // 310                
                            CreateSalesItem(311,     128,    1,      11.95M      ,7M),         // 311               
                            CreateSalesItem(312,     8,      1,      11.95M      ,8M),         // 312                
                            CreateSalesItem(313,     28,     1,      11.95M      ,9M),         // 313               
                            CreateSalesItem(314,     94,     1,      18.95M      ,15M),        // 314
                            CreateSalesItem(314,     95,     1,      13.25M      ,9M),                    
                            CreateSalesItem(315,     113,    1,      10.75M      ,7M),         // 315               
                            CreateSalesItem(316,     1,      1,      7.95M       ,5M),          // 316
                            CreateSalesItem(316,     3,      1,      7.95M       ,5M),
                            CreateSalesItem(317,     74,     2,      3.55M       ,1.50M),       // 317
                            CreateSalesItem(317,     75,     1,      3.55M       ,1.50M),
                            CreateSalesItem(317,     76,     1,      3.55M       ,1.50M),
                            CreateSalesItem(317,     77,     1,      3.55M       ,1.50M),                  
                            CreateSalesItem(318,     126,    1,      9.95M       ,6M),          // 318
                            CreateSalesItem(318,     29,     1,      5.50M       ,3M),                     
                            CreateSalesItem(319,     5,      2,      15.95M      ,10M),        // 319               
                            CreateSalesItem(320,     61,     1,      5.50M       ,3M),          // 320               
                            CreateSalesItem(321,     63,     1,      24.95M      ,18M),        // 321               
                            CreateSalesItem(322,     123,    1,      14.95M      ,10M),        // 322               
                            CreateSalesItem(323,     117,    1,      12.95M      ,7M),         // 323                
                            CreateSalesItem(324,     20,     1,      7.95M       ,5M),          // 324
                            CreateSalesItem(324,     19,     1,      13.95M      ,10M),
                            CreateSalesItem(324,     1,      1,      7.95M       ,5M),
                            CreateSalesItem(325,     23,     1,      1.95M       ,1M),          // 325
                            CreateSalesItem(326,     128,    1,      11.95M      ,7M),         // 326
                            CreateSalesItem(327,     21,     1,      7.95M       ,5M),          // 327                
                            CreateSalesItem(328,     27,     1,      10.00M      ,8M),         // 328                
                            CreateSalesItem(329,     125,    1,      7.95M       ,5M),          // 329              
                            CreateSalesItem(330,     30,     1,      5.50M       ,3M),          // 330               
                            CreateSalesItem(331,     3,      1,      7.95M       ,5M),          // 331
                            CreateSalesItem(331,     28,     1,      11.95M      ,9M),                    
                            CreateSalesItem(332,     3,      1,      7.95M       ,5M),          // 332                
                            CreateSalesItem(333,     49,     1,      39.95M      ,25M),        // 333                
                            CreateSalesItem(334,     67,     1,      61.95M      ,55M),        // 334                
                            CreateSalesItem(335,     71,     1,      13.95M      ,8M),         // 335                
                            CreateSalesItem(336,     46,     1,      9.95M       ,6M),          // 336                
                            CreateSalesItem(337,     8,      1,      10.95M      ,8M),         // 337
                            CreateSalesItem(338,     35,     1,      34.95M      ,25M),        // 338           
                            CreateSalesItem(339,     7,      1,      10.95M      ,7M),         // 339               
                            CreateSalesItem(340,     97,     1,      13.50M      ,9M),         // 340              
                            CreateSalesItem(341,     45,     1,      4.95M       ,2M),          // 341               
                            CreateSalesItem(342,     3,      1,      7.95M       ,5M),          // 342
                            CreateSalesItem(343,     32,     1,      5.50M       ,3M),          // 343
                            CreateSalesItem(344,     40,     1,      19.95M      ,15M),        // 344
                            CreateSalesItem(344,     8,      1,      10.95M      ,8M),
                            CreateSalesItem(345,     99,     1,      23.95M      ,15M),        // 345
                            CreateSalesItem(346,     83,     1,      15.95M      ,10M),         // 346
                            CreateSalesItem(347,     120,    1,      33.95M      ,20M),         //347
                            CreateSalesItem(348,     11,     1,      20.95M      ,12M),        //348
                            CreateSalesItem(349,     2,      1,       7.95M      ,5M),         //349
                            CreateSalesItem(350,     1,      1,       7.95M      ,5M),         //350
                            CreateSalesItem(351,     111,    1,       9.95M      ,5M),         //351
                            CreateSalesItem(352,     47,     1,       28.95M     ,20M),       //352
                            CreateSalesItem(353,     128,    1,       11.95M     ,7M),         //353
                            CreateSalesItem(354,     68,     1,      83.95M      ,75M),        //354
                            CreateSalesItem(354,     65,     1,      42.95M      ,35M),  
                            CreateSalesItem(355,     105,    1,      7.95M       ,5M),          //355
                            CreateSalesItem(356,     8,      1,      10.95M      ,8M),         //356
                            CreateSalesItem(357,     9,      1,      12.95M      ,9M),         //357
                            CreateSalesItem(358,     8,      1,      10.95M      ,8M),         //358
                            CreateSalesItem(359,     3,      1,      7.95M       ,5M),          //359
                            CreateSalesItem(360,     19,     1,      13.95M      ,10M),        //360
                            CreateSalesItem(361,     39,     1,      27.95M      ,20M),        //361
                            CreateSalesItem(362,     128,    1,      11.95M      ,7M),         //362
                            CreateSalesItem(363,     114,    1,      6.50M       ,3.5M),        //363
                            CreateSalesItem(364,     41,     1,      27.95M      ,20M),        //364
                            CreateSalesItem(365,     49,     1,      39.95M      ,25M),        //365
                            CreateSalesItem(366,     3,      1,      7.95M       ,5M),          //366
                            CreateSalesItem(366,     17,     1,      12.95M      ,10M),       
                            CreateSalesItem(367,     9,      1,      12.95M      ,9M),         //367
                            CreateSalesItem(368,     3,      1,      7.95M       ,5M),          //368
                            CreateSalesItem(369,     127,    1,      15.95M      ,10M),        //369
                            CreateSalesItem(370,     102,    1,      6.95M       ,3M),          //370
                            CreateSalesItem(371,     91,     1,      2.95M       ,1M),          //371
                            CreateSalesItem(372,     8,      1,      10.95M      ,8M),         //372
                            CreateSalesItem(373,     11,     1,      20.95M      ,12M),        //373
                            CreateSalesItem(374,     70,     1,      17.95M      ,12M),        //374
                            CreateSalesItem(375,     116,    1,      32.95M      ,20M),        //375
                            CreateSalesItem(376,     101,    1,      24.95M      ,18M),        //376
                            CreateSalesItem(377,     99,     1,      23.95M      ,15M),        //377
                            CreateSalesItem(378,     71,     1,      13.95M      ,8M),         //378
                            CreateSalesItem(379,     8,      1,      10.95M      ,8M),         //379
                            CreateSalesItem(380,     53,     1,      14.95M      ,8M),         //380
                            CreateSalesItem(381,     58,     1,      9.95M       ,6M),          //381
                            CreateSalesItem(382,     3,      1,      7.95M       ,5M),          //382
                            CreateSalesItem(383,     26,     1,      11.50M      ,9M),         //383
                            CreateSalesItem(384,     38,     1,      13.95M      ,10M),        //384
                            CreateSalesItem(385,     36,     1,      29.95M      ,20M),        //385
                            CreateSalesItem(386,     122,    1,      15.95M      ,10M),        //386
                            CreateSalesItem(387,     66,     1,      27.95M      ,22M),        //387
                            CreateSalesItem(388,     2,      1,      7.95M       ,5M),          //388
                            CreateSalesItem(389,     3,      1,      7.95M       ,5M),          //389
                            CreateSalesItem(390,     124,    1,      9.95M       ,6M),          //390
                            CreateSalesItem(391,     123,    1,      14.95M      ,10M),        //391
                            CreateSalesItem(392,     9,      1,      12.95M      ,9M),         //392
                            CreateSalesItem(393,     8,      1,      10.95M      ,8M),         //393
                            CreateSalesItem(394,    8,       1,      10.95M      ,8M),          //394
                            CreateSalesItem(394,     9,      1,      12.95M      ,9M),
                            CreateSalesItem(395,      20,    1,      7.95M       ,5M),          //395
                            CreateSalesItem(396,     8,      1,      10.95M      ,8M),        //396
                            CreateSalesItem(397,     58,     1,      9.95M       ,6M),          //397
                            CreateSalesItem(398,     91,     1,      2.95M       ,1M),          //398
                            CreateSalesItem(399,     40,     1,      19.95M      ,15M),        // 399
                            CreateSalesItem(399,     8,      1,      10.95M      ,8M),
                            CreateSalesItem(400,     3,     1,      7.95M        ,5M),          //400

                                     // Sale No.401
                            CreateSalesItem(401,      8,      1,      10.95M    ,8M),
                            CreateSalesItem(401,      9,      1,      12.95M    ,9M),                     
                            CreateSalesItem(402,      20,     1,      7.95M     ,5M),          // 402                
                            CreateSalesItem(403,      127,    1,      15.95M    ,10M),        // 403               
                            CreateSalesItem(404,      116,    1,      32.95M    ,20M),        // 404                
                            CreateSalesItem(405,      118,    1,      22.95M    ,15M),        // 405                
                            CreateSalesItem(406,      89,     4,      2.95M     ,1M),          // 406               
                            CreateSalesItem(408,      128,    1,      11.95M    ,7M),         // 408
                            CreateSalesItem(408,      25,     1,      12.95M    ,10M),                    
                            CreateSalesItem(409,      8,      1,      10.95M    ,8M),         // 409
                            CreateSalesItem(409,      9,      1,      12.95M    ,9M),                    
                            CreateSalesItem(410,      8,      1,      10.95M    ,8M),         // 410                
                            CreateSalesItem(410,     98,     1,      23.95M     ,15M),        // 410                
                            CreateSalesItem(411,     128,    1,      11.95M     ,7M),         // 411               
                            CreateSalesItem(412,     8,      1,      11.95M     ,8M),         // 412                
                            CreateSalesItem(413,     28,     1,      11.95M     ,9M),         // 413               
                            CreateSalesItem(414,     94,     1,      18.95M     ,15M),        // 414
                            CreateSalesItem(414,     95,     1,      13.25M     ,9M),                    
                            CreateSalesItem(415,     113,    1,      10.75M     ,7M),         // 415               
                            CreateSalesItem(416,     1,      1,      7.95M      ,5M),          // 416
                            CreateSalesItem(416,     3,      1,      7.95M      ,5M),
                            CreateSalesItem(417,     74,     2,      3.55M      ,1.50M),       // 417
                            CreateSalesItem(417,     75,     1,      3.55M      ,1.50M),
                            CreateSalesItem(417,     76,     1,      3.55M      ,1.50M),
                            CreateSalesItem(417,     77,     1,      3.55M      ,1.50M),                  
                            CreateSalesItem(418,     126,    1,      9.95M      ,6M),          // 418
                            CreateSalesItem(418,     29,     1,      5.50M      ,3M),                     
                            CreateSalesItem(419,     5,      2,      15.95M     ,10M),        // 419               
                            CreateSalesItem(420,     61,     1,      5.50M      ,3M),          // 420               
                            CreateSalesItem(421,     63,     1,      24.95M     ,18M),        // 421               
                            CreateSalesItem(422,     123,    1,      14.95M     ,10M),        // 422               
                            CreateSalesItem(423,     117,    1,      12.95M     ,7M),         // 423                
                            CreateSalesItem(424,     20,     1,      7.95M      ,5M),          // 424
                            CreateSalesItem(424,     19,     1,      13.95M     ,10M),
                            CreateSalesItem(424,     1,      1,      7.95M      ,5M),
                            CreateSalesItem(425,     23,     1,      1.95M      ,1M),          // 425
                            CreateSalesItem(426,     128,    1,      11.95M     ,7M),         // 426
                            CreateSalesItem(427,     21,     1,      7.95M      ,5M),          // 427                
                            CreateSalesItem(428,     27,     1,      10.00M     ,8M),         // 428                
                            CreateSalesItem(429,     125,    1,      7.95M      ,5M),          // 429              
                            CreateSalesItem(430,     30,     1,      5.50M      ,3M),          // 430               
                            CreateSalesItem(431,     3,      1,      7.95M      ,5M),          // 431
                            CreateSalesItem(431,     28,     1,      11.95M     ,9M),                    
                            CreateSalesItem(432,     3,      1,      7.95M      ,5M),          // 432                
                            CreateSalesItem(433,     49,     1,      39.95M     ,25M),        // 433                
                            CreateSalesItem(434,     67,     1,      61.95M     ,55M),        // 434                
                            CreateSalesItem(435,     71,     1,      13.95M     ,8M),         // 435                
                            CreateSalesItem(436,     46,     1,      9.95M      ,6M),          // 436                
                            CreateSalesItem(437,     8,      1,      10.95M     ,8M),         // 437
                            CreateSalesItem(438,     35,     1,      34.95M     ,25M),        // 438           
                            CreateSalesItem(439,     7,      1,      10.95M     ,7M),         // 439               
                            CreateSalesItem(440,     97,     1,      13.50M     ,9M),         // 440              
                            CreateSalesItem(441,     45,     1,      4.95M      ,2M),          // 441               
                            CreateSalesItem(442,     3,      1,      7.95M      ,5M),          // 442
                            CreateSalesItem(443,     32,     1,      5.50M      ,3M),          // 443
                            CreateSalesItem(444,     40,     1,      19.95M     ,15M),        // 444
                            CreateSalesItem(444,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(445,     99,     1,      23.95M     ,15M),        // 445
                            CreateSalesItem(446,     83,     1,      15.95M     ,10M),         // 446
                            CreateSalesItem(447,     120,    1,      33.95M     ,20M),         //447
                            CreateSalesItem(448,     11,     1,      20.95M     ,12M),        //448
                            CreateSalesItem(449,     2,      1,       7.95M     ,5M),         //449
                            CreateSalesItem(450,     1,      1,       7.95M     ,5M),         //450
                            CreateSalesItem(451,     111,    1,       9.95M     ,5M),         //451
                            CreateSalesItem(452,     47,     1,       28.95M    ,20M),       //452
                            CreateSalesItem(453,     128,    1,       11.95M    ,7M),         //453
                            CreateSalesItem(454,     68,     1,      83.95M     ,75M),        //454
                            CreateSalesItem(454,     65,     1,      42.95M     ,35M),  
                            CreateSalesItem(455,     105,    1,      7.95M      ,5M),          //455
                            CreateSalesItem(456,     8,      1,      10.95M     ,8M),         //456
                            CreateSalesItem(457,     9,      1,      12.95M     ,9M),         //457
                            CreateSalesItem(458,     8,      1,      10.95M     ,8M),         //458
                            CreateSalesItem(459,     3,      1,      7.95M      ,5M),          //459
                            CreateSalesItem(460,     19,     1,      13.95M     ,10M),        //460
                            CreateSalesItem(461,     39,     1,      27.95M     ,20M),        //461
                            CreateSalesItem(462,     128,    1,      11.95M     ,7M),         //462
                            CreateSalesItem(463,     114,    1,      6.50M      ,3.5M),        //463
                            CreateSalesItem(464,     41,     1,      27.95M     ,20M),        //464
                            CreateSalesItem(465,     49,     1,      39.95M     ,25M),        //465
                            CreateSalesItem(466,     3,      1,      7.95M      ,5M),          //466
                            CreateSalesItem(466,     17,     1,      12.95M     ,10M),       
                            CreateSalesItem(467,     9,      1,      12.95M     ,9M),         //467
                            CreateSalesItem(468,     3,      1,      7.95M      ,5M),          //468
                            CreateSalesItem(469,     127,    1,      15.95M     ,10M),        //469
                            CreateSalesItem(470,     102,    1,      6.95M      ,3M),          //470
                            CreateSalesItem(471,     91,     1,      2.95M      ,1M),          //471
                            CreateSalesItem(472,     8,      1,      10.95M     ,8M),         //472
                            CreateSalesItem(473,     11,     1,      20.95M     ,12M),        //473
                            CreateSalesItem(474,     70,     1,      17.95M     ,12M),        //474
                            CreateSalesItem(475,     116,    1,      32.95M     ,20M),        //475
                            CreateSalesItem(476,     101,    1,      24.95M     ,18M),        //476
                            CreateSalesItem(477,     99,     1,      23.95M     ,15M),        //477
                            CreateSalesItem(478,     71,     1,      13.95M     ,8M),         //478
                            CreateSalesItem(479,     8,      1,      10.95M     ,8M),         //479
                            CreateSalesItem(480,     53,     1,      14.95M     ,8M),         //480
                            CreateSalesItem(481,     58,     1,      9.95M      ,6M),          //481
                            CreateSalesItem(482,     3,      1,      7.95M      ,5M),          //482
                            CreateSalesItem(483,     26,     1,      11.50M     ,9M),         //483
                            CreateSalesItem(484,     38,     1,      13.95M     ,10M),        //484
                            CreateSalesItem(485,     36,     1,      29.95M     ,20M),        //485
                            CreateSalesItem(486,     122,    1,      15.95M     ,10M),        //486
                            CreateSalesItem(487,     66,     1,      27.95M     ,22M),        //487
                            CreateSalesItem(488,     2,      1,      7.95M      ,5M),          //488
                            CreateSalesItem(489,     3,      1,      7.95M      ,5M),          //489
                            CreateSalesItem(490,     124,    1,      9.95M      ,6M),          //490
                            CreateSalesItem(491,     123,    1,      14.95M     ,10M),        //491
                            CreateSalesItem(492,     9,      1,      12.95M     ,9M),         //492
                            CreateSalesItem(493,     8,      1,      10.95M     ,8M),         //493
                            CreateSalesItem(494,    8,       1,      10.95M     ,8M),          //494
                            CreateSalesItem(494,     9,      1,      12.95M     ,9M),
                            CreateSalesItem(495,      20,    1,      7.95M      ,5M),          //495
                            CreateSalesItem(496,     8,      1,      10.95M     ,8M),        //496
                            CreateSalesItem(497,     58,     1,      9.95M      ,6M),          //497
                            CreateSalesItem(498,     91,     1,      2.95M      ,1M),          //498
                            CreateSalesItem(499,     40,     1,      19.95M     ,15M),        // 499
                            CreateSalesItem(499,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(500,     3,     1,      7.95M       ,5M),          //500
                            #endregion

                #region August Sale Items
                                            // Sale No.501
                            CreateSalesItem(501,      8,      1,      10.95M    ,8M),
                            CreateSalesItem(501,      9,      1,      12.95M    ,9M),                      
                            CreateSalesItem(502,      20,     1,      7.95M     ,5M),           // 2                
                            CreateSalesItem(503,      127,    1,      15.95M    ,10M),         // 3               
                            CreateSalesItem(504,      116,    1,      32.95M    ,20M),         // 4                
                            CreateSalesItem(505,      118,    1,      22.95M    ,15M),         // 5                
                            CreateSalesItem(506,      89,     4,      2.95M     ,1M),           // 6               
                            CreateSalesItem(508,      128,    1,      11.95M    ,7M),          // 7
                            CreateSalesItem(508,      25,     1,      12.95M    ,10M),                     
                            CreateSalesItem(509,      8,      1,      10.95M    ,8M),          // 8
                            CreateSalesItem(509,      9,      1,      12.95M    ,9M),                     
                            CreateSalesItem(510,      8,      1,      10.95M    ,8M),          // 9                
                            CreateSalesItem(510,     98,     1,      23.95M     ,15M),         // 10                
                            CreateSalesItem(511,     128,    1,      11.95M     ,7M),          // 11               
                            CreateSalesItem(512,     8,      1,      11.95M     ,8M),          // 12                
                            CreateSalesItem(513,     28,     1,      11.95M     ,9M),          // 13               
                            CreateSalesItem(514,     94,     1,      18.95M     ,15M),         // 14
                            CreateSalesItem(514,     95,     1,      13.25M     ,9M),                     
                            CreateSalesItem(515,     113,    1,      10.75M     ,7M),          // 15               
                            CreateSalesItem(516,     1,      1,      7.95M      ,5M),           // 16
                            CreateSalesItem(516,     3,      1,      7.95M      ,5M),
                            CreateSalesItem(517,     74,     2,      3.55M      ,1.50M),        // 17
                            CreateSalesItem(517,     75,     1,      3.55M      ,1.50M),
                            CreateSalesItem(517,     76,     1,      3.55M      ,1.50M),
                            CreateSalesItem(517,     77,     1,      3.55M      ,1.50M),                   
                            CreateSalesItem(518,     126,    1,      9.95M      ,6M),           // 18
                            CreateSalesItem(518,     29,     1,      5.50M      ,3M),                      
                            CreateSalesItem(519,     5,      2,      15.95M     ,10M),         // 19               
                            CreateSalesItem(520,     61,     1,      5.50M      ,3M),           // 20               
                            CreateSalesItem(521,     63,     1,      24.95M     ,18M),         // 21               
                            CreateSalesItem(522,     123,    1,      14.95M     ,10M),         // 22               
                            CreateSalesItem(523,     117,    1,      12.95M     ,7M),          // 23                
                            CreateSalesItem(524,     20,     1,      7.95M      ,5M),           // 24
                            CreateSalesItem(524,     19,     1,      13.95M     ,10M),
                            CreateSalesItem(524,     1,      1,      7.95M      ,5M),
                            CreateSalesItem(525,     23,     1,      1.95M      ,1M),           // 25
                            CreateSalesItem(526,     128,    1,      11.95M     ,7M),          // 26
                            CreateSalesItem(527,     21,     1,      7.95M      ,5M),           // 27                
                            CreateSalesItem(528,     27,     1,      10.00M     ,8M),          // 28                
                            CreateSalesItem(529,     125,    1,      7.95M      ,5M),           // 29              
                            CreateSalesItem(530,     30,     1,      5.50M      ,3M),           // 30               
                            CreateSalesItem(531,     3,      1,      7.95M      ,5M),           // 31
                            CreateSalesItem(531,     28,     1,      11.95M     ,9M),                     
                            CreateSalesItem(532,     3,      1,      7.95M      ,5M),           // 32                
                            CreateSalesItem(533,     49,     1,      39.95M     ,30M),         // 33                
                            CreateSalesItem(534,     67,     1,      61.95M     ,58M),         // 34                
                            CreateSalesItem(535,     71,     1,      13.95M     ,8M),          // 35                
                            CreateSalesItem(536,     46,     1,      9.95M      ,6M),           // 36                
                            CreateSalesItem(537,     8,      1,      10.95M     ,8M),          // 37
                            CreateSalesItem(538,     35,     1,      34.95M     ,25M),         // 38           
                            CreateSalesItem(539,     7,      1,      10.95M     ,7M),          // 39               
                            CreateSalesItem(540,     97,     1,      13.50M     ,9M),          // 40              
                            CreateSalesItem(541,     45,     1,      4.95M      ,2M),           // 41               
                            CreateSalesItem(542,     3,      1,      7.95M      ,5M),           // 42
                            CreateSalesItem(543,     32,     1,      5.50M      ,3M),           // 43
                            CreateSalesItem(544,     40,     1,      19.95M     ,15M),         // 44
                            CreateSalesItem(544,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(545,     99,     1,      23.95M     ,15M),         // 45
                            CreateSalesItem(546,     83,     1,      15.95M     ,10M),          // 46
                            CreateSalesItem(547,     120,    1,      33.95M     ,20M),          //47
                            CreateSalesItem(548,     11,     1,      20.95M     ,12M),         //48
                            CreateSalesItem(549,     2,      1,       7.95M     ,5M),          //49
                            CreateSalesItem(550,     1,      1,       7.95M     ,5M),          //50
                            CreateSalesItem(551,     111,    1,       9.95M     ,5M),          //51
                            CreateSalesItem(552,     47,     1,       28.95M    ,20M),        //52
                            CreateSalesItem(553,     128,    1,       11.95M    ,7M),          //53
                            CreateSalesItem(554,     68,     1,      83.95M     ,78M),         //54
                            CreateSalesItem(554,     65,     1,      42.95M     ,35M),   
                            CreateSalesItem(555,     105,    1,      7.95M      ,5M),           //55
                            CreateSalesItem(556,     8,      1,      10.95M     ,8M),          //56
                            CreateSalesItem(557,     9,      1,      12.95M     ,9M),          //57
                            CreateSalesItem(558,     8,      1,      10.95M     ,8M),          //58
                            CreateSalesItem(559,     3,      1,      7.95M      ,5M),           //59
                            CreateSalesItem(560,     19,     1,      13.95M     ,10M),         //60
                            CreateSalesItem(561,     39,     1,      27.95M     ,20M),         //61
                            CreateSalesItem(562,     128,    1,      11.95M     ,7M),          //62
                            CreateSalesItem(653,     114,    1,      6.50M      ,3.5M),         //63
                            CreateSalesItem(564,     41,     1,      27.95M     ,20M),         //64
                            CreateSalesItem(565,     49,     1,      39.95M     ,25M),         //65
                            CreateSalesItem(566,     3,      1,      7.95M      ,5M),           //66
                            CreateSalesItem(566,     17,     1,      12.95M     ,10M),        
                            CreateSalesItem(567,     9,      1,      12.95M     ,9M),          //67
                            CreateSalesItem(568,     3,      1,      7.95M      ,5M),           //68
                            CreateSalesItem(569,     127,    1,      15.95M     ,10M),         //69
                            CreateSalesItem(570,     102,    1,      6.95M      ,3M),           //70
                            CreateSalesItem(571,     91,     1,      2.95M      ,1M),           //71
                            CreateSalesItem(572,     8,      1,      10.95M     ,8M),          //72
                            CreateSalesItem(573,     11,     1,      20.95M     ,12M),         //73
                            CreateSalesItem(574,     70,     1,      17.95M     ,12M),         //74
                            CreateSalesItem(575,     116,    1,      32.95M     ,20M),         //75
                            CreateSalesItem(576,     101,    1,      24.95M     ,18M),         //76
                            CreateSalesItem(577,     99,     1,      23.95M     ,15M),         //77
                            CreateSalesItem(578,     71,     1,      13.95M     ,8M),          //78
                            CreateSalesItem(579,     8,      1,      10.95M     ,8M),          //79
                            CreateSalesItem(580,     53,     1,      14.95M     ,8M),          //80
                            CreateSalesItem(581,     58,     1,      9.95M      ,6M),           //81
                            CreateSalesItem(582,     3,      1,      7.95M      ,5M),           //82
                            CreateSalesItem(583,     26,     1,      11.50M     ,9M),          //83
                            CreateSalesItem(584,     38,     1,      13.95M     ,10M),         //84
                            CreateSalesItem(585,     36,     1,      29.95M     ,20M),         //85
                            CreateSalesItem(586,     122,    1,      15.95M     ,10M),         //86
                            CreateSalesItem(587,     66,     1,      27.95M     ,22M),         //87
                            CreateSalesItem(588,     2,      1,      7.95M      ,5M),           //88
                            CreateSalesItem(589,     3,      1,      7.95M      ,5M),           //89
                            CreateSalesItem(590,     124,    1,      9.95M      ,6M),           //90
                            CreateSalesItem(591,     123,    1,      14.95M     ,10M),         //91
                            CreateSalesItem(592,     9,      1,      12.95M     ,9M),          //92
                            CreateSalesItem(593,     8,      1,      10.95M     ,8M),          //93
                            CreateSalesItem(594,    8,       1,      10.95M     ,8M),           //94
                            CreateSalesItem(594,     9,      1,      12.95M     ,9M),
                            CreateSalesItem(595,      20,    1,      7.95M      ,5M),           //95
                            CreateSalesItem(596,     8,      1,      10.95M     ,8M),         //96
                            CreateSalesItem(597,     58,     1,      9.95M      ,6M),           //97
                            CreateSalesItem(598,     91,     1,      2.95M      ,1M),           //98
                            CreateSalesItem(599,     40,     1,      19.95M     ,15M),         // 99
                            CreateSalesItem(599,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(600,     3,     1,      7.95M       ,5M),           //100

                             // Sale No.601
                            CreateSalesItem(601,      8,      1,      10.95M    ,8M),
                            CreateSalesItem(601,      9,      1,      12.95M    ,9M),                      
                            CreateSalesItem(602,      20,     1,      7.95M     ,5M),           // 102                
                            CreateSalesItem(603,      127,    1,      15.95M    ,10M),         // 103               
                            CreateSalesItem(604,      116,    1,      32.95M    ,20M),         // 104                
                            CreateSalesItem(605,      118,    1,      22.95M    ,15M),         // 105                
                            CreateSalesItem(606,      89,     4,      2.95M     ,1M),           // 106               
                            CreateSalesItem(608,      128,    1,      11.95M    ,7M),          // 108
                            CreateSalesItem(608,      25,     1,      12.95M    ,10M),                     
                            CreateSalesItem(609,      8,      1,      10.95M    ,8M),          // 109
                            CreateSalesItem(609,      9,      1,      12.95M    ,9M),                     
                            CreateSalesItem(610,      8,      1,      10.95M    ,8M),          // 110                
                            CreateSalesItem(610,     98,     1,      23.95M     ,15M),         // 110                
                            CreateSalesItem(611,     128,    1,      11.95M     ,7M),          // 111               
                            CreateSalesItem(612,     8,      1,      11.95M     ,8M),          // 112                
                            CreateSalesItem(613,     28,     1,      11.95M     ,9M),          // 113               
                            CreateSalesItem(614,     94,     1,      18.95M     ,15M),         // 114
                            CreateSalesItem(614,     95,     1,      13.25M     ,9M),                     
                            CreateSalesItem(615,     113,    1,      10.75M     ,7M),          // 115               
                            CreateSalesItem(616,     1,      1,      7.95M      ,5M),           // 116
                            CreateSalesItem(616,     3,      1,      7.95M      ,5M),
                            CreateSalesItem(617,     74,     2,      3.55M      ,1.50M),        // 117
                            CreateSalesItem(617,     75,     1,      3.55M      ,1.50M),
                            CreateSalesItem(617,     76,     1,      3.55M      ,1.50M),
                            CreateSalesItem(617,     77,     1,      3.55M      ,1.50M),                   
                            CreateSalesItem(618,     126,    1,      9.95M      ,6M),           // 118
                            CreateSalesItem(618,     29,     1,      5.50M      ,3M),                      
                            CreateSalesItem(619,     5,      2,      15.95M     ,10M),         // 119               
                            CreateSalesItem(620,     61,     1,      5.50M      ,3M),           // 120               
                            CreateSalesItem(621,     63,     1,      24.95M     ,18M),         // 121               
                            CreateSalesItem(622,     123,    1,      14.95M     ,10M),         // 122               
                            CreateSalesItem(623,     117,    1,      12.95M     ,7M),          // 123                
                            CreateSalesItem(624,     20,     1,      7.95M      ,5M),           // 124
                            CreateSalesItem(624,     19,     1,      13.95M     ,10M),
                            CreateSalesItem(624,     1,      1,      7.95M      ,5M),
                            CreateSalesItem(625,     23,     1,      1.95M      ,1M),           // 125
                            CreateSalesItem(626,     128,    1,      11.95M     ,7M),          // 126
                            CreateSalesItem(627,     21,     1,      7.95M      ,5M),           // 127                
                            CreateSalesItem(628,     27,     1,      10.00M     ,8M),          // 128                
                            CreateSalesItem(629,     125,    1,      7.95M      ,5M),           // 129              
                            CreateSalesItem(630,     30,     1,      5.50M      ,3M),           // 130               
                            CreateSalesItem(631,     3,      1,      7.95M      ,5M),           // 131
                            CreateSalesItem(631,     28,     1,      11.95M     ,9M),                     
                            CreateSalesItem(632,     3,      1,      7.95M      ,5M),           // 132                
                            CreateSalesItem(633,     49,     1,      39.95M     ,30M),         // 133                
                            CreateSalesItem(634,     67,     1,      61.95M     ,55M),         // 134                
                            CreateSalesItem(635,     71,     1,      13.95M     ,8M),          // 135                
                            CreateSalesItem(636,     46,     1,      9.95M      ,6M),           // 136                
                            CreateSalesItem(637,     8,      1,      10.95M     ,8M),          // 137
                            CreateSalesItem(638,     35,     1,      34.95M     ,28M),         // 138           
                            CreateSalesItem(639,     7,      1,      10.95M     ,7M),          // 139               
                            CreateSalesItem(640,     97,     1,      13.50M     ,9M),          // 140              
                            CreateSalesItem(641,     45,     1,      4.95M      ,2M),           // 141               
                            CreateSalesItem(642,     3,      1,      7.95M      ,5M),           // 142
                            CreateSalesItem(643,     32,     1,      5.50M      ,3M),           // 143
                            CreateSalesItem(644,     40,     1,      19.95M     ,15M),         // 144
                            CreateSalesItem(644,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(645,     99,     1,      23.95M     ,15M),         // 145
                            CreateSalesItem(646,     83,     1,      15.95M     ,10M),          // 146
                            CreateSalesItem(647,     120,    1,      33.95M     ,20M),          //147
                            CreateSalesItem(648,     11,     1,      20.95M     ,12M),         //148
                            CreateSalesItem(649,     2,      1,       7.95M     ,5M),          //149
                            CreateSalesItem(650,     1,      1,       7.95M     ,5M),          //150
                            CreateSalesItem(651,     111,    1,       9.95M     ,5M),          //151
                            CreateSalesItem(652,     47,     1,       28.95M    ,20M),        //152
                            CreateSalesItem(653,     128,    1,       11.95M    ,7M),          //153
                            CreateSalesItem(654,     68,     1,      83.95M     ,78M),         //154
                            CreateSalesItem(654,     65,     1,      42.95M     ,35M),   
                            CreateSalesItem(655,     105,    1,      7.95M      ,5M),           //155
                            CreateSalesItem(656,     8,      1,      10.95M     ,8M),          //156
                            CreateSalesItem(657,     9,      1,      12.95M     ,9M),          //157
                            CreateSalesItem(658,     8,      1,      10.95M     ,8M),          //158
                            CreateSalesItem(659,     3,      1,      7.95M      ,5M),           //159
                            CreateSalesItem(660,     19,     1,      13.95M     ,10M),         //160
                            CreateSalesItem(661,     39,     1,      27.95M     ,20M),         //161
                            CreateSalesItem(662,     128,    1,      11.95M     ,7M),          //162
                            CreateSalesItem(663,     114,    1,      6.50M      ,3.5M),         //163
                            CreateSalesItem(664,     41,     1,      27.95M     ,20M),         //164
                            CreateSalesItem(665,     49,     1,      39.95M     ,25M),         //165
                            CreateSalesItem(666,     3,      1,      7.95M      ,5M),           //166
                            CreateSalesItem(666,     17,     1,      12.95M     ,10M),        
                            CreateSalesItem(667,     9,      1,      12.95M     ,9M),          //167
                            CreateSalesItem(668,     3,      1,      7.95M      ,5M),           //168
                            CreateSalesItem(669,     127,    1,      15.95M     ,10M),         //169
                            CreateSalesItem(670,     102,    1,      6.95M      ,3M),           //170
                            CreateSalesItem(671,     91,     1,      2.95M      ,1M),           //171
                            CreateSalesItem(672,     8,      1,      10.95M     ,8M),          //172
                            CreateSalesItem(673,     11,     1,      20.95M     ,12M),         //173
                            CreateSalesItem(674,     70,     1,      17.95M     ,12M),         //174
                            CreateSalesItem(675,     116,    1,      32.95M     ,20M),         //175
                            CreateSalesItem(676,     101,    1,      24.95M     ,18M),         //176
                            CreateSalesItem(677,     99,     1,      23.95M     ,15M),         //177
                            CreateSalesItem(678,     71,     1,      13.95M     ,8M),          //178
                            CreateSalesItem(679,     8,      1,      10.95M     ,8M),          //179
                            CreateSalesItem(680,     53,     1,      14.95M     ,8M),          //180
                            CreateSalesItem(681,     58,     1,      9.95M      ,6M),           //181
                            CreateSalesItem(682,     3,      1,      7.95M      ,5M),           //182
                            CreateSalesItem(683,     26,     1,      11.50M     ,9M),          //183
                            CreateSalesItem(684,     38,     1,      13.95M     ,10M),         //184
                            CreateSalesItem(685,     36,     1,      29.95M     ,20M),         //185
                            CreateSalesItem(686,     122,    1,      15.95M     ,10M),         //186
                            CreateSalesItem(687,     66,     1,      27.95M     ,22M),         //187
                            CreateSalesItem(688,     2,      1,      7.95M      ,5M),           //188
                            CreateSalesItem(689,     3,      1,      7.95M      ,5M),           //189
                            CreateSalesItem(690,     124,    1,      9.95M      ,6M),           //190
                            CreateSalesItem(691,     123,    1,      14.95M     ,10M),         //191
                            CreateSalesItem(692,     9,      1,      12.95M     ,9M),          //192
                            CreateSalesItem(693,     8,      1,      10.95M     ,8M),          //193
                            CreateSalesItem(694,     8,      1,      10.95M     ,8M),           //194
                            CreateSalesItem(694,     9,      1,      12.95M     ,9M),
                            CreateSalesItem(695,     20,    1,      7.95M       ,5M),           //195
                            CreateSalesItem(696,     8,      1,      10.95M     ,8M),         //196
                            CreateSalesItem(697,     58,     1,      9.95M      ,6M),           //197
                            CreateSalesItem(698,     91,     1,      2.95M      ,1M),           //198
                            CreateSalesItem(699,     40,     1,      19.95M     ,15M),         // 199
                            CreateSalesItem(699,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(700,    3,     1,      7.95M        ,5M),            //200

                               // Sale No.701
                            CreateSalesItem(701,      8,      1,      10.95M    ,8M),
                            CreateSalesItem(701,      9,      1,      12.95M    ,9M),                      
                            CreateSalesItem(702,      20,     1,      7.95M     ,5M),           // 202                
                            CreateSalesItem(703,      127,    1,      15.95M    ,10M),         // 203               
                            CreateSalesItem(704,      116,    1,      32.95M    ,20M),         // 204                
                            CreateSalesItem(705,      118,    1,      22.95M    ,15M),         // 205                
                            CreateSalesItem(706,      89,     4,      2.95M     ,1M),           // 206               
                            CreateSalesItem(708,      128,    1,      11.95M    ,7M),          // 208
                            CreateSalesItem(708,      25,     1,      12.95M    ,10M),                     
                            CreateSalesItem(709,      8,      1,      10.95M    ,8M),          // 209
                            CreateSalesItem(709,      9,      1,      12.95M    ,9M),                     
                            CreateSalesItem(710,      8,      1,      10.95M    ,8M),          // 210                
                            CreateSalesItem(710,     98,     1,      23.95M     ,15M),         // 210                
                            CreateSalesItem(711,     128,    1,      11.95M     ,7M),          // 211               
                            CreateSalesItem(712,     8,      1,      11.95M     ,8M),          // 212                
                            CreateSalesItem(713,     28,     1,      11.95M     ,9M),          // 213               
                            CreateSalesItem(714,     94,     1,      18.95M     ,15M),         // 214
                            CreateSalesItem(714,     95,     1,      13.25M     ,9M),                     
                            CreateSalesItem(715,     113,    1,      10.75M     ,7M),          // 215               
                            CreateSalesItem(716,     1,      1,      7.95M      ,5M),           // 216
                            CreateSalesItem(716,     3,      1,      7.95M      ,5M),
                            CreateSalesItem(717,     74,     2,      3.55M      ,1.50M),        // 217
                            CreateSalesItem(717,     75,     1,      3.55M      ,1.50M),
                            CreateSalesItem(717,     76,     1,      3.55M      ,1.50M),
                            CreateSalesItem(717,     77,     1,      3.55M      ,1.50M),                   
                            CreateSalesItem(718,     126,    1,      9.95M      ,6M),           // 218
                            CreateSalesItem(718,     29,     1,      5.50M      ,3M),                      
                            CreateSalesItem(719,     5,      2,      15.95M     ,10M),         // 219               
                            CreateSalesItem(720,     61,     1,      5.50M      ,3M),           // 220               
                            CreateSalesItem(721,     63,     1,      24.95M     ,18M),         // 221               
                            CreateSalesItem(722,     123,    1,      14.95M     ,10M),         // 222               
                            CreateSalesItem(723,     117,    1,      12.95M     ,7M),          // 223                
                            CreateSalesItem(724,     20,     1,      7.95M      ,5M),           // 224
                            CreateSalesItem(724,     19,     1,      13.95M     ,10M),
                            CreateSalesItem(724,     1,      1,      7.95M      ,5M),
                            CreateSalesItem(725,     23,     1,      1.95M      ,1M),           // 225
                            CreateSalesItem(726,     128,    1,      11.95M     ,7M),          // 226
                            CreateSalesItem(727,     21,     1,      7.95M      ,5M),           // 227                
                            CreateSalesItem(728,     27,     1,      10.00M     ,8M),          // 228                
                            CreateSalesItem(729,     125,    1,      7.95M      ,5M),           // 229              
                            CreateSalesItem(730,     30,     1,      5.50M      ,3M),           // 230               
                            CreateSalesItem(731,     3,      1,      7.95M      ,5M),           // 231
                            CreateSalesItem(731,     28,     1,      11.95M     ,9M),                     
                            CreateSalesItem(732,     3,      1,      7.95M      ,5M),           // 232                
                            CreateSalesItem(733,     49,     1,      39.95M     ,25M),         // 233                
                            CreateSalesItem(734,     67,     1,      61.95M     ,58M),         // 234                
                            CreateSalesItem(735,     71,     1,      13.95M     ,8M),          // 235                
                            CreateSalesItem(736,     46,     1,      9.95M      ,6M),           // 236                
                            CreateSalesItem(737,     8,      1,      10.95M     ,8M),          // 237
                            CreateSalesItem(738,     35,     1,      34.95M     ,25M),         // 238           
                            CreateSalesItem(739,     7,      1,      10.95M     ,7M),          // 239               
                            CreateSalesItem(740,     97,     1,      13.50M     ,9M),          // 240              
                            CreateSalesItem(741,     45,     1,      4.95M      ,2M),           // 241               
                            CreateSalesItem(742,     3,      1,      7.95M      ,5M),           // 242
                            CreateSalesItem(743,     32,     1,      5.50M      ,3M),           // 243
                            CreateSalesItem(744,     40,     1,      19.95M     ,15M),         // 244
                            CreateSalesItem(744,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(745,     99,     1,      23.95M     ,15M),         // 245
                            CreateSalesItem(746,     83,     1,      15.95M     ,10M),          // 246
                            CreateSalesItem(747,     120,    1,      33.95M     ,20M),          //247
                            CreateSalesItem(748,     11,     1,      20.95M     ,12M),         //248
                            CreateSalesItem(749,     2,      1,       7.95M     ,5M),          //249
                            CreateSalesItem(750,     1,      1,       7.95M     ,5M),          //250
                            CreateSalesItem(751,     111,    1,       9.95M     ,5M),          //251
                            CreateSalesItem(752,     47,     1,       28.95M    ,20M),        //252
                            CreateSalesItem(753,     128,    1,       11.95M    ,7M),          //253
                            CreateSalesItem(754,     68,     1,      83.95M     ,78M),         //254
                            CreateSalesItem(754,     65,     1,      42.95M     ,35M),   
                            CreateSalesItem(755,     105,    1,      7.95M      ,5M),           //255
                            CreateSalesItem(756,     8,      1,      10.95M     ,8M),          //256
                            CreateSalesItem(757,     9,      1,      12.95M     ,9M),          //257
                            CreateSalesItem(758,     8,      1,      10.95M     ,8M),          //258
                            CreateSalesItem(759,     3,      1,      7.95M      ,5M),           //259
                            CreateSalesItem(760,     19,     1,      13.95M     ,10M),         //260
                            CreateSalesItem(761,     39,     1,      27.95M     ,20M),         //261
                            CreateSalesItem(762,     128,    1,      11.95M     ,7M),          //262
                            CreateSalesItem(763,     114,    1,      6.50M      ,3.5M),         //263
                            CreateSalesItem(764,     41,     1,      27.95M     ,20M),         //264
                            CreateSalesItem(765,     49,     1,      39.95M     ,25M),         //265
                            CreateSalesItem(766,     3,      1,      7.95M      ,5M),           //266
                            CreateSalesItem(766,     17,     1,      12.95M     ,10M),        
                            CreateSalesItem(767,     9,      1,      12.95M     ,9M),          //267
                            CreateSalesItem(768,     3,      1,      7.95M      ,5M),           //268
                            CreateSalesItem(769,     127,    1,      15.95M     ,10M),         //269
                            CreateSalesItem(770,     102,    1,      6.95M      ,3M),           //270
                            CreateSalesItem(771,     91,     1,      2.95M      ,1M),           //271
                            CreateSalesItem(772,     8,      1,      10.95M     ,8M),          //272
                            CreateSalesItem(773,     11,     1,      20.95M     ,12M),         //273
                            CreateSalesItem(774,     70,     1,      17.95M     ,12M),         //274
                            CreateSalesItem(775,     116,    1,      32.95M     ,20M),         //275
                            CreateSalesItem(776,     101,    1,      24.95M     ,18M),         //276
                            CreateSalesItem(777,     99,     1,      23.95M     ,15M),         //277
                            CreateSalesItem(778,     71,     1,      13.95M     ,8M),          //278
                            CreateSalesItem(779,     8,      1,      10.95M     ,8M),          //279
                            CreateSalesItem(780,     53,     1,      14.95M     ,8M),          //280
                            CreateSalesItem(781,     58,     1,      9.95M      ,6M),           //281
                            CreateSalesItem(782,     3,      1,      7.95M      ,5M),           //282
                            CreateSalesItem(783,     26,     1,      11.50M     ,9M),          //283
                            CreateSalesItem(784,     38,     1,      13.95M     ,10M),         //284
                            CreateSalesItem(785,     36,     1,      29.95M     ,20M),         //285
                            CreateSalesItem(786,     122,    1,      15.95M     ,10M),         //286
                            CreateSalesItem(787,     66,     1,      27.95M     ,22M),         //287
                            CreateSalesItem(788,     2,      1,      7.95M      ,5M),           //288
                            CreateSalesItem(789,     3,      1,      7.95M      ,5M),           //289
                            CreateSalesItem(790,     124,    1,      9.95M      ,6M),           //290
                            CreateSalesItem(791,     123,    1,      14.95M     ,10M),         //291
                            CreateSalesItem(792,     9,      1,      12.95M     ,9M),          //292
                            CreateSalesItem(793,     8,      1,      10.95M     ,8M),          //293
                            CreateSalesItem(794,    8,       1,      10.95M     ,8M),           //294
                            CreateSalesItem(794,     9,      1,      12.95M     ,9M),
                            CreateSalesItem(795,      20,    1,      7.95M      ,5M),           //295
                            CreateSalesItem(796,     8,      1,      10.95M     ,8M),         //296
                            CreateSalesItem(797,     58,     1,      9.95M      ,6M),           //297
                            CreateSalesItem(798,     91,     1,      2.95M      ,1M),           //298
                            CreateSalesItem(799,     40,     1,      19.95M     ,15M),         // 299
                            CreateSalesItem(799,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(800,     3,     1,      7.95M       ,5M),           //300

                                    // Sale No.801
                            CreateSalesItem(801,      8,      1,      10.95M     ,8M),
                            CreateSalesItem(801,      9,      1,      12.95M     ,9M),                     
                            CreateSalesItem(802,      20,     1,      7.95M      ,5M),          // 302                
                            CreateSalesItem(803,      127,    1,      15.95M     ,10M),        // 303               
                            CreateSalesItem(804,      116,    1,      32.95M     ,20M),        // 304                
                            CreateSalesItem(805,      118,    1,      22.95M     ,15M),        // 305                
                            CreateSalesItem(806,      89,     4,      2.95M      ,1M),          // 306               
                            CreateSalesItem(808,      128,    1,      11.95M     ,7M),         // 308
                            CreateSalesItem(808,      25,     1,      12.95M     ,10M),                    
                            CreateSalesItem(809,      8,      1,      10.95M     ,8M),         // 309
                            CreateSalesItem(809,      9,      1,      12.95M     ,9M),                    
                            CreateSalesItem(810,      8,      1,      10.95M     ,8M),         // 310                
                            CreateSalesItem(810,     98,     1,      23.95M      ,15M),        // 310                
                            CreateSalesItem(811,     128,    1,      11.95M      ,7M),         // 311               
                            CreateSalesItem(812,     8,      1,      11.95M      ,8M),         // 312                
                            CreateSalesItem(813,     28,     1,      11.95M      ,9M),         // 313               
                            CreateSalesItem(814,     94,     1,      18.95M      ,15M),        // 314
                            CreateSalesItem(814,     95,     1,      13.25M      ,9M),                    
                            CreateSalesItem(815,     113,    1,      10.75M      ,7M),         // 315               
                            CreateSalesItem(816,     1,      1,      7.95M       ,5M),          // 316
                            CreateSalesItem(816,     3,      1,      7.95M       ,5M),
                            CreateSalesItem(817,     74,     2,      3.55M       ,1.50M),       // 317
                            CreateSalesItem(817,     75,     1,      3.55M       ,1.50M),
                            CreateSalesItem(817,     76,     1,      3.55M       ,1.50M),
                            CreateSalesItem(817,     77,     1,      3.55M       ,1.50M),                  
                            CreateSalesItem(818,     126,    1,      9.95M       ,6M),          // 318
                            CreateSalesItem(818,     29,     1,      5.50M       ,3M),                     
                            CreateSalesItem(819,     5,      2,      15.95M      ,10M),        // 319               
                            CreateSalesItem(820,     61,     1,      5.50M       ,3M),          // 320               
                            CreateSalesItem(821,     63,     1,      24.95M      ,18M),        // 321               
                            CreateSalesItem(822,     123,    1,      14.95M      ,10M),        // 322               
                            CreateSalesItem(823,     117,    1,      12.95M      ,7M),         // 323                
                            CreateSalesItem(824,     20,     1,      7.95M       ,5M),          // 324
                            CreateSalesItem(824,     19,     1,      13.95M      ,10M),
                            CreateSalesItem(824,     1,      1,      7.95M       ,5M),
                            CreateSalesItem(825,     23,     1,      1.95M       ,1M),          // 325
                            CreateSalesItem(826,     128,    1,      11.95M      ,7M),         // 326
                            CreateSalesItem(827,     21,     1,      7.95M       ,5M),          // 327                
                            CreateSalesItem(828,     27,     1,      10.00M      ,8M),         // 328                
                            CreateSalesItem(829,     125,    1,      7.95M       ,5M),          // 329              
                            CreateSalesItem(830,     30,     1,      5.50M       ,3M),          // 330               
                            CreateSalesItem(831,     3,      1,      7.95M       ,5M),          // 331
                            CreateSalesItem(831,     28,     1,      11.95M      ,9M),                    
                            CreateSalesItem(832,     3,      1,      7.95M       ,5M),          // 332                
                            CreateSalesItem(833,     49,     1,      39.95M      ,30M),        // 333                
                            CreateSalesItem(834,     67,     1,      61.95M      ,55M),        // 334                
                            CreateSalesItem(835,     71,     1,      13.95M      ,8M),         // 335                
                            CreateSalesItem(836,     46,     1,      9.95M       ,6M),          // 336                
                            CreateSalesItem(837,     8,      1,      10.95M      ,8M),         // 337
                            CreateSalesItem(838,     35,     1,      34.95M      ,28M),        // 338           
                            CreateSalesItem(839,     7,      1,      10.95M      ,7M),         // 339               
                            CreateSalesItem(840,     97,     1,      13.50M      ,9M),         // 340              
                            CreateSalesItem(841,     45,     1,      4.95M       ,2M),          // 341               
                            CreateSalesItem(842,     3,      1,      7.95M       ,5M),          // 342
                            CreateSalesItem(843,     32,     1,      5.50M       ,3M),          // 343
                            CreateSalesItem(844,     40,     1,      19.95M      ,15M),        // 344
                            CreateSalesItem(844,     8,      1,      10.95M      ,8M),
                            CreateSalesItem(845,     99,     1,      23.95M      ,15M),        // 345
                            CreateSalesItem(846,     83,     1,      15.95M      ,10M),         // 346
                            CreateSalesItem(847,     120,    1,      33.95M      ,20M),         //347
                            CreateSalesItem(848,     11,     1,      20.95M      ,12M),        //348
                            CreateSalesItem(849,     2,      1,       7.95M      ,5M),         //349
                            CreateSalesItem(850,     1,      1,       7.95M      ,5M),         //350
                            CreateSalesItem(851,     111,    1,       9.95M      ,5M),         //351
                            CreateSalesItem(852,     47,     1,       28.95M     ,20M),       //352
                            CreateSalesItem(853,     128,    1,       11.95M     ,7M),         //353
                            CreateSalesItem(854,     68,     1,      83.95M      ,75M),        //354
                            CreateSalesItem(854,     65,     1,      42.95M      ,38M),  
                            CreateSalesItem(855,     105,    1,      7.95M       ,5M),          //355
                            CreateSalesItem(856,     8,      1,      10.95M      ,8M),         //356
                            CreateSalesItem(857,     9,      1,      12.95M      ,9M),         //357
                            CreateSalesItem(858,     8,      1,      10.95M      ,8M),         //358
                            CreateSalesItem(859,     3,      1,      7.95M       ,5M),          //359
                            CreateSalesItem(860,     19,     1,      13.95M      ,10M),        //360
                            CreateSalesItem(861,     39,     1,      27.95M      ,20M),        //361
                            CreateSalesItem(862,     128,    1,      11.95M      ,7M),         //362
                            CreateSalesItem(863,     114,    1,      6.50M       ,3.5M),        //363
                            CreateSalesItem(864,     41,     1,      27.95M      ,24M),        //364
                            CreateSalesItem(865,     49,     1,      39.95M      ,30M),        //365
                            CreateSalesItem(866,     3,      1,      7.95M       ,5M),          //366
                            CreateSalesItem(866,     17,     1,      12.95M      ,10M),       
                            CreateSalesItem(867,     9,      1,      12.95M      ,9M),         //367
                            CreateSalesItem(868,     3,      1,      7.95M       ,5M),          //368
                            CreateSalesItem(869,     127,    1,      15.95M      ,10M),        //369
                            CreateSalesItem(870,     102,    1,      6.95M       ,3M),          //370
                            CreateSalesItem(871,     91,     1,      2.95M       ,1M),          //371
                            CreateSalesItem(872,     8,      1,      10.95M      ,8M),         //372
                            CreateSalesItem(873,     11,     1,      20.95M      ,12M),        //373
                            CreateSalesItem(874,     70,     1,      17.95M      ,12M),        //374
                            CreateSalesItem(875,     116,    1,      32.95M      ,20M),        //375
                            CreateSalesItem(876,     101,    1,      24.95M      ,18M),        //376
                            CreateSalesItem(877,     99,     1,      23.95M      ,15M),        //377
                            CreateSalesItem(878,     71,     1,      13.95M      ,8M),         //378
                            CreateSalesItem(879,     8,      1,      10.95M      ,8M),         //379
                            CreateSalesItem(880,     53,     1,      14.95M      ,8M),         //380
                            CreateSalesItem(881,     58,     1,      9.95M       ,6M),          //381
                            CreateSalesItem(882,     3,      1,      7.95M       ,5M),          //382
                            CreateSalesItem(883,     26,     1,      11.50M      ,9M),         //383
                            CreateSalesItem(884,     38,     1,      13.95M      ,10M),        //384
                            CreateSalesItem(885,     36,     1,      29.95M      ,20M),        //385
                            CreateSalesItem(886,     122,    1,      15.95M      ,10M),        //386
                            CreateSalesItem(887,     66,     1,      27.95M      ,22M),        //387
                            CreateSalesItem(888,     2,      1,      7.95M       ,5M),          //388
                            CreateSalesItem(889,     3,      1,      7.95M       ,5M),          //389
                            CreateSalesItem(890,     124,    1,      9.95M       ,6M),          //390
                            CreateSalesItem(891,     123,    1,      14.95M      ,10M),        //391
                            CreateSalesItem(892,     9,      1,      12.95M      ,9M),         //392
                            CreateSalesItem(893,     8,      1,      10.95M      ,8M),         //393
                            CreateSalesItem(894,    8,       1,      10.95M      ,8M),          //394
                            CreateSalesItem(894,     9,      1,      12.95M      ,9M),
                            CreateSalesItem(895,      20,    1,      7.95M       ,5M),          //395
                            CreateSalesItem(896,     8,      1,      10.95M      ,8M),        //396
                            CreateSalesItem(897,     58,     1,      9.95M       ,6M),          //397
                            CreateSalesItem(898,     91,     1,      2.95M       ,1M),          //398
                            CreateSalesItem(899,     40,     1,      19.95M      ,15M),        // 399
                            CreateSalesItem(899,     8,      1,      10.95M      ,8M),
                            CreateSalesItem(900,     3,     1,      7.95M        ,5M),          //400

                                     // Sale No.901
                            CreateSalesItem(901,      8,      1,      10.95M     ,8M),
                            CreateSalesItem(901,      9,      1,      12.95M     ,9M),                      
                            CreateSalesItem(902,      20,     1,      7.95M      ,5M),           // 402                
                            CreateSalesItem(903,      127,    1,      15.95M     ,10M),         // 403               
                            CreateSalesItem(904,      116,    1,      32.95M     ,20M),         // 404                
                            CreateSalesItem(905,      118,    1,      22.95M     ,15M),         // 405                
                            CreateSalesItem(906,      89,     4,      2.95M      ,1M),           // 406               
                            CreateSalesItem(908,      128,    1,      11.95M     ,7M),          // 408
                            CreateSalesItem(908,      25,     1,      12.95M     ,10M),                     
                            CreateSalesItem(909,      8,      1,      10.95M     ,8M),          // 409
                            CreateSalesItem(909,      9,      1,      12.95M     ,9M),                     
                            CreateSalesItem(910,      8,      1,      10.95M     ,8M),          // 410                
                            CreateSalesItem(910,     98,     1,      23.95M      ,15M),         // 410                
                            CreateSalesItem(911,     128,    1,      11.95M      ,7M),          // 411               
                            CreateSalesItem(912,     8,      1,      11.95M      ,8M),          // 412                
                            CreateSalesItem(913,     28,     1,      11.95M      ,9M),          // 413               
                            CreateSalesItem(914,     94,     1,      18.95M      ,15M),         // 414
                            CreateSalesItem(914,     95,     1,      13.25M      ,9M),                     
                            CreateSalesItem(915,     113,    1,      10.75M      ,7M),          // 415               
                            CreateSalesItem(916,     1,      1,      7.95M       ,5M),           // 416
                            CreateSalesItem(916,     3,      1,      7.95M       ,5M),
                            CreateSalesItem(917,     74,     2,      3.55M       ,1.50M),        // 417
                            CreateSalesItem(917,     75,     1,      3.55M       ,1.50M),
                            CreateSalesItem(917,     76,     1,      3.55M       ,1.50M),
                            CreateSalesItem(917,     77,     1,      3.55M       ,1.50M),                   
                            CreateSalesItem(918,     126,    1,      9.95M       ,6M),           // 418
                            CreateSalesItem(918,     29,     1,      5.50M       ,3M),                      
                            CreateSalesItem(919,     5,      2,      15.95M      ,10M),         // 419               
                            CreateSalesItem(920,     61,     1,      5.50M       ,3M),           // 420               
                            CreateSalesItem(921,     63,     1,      24.95M      ,18M),         // 421               
                            CreateSalesItem(922,     123,    1,      14.95M      ,10M),         // 422               
                            CreateSalesItem(923,     117,    1,      12.95M      ,7M),          // 423                
                            CreateSalesItem(924,     20,     1,      7.95M       ,5M),           // 424
                            CreateSalesItem(924,     19,     1,      13.95M      ,10M),
                            CreateSalesItem(924,     1,      1,      7.95M       ,5M),
                            CreateSalesItem(925,     23,     1,      1.95M       ,1M),           // 425
                            CreateSalesItem(926,     128,    1,      11.95M      ,7M),          // 426
                            CreateSalesItem(927,     21,     1,      7.95M       ,5M),           // 427                
                            CreateSalesItem(928,     27,     1,      10.00M      ,8M),          // 428                
                            CreateSalesItem(929,     125,    1,      7.95M       ,5M),           // 429              
                            CreateSalesItem(930,     30,     1,      5.50M       ,3M),           // 430               
                            CreateSalesItem(931,     3,      1,      7.95M       ,5M),           // 431
                            CreateSalesItem(931,     28,     1,      11.95M      ,9M),                     
                            CreateSalesItem(932,     3,      1,      7.95M       ,5M),           // 432                
                            CreateSalesItem(933,     49,     1,      39.95M      ,28M),         // 433                
                            CreateSalesItem(934,     67,     1,      61.95M      ,55M),         // 434                
                            CreateSalesItem(935,     71,     1,      13.95M      ,8M),          // 435                
                            CreateSalesItem(936,     46,     1,      9.95M       ,6M),           // 436                
                            CreateSalesItem(937,     8,      1,      10.95M      ,8M),          // 437
                            CreateSalesItem(938,     35,     1,      34.95M      ,25M),         // 438           
                            CreateSalesItem(939,     7,      1,      10.95M      ,7M),          // 439               
                            CreateSalesItem(940,     97,     1,      13.50M      ,9M),          // 440              
                            CreateSalesItem(941,     45,     1,      4.95M       ,2M),           // 441               
                            CreateSalesItem(942,     3,      1,      7.95M       ,5M),           // 442
                            CreateSalesItem(943,     32,     1,      5.50M       ,3M),           // 443
                            CreateSalesItem(944,     40,     1,      19.95M      ,15M),         // 444
                            CreateSalesItem(944,     8,      1,      10.95M      ,8M),
                            CreateSalesItem(945,     99,     1,      23.95M      ,15M),         // 445
                            CreateSalesItem(946,     83,     1,      15.95M      ,10M),          // 446
                            CreateSalesItem(947,     120,    1,      33.95M      ,20M),          //447
                            CreateSalesItem(948,     11,     1,      20.95M      ,12M),         //448
                            CreateSalesItem(949,     2,      1,       7.95M      ,5M),          //449
                            CreateSalesItem(950,     1,      1,       7.95M      ,5M),          //450
                            CreateSalesItem(951,     111,    1,       9.95M      ,5M),          //451
                            CreateSalesItem(952,     47,     1,       28.95M     ,20M),        //452
                            CreateSalesItem(953,     128,    1,       11.95M     ,7M),          //453
                            CreateSalesItem(954,     68,     1,      83.95M      ,75M),         //454
                            CreateSalesItem(954,     65,     1,      42.95M      ,35M),   
                            CreateSalesItem(955,     105,    1,      7.95M       ,5M),           //455
                            CreateSalesItem(956,     8,      1,      10.95M      ,8M),          //456
                            CreateSalesItem(957,     9,      1,      12.95M      ,9M),          //457
                            CreateSalesItem(958,     8,      1,      10.95M      ,8M),          //458
                            CreateSalesItem(959,     3,      1,      7.95M       ,5M),           //459
                            CreateSalesItem(960,     19,     1,      13.95M      ,10M),         //460
                            CreateSalesItem(961,     39,     1,      27.95M      ,20M),         //461
                            CreateSalesItem(962,     128,    1,      11.95M      ,7M),          //462
                            CreateSalesItem(963,     114,    1,      6.50M       ,3.5M),         //463
                            CreateSalesItem(964,     41,     1,      27.95M      ,20M),         //464
                            CreateSalesItem(965,     49,     1,      39.95M      ,25M),         //465
                            CreateSalesItem(966,     3,      1,      7.95M       ,5M),           //466
                            CreateSalesItem(966,     17,     1,      12.95M      ,10M),        
                            CreateSalesItem(967,     9,      1,      12.95M      ,9M),          //467
                            CreateSalesItem(968,     3,      1,      7.95M       ,5M),           //468
                            CreateSalesItem(969,     127,    1,      15.95M      ,10M),         //469
                            CreateSalesItem(970,     102,    1,      6.95M       ,3M),           //470
                            CreateSalesItem(971,     91,     1,      2.95M       ,1M),           //471
                            CreateSalesItem(972,     8,      1,      10.95M      ,8M),          //472
                            CreateSalesItem(973,     11,     1,      20.95M      ,12M),         //473
                            CreateSalesItem(974,     70,     1,      17.95M      ,12M),         //474
                            CreateSalesItem(975,     116,    1,      32.95M      ,20M),         //475
                            CreateSalesItem(976,     101,    1,      24.95M      ,18M),         //476
                            CreateSalesItem(977,     99,     1,      23.95M      ,15M),         //477
                            CreateSalesItem(978,     71,     1,      13.95M      ,8M),          //478
                            CreateSalesItem(979,     8,      1,      10.95M      ,8M),          //479
                            CreateSalesItem(980,     53,     1,      14.95M      ,8M),          //480
                            CreateSalesItem(981,     58,     1,      9.95M       ,6M),           //481
                            CreateSalesItem(982,     3,      1,      7.95M       ,5M),           //482
                            CreateSalesItem(983,     26,     1,      11.50M      ,9M),          //483
                            CreateSalesItem(984,     38,     1,      13.95M      ,10M),         //484
                            CreateSalesItem(985,     36,     1,      29.95M      ,20M),         //485
                            CreateSalesItem(986,     122,    1,      15.95M      ,10M),         //486
                            CreateSalesItem(987,     66,     1,      27.95M      ,22M),         //487
                            CreateSalesItem(988,     2,      1,      7.95M       ,5M),           //488
                            CreateSalesItem(989,     3,      1,      7.95M       ,5M),           //489
                            CreateSalesItem(990,     124,    1,      9.95M       ,6M),           //490
                            CreateSalesItem(991,     123,    1,      14.95M      ,10M),         //491
                            CreateSalesItem(992,     9,      1,      12.95M      ,9M),          //492
                            CreateSalesItem(993,     8,      1,      10.95M      ,8M),          //493
                            CreateSalesItem(994,    8,       1,      10.95M      ,8M),           //494
                            CreateSalesItem(994,     9,      1,      12.95M      ,9M),
                            CreateSalesItem(995,      20,    1,      7.95M       ,5M),           //495
                            CreateSalesItem(996,     8,      1,      10.95M      ,8M),         //496
                            CreateSalesItem(997,     58,     1,      9.95M       ,6M),           //497
                            CreateSalesItem(998,     91,     1,      2.95M       ,1M),           //498
                            CreateSalesItem(999,     40,     1,      19.95M      ,15M),         // 999
                            CreateSalesItem(999,     8,      1,      10.95M      ,8M),
                            CreateSalesItem(1000,     3,     1,      7.95M       ,5M),           //1000






            #endregion

                #region September Sale Items
                                    // Sale No.1001
                            CreateSalesItem(1001,      8,      1,      10.95M   ,8M),
                            CreateSalesItem(1001,      9,      1,      12.95M   ,9M),                     
                            CreateSalesItem(1002,      20,     1,      7.95M    ,5M),          // 2                
                            CreateSalesItem(1003,      127,    1,      15.95M   ,10M),        // 3               
                            CreateSalesItem(1004,      116,    1,      32.95M   ,20M),        // 4                
                            CreateSalesItem(1005,      118,    1,      22.95M   ,15M),        // 5                
                            CreateSalesItem(1006,      89,     4,      2.95M    ,1M),          // 6               
                            CreateSalesItem(1008,      128,    1,      11.95M   ,7M),         // 7
                            CreateSalesItem(1008,      25,     1,      12.95M   ,10M),                    
                            CreateSalesItem(1009,      8,      1,      10.95M   ,8M),         // 8
                            CreateSalesItem(1009,      9,      1,      12.95M   ,9M),                    
                            CreateSalesItem(1010,      8,      1,      10.95M   ,8M),         // 9                
                            CreateSalesItem(1010,     98,     1,      23.95M    ,15M),        // 10                
                            CreateSalesItem(1011,     128,    1,      11.95M    ,7M),         // 11               
                            CreateSalesItem(1012,     8,      1,      11.95M    ,8M),         // 12                
                            CreateSalesItem(1013,     28,     1,      11.95M    ,9M),         // 13               
                            CreateSalesItem(1014,     94,     1,      18.95M    ,15M),        // 14
                            CreateSalesItem(1014,     95,     1,      13.25M    ,9M),                    
                            CreateSalesItem(1015,     113,    1,      10.75M    ,7M),         // 15               
                            CreateSalesItem(1016,     1,      1,      7.95M     ,5M),          // 16
                            CreateSalesItem(1016,     3,      1,      7.95M     ,5M),
                            CreateSalesItem(1017,     74,     2,      3.55M     ,1.50M),       // 17
                            CreateSalesItem(1017,     75,     1,      3.55M     ,1.50M),
                            CreateSalesItem(1017,     76,     1,      3.55M     ,1.50M),
                            CreateSalesItem(1017,     77,     1,      3.55M     ,1.50M),                  
                            CreateSalesItem(1018,     126,    1,      9.95M     ,6M),          // 18
                            CreateSalesItem(1018,     29,     1,      5.50M     ,3M),                     
                            CreateSalesItem(1019,     5,      2,      15.95M    ,10M),        // 19               
                            CreateSalesItem(1020,     61,     1,      5.50M     ,3M),          // 20               
                            CreateSalesItem(1021,     63,     1,      24.95M    ,18M),        // 21               
                            CreateSalesItem(1022,     123,    1,      14.95M    ,10M),        // 22               
                            CreateSalesItem(1023,     117,    1,      12.95M    ,7M),         // 23                
                            CreateSalesItem(1024,     20,     1,      7.95M     ,5M),          // 24
                            CreateSalesItem(1024,     19,     1,      13.95M    ,10M),
                            CreateSalesItem(1024,     1,      1,      7.95M     ,5M),
                            CreateSalesItem(1025,     23,     1,      1.95M     ,1M),          // 25
                            CreateSalesItem(1026,     128,    1,      11.95M    ,7M),         // 26
                            CreateSalesItem(1027,     21,     1,      7.95M     ,5M),          // 27                
                            CreateSalesItem(1028,     27,     1,      10.00M    ,8M),         // 28                
                            CreateSalesItem(1029,     125,    1,      7.95M     ,5M),          // 29              
                            CreateSalesItem(1030,     30,     1,      5.50M     ,3M),          // 30               
                            CreateSalesItem(1031,     3,      1,      7.95M     ,5M),          // 31
                            CreateSalesItem(1031,     28,     1,      11.95M    ,9M),                    
                            CreateSalesItem(1032,     3,      1,      7.95M     ,5M),          // 32                
                            CreateSalesItem(1033,     49,     1,      39.95M    ,25M),        // 33                
                            CreateSalesItem(1034,     67,     1,      61.95M    ,55M),        // 34                
                            CreateSalesItem(1035,     71,     1,      13.95M    ,8M),         // 35                
                            CreateSalesItem(1036,     46,     1,      9.95M     ,6M),          // 36                
                            CreateSalesItem(1037,     8,      1,      10.95M    ,8M),         // 37
                            CreateSalesItem(1038,     35,     1,      34.95M    ,25M),        // 38           
                            CreateSalesItem(1039,     7,      1,      10.95M    ,7M),         // 39               
                            CreateSalesItem(1040,     97,     1,      13.50M    ,9M),         // 40              
                            CreateSalesItem(1041,     45,     1,      4.95M     ,2M),          // 41               
                            CreateSalesItem(1042,     3,      1,      7.95M     ,5M),          // 42
                            CreateSalesItem(1043,     32,     1,      5.50M     ,3M),          // 43
                            CreateSalesItem(1044,     40,     1,      19.95M    ,15M),        // 44
                            CreateSalesItem(1044,     8,      1,      10.95M    ,8M),
                            CreateSalesItem(1045,     99,     1,      23.95M    ,15M),        // 45
                            CreateSalesItem(1046,     83,     1,      15.95M    ,10M),         // 46
                            CreateSalesItem(1047,     120,    1,      33.95M    ,20M),         //47
                            CreateSalesItem(1048,     11,     1,      20.95M    ,12M),        //48
                            CreateSalesItem(1049,     2,      1,       7.95M    ,5M),         //49
                            CreateSalesItem(1050,     1,      1,       7.95M    ,5M),         //50
                            CreateSalesItem(1051,     111,    1,       9.95M    ,5M),         //51
                            CreateSalesItem(1052,     47,     1,       28.95M   ,20M),       //52
                            CreateSalesItem(1053,     128,    1,       11.95M   ,7M),         //53
                            CreateSalesItem(1054,     68,     1,      83.95M    ,75M),        //54
                            CreateSalesItem(1054,     65,     1,      42.95M    ,35M),  
                            CreateSalesItem(1055,     105,    1,      7.95M     ,5M),          //55
                            CreateSalesItem(1056,     8,      1,      10.95M    ,8M),         //56
                            CreateSalesItem(1057,     9,      1,      12.95M    ,9M),         //57
                            CreateSalesItem(1058,     8,      1,      10.95M    ,8M),         //58
                            CreateSalesItem(1059,     3,      1,      7.95M     ,5M),          //59
                            CreateSalesItem(1060,     19,     1,      13.95M    ,10M),        //60
                            CreateSalesItem(1061,     39,     1,      27.95M    ,20M),        //61
                            CreateSalesItem(1062,     128,    1,      11.95M    ,7M),         //62
                            CreateSalesItem(1063,     114,    1,      6.50M     ,3.5M),        //63
                            CreateSalesItem(1064,     41,     1,      27.95M    ,20M),        //64
                            CreateSalesItem(1065,     49,     1,      39.95M    ,25M),        //65
                            CreateSalesItem(1066,     3,      1,      7.95M     ,5M),          //66
                            CreateSalesItem(1066,     17,     1,      12.95M    ,10M),       
                            CreateSalesItem(1067,     9,      1,      12.95M    ,9M),         //67
                            CreateSalesItem(1068,     3,      1,      7.95M     ,5M),          //68
                            CreateSalesItem(1069,     127,    1,      15.95M    ,10M),        //69
                            CreateSalesItem(1080,     102,    1,      6.95M     ,3M),          //70
                            CreateSalesItem(1081,     91,     1,      2.95M     ,1M),          //71
                            CreateSalesItem(1082,     8,      1,      10.95M    ,8M),         //72
                            CreateSalesItem(1083,     11,     1,      20.95M    ,12M),        //73
                            CreateSalesItem(1084,     70,     1,      17.95M    ,12M),        //74
                            CreateSalesItem(1085,     116,    1,      32.95M    ,20M),        //75
                            CreateSalesItem(1086,     101,    1,      24.95M    ,18M),        //76
                            CreateSalesItem(1087,     99,     1,      23.95M    ,15M),        //77
                            CreateSalesItem(1088,     71,     1,      13.95M    ,8M),         //78
                            CreateSalesItem(1089,     8,      1,      10.95M    ,8M),         //79
                            CreateSalesItem(1090,     53,     1,      14.95M    ,8M),         //80
                            CreateSalesItem(1091,     58,     1,      9.95M     ,6M),          //81
                            CreateSalesItem(1092,     3,      1,      7.95M     ,5M),          //82
                            CreateSalesItem(1093,     26,     1,      11.50M    ,9M),         //83
                            CreateSalesItem(1094,     38,     1,      13.95M    ,10M),        //84
                            CreateSalesItem(1095,     36,     1,      29.95M    ,20M),        //85
                            CreateSalesItem(1096,     122,    1,      15.95M    ,10M),        //86
                            CreateSalesItem(1097,     66,     1,      27.95M    ,22M),        //87
                            CreateSalesItem(1098,     2,      1,      7.95M     ,5M),          //88
                            CreateSalesItem(1099,     3,      1,      7.95M     ,5M),          //89
                            CreateSalesItem(1100,     124,    1,      9.95M     ,6M),          //90
                            CreateSalesItem(1101,     123,    1,      14.95M    ,10M),        //91
                            CreateSalesItem(1102,     9,      1,      12.95M    ,9M),         //92
                            CreateSalesItem(1103,     8,      1,      10.95M    ,8M),         //93
                            CreateSalesItem(1104,    8,       1,      10.95M    ,8M),          //94
                            CreateSalesItem(1104,     9,      1,      12.95M    ,9M),
                            CreateSalesItem(1105,      20,    1,      7.95M     ,5M),          //95
                            CreateSalesItem(1106,     8,      1,      10.95M    ,8M),        //96
                            CreateSalesItem(1108,     58,     1,      9.95M     ,6M),          //97
                            CreateSalesItem(1109,     91,     1,      2.95M     ,1M),          //98
                            CreateSalesItem(1109,     40,     1,      19.95M    ,15M),        // 99
                            CreateSalesItem(1109,     8,      1,      10.95M    ,8M),
                            CreateSalesItem(1100,     3,     1,      7.95M      ,5M),          //100

                             // Sale No.1101
                            CreateSalesItem(1101,      8,      1,      10.95M   ,8M),
                            CreateSalesItem(1101,      9,      1,      12.95M   ,9M),                     
                            CreateSalesItem(1102,      20,     1,      7.95M    ,5M),          // 102                
                            CreateSalesItem(1103,      127,    1,      15.95M   ,10M),        // 103               
                            CreateSalesItem(1104,      116,    1,      32.95M   ,20M),        // 104                
                            CreateSalesItem(1105,      118,    1,      22.95M   ,15M),        // 105                
                            CreateSalesItem(1106,      89,     4,      2.95M    ,1M),          // 106               
                            CreateSalesItem(1108,      128,    1,      11.95M   ,7M),         // 108
                            CreateSalesItem(1108,      25,     1,      12.95M   ,10M),                    
                            CreateSalesItem(1109,      8,      1,      10.95M   ,8M),         // 109
                            CreateSalesItem(1109,      9,      1,      12.95M   ,9M),                    
                            CreateSalesItem(1110,      8,      1,      10.95M   ,8M),         // 110                
                            CreateSalesItem(1110,     98,     1,      23.95M    ,15M),        // 110                
                            CreateSalesItem(1111,     128,    1,      11.95M    ,7M),         // 111               
                            CreateSalesItem(1112,     8,      1,      11.95M    ,8M),         // 112                
                            CreateSalesItem(1113,     28,     1,      11.95M    ,9M),         // 113               
                            CreateSalesItem(1114,     94,     1,      18.95M    ,15M),        // 114
                            CreateSalesItem(1114,     95,     1,      13.25M    ,9M),                    
                            CreateSalesItem(1115,     113,    1,      10.75M    ,7M),         // 115               
                            CreateSalesItem(1116,     1,      1,      7.95M     ,5M),          // 116
                            CreateSalesItem(1116,     3,      1,      7.95M     ,5M),
                            CreateSalesItem(1117,     74,     2,      3.55M     ,1.50M),       // 117
                            CreateSalesItem(1117,     75,     1,      3.55M     ,1.50M),
                            CreateSalesItem(1117,     76,     1,      3.55M     ,1.50M),
                            CreateSalesItem(1117,     77,     1,      3.55M     ,1.50M),                  
                            CreateSalesItem(1118,     126,    1,      9.95M     ,6M),          // 118
                            CreateSalesItem(1118,     29,     1,      5.50M     ,3M),                     
                            CreateSalesItem(1119,     5,      2,      15.95M    ,10M),        // 119               
                            CreateSalesItem(1120,     61,     1,      5.50M     ,3M),          // 120               
                            CreateSalesItem(1121,     63,     1,      24.95M    ,18M),        // 121               
                            CreateSalesItem(1122,     123,    1,      14.95M    ,10M),        // 122               
                            CreateSalesItem(1123,     117,    1,      12.95M    ,7M),         // 123                
                            CreateSalesItem(1124,     20,     1,      7.95M     ,5M),          // 124
                            CreateSalesItem(1124,     19,     1,      13.95M    ,10M),
                            CreateSalesItem(1124,     1,      1,      7.95M     ,5M),
                            CreateSalesItem(1125,     23,     1,      1.95M     ,1M),          // 125
                            CreateSalesItem(1126,     128,    1,      11.95M    ,7M),         // 126
                            CreateSalesItem(1127,     21,     1,      7.95M     ,5M),          // 127                
                            CreateSalesItem(1128,     27,     1,      10.00M    ,8M),         // 128                
                            CreateSalesItem(1129,     125,    1,      7.95M     ,5M),          // 129              
                            CreateSalesItem(1130,     30,     1,      5.50M     ,3M),          // 130               
                            CreateSalesItem(1131,     3,      1,      7.95M     ,5M),          // 131
                            CreateSalesItem(1131,     28,     1,      11.95M    ,9M),                    
                            CreateSalesItem(1132,     3,      1,      7.95M     ,5M),          // 132                
                            CreateSalesItem(1133,     49,     1,      39.95M    ,25M),        // 133                
                            CreateSalesItem(1134,     67,     1,      61.95M    ,55M),        // 134                
                            CreateSalesItem(1135,     71,     1,      13.95M    ,8M),         // 135                
                            CreateSalesItem(1136,     46,     1,      9.95M     ,6M),          // 136                
                            CreateSalesItem(1137,     8,      1,      10.95M    ,8M),         // 137
                            CreateSalesItem(1138,     35,     1,      34.95M    ,25M),        // 138           
                            CreateSalesItem(1139,     7,      1,      10.95M    ,7M),         // 139               
                            CreateSalesItem(1140,     97,     1,      13.50M    ,9M),         // 140              
                            CreateSalesItem(1141,     45,     1,      4.95M     ,2M),          // 141               
                            CreateSalesItem(1142,     3,      1,      7.95M     ,5M),          // 142
                            CreateSalesItem(1143,     32,     1,      5.50M     ,3M),          // 143
                            CreateSalesItem(1144,     40,     1,      19.95M    ,15M),        // 144
                            CreateSalesItem(1144,     8,      1,      10.95M    ,8M),
                            CreateSalesItem(1145,     99,     1,      23.95M    ,15M),        // 145
                            CreateSalesItem(1146,     83,     1,      15.95M    ,10M),         // 146
                            CreateSalesItem(1147,     120,    1,      33.95M    ,20M),         //147
                            CreateSalesItem(1148,     11,     1,      20.95M    ,12M),        //148
                            CreateSalesItem(1149,     2,      1,       7.95M    ,5M),         //149
                            CreateSalesItem(1150,     1,      1,       7.95M    ,5M),         //150
                            CreateSalesItem(1151,     111,    1,       9.95M    ,5M),         //151
                            CreateSalesItem(1152,     47,     1,       28.95M   ,20M),       //152
                            CreateSalesItem(1153,     128,    1,       11.95M   ,7M),         //153
                            CreateSalesItem(1154,     68,     1,      83.95M    ,75M),        //154
                            CreateSalesItem(1154,     65,     1,      42.95M    ,35M),  
                            CreateSalesItem(1155,     105,    1,      7.95M     ,5M),          //155
                            CreateSalesItem(1156,     8,      1,      10.95M    ,8M),         //156
                            CreateSalesItem(1157,     9,      1,      12.95M    ,9M),         //157
                            CreateSalesItem(1158,     8,      1,      10.95M    ,8M),         //158
                            CreateSalesItem(1159,     3,      1,      7.95M     ,5M),          //159
                            CreateSalesItem(1160,     19,     1,      13.95M    ,10M),        //160
                            CreateSalesItem(1161,     39,     1,      27.95M    ,20M),        //161
                            CreateSalesItem(1162,     128,    1,      11.95M    ,7M),         //162
                            CreateSalesItem(1163,     114,    1,      6.50M     ,3.5M),        //163
                            CreateSalesItem(1164,     41,     1,      27.95M    ,20M),        //164
                            CreateSalesItem(1165,     49,     1,      39.95M    ,25M),        //165
                            CreateSalesItem(1166,     3,      1,      7.95M     ,5M),          //166
                            CreateSalesItem(1166,     17,     1,      12.95M    ,10M),       
                            CreateSalesItem(1167,     9,      1,      12.95M    ,9M),         //167
                            CreateSalesItem(1168,     3,      1,      7.95M     ,5M),          //168
                            CreateSalesItem(1169,     127,    1,      15.95M    ,10M),        //169
                            CreateSalesItem(1170,     102,    1,      6.95M     ,3M),          //170
                            CreateSalesItem(1171,     91,     1,      2.95M     ,1M),          //171
                            CreateSalesItem(1172,     8,      1,      10.95M    ,8M),         //172
                            CreateSalesItem(1173,     11,     1,      20.95M    ,12M),        //173
                            CreateSalesItem(1174,     70,     1,      17.95M    ,12M),        //174
                            CreateSalesItem(1175,     116,    1,      32.95M    ,20M),        //175
                            CreateSalesItem(1176,     101,    1,      24.95M    ,18M),        //176
                            CreateSalesItem(1177,     99,     1,      23.95M    ,15M),        //177
                            CreateSalesItem(1178,     71,     1,      13.95M    ,8M),         //178
                            CreateSalesItem(1179,     8,      1,      10.95M    ,8M),         //179
                            CreateSalesItem(1180,     53,     1,      14.95M    ,8M),         //180
                            CreateSalesItem(1181,     58,     1,      9.95M     ,6M),          //181
                            CreateSalesItem(1182,     3,      1,      7.95M     ,5M),          //182
                            CreateSalesItem(1183,     26,     1,      11.50M    ,9M),         //183
                            CreateSalesItem(1184,     38,     1,      13.95M    ,10M),        //184
                            CreateSalesItem(1185,     36,     1,      29.95M    ,20M),        //185
                            CreateSalesItem(1186,     122,    1,      15.95M    ,10M),        //186
                            CreateSalesItem(1187,     66,     1,      27.95M    ,22M),        //187
                            CreateSalesItem(1188,     2,      1,      7.95M     ,5M),          //188
                            CreateSalesItem(1189,     3,      1,      7.95M     ,5M),          //189
                            CreateSalesItem(1190,     124,    1,      9.95M     ,6M),          //190
                            CreateSalesItem(1191,     123,    1,      14.95M    ,10M),        //191
                            CreateSalesItem(1192,     9,      1,      12.95M    ,9M),         //192
                            CreateSalesItem(1193,     8,      1,      10.95M    ,8M),         //193
                            CreateSalesItem(1194,     8,      1,      10.95M    ,8M),          //194
                            CreateSalesItem(1194,     9,      1,      12.95M    ,9M),
                            CreateSalesItem(1195,     20,    1,      7.95M      ,5M),          //195
                            CreateSalesItem(1196,     8,      1,      10.95M    ,8M),        //196
                            CreateSalesItem(1197,     58,     1,      9.95M     ,6M),          //197
                            CreateSalesItem(1198,     91,     1,      2.95M     ,1M),          //198
                            CreateSalesItem(1199,     40,     1,      19.95M    ,15M),        // 199
                            CreateSalesItem(1199,     8,      1,      10.95M    ,8M),
                            CreateSalesItem(1200,    3,     1,      7.95M       ,5M),           //200

                               // Sale No.1201
                            CreateSalesItem(1201,      8,      1,      10.95M    ,8M),
                            CreateSalesItem(1201,      9,      1,      12.95M    ,9M),                  
                            CreateSalesItem(1202,      20,     1,      7.95M     ,5M),       // 202                
                            CreateSalesItem(1203,      127,    1,      15.95M    ,10M),     // 203               
                            CreateSalesItem(1204,      116,    1,      32.95M    ,20M),     // 204                
                            CreateSalesItem(1205,      118,    1,      22.95M    ,15M),     // 205                
                            CreateSalesItem(1206,      89,     4,      2.95M     ,1M),       // 206               
                            CreateSalesItem(1208,      128,    1,      11.95M    ,7M),      // 208
                            CreateSalesItem(1208,      25,     1,      12.95M    ,10M),                 
                            CreateSalesItem(1209,      8,      1,      10.95M    ,8M),      // 209
                            CreateSalesItem(1209,      9,      1,      12.95M    ,9M),                 
                            CreateSalesItem(1210,      8,      1,      10.95M    ,8M),      // 210                
                            CreateSalesItem(1210,     98,     1,      23.95M     ,15M),     // 210                
                            CreateSalesItem(1211,     128,    1,      11.95M     ,7M),      // 211               
                            CreateSalesItem(1212,     8,      1,      11.95M     ,8M),      // 212                
                            CreateSalesItem(1213,     28,     1,      11.95M     ,9M),      // 213               
                            CreateSalesItem(1214,     94,     1,      18.95M     ,15M),     // 214
                            CreateSalesItem(1214,     95,     1,      13.25M     ,9M),                 
                            CreateSalesItem(1215,     113,    1,      10.75M     ,7M),      // 215               
                            CreateSalesItem(1216,     1,      1,      7.95M      ,5M),       // 216
                            CreateSalesItem(1216,     3,      1,      7.95M      ,5M),
                            CreateSalesItem(1217,     74,     2,      3.55M      ,1.50M),    // 217
                            CreateSalesItem(1217,     75,     1,      3.55M      ,1.50M),
                            CreateSalesItem(1217,     76,     1,      3.55M      ,1.50M),
                            CreateSalesItem(1217,     77,     1,      3.55M      ,1.50M),               
                            CreateSalesItem(1218,     126,    1,      9.95M      ,6M),       // 218
                            CreateSalesItem(1218,     29,     1,      5.50M      ,3M),                  
                            CreateSalesItem(1219,     5,      2,      15.95M     ,10M),     // 219               
                            CreateSalesItem(1220,     61,     1,      5.50M      ,3M),       // 220               
                            CreateSalesItem(1221,     63,     1,      24.95M     ,18M),     // 221               
                            CreateSalesItem(1222,     123,    1,      14.95M     ,10M),     // 222               
                            CreateSalesItem(1223,     117,    1,      12.95M     ,7M),      // 223                
                            CreateSalesItem(1224,     20,     1,      7.95M      ,5M),       // 224
                            CreateSalesItem(1224,     19,     1,      13.95M     ,10M),
                            CreateSalesItem(1224,     1,      1,      7.95M      ,5M),
                            CreateSalesItem(1225,     23,     1,      1.95M      ,1M),       // 225
                            CreateSalesItem(1226,     128,    1,      11.95M     ,7M),      // 226
                            CreateSalesItem(1227,     21,     1,      7.95M      ,5M),       // 227                
                            CreateSalesItem(1228,     27,     1,      10.00M     ,8M),      // 228                
                            CreateSalesItem(1229,     125,    1,      7.95M      ,5M),       // 229              
                            CreateSalesItem(1230,     30,     1,      5.50M      ,3M),       // 230               
                            CreateSalesItem(1231,     3,      1,      7.95M      ,5M),       // 231
                            CreateSalesItem(1231,     28,     1,      11.95M     ,9M),                 
                            CreateSalesItem(1232,     3,      1,      7.95M      ,5M),       // 232                
                            CreateSalesItem(1233,     49,     1,      39.95M     ,25M),     // 233                
                            CreateSalesItem(1234,     67,     1,      61.95M     ,55M),     // 234                
                            CreateSalesItem(1235,     71,     1,      13.95M     ,8M),      // 235                
                            CreateSalesItem(1236,     46,     1,      9.95M      ,6M),       // 236                
                            CreateSalesItem(1237,     8,      1,      10.95M     ,8M),      // 237
                            CreateSalesItem(1238,     35,     1,      34.95M     ,25M),     // 238           
                            CreateSalesItem(1239,     7,      1,      10.95M     ,7M),      // 239               
                            CreateSalesItem(1240,     97,     1,      13.50M     ,9M),      // 240              
                            CreateSalesItem(1241,     45,     1,      4.95M      ,2M),       // 241               
                            CreateSalesItem(1242,     3,      1,      7.95M      ,5M),       // 242
                            CreateSalesItem(1243,     32,     1,      5.50M      ,3M),       // 243
                            CreateSalesItem(1244,     40,     1,      19.95M     ,15M),     // 244
                            CreateSalesItem(1244,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(1245,     99,     1,      23.95M     ,15M),     // 245
                            CreateSalesItem(1246,     83,     1,      15.95M     ,10M),      // 246
                            CreateSalesItem(1247,     120,    1,      33.95M     ,20M),          //247
                            CreateSalesItem(1248,     11,     1,      20.95M     ,12M),         //248
                            CreateSalesItem(1249,     2,      1,       7.95M     ,5M),          //249
                            CreateSalesItem(1250,     1,      1,       7.95M     ,5M),          //250
                            CreateSalesItem(1251,     111,    1,       9.95M     ,5M),          //251
                            CreateSalesItem(1252,     47,     1,       28.95M    ,20M),        //252
                            CreateSalesItem(1253,     128,    1,       11.95M    ,7M),          //253
                            CreateSalesItem(1254,     68,     1,      83.95M     ,75M),         //254
                            CreateSalesItem(1254,     65,     1,      42.95M     ,35M),   
                            CreateSalesItem(1255,     105,    1,      7.95M      ,5M),           //255
                            CreateSalesItem(1256,     8,      1,      10.95M     ,8M),          //256
                            CreateSalesItem(1257,     9,      1,      12.95M     ,9M),          //257
                            CreateSalesItem(1258,     8,      1,      10.95M     ,8M),          //258
                            CreateSalesItem(1259,     3,      1,      7.95M      ,5M),           //259
                            CreateSalesItem(1260,     19,     1,      13.95M     ,10M),         //260
                            CreateSalesItem(1261,     39,     1,      27.95M     ,20M),         //261
                            CreateSalesItem(1262,     128,    1,      11.95M     ,7M),          //262
                            CreateSalesItem(1263,     114,    1,      6.50M      ,3.5M),         //263
                            CreateSalesItem(1264,     41,     1,      27.95M     ,20M),         //264
                            CreateSalesItem(1265,     49,     1,      39.95M     ,25M),         //265
                            CreateSalesItem(1266,     3,      1,      7.95M      ,5M),           //266
                            CreateSalesItem(1266,     17,     1,      12.95M     ,10M),        
                            CreateSalesItem(1267,     9,      1,      12.95M     ,9M),          //267
                            CreateSalesItem(1268,     3,      1,      7.95M      ,5M),           //268
                            CreateSalesItem(1269,     127,    1,      15.95M     ,10M),         //269
                            CreateSalesItem(1270,     102,    1,      6.95M      ,3M),           //270
                            CreateSalesItem(1271,     91,     1,      2.95M      ,1M),           //271
                            CreateSalesItem(1272,     8,      1,      10.95M     ,8M),          //272
                            CreateSalesItem(1273,     11,     1,      20.95M     ,12M),         //273
                            CreateSalesItem(1274,     70,     1,      17.95M     ,12M),         //274
                            CreateSalesItem(1275,     116,    1,      32.95M     ,20M),         //275
                            CreateSalesItem(1276,     101,    1,      24.95M     ,18M),         //276
                            CreateSalesItem(1277,     99,     1,      23.95M     ,15M),         //277
                            CreateSalesItem(1278,     71,     1,      13.95M     ,8M),          //278
                            CreateSalesItem(1279,     8,      1,      10.95M     ,8M),          //279
                            CreateSalesItem(1280,     53,     1,      14.95M     ,8M),          //280
                            CreateSalesItem(1281,     58,     1,      9.95M      ,6M),           //281
                            CreateSalesItem(1282,     3,      1,      7.95M      ,5M),           //282
                            CreateSalesItem(1283,     26,     1,      11.50M     ,9M),          //283
                            CreateSalesItem(1284,     38,     1,      13.95M     ,10M),         //284
                            CreateSalesItem(1285,     36,     1,      29.95M     ,20M),         //285
                            CreateSalesItem(1286,     122,    1,      15.95M     ,10M),         //286
                            CreateSalesItem(1287,     66,     1,      27.95M     ,22M),         //287
                            CreateSalesItem(1288,     2,      1,      7.95M      ,5M),           //288
                            CreateSalesItem(1289,     3,      1,      7.95M      ,5M),           //289
                            CreateSalesItem(1290,     124,    1,      9.95M      ,6M),           //290
                            CreateSalesItem(1291,     123,    1,      14.95M     ,10M),         //291
                            CreateSalesItem(1292,     9,      1,      12.95M     ,9M),          //292
                            CreateSalesItem(1293,     8,      1,      10.95M     ,8M),          //293
                            CreateSalesItem(1294,    8,       1,      10.95M     ,8M),           //294
                            CreateSalesItem(1294,     9,      1,      12.95M     ,9M),
                            CreateSalesItem(1295,      20,    1,      7.95M      ,5M),           //295
                            CreateSalesItem(1296,     8,      1,      10.95M     ,8M),         //296
                            CreateSalesItem(1297,     58,     1,      9.95M      ,6M),           //297
                            CreateSalesItem(1298,     91,     1,      2.95M      ,1M),           //298
                            CreateSalesItem(1299,     40,     1,      19.95M     ,15M),         // 299
                            CreateSalesItem(1299,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(1300,     3,     1,      7.95M       ,5M),           //300

                                    // Sale No.301
                            CreateSalesItem(1301,      8,      1,      10.95M     ,8M),
                            CreateSalesItem(1301,      9,      1,      12.95M     ,9M),                     
                            CreateSalesItem(1302,      20,     1,      7.95M      ,5M),          // 302                
                            CreateSalesItem(1303,      127,    1,      15.95M     ,10M),        // 303               
                            CreateSalesItem(1304,      116,    1,      32.95M     ,20M),        // 304                
                            CreateSalesItem(1305,      118,    1,      22.95M     ,15M),        // 305                
                            CreateSalesItem(1306,      89,     4,      2.95M      ,1M),          // 306               
                            CreateSalesItem(1308,      128,    1,      11.95M     ,7M),         // 308
                            CreateSalesItem(1308,      25,     1,      12.95M     ,10M),                    
                            CreateSalesItem(1309,      8,      1,      10.95M     ,8M),         // 309
                            CreateSalesItem(1309,      9,      1,      12.95M     ,9M),                    
                            CreateSalesItem(1310,      8,      1,      10.95M     ,8M),         // 310                
                            CreateSalesItem(1310,     98,     1,      23.95M      ,15M),        // 310                
                            CreateSalesItem(1311,     128,    1,      11.95M      ,7M),         // 311               
                            CreateSalesItem(1312,     8,      1,      11.95M      ,8M),         // 312                
                            CreateSalesItem(1313,     28,     1,      11.95M      ,9M),         // 313               
                            CreateSalesItem(1314,     94,     1,      18.95M      ,15M),        // 314
                            CreateSalesItem(1314,     95,     1,      13.25M      ,9M),                    
                            CreateSalesItem(1315,     113,    1,      10.75M      ,7M),         // 315               
                            CreateSalesItem(1316,     1,      1,      7.95M       ,5M),          // 316
                            CreateSalesItem(1316,     3,      1,      7.95M       ,5M),
                            CreateSalesItem(1317,     74,     2,      3.55M       ,1.50M),       // 317
                            CreateSalesItem(1317,     75,     1,      3.55M       ,1.50M),
                            CreateSalesItem(1317,     76,     1,      3.55M       ,1.50M),
                            CreateSalesItem(1317,     77,     1,      3.55M       ,1.50M),                  
                            CreateSalesItem(1318,     126,    1,      9.95M       ,6M),          // 318
                            CreateSalesItem(1318,     29,     1,      5.50M       ,3M),                     
                            CreateSalesItem(1319,     5,      2,      15.95M      ,10M),        // 319               
                            CreateSalesItem(1320,     61,     1,      5.50M       ,3M),          // 320               
                            CreateSalesItem(1321,     63,     1,      24.95M      ,18M),        // 321               
                            CreateSalesItem(1322,     123,    1,      14.95M      ,10M),        // 322               
                            CreateSalesItem(1323,     117,    1,      12.95M      ,7M),         // 323                
                            CreateSalesItem(1324,     20,     1,      7.95M       ,5M),          // 324
                            CreateSalesItem(1324,     19,     1,      13.95M      ,10M),
                            CreateSalesItem(1324,     1,      1,      7.95M       ,5M),
                            CreateSalesItem(1325,     23,     1,      1.95M       ,1M),          // 325
                            CreateSalesItem(1326,     128,    1,      11.95M      ,7M),         // 326
                            CreateSalesItem(1327,     21,     1,      7.95M       ,5M),          // 327                
                            CreateSalesItem(1328,     27,     1,      10.00M      ,8M),         // 328                
                            CreateSalesItem(1329,     125,    1,      7.95M       ,5M),          // 329              
                            CreateSalesItem(1330,     30,     1,      5.50M       ,3M),          // 330               
                            CreateSalesItem(1331,     3,      1,      7.95M       ,5M),          // 331
                            CreateSalesItem(1331,     28,     1,      11.95M      ,9M),                    
                            CreateSalesItem(1332,     3,      1,      7.95M       ,5M),          // 332                
                            CreateSalesItem(1333,     49,     1,      39.95M      ,25M),        // 333                
                            CreateSalesItem(1334,     67,     1,      61.95M      ,55M),        // 334                
                            CreateSalesItem(1335,     71,     1,      13.95M      ,8M),         // 335                
                            CreateSalesItem(1336,     46,     1,      9.95M       ,6M),          // 336                
                            CreateSalesItem(1337,     8,      1,      10.95M      ,8M),         // 337
                            CreateSalesItem(1338,     35,     1,      34.95M      ,25M),        // 338           
                            CreateSalesItem(1339,     7,      1,      10.95M      ,7M),         // 339               
                            CreateSalesItem(1340,     97,     1,      13.50M      ,9M),         // 340              
                            CreateSalesItem(1341,     45,     1,      4.95M       ,2M),          // 341               
                            CreateSalesItem(1342,     3,      1,      7.95M       ,5M),          // 342
                            CreateSalesItem(1343,     32,     1,      5.50M       ,3M),          // 343
                            CreateSalesItem(1344,     40,     1,      19.95M      ,15M),        // 344
                            CreateSalesItem(1344,     8,      1,      10.95M      ,8M),
                            CreateSalesItem(1345,     99,     1,      23.95M      ,15M),        // 345
                            CreateSalesItem(1346,     83,     1,      15.95M      ,10M),         // 346
                            CreateSalesItem(1347,     120,    1,      33.95M      ,20M),         //347
                            CreateSalesItem(1348,     11,     1,      20.95M      ,12M),        //348
                            CreateSalesItem(1349,     2,      1,       7.95M      ,5M),         //349
                            CreateSalesItem(1350,     1,      1,       7.95M      ,5M),         //350
                            CreateSalesItem(1351,     111,    1,       9.95M      ,5M),         //351
                            CreateSalesItem(1352,     47,     1,       28.95M     ,20M),       //352
                            CreateSalesItem(1353,     128,    1,       11.95M     ,7M),         //353
                            CreateSalesItem(1354,     68,     1,      83.95M      ,75M),        //354
                            CreateSalesItem(1354,     65,     1,      42.95M      ,35M),  
                            CreateSalesItem(1355,     105,    1,      7.95M       ,5M),          //355
                            CreateSalesItem(1356,     8,      1,      10.95M      ,8M),         //356
                            CreateSalesItem(1357,     9,      1,      12.95M      ,9M),         //357
                            CreateSalesItem(1358,     8,      1,      10.95M      ,8M),         //358
                            CreateSalesItem(1359,     3,      1,      7.95M       ,5M),          //359
                            CreateSalesItem(1360,     19,     1,      13.95M      ,10M),        //360
                            CreateSalesItem(1361,     39,     1,      27.95M      ,20M),        //361
                            CreateSalesItem(1362,     128,    1,      11.95M      ,7M),         //362
                            CreateSalesItem(1363,     114,    1,      6.50M       ,3.5M),        //363
                            CreateSalesItem(1364,     41,     1,      27.95M      ,20M),        //364
                            CreateSalesItem(1365,     49,     1,      39.95M      ,25M),        //365
                            CreateSalesItem(1366,     3,      1,      7.95M       ,5M),          //366
                            CreateSalesItem(1366,     17,     1,      12.95M      ,10M),       
                            CreateSalesItem(1367,     9,      1,      12.95M      ,9M),         //367
                            CreateSalesItem(1368,     3,      1,      7.95M       ,5M),          //368
                            CreateSalesItem(1369,     127,    1,      15.95M      ,10M),        //369
                            CreateSalesItem(1370,     102,    1,      6.95M       ,3M),          //370
                            CreateSalesItem(1371,     91,     1,      2.95M       ,1M),          //371
                            CreateSalesItem(1372,     8,      1,      10.95M      ,8M),         //372
                            CreateSalesItem(1373,     11,     1,      20.95M      ,12M),        //373
                            CreateSalesItem(1374,     70,     1,      17.95M      ,12M),        //374
                            CreateSalesItem(1375,     116,    1,      32.95M      ,20M),        //375
                            CreateSalesItem(1376,     101,    1,      24.95M      ,18M),        //376
                            CreateSalesItem(1377,     99,     1,      23.95M      ,15M),        //377
                            CreateSalesItem(1378,     71,     1,      13.95M      ,8M),         //378
                            CreateSalesItem(1379,     8,      1,      10.95M      ,8M),         //379
                            CreateSalesItem(1380,     53,     1,      14.95M      ,8M),         //380
                            CreateSalesItem(1381,     58,     1,      9.95M       ,6M),          //381
                            CreateSalesItem(1382,     3,      1,      7.95M       ,5M),          //382
                            CreateSalesItem(1383,     26,     1,      11.50M      ,9M),         //383
                            CreateSalesItem(1384,     38,     1,      13.95M      ,10M),        //384
                            CreateSalesItem(1385,     36,     1,      29.95M      ,20M),        //385
                            CreateSalesItem(1386,     122,    1,      15.95M      ,10M),        //386
                            CreateSalesItem(1387,     66,     1,      27.95M      ,22M),        //387
                            CreateSalesItem(1388,     2,      1,      7.95M       ,5M),          //388
                            CreateSalesItem(1389,     3,      1,      7.95M       ,5M),          //389
                            CreateSalesItem(1390,     124,    1,      9.95M       ,6M),          //390
                            CreateSalesItem(1391,     123,    1,      14.95M      ,10M),        //391
                            CreateSalesItem(1392,     9,      1,      12.95M      ,9M),         //392
                            CreateSalesItem(1393,     8,      1,      10.95M      ,8M),         //393
                            CreateSalesItem(1394,    8,       1,      10.95M      ,8M),          //394
                            CreateSalesItem(1394,     9,      1,      12.95M      ,9M),
                            CreateSalesItem(1395,      20,    1,      7.95M       ,5M),          //395
                            CreateSalesItem(1396,     8,      1,      10.95M      ,8M),        //396
                            CreateSalesItem(1397,     58,     1,      9.95M       ,6M),          //397
                            CreateSalesItem(1398,     91,     1,      2.95M       ,1M),          //398
                            CreateSalesItem(1399,     40,     1,      19.95M      ,15M),        // 399
                            CreateSalesItem(1399,     8,      1,      10.95M      ,8M),
                            CreateSalesItem(1400,     3,     1,      7.95M        ,5M),          //400

                                     // Sale No.401
                            CreateSalesItem(1401,      8,      1,      10.95M    ,8M),
                            CreateSalesItem(1401,      9,      1,      12.95M    ,9M),                     
                            CreateSalesItem(1402,      20,     1,      7.95M     ,5M),          // 402                
                            CreateSalesItem(1403,      127,    1,      15.95M    ,10M),        // 403               
                            CreateSalesItem(1404,      116,    1,      32.95M    ,20M),        // 404                
                            CreateSalesItem(1405,      118,    1,      22.95M    ,15M),        // 405                
                            CreateSalesItem(1406,      89,     4,      2.95M     ,1M),          // 406               
                            CreateSalesItem(1408,      128,    1,      11.95M    ,7M),         // 408
                            CreateSalesItem(1408,      25,     1,      12.95M    ,10M),                    
                            CreateSalesItem(1409,      8,      1,      10.95M    ,8M),         // 409
                            CreateSalesItem(1409,      9,      1,      12.95M    ,9M),                    
                            CreateSalesItem(1410,      8,      1,      10.95M    ,8M),         // 410                
                            CreateSalesItem(1410,     98,     1,      23.95M     ,15M),        // 410                
                            CreateSalesItem(1411,     128,    1,      11.95M     ,7M),         // 411               
                            CreateSalesItem(1412,     8,      1,      11.95M     ,8M),         // 412                
                            CreateSalesItem(1413,     28,     1,      11.95M     ,9M),         // 413               
                            CreateSalesItem(1414,     94,     1,      18.95M     ,15M),        // 414
                            CreateSalesItem(1414,     95,     1,      13.25M     ,9M),                    
                            CreateSalesItem(1415,     113,    1,      10.75M     ,7M),         // 415               
                            CreateSalesItem(1416,     1,      1,      7.95M      ,5M),          // 416
                            CreateSalesItem(1416,     3,      1,      7.95M      ,5M),
                            CreateSalesItem(1417,     74,     2,      3.55M      ,1.50M),       // 417
                            CreateSalesItem(1417,     75,     1,      3.55M      ,1.50M),
                            CreateSalesItem(1417,     76,     1,      3.55M      ,1.50M),
                            CreateSalesItem(1417,     77,     1,      3.55M      ,1.50M),                  
                            CreateSalesItem(1418,     126,    1,      9.95M      ,6M),          // 418
                            CreateSalesItem(1418,     29,     1,      5.50M      ,3M),                     
                            CreateSalesItem(1419,     5,      2,      15.95M     ,10M),        // 419               
                            CreateSalesItem(1420,     61,     1,      5.50M      ,3M),          // 420               
                            CreateSalesItem(1421,     63,     1,      24.95M     ,18M),        // 421               
                            CreateSalesItem(1422,     123,    1,      14.95M     ,10M),        // 422               
                            CreateSalesItem(1423,     117,    1,      12.95M     ,7M),         // 423                
                            CreateSalesItem(1424,     20,     1,      7.95M      ,5M),          // 424
                            CreateSalesItem(1424,     19,     1,      13.95M     ,10M),
                            CreateSalesItem(1424,     1,      1,      7.95M      ,5M),
                            CreateSalesItem(1425,     23,     1,      1.95M      ,1M),          // 425
                            CreateSalesItem(1426,     128,    1,      11.95M     ,7M),         // 426
                            CreateSalesItem(1427,     21,     1,      7.95M      ,5M),          // 427                
                            CreateSalesItem(1428,     27,     1,      10.00M     ,8M),         // 428                
                            CreateSalesItem(1429,     125,    1,      7.95M      ,5M),          // 429              
                            CreateSalesItem(1430,     30,     1,      5.50M      ,3M),          // 430               
                            CreateSalesItem(1431,     3,      1,      7.95M      ,5M),          // 431
                            CreateSalesItem(1431,     28,     1,      11.95M     ,9M),                    
                            CreateSalesItem(1432,     3,      1,      7.95M      ,5M),          // 432                
                            CreateSalesItem(1433,     49,     1,      39.95M     ,25M),        // 433                
                            CreateSalesItem(1434,     67,     1,      61.95M     ,55M),        // 434                
                            CreateSalesItem(1435,     71,     1,      13.95M     ,8M),         // 435                
                            CreateSalesItem(1436,     46,     1,      9.95M      ,6M),          // 436                
                            CreateSalesItem(1437,     8,      1,      10.95M     ,8M),         // 437
                            CreateSalesItem(1438,     35,     1,      34.95M     ,25M),        // 438           
                            CreateSalesItem(1439,     7,      1,      10.95M     ,7M),         // 439               
                            CreateSalesItem(1440,     97,     1,      13.50M     ,9M),         // 440              
                            CreateSalesItem(1441,     45,     1,      4.95M      ,2M),          // 441               
                            CreateSalesItem(1442,     3,      1,      7.95M      ,5M),          // 442
                            CreateSalesItem(1443,     32,     1,      5.50M      ,3M),          // 443
                            CreateSalesItem(1444,     40,     1,      19.95M     ,15M),        // 444
                            CreateSalesItem(1444,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(1445,     99,     1,      23.95M     ,15M),        // 445
                            CreateSalesItem(1446,     83,     1,      15.95M     ,10M),         // 446
                            CreateSalesItem(1447,     120,    1,      33.95M     ,20M),         //447
                            CreateSalesItem(1448,     11,     1,      20.95M     ,12M),        //448
                            CreateSalesItem(1449,     2,      1,       7.95M     ,5M),         //449
                            CreateSalesItem(1450,     1,      1,       7.95M     ,5M),         //450
                            CreateSalesItem(1451,     111,    1,       9.95M     ,5M),         //451
                            CreateSalesItem(1452,     47,     1,       28.95M    ,20M),       //452
                            CreateSalesItem(1453,     128,    1,       11.95M    ,7M),         //453
                            CreateSalesItem(1454,     68,     1,      83.95M     ,75M),        //454
                            CreateSalesItem(1454,     65,     1,      42.95M     ,35M),  
                            CreateSalesItem(1455,     105,    1,      7.95M      ,5M),          //455
                            CreateSalesItem(1456,     8,      1,      10.95M     ,8M),         //456
                            CreateSalesItem(1457,     9,      1,      12.95M     ,9M),         //457
                            CreateSalesItem(1458,     8,      1,      10.95M     ,8M),         //458
                            CreateSalesItem(1459,     3,      1,      7.95M      ,5M),          //459
                            CreateSalesItem(1460,     19,     1,      13.95M     ,10M),        //460
                            CreateSalesItem(1461,     39,     1,      27.95M     ,20M),        //461
                            CreateSalesItem(1462,     128,    1,      11.95M     ,7M),         //462
                            CreateSalesItem(1463,     114,    1,      6.50M      ,3.5M),        //463
                            CreateSalesItem(1464,     41,     1,      27.95M     ,20M),        //464
                            CreateSalesItem(1465,     49,     1,      39.95M     ,25M),        //465
                            CreateSalesItem(1466,     3,      1,      7.95M      ,5M),          //466
                            CreateSalesItem(1466,     17,     1,      12.95M     ,10M),       
                            CreateSalesItem(1467,     9,      1,      12.95M     ,9M),         //467
                            CreateSalesItem(1468,     3,      1,      7.95M      ,5M),          //468
                            CreateSalesItem(1469,     127,    1,      15.95M     ,10M),        //469
                            CreateSalesItem(1470,     102,    1,      6.95M      ,3M),          //470
                            CreateSalesItem(1471,     91,     1,      2.95M      ,1M),          //471
                            CreateSalesItem(1472,     8,      1,      10.95M     ,8M),         //472
                            CreateSalesItem(1473,     11,     1,      20.95M     ,12M),        //473
                            CreateSalesItem(1474,     70,     1,      17.95M     ,12M),        //474
                            CreateSalesItem(1475,     116,    1,      32.95M     ,20M),        //475
                            CreateSalesItem(1476,     101,    1,      24.95M     ,18M),        //476
                            CreateSalesItem(1477,     99,     1,      23.95M     ,15M),        //477
                            CreateSalesItem(1478,     71,     1,      13.95M     ,8M),         //478
                            CreateSalesItem(1479,     8,      1,      10.95M     ,8M),         //479
                            CreateSalesItem(1480,     53,     1,      14.95M     ,8M),         //480
                            CreateSalesItem(1481,     58,     1,      9.95M      ,6M),          //481
                            CreateSalesItem(1482,     3,      1,      7.95M      ,5M),          //482
                            CreateSalesItem(1483,     26,     1,      11.50M     ,9M),         //483
                            CreateSalesItem(1484,     38,     1,      13.95M     ,10M),        //484
                            CreateSalesItem(1485,     36,     1,      29.95M     ,20M),        //485
                            CreateSalesItem(1486,     122,    1,      15.95M     ,10M),        //486
                            CreateSalesItem(1487,     66,     1,      27.95M     ,22M),        //487
                            CreateSalesItem(1488,     2,      1,      7.95M      ,5M),          //488
                            CreateSalesItem(1489,     3,      1,      7.95M      ,5M),          //489
                            CreateSalesItem(1490,     124,    1,      9.95M      ,6M),          //490
                            CreateSalesItem(1491,     123,    1,      14.95M     ,10M),        //491
                            CreateSalesItem(1492,     9,      1,      12.95M     ,9M),         //492
                            CreateSalesItem(1493,     8,      1,      10.95M     ,8M),         //493
                            CreateSalesItem(1494,    8,       1,      10.95M     ,8M),          //494
                            CreateSalesItem(1494,     9,      1,      12.95M     ,9M),
                            CreateSalesItem(1495,      20,    1,      7.95M      ,5M),          //495
                            CreateSalesItem(1496,     8,      1,      10.95M     ,8M),        //496
                            CreateSalesItem(1497,     58,     1,      9.95M      ,6M),          //497
                            CreateSalesItem(1498,     91,     1,      2.95M      ,1M),          //498
                            CreateSalesItem(1499,     40,     1,      19.95M     ,15M),        // 499
                            CreateSalesItem(1499,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(1500,     3,     1,      7.95M       ,5M),          //500

                            #endregion

                #region October Sale Items
                 CreateSalesItem(501,      8,      1,      10.95M    ,8M),
                            CreateSalesItem(1501,      9,      1,      12.95M    ,9M),                      
                            CreateSalesItem(1502,      20,     1,      7.95M     ,5M),           // 2                
                            CreateSalesItem(1503,      127,    1,      15.95M    ,10M),         // 3               
                            CreateSalesItem(1504,      116,    1,      32.95M    ,20M),         // 4                
                            CreateSalesItem(1505,      118,    1,      22.95M    ,15M),         // 5                
                            CreateSalesItem(1506,      89,     4,      2.95M     ,1M),           // 6               
                            CreateSalesItem(1508,      128,    1,      11.95M    ,7M),          // 7
                            CreateSalesItem(1508,      25,     1,      12.95M    ,10M),                     
                            CreateSalesItem(1509,      8,      1,      10.95M    ,8M),          // 8
                            CreateSalesItem(1509,      9,      1,      12.95M    ,9M),                     
                            CreateSalesItem(1510,      8,      1,      10.95M    ,8M),          // 9                
                            CreateSalesItem(1510,     98,     1,      23.95M     ,15M),         // 10                
                            CreateSalesItem(1511,     128,    1,      11.95M     ,7M),          // 11               
                            CreateSalesItem(1512,     8,      1,      11.95M     ,8M),          // 12                
                            CreateSalesItem(1513,     28,     1,      11.95M     ,9M),          // 13               
                            CreateSalesItem(1514,     94,     1,      18.95M     ,15M),         // 14
                            CreateSalesItem(1514,     95,     1,      13.25M     ,9M),                     
                            CreateSalesItem(1515,     113,    1,      10.75M     ,7M),          // 15               
                            CreateSalesItem(1516,     1,      1,      7.95M      ,5M),           // 16
                            CreateSalesItem(1516,     3,      1,      7.95M      ,5M),
                            CreateSalesItem(1517,     74,     2,      3.55M      ,1.50M),        // 17
                            CreateSalesItem(1517,     75,     1,      3.55M      ,1.50M),
                            CreateSalesItem(1517,     76,     1,      3.55M      ,1.50M),
                            CreateSalesItem(1517,     77,     1,      3.55M      ,1.50M),                   
                            CreateSalesItem(1518,     126,    1,      9.95M      ,6M),           // 18
                            CreateSalesItem(1518,     29,     1,      5.50M      ,3M),                      
                            CreateSalesItem(1519,     5,      2,      15.95M     ,10M),         // 19               
                            CreateSalesItem(1520,     61,     1,      5.50M      ,3M),           // 20               
                            CreateSalesItem(1521,     63,     1,      24.95M     ,18M),         // 21               
                            CreateSalesItem(1522,     123,    1,      14.95M     ,10M),         // 22               
                            CreateSalesItem(1523,     117,    1,      12.95M     ,7M),          // 23                
                            CreateSalesItem(1524,     20,     1,      7.95M      ,5M),           // 24
                            CreateSalesItem(1524,     19,     1,      13.95M     ,10M),
                            CreateSalesItem(1524,     1,      1,      7.95M      ,5M),
                            CreateSalesItem(1525,     23,     1,      1.95M      ,1M),           // 25
                            CreateSalesItem(1526,     128,    1,      11.95M     ,7M),          // 26
                            CreateSalesItem(1527,     21,     1,      7.95M      ,5M),           // 27                
                            CreateSalesItem(1528,     27,     1,      10.00M     ,8M),          // 28                
                            CreateSalesItem(1529,     125,    1,      7.95M      ,5M),           // 29              
                            CreateSalesItem(1530,     30,     1,      5.50M      ,3M),           // 30               
                            CreateSalesItem(1531,     3,      1,      7.95M      ,5M),           // 31
                            CreateSalesItem(1531,     28,     1,      11.95M     ,9M),                     
                            CreateSalesItem(1532,     3,      1,      7.95M      ,5M),           // 32                
                            CreateSalesItem(1533,     49,     1,      39.95M     ,30M),         // 33                
                            CreateSalesItem(1534,     67,     1,      61.95M     ,58M),         // 34                
                            CreateSalesItem(1535,     71,     1,      13.95M     ,8M),          // 35                
                            CreateSalesItem(1536,     46,     1,      9.95M      ,6M),           // 36                
                            CreateSalesItem(1537,     8,      1,      10.95M     ,8M),          // 37
                            CreateSalesItem(1538,     35,     1,      34.95M     ,25M),         // 38           
                            CreateSalesItem(1539,     7,      1,      10.95M     ,7M),          // 39               
                            CreateSalesItem(1540,     97,     1,      13.50M     ,9M),          // 40              
                            CreateSalesItem(1541,     45,     1,      4.95M      ,2M),           // 41               
                            CreateSalesItem(1542,     3,      1,      7.95M      ,5M),           // 42
                            CreateSalesItem(1543,     32,     1,      5.50M      ,3M),           // 43
                            CreateSalesItem(1544,     40,     1,      19.95M     ,15M),         // 44
                            CreateSalesItem(1544,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(1545,     99,     1,      23.95M     ,15M),         // 45
                            CreateSalesItem(1546,     83,     1,      15.95M     ,10M),          // 46
                            CreateSalesItem(1547,     120,    1,      33.95M     ,20M),          //47
                            CreateSalesItem(1548,     11,     1,      20.95M     ,12M),         //48
                            CreateSalesItem(1549,     2,      1,       7.95M     ,5M),          //49
                            CreateSalesItem(1550,     1,      1,       7.95M     ,5M),          //50
                            CreateSalesItem(1551,     111,    1,       9.95M     ,5M),          //51
                            CreateSalesItem(1552,     47,     1,       28.95M    ,20M),        //52
                            CreateSalesItem(1553,     128,    1,       11.95M    ,7M),          //53
                            CreateSalesItem(1554,     68,     1,      83.95M     ,78M),         //54
                            CreateSalesItem(1554,     65,     1,      42.95M     ,35M),   
                            CreateSalesItem(1555,     105,    1,      7.95M      ,5M),           //55
                            CreateSalesItem(1556,     8,      1,      10.95M     ,8M),          //56
                            CreateSalesItem(1557,     9,      1,      12.95M     ,9M),          //57
                            CreateSalesItem(1558,     8,      1,      10.95M     ,8M),          //58
                            CreateSalesItem(1559,     3,      1,      7.95M      ,5M),           //59
                            CreateSalesItem(1560,     19,     1,      13.95M     ,10M),         //60
                            CreateSalesItem(1561,     39,     1,      27.95M     ,20M),         //61
                            CreateSalesItem(1562,     128,    1,      11.95M     ,7M),          //62
                            CreateSalesItem(1653,     114,    1,      6.50M      ,3.5M),         //63
                            CreateSalesItem(1564,     41,     1,      27.95M     ,20M),         //64
                            CreateSalesItem(1565,     49,     1,      39.95M     ,25M),         //65
                            CreateSalesItem(1566,     3,      1,      7.95M      ,5M),           //66
                            CreateSalesItem(1566,     17,     1,      12.95M     ,10M),        
                            CreateSalesItem(1567,     9,      1,      12.95M     ,9M),          //67
                            CreateSalesItem(1568,     3,      1,      7.95M      ,5M),           //68
                            CreateSalesItem(1569,     127,    1,      15.95M     ,10M),         //69
                            CreateSalesItem(1570,     102,    1,      6.95M      ,3M),           //70
                            CreateSalesItem(1571,     91,     1,      2.95M      ,1M),           //71
                            CreateSalesItem(1572,     8,      1,      10.95M     ,8M),          //72
                            CreateSalesItem(1573,     11,     1,      20.95M     ,12M),         //73
                            CreateSalesItem(1574,     70,     1,      17.95M     ,12M),         //74
                            CreateSalesItem(1575,     116,    1,      32.95M     ,20M),         //75
                            CreateSalesItem(1576,     101,    1,      24.95M     ,18M),         //76
                            CreateSalesItem(1577,     99,     1,      23.95M     ,15M),         //77
                            CreateSalesItem(1578,     71,     1,      13.95M     ,8M),          //78
                            CreateSalesItem(1579,     8,      1,      10.95M     ,8M),          //79
                            CreateSalesItem(1580,     53,     1,      14.95M     ,8M),          //80
                            CreateSalesItem(1581,     58,     1,      9.95M      ,6M),           //81
                            CreateSalesItem(1582,     3,      1,      7.95M      ,5M),           //82
                            CreateSalesItem(1583,     26,     1,      11.50M     ,9M),          //83
                            CreateSalesItem(1584,     38,     1,      13.95M     ,10M),         //84
                            CreateSalesItem(1585,     36,     1,      29.95M     ,20M),         //85
                            CreateSalesItem(1586,     122,    1,      15.95M     ,10M),         //86
                            CreateSalesItem(1587,     66,     1,      27.95M     ,22M),         //87
                            CreateSalesItem(1588,     2,      1,      7.95M      ,5M),           //88
                            CreateSalesItem(1589,     3,      1,      7.95M      ,5M),           //89
                            CreateSalesItem(1590,     124,    1,      9.95M      ,6M),           //90
                            CreateSalesItem(1591,     123,    1,      14.95M     ,10M),         //91
                            CreateSalesItem(1592,     9,      1,      12.95M     ,9M),          //92
                            CreateSalesItem(1593,     8,      1,      10.95M     ,8M),          //93
                            CreateSalesItem(1594,    8,       1,      10.95M     ,8M),           //94
                            CreateSalesItem(1594,     9,      1,      12.95M     ,9M),
                            CreateSalesItem(1595,      20,    1,      7.95M      ,5M),           //95
                            CreateSalesItem(1596,     8,      1,      10.95M     ,8M),         //96
                            CreateSalesItem(1597,     58,     1,      9.95M      ,6M),           //97
                            CreateSalesItem(1598,     91,     1,      2.95M      ,1M),           //98
                            CreateSalesItem(1599,     40,     1,      19.95M     ,15M),         // 99
                            CreateSalesItem(1599,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(1600,     3,     1,      7.95M       ,5M),           //100
                                            
                             // Sale No.601
                            CreateSalesItem(1601,      8,      1,      10.95M    ,8M),
                            CreateSalesItem(1601,      9,      1,      12.95M    ,9M),                      
                            CreateSalesItem(1602,      20,     1,      7.95M     ,5M),           // 102                
                            CreateSalesItem(1603,      127,    1,      15.95M    ,10M),         // 103               
                            CreateSalesItem(1604,      116,    1,      32.95M    ,20M),         // 104                
                            CreateSalesItem(1605,      118,    1,      22.95M    ,15M),         // 105                
                            CreateSalesItem(1606,      89,     4,      2.95M     ,1M),           // 106               
                            CreateSalesItem(1608,      128,    1,      11.95M    ,7M),          // 108
                            CreateSalesItem(1608,      25,     1,      12.95M    ,10M),                     
                            CreateSalesItem(1609,      8,      1,      10.95M    ,8M),          // 109
                            CreateSalesItem(1609,      9,      1,      12.95M    ,9M),                     
                            CreateSalesItem(1610,      8,      1,      10.95M    ,8M),          // 110                
                            CreateSalesItem(1610,     98,     1,      23.95M     ,15M),         // 110                
                            CreateSalesItem(1611,     128,    1,      11.95M     ,7M),          // 111               
                            CreateSalesItem(1612,     8,      1,      11.95M     ,8M),          // 112                
                            CreateSalesItem(1613,     28,     1,      11.95M     ,9M),          // 113               
                            CreateSalesItem(1614,     94,     1,      18.95M     ,15M),         // 114
                            CreateSalesItem(1614,     95,     1,      13.25M     ,9M),                     
                            CreateSalesItem(1615,     113,    1,      10.75M     ,7M),          // 115               
                            CreateSalesItem(1616,     1,      1,      7.95M      ,5M),           // 116
                            CreateSalesItem(1616,     3,      1,      7.95M      ,5M),
                            CreateSalesItem(1617,     74,     2,      3.55M      ,1.50M),        // 117
                            CreateSalesItem(1617,     75,     1,      3.55M      ,1.50M),
                            CreateSalesItem(1617,     76,     1,      3.55M      ,1.50M),
                            CreateSalesItem(1617,     77,     1,      3.55M      ,1.50M),                   
                            CreateSalesItem(1618,     126,    1,      9.95M      ,6M),           // 118
                            CreateSalesItem(1618,     29,     1,      5.50M      ,3M),                      
                            CreateSalesItem(1619,     5,      2,      15.95M     ,10M),         // 119               
                            CreateSalesItem(1620,     61,     1,      5.50M      ,3M),           // 120               
                            CreateSalesItem(1621,     63,     1,      24.95M     ,18M),         // 121               
                            CreateSalesItem(1622,     123,    1,      14.95M     ,10M),         // 122               
                            CreateSalesItem(1623,     117,    1,      12.95M     ,7M),          // 123                
                            CreateSalesItem(1624,     20,     1,      7.95M      ,5M),           // 124
                            CreateSalesItem(1624,     19,     1,      13.95M     ,10M),
                            CreateSalesItem(1624,     1,      1,      7.95M      ,5M),
                            CreateSalesItem(1625,     23,     1,      1.95M      ,1M),           // 125
                            CreateSalesItem(1626,     128,    1,      11.95M     ,7M),          // 126
                            CreateSalesItem(1627,     21,     1,      7.95M      ,5M),           // 127                
                            CreateSalesItem(1628,     27,     1,      10.00M     ,8M),          // 128                
                            CreateSalesItem(1629,     125,    1,      7.95M      ,5M),           // 129              
                            CreateSalesItem(1630,     30,     1,      5.50M      ,3M),           // 130               
                            CreateSalesItem(1631,     3,      1,      7.95M      ,5M),           // 131
                            CreateSalesItem(1631,     28,     1,      11.95M     ,9M),                     
                            CreateSalesItem(1632,     3,      1,      7.95M      ,5M),           // 132                
                            CreateSalesItem(1633,     49,     1,      39.95M     ,30M),         // 133                
                            CreateSalesItem(1634,     67,     1,      61.95M     ,55M),         // 134                
                            CreateSalesItem(1635,     71,     1,      13.95M     ,8M),          // 135                
                            CreateSalesItem(1636,     46,     1,      9.95M      ,6M),           // 136                
                            CreateSalesItem(1637,     8,      1,      10.95M     ,8M),          // 137
                            CreateSalesItem(1638,     35,     1,      34.95M     ,28M),         // 138           
                            CreateSalesItem(1639,     7,      1,      10.95M     ,7M),          // 139               
                            CreateSalesItem(1640,     97,     1,      13.50M     ,9M),          // 140              
                            CreateSalesItem(1641,     45,     1,      4.95M      ,2M),           // 141               
                            CreateSalesItem(1642,     3,      1,      7.95M      ,5M),           // 142
                            CreateSalesItem(1643,     32,     1,      5.50M      ,3M),           // 143
                            CreateSalesItem(1644,     40,     1,      19.95M     ,15M),         // 144
                            CreateSalesItem(1644,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(1645,     99,     1,      23.95M     ,15M),         // 145
                            CreateSalesItem(1646,     83,     1,      15.95M     ,10M),          // 146
                            CreateSalesItem(1647,     120,    1,      33.95M     ,20M),          //147
                            CreateSalesItem(1648,     11,     1,      20.95M     ,12M),         //148
                            CreateSalesItem(1649,     2,      1,       7.95M     ,5M),          //149
                            CreateSalesItem(1650,     1,      1,       7.95M     ,5M),          //150
                            CreateSalesItem(1651,     111,    1,       9.95M     ,5M),          //151
                            CreateSalesItem(1652,     47,     1,       28.95M    ,20M),        //152
                            CreateSalesItem(1653,     128,    1,       11.95M    ,7M),          //153
                            CreateSalesItem(1654,     68,     1,      83.95M     ,78M),         //154
                            CreateSalesItem(1654,     65,     1,      42.95M     ,35M),   
                            CreateSalesItem(1655,     105,    1,      7.95M      ,5M),           //155
                            CreateSalesItem(1656,     8,      1,      10.95M     ,8M),          //156
                            CreateSalesItem(1657,     9,      1,      12.95M     ,9M),          //157
                            CreateSalesItem(1658,     8,      1,      10.95M     ,8M),          //158
                            CreateSalesItem(1659,     3,      1,      7.95M      ,5M),           //159
                            CreateSalesItem(1660,     19,     1,      13.95M     ,10M),         //160
                            CreateSalesItem(1661,     39,     1,      27.95M     ,20M),         //161
                            CreateSalesItem(1662,     128,    1,      11.95M     ,7M),          //162
                            CreateSalesItem(1663,     114,    1,      6.50M      ,3.5M),         //163
                            CreateSalesItem(1664,     41,     1,      27.95M     ,20M),         //164
                            CreateSalesItem(1665,     49,     1,      39.95M     ,25M),         //165
                            CreateSalesItem(1666,     3,      1,      7.95M      ,5M),           //166
                            CreateSalesItem(1666,     17,     1,      12.95M     ,10M),        
                            CreateSalesItem(1667,     9,      1,      12.95M     ,9M),          //167
                            CreateSalesItem(1668,     3,      1,      7.95M      ,5M),           //168
                            CreateSalesItem(1669,     127,    1,      15.95M     ,10M),         //169
                            CreateSalesItem(1670,     102,    1,      6.95M      ,3M),           //170
                            CreateSalesItem(1671,     91,     1,      2.95M      ,1M),           //171
                            CreateSalesItem(1672,     8,      1,      10.95M     ,8M),          //172
                            CreateSalesItem(1673,     11,     1,      20.95M     ,12M),         //173
                            CreateSalesItem(1674,     70,     1,      17.95M     ,12M),         //174
                            CreateSalesItem(1675,     116,    1,      32.95M     ,20M),         //175
                            CreateSalesItem(1676,     101,    1,      24.95M     ,18M),         //176
                            CreateSalesItem(1677,     99,     1,      23.95M     ,15M),         //177
                            CreateSalesItem(1678,     71,     1,      13.95M     ,8M),          //178
                            CreateSalesItem(1679,     8,      1,      10.95M     ,8M),          //179
                            CreateSalesItem(1680,     53,     1,      14.95M     ,8M),          //180
                            CreateSalesItem(1681,     58,     1,      9.95M      ,6M),           //181
                            CreateSalesItem(1682,     3,      1,      7.95M      ,5M),           //182
                            CreateSalesItem(1683,     26,     1,      11.50M     ,9M),          //183
                            CreateSalesItem(1684,     38,     1,      13.95M     ,10M),         //184
                            CreateSalesItem(1685,     36,     1,      29.95M     ,20M),         //185
                            CreateSalesItem(1686,     122,    1,      15.95M     ,10M),         //186
                            CreateSalesItem(1687,     66,     1,      27.95M     ,22M),         //187
                            CreateSalesItem(1688,     2,      1,      7.95M      ,5M),           //188
                            CreateSalesItem(1689,     3,      1,      7.95M      ,5M),           //189
                            CreateSalesItem(1690,     124,    1,      9.95M      ,6M),           //190
                            CreateSalesItem(1691,     123,    1,      14.95M     ,10M),         //191
                            CreateSalesItem(1692,     9,      1,      12.95M     ,9M),          //192
                            CreateSalesItem(1693,     8,      1,      10.95M     ,8M),          //193
                            CreateSalesItem(1694,     8,      1,      10.95M     ,8M),           //194
                            CreateSalesItem(1694,     9,      1,      12.95M     ,9M),
                            CreateSalesItem(1695,     20,    1,      7.95M       ,5M),           //195
                            CreateSalesItem(1696,     8,      1,      10.95M     ,8M),         //196
                            CreateSalesItem(1697,     58,     1,      9.95M      ,6M),           //197
                            CreateSalesItem(1698,     91,     1,      2.95M      ,1M),           //198
                            CreateSalesItem(1699,     40,     1,      19.95M     ,15M),         // 199
                            CreateSalesItem(1699,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(1700,    3,     1,      7.95M        ,5M),            //200
                                            
                               // Sale No.701
                            CreateSalesItem(1701,      8,      1,      10.95M    ,8M),
                            CreateSalesItem(1701,      9,      1,      12.95M    ,9M),                      
                            CreateSalesItem(1702,      20,     1,      7.95M     ,5M),           // 202                
                            CreateSalesItem(1703,      127,    1,      15.95M    ,10M),         // 203               
                            CreateSalesItem(1704,      116,    1,      32.95M    ,20M),         // 204                
                            CreateSalesItem(1705,      118,    1,      22.95M    ,15M),         // 205                
                            CreateSalesItem(1706,      89,     4,      2.95M     ,1M),           // 206               
                            CreateSalesItem(1708,      128,    1,      11.95M    ,7M),          // 208
                            CreateSalesItem(1708,      25,     1,      12.95M    ,10M),                     
                            CreateSalesItem(1709,      8,      1,      10.95M    ,8M),          // 209
                            CreateSalesItem(1709,      9,      1,      12.95M    ,9M),                     
                            CreateSalesItem(1710,      8,      1,      10.95M    ,8M),          // 210                
                            CreateSalesItem(1710,     98,     1,      23.95M     ,15M),         // 210                
                            CreateSalesItem(1711,     128,    1,      11.95M     ,7M),          // 211               
                            CreateSalesItem(1712,     8,      1,      11.95M     ,8M),          // 212                
                            CreateSalesItem(1713,     28,     1,      11.95M     ,9M),          // 213               
                            CreateSalesItem(1714,     94,     1,      18.95M     ,15M),         // 214
                            CreateSalesItem(1714,     95,     1,      13.25M     ,9M),                     
                            CreateSalesItem(1715,     113,    1,      10.75M     ,7M),          // 215               
                            CreateSalesItem(1716,     1,      1,      7.95M      ,5M),           // 216
                            CreateSalesItem(1716,     3,      1,      7.95M      ,5M),
                            CreateSalesItem(1717,     74,     2,      3.55M      ,1.50M),        // 217
                            CreateSalesItem(1717,     75,     1,      3.55M      ,1.50M),
                            CreateSalesItem(1717,     76,     1,      3.55M      ,1.50M),
                            CreateSalesItem(1717,     77,     1,      3.55M      ,1.50M),                   
                            CreateSalesItem(1718,     126,    1,      9.95M      ,6M),           // 218
                            CreateSalesItem(1718,     29,     1,      5.50M      ,3M),                      
                            CreateSalesItem(1719,     5,      2,      15.95M     ,10M),         // 219               
                            CreateSalesItem(1720,     61,     1,      5.50M      ,3M),           // 220               
                            CreateSalesItem(1721,     63,     1,      24.95M     ,18M),         // 221               
                            CreateSalesItem(1722,     123,    1,      14.95M     ,10M),         // 222               
                            CreateSalesItem(1723,     117,    1,      12.95M     ,7M),          // 223                
                            CreateSalesItem(1724,     20,     1,      7.95M      ,5M),           // 224
                            CreateSalesItem(1724,     19,     1,      13.95M     ,10M),
                            CreateSalesItem(1724,     1,      1,      7.95M      ,5M),
                            CreateSalesItem(1725,     23,     1,      1.95M      ,1M),           // 225
                            CreateSalesItem(1726,     128,    1,      11.95M     ,7M),          // 226
                            CreateSalesItem(1727,     21,     1,      7.95M      ,5M),           // 227                
                            CreateSalesItem(1728,     27,     1,      10.00M     ,8M),          // 228                
                            CreateSalesItem(1729,     125,    1,      7.95M      ,5M),           // 229              
                            CreateSalesItem(1730,     30,     1,      5.50M      ,3M),           // 230               
                            CreateSalesItem(1731,     3,      1,      7.95M      ,5M),           // 231
                            CreateSalesItem(1731,     28,     1,      11.95M     ,9M),                     
                            CreateSalesItem(1732,     3,      1,      7.95M      ,5M),           // 232                
                            CreateSalesItem(1733,     49,     1,      39.95M     ,25M),         // 233                
                            CreateSalesItem(1734,     67,     1,      61.95M     ,58M),         // 234                
                            CreateSalesItem(1735,     71,     1,      13.95M     ,8M),          // 235                
                            CreateSalesItem(1736,     46,     1,      9.95M      ,6M),           // 236                
                            CreateSalesItem(1737,     8,      1,      10.95M     ,8M),          // 237
                            CreateSalesItem(1738,     35,     1,      34.95M     ,25M),         // 238           
                            CreateSalesItem(1739,     7,      1,      10.95M     ,7M),          // 239               
                            CreateSalesItem(1740,     97,     1,      13.50M     ,9M),          // 240              
                            CreateSalesItem(1741,     45,     1,      4.95M      ,2M),           // 241               
                            CreateSalesItem(1742,     3,      1,      7.95M      ,5M),           // 242
                            CreateSalesItem(1743,     32,     1,      5.50M      ,3M),           // 243
                            CreateSalesItem(1744,     40,     1,      19.95M     ,15M),         // 244
                            CreateSalesItem(1744,     8,      1,      10.95M     ,8M),
                            CreateSalesItem(1745,     99,     1,      23.95M     ,15M),         // 245
                            CreateSalesItem(1746,     83,     1,      15.95M     ,10M),          // 246
                #endregion                  
            }.ForEach(i => context.SalesItems.Add(i));
            #endregion

            context.SaveChanges(); // Save to the JampContext
        #endregion

        }

        #region Object creation
        // Structure the product attributes
        private static Product CreateProduct(string brand, string name, string size, int quantity, decimal sellingPrice, decimal costPrice, int reOrderLvl, string _creationDate, string description, int categoryId, int businessId, int empID, bool _archived, string _archivedDate, string _special)
        {
            return new Product
            {
                BrandName = brand,
                ProductName = name,
                Description = description,
                Size = size,
                Quantity = quantity,
                CostPrice = costPrice,
                SellingPrice = sellingPrice,
                ReorderLevel = reOrderLvl,
                CreatedDate = _creationDate,
                CategoryID = categoryId,
                BusinessID = businessId,
                EmployeeID = empID,
                Archived = _archived,
                ArchivedDate = _archivedDate,
                Special = _special
            };
        }

        // Structure the category attributes
        private static Category CreateCategory(string _categoryName, string _Date, int _busId)
        {
            _baseCreatedAtDate = _baseCreatedAtDate.AddMinutes(1);

            return new Category
            {
                CategoryName = _categoryName,
                BusinessID = _busId,
                CreatedDate = _Date
            };
        }

        // Structure the business attributes
        private static Business CreateBusiness(string _busName, string _busStreet, string _busCity, string _busPostCode, string _busEmail, string _busContact)
        {
            _baseCreatedAtDate = _baseCreatedAtDate.AddMinutes(1);

            return new Business
            {
                BusinessName = _busName,
                BusinessStreet = _busStreet,
                BusinessCity = _busCity,
                BusinessPostalCode = _busPostCode,
                BusinessEmail = _busEmail,
                BusinessContacts = _busContact,
                CreatedDate = _baseCreatedAtDate.ToString("yyyy-MM-dd, h:mm tt").ToLower()
            };
        }

        // Structure the employee attributes
        private static Employee CreateEmployee(string firstName, string lastName, string Contacts, string email, string roleName, string password, string imageLocation, int businessId)
        {
            _baseCreatedAtDate = _baseCreatedAtDate.AddMinutes(1);

            return new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Contacts = Contacts,
                Email = email,
                RoleName = roleName,
                Password = password,
                BusinessID = businessId,
                ImageLocation = imageLocation,
                CreatedDate = _baseCreatedAtDate.ToString("yyyy-MM-dd, h:mm tt").ToLower(),
            };
        }

        // Structure the supplier attributes
        private static Supplier CreateSupplier(string Name, string Address, string Email, string Contact, string _Description, string _City, string _PostCode, string _Date, int _BusinessId, int _EmployeeId)
        {
            return new Supplier
            {
                SupplierName = Name,
                SupplierAddress = Address,
                SupplierEmail = Email,
                SupplierContact = Contact,
                Description = _Description,
                SupplierCity = _City,
                SupplierPostCode = _PostCode,
                BusinessID = _BusinessId,
                CreatedDate = _Date,
                EmployeeID = _EmployeeId,
            };
        }

        // Structure the SupplierAccount attributes
        private static SupplierAccount CreateSupplierAccount(int _supplierID, string _AccountName, double _Balance, double _CreditLimit, string _Comments, string _payment)
        {
            return new SupplierAccount
            {
                SupplierID = _supplierID,
                AccountName = _AccountName,
                AmountOwed = _Balance,
                CreditLimit = _CreditLimit,
                Comments = _Comments,
                PaymentDate = _payment
            };
        }

        // Structure the SupplierPayment attributes
        private static SupplierPayment CreateSupplierPayment(string _createdDate, decimal _Amount, string _reference, int _SupplierAccountId, int _Employee)
        {
            return new SupplierPayment
            {
                CreatedDate = _createdDate,
                AmountPaid = _Amount,
                Reference = _reference,
                SupplierID = _SupplierAccountId,
                EmployeeID = _Employee
            };
        }

        // Structure the order attributes
        private static Order CreateOrder(string _DateMade, string _DateDue, decimal _Cost, string _status, int _busId, int _SupplierId, int _empId)
        {
            return new Order
            {
                CreatedDate = _DateMade,
                DateDue = _DateDue,
                TotalCost = _Cost,
                Completed = _status,
                SupplierID = _SupplierId,
                BusinessID = _busId,
                EmployeeID = _empId
            };
        }

        // Structure the ProductOrder attributes
        private static ProductOrder CreateProductOrder(int _orderId, int _productId, int _quantityOrder, int _quantityDel, decimal _costPrice)
        {
            return new ProductOrder
            {
                OrderID = _orderId,
                ProductID = _productId,
                QuantityOrdered = _quantityOrder,
                QuantityDelivered = _quantityDel,
                CostPrice = _costPrice
            };
        }

        // Structure the ProductOrder attributes
        private static ProductDelivery CreateProductDelivery(int _deliveryId, int _productId, int _quantitydel)
        {
            return new ProductDelivery
            {
                DeliveryID = _deliveryId,
                ProductID = _productId,
                QuantityDelivered = _quantitydel
            };
        }

        // Structure the Delivery attributes
        private static Delivery CreateDelivery(string _receivedDate, int _employeeId, int _orderID)
        {
            return new Delivery
            {
                CreatedDate = _receivedDate,
                EmployeeID = _employeeId,
                OrderID = _orderID
            };
        }

        //Structure the customer attributes
        private static Customer CreateCustomer(string _Name, string _Surname, string _Contact, string _Address, string _PostCode, string _Email, string _Date, int _Business, int _employee)
        {
            return new Customer
            {
                CustomerName = _Name,
                CustomerSurname = _Surname,
                CustContacts = _Contact,
                Address = _Address,
                PostCode = _PostCode,
                CustEmail = _Email,
                BusinessID = _Business,
                CreatedDate = _Date,
                EmployeeID = _employee
            };
        }

        //Structure the customer account attributes
        private static CustomerAccount CreateCustomerAccount(int _cust, string _AccountName, double _Balance, double _CreditLimit, string _Comments, string _payment)
        {
            return new CustomerAccount
            {
                //CustomerAccountID = _id,
                CustomerID = _cust,
                AccountName = _AccountName,
                AmountOwing = _Balance,
                CreditLimit = _CreditLimit,
                Comments = _Comments,
                PaymentDate = _payment
            };
        }

        // Structure the Incident attributes
        private static Incident CreateIncident(string _creationDate, string _notes, string _type, int _empId, int _businessId)
        {
            return new Incident
            {
                CreatedDate = _creationDate,
                Notes = _notes,
                Type = _type,
                BusinessID = _businessId,
                EmployeeID = _empId
            };
        }

        // Structure the ProductIncident attributes
        private static ProductIncident CreateProductIncident(int _incidentId, int _productId, int _quantity, bool _action)
        {
            return new ProductIncident
            {
                IncidentID = _incidentId,
                ProductID = _productId,
                Quantity = _quantity,
                Removed = _action
            };
        }

        //Structure the AccountPayment attributes
        private static CustomerPayment CreateAccountPayment(string _createdDate, decimal _Amount, string _reference, int _CustomerAccountId, int _Employee)
        {
            return new CustomerPayment
            {
                CreatedDate = _createdDate,
                AmountPaid = _Amount,
                Reference = _reference,
                CustomerID = _CustomerAccountId,
                EmployeeID = _Employee
            };
        }

        // Structure the Sale attributes
        private static Sale CreateSale(string _createdDate, decimal _amountCharged, decimal _amountReceived, decimal _change, int? _customerId, int _businessId, int _employeeId, bool _credit)
        {
            return new Sale
            {
                CreatedDate = _createdDate,
                AmountCharged = _amountCharged,
                AmountReceived = _amountReceived,
                Change = _change,
                CustomerID = _customerId,
                BusinessID = _businessId,
                EmployeeID = _employeeId,
                Credit = _credit
            };
        }

        // Structure the Sale Item attributes
        private static SalesItem CreateSalesItem(int _saleId, int _productId, int _quantity, decimal _price, decimal _cost)
        {
            return new SalesItem
            {
                SaleID = _saleId,
                ProductID = _productId,
                Quantity = _quantity,
                Price = _price,
                CostPrice = _cost 
            };
        }

        // Structure the Device attributes
        private static Device CreateDevice(int _empId, string _number)
        {
            return new Device
            {
                EmployeeID = _empId,
                Number = _number,
            };
        }

        // Structure the User settings attributes
        private static SettingUser CreateSettingUser(int _empID, string _autoUpdate, string _desciption)
        {
            return new SettingUser
            {
                EmployeeID = _empID,
                AutoUpdate = _autoUpdate,
                Descriptions = _desciption,
            };
        }

        #endregion

    }
}