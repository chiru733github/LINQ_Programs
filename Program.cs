using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Channels;

namespace LINQ
{
    public class Program
    {

        public static void Main()
        {
            int[] num = [-2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
            var a = from i in num where i % 2 == 0 select i;//Even Numbers
            var b = from i in num where i >= 0 select i; // Positive Numbers
            var c = from i in num orderby i descending select i;//decending order Number List.
            foreach (var i in a) Console.WriteLine(i);

            //Write a program in C# Sharp to find the number of an array and the square of each number.
            var sq = from i in num select i * i;
            foreach (int i in sq) Console.Write(i + " ");
            Console.WriteLine();

            //Write a program in C# Sharp to display the number and frequency of a given number from an array.
            int[] arr1 = [ 1, 1, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4 ];
            var frequency = from x in arr1 group x by x into y select y;
            foreach (var i in frequency)
            {
                Console.WriteLine(i.Key + " " + i.Count());
            }

            //Write a program in C# Sharp to display the characters and frequency of each character in a given string.
            string Name = "malayalam";
            var count = from i in Name group i by i into j select j;
            foreach (var i in count) Console.WriteLine(i.Key + " " + i.Count());

            //problem 6
            string[] WeekDay = [ "Sunday", "Monday", "Tuesday", "Wednesday", "Thruday", "Friday", "Saturaday" ];
            var DayOfWeek = from i in WeekDay select i;
            foreach (var i in DayOfWeek) { Console.WriteLine(i + " "); }

            //problem 7
            int[] arr2 = [ 5, 1, 9, 2, 3, 7, 4, 5, 6, 8, 7, 6, 3, 4, 5, 2 ];
            var multiple = from i in arr2 group i by i into j select j;
            foreach (var i in multiple) Console.WriteLine(i.Key + " " + i.Count() + " " + i.Count() * i.Key);
            
            //problem 8
            string[] cities = ["ROME", "LONDON", "NAIROBI", "CALIFORNIA", "ZURICH", "NEW DELHI", "AMSTERDAM", "ABU DHABI", "PARIS"];
            var ordercity = cities.OrderBy(i => i.Length).ThenBy(i => i);
            foreach (var item in ordercity)
            {
                Console.WriteLine(item);
            }
            var city = from i in cities where i.StartsWith('N') && i.EndsWith('I') select i;
            foreach (var i in city) Console.WriteLine(i + " ");

            //problem 9
            int[] arr3 = { 55, 200, 740, 76, 230, 482, 95 };
            var greatthanvalue = from i in arr3 where i > 80 select i;
            foreach (var i in greatthanvalue) Console.WriteLine(i + " ");

            ////problem 10
            List<int> templist = new List<int>() { 5, 7, 13, 24, 6, 9, 8 };
            var TopValues = from i in templist orderby i descending select i;
            foreach (var i in TopValues.Take(3)) Console.Write(i + " ");

            ////problem 11
            string s1 = "this IS a STRING";
            var upper = from i in s1.Split(' ') where i.Equals(i.ToUpper()) select i;
            foreach (string i in upper) Console.Write(i + " ");

            ////Write a program in C# Sharp to convert a string array to a string.
            string WeekDays = string.Join(" ", WeekDay
                            .Select(s => s.ToString()));//Lambda expression.
            Console.WriteLine("\n" + WeekDays);
            
            //Write a program in C# Sharp to count file extensions and group it using LINQ.
            string[] strings = ["aaa.frx", "bbb.TXT", "xyz.dbf", "abc.pdf", "aaaa.PDF", "xyz.frt", "abc.xml", "ccc.txt", "zzz.txt"];
            var get = from i in strings group i by i.Split('.')[1].ToLower() into j select j;
            foreach (var i in get) Console.WriteLine(i.Count() + " " + i.Key);
            //Lambda Expression
            var fGrp = strings.Select(file => Path.GetExtension(file).TrimStart('.').ToLower())
                     .GroupBy(z => z, (fExt, extCtr) => new
                     {
                         Extension = fExt,
                         Count = extCtr.Count()
                     });
            foreach (var m in fGrp)
                Console.WriteLine("{0} File(s) with {1} Extension ", m.Count, m.Extension);

            //Write a program that prints the numbers from 1 to 100. But for multiples of
            //three, print "Fizz" instead of the number, and for the multiples of five, print "Buzz".
            //For numbers that are multiples of both three and five, print "FizzBuzz".
            var arrr = Enumerable.Range(1, 100).Select(
                n =>
                {
                    if (n % 3 == 0 && n % 5 == 0) return "FizzBuzz";
                    else if (n % 3 == 0) return "Fizz";
                    else if (n % 5 == 0) return "Buzz";
                    return n.ToString();
                });
            foreach (string i in arrr) Console.WriteLine(i);

            //Count of unique elements in array.
            int[] numsArr = [2, 2, 3, 5, 5, 7];
            int uniqueElement = numsArr.Distinct().Count();
            Console.WriteLine("There are {0} unique value present in array", uniqueElement);

            //Finding the average kb of files present in a location.
            string[] dirfiles = Directory.GetFiles(@"D:\HTML\Jspider");
            var avgFsize = dirfiles.Select(file => Math.Ceiling(new FileInfo(file).Length / 1000f)).Average();
            avgFsize = Math.Round(avgFsize, 2);
            Console.WriteLine("The Average file size is {0} KB", avgFsize);

            //Create a program to manage student grades.Implement a class representing a student with
            //properties like name, ID, and list of grades. Use LINQ to calculate
            //the average grade, highest grade, and lowest grade for each student.
            List<ManageStudent> students =
            [
                new ManageStudent {Name = "Chiru",Id = 1,Marks = [50,75,80]},
                new ManageStudent {Name = "Dileep",Id = 2, Marks = [85,86,90]},
                new ManageStudent {Name = "Badri",Id= 3, Marks = [75,78,80]}
            ];

            students.ForEach(i => Console.WriteLine($"Name of student is {i.Name} and Average of student is {i.Marks.Average()}."));
            var average = students.Select(x => new { StuName = x.Name, Average = Math.Round(x.Marks.Average(), 2) })
                                  .OrderBy(y => y.Average);
            var test = from i in students
                       select new
                       {
                           i.Name,
                           avg = i.Marks.Average(),
                           max = i.Marks.Max(),
                           min = i.Marks.Min()
                       };

            Console.WriteLine("=-------------");
            foreach (var item in test)
            {
                Console.WriteLine($"Name of student:{item.Name}\n" +
                $"Average grade = {Math.Round(item.avg, 2)}\nMaximum grade = {item.max}\n" +
                $"Minimum grade = {item.min}");
            }
            foreach (var i in average) Console.WriteLine(i.StuName + " " + i.Average);
            Console.WriteLine("--------------------");
            var average1 = from i in students orderby i.Marks.Average() descending select new { i.Name, Average1 = i.Marks.Average() };
            foreach (var student in average1) { Console.WriteLine(student); }
            List<int> list = [-1, -2, -3, 4, 5, 6];
            var pos = list.FindAll(x => x > 0);
            foreach (var i in pos) { Console.Write(i + " "); }

            //Problem 17
            char[] character1 = ['m', 'n', 'o', 'p', 'm'];
            //remove m character by using linq.
            var remove = from i in character1 where i != 'm' select i;
            //remove m character by using lambda.
            var remove1 = character1.ToList().FindAll(x => x != 'm');
            foreach (var i in remove1) { Console.Write(i + " "); }
            Console.WriteLine("\n------------");
            foreach (var i in remove) { Console.Write(i + " "); }

            //Problem 22
            string[] name = ["this", "is", "a", "string"];
            int min = 5;
            var minstr = from i in name where i.Length > min select i;

            //problem 24
            ThreeArrayCrossJoin();
            // Combined two list in linq in different joins.
            List<Item_mast> itemlist =
            [
            new Item_mast { Id = 1, ItemDes = "Biscuit  " },
            new Item_mast { Id = 2, ItemDes = "Chocolate" },
            new Item_mast { Id = 3, ItemDes = "Butter   " },
            new Item_mast { Id = 4, ItemDes = "Brade    " },
            new Item_mast { Id = 5, ItemDes = "Honey    " }
            ];
            List<Purchase> purchlist =
            [
            new Purchase { InvNo=100, ItemId = 3,  PurQty = 800 },
            new Purchase { InvNo=101, ItemId = 2,  PurQty = 650 },
            new Purchase { InvNo=102, ItemId = 3,  PurQty = 900 },
            new Purchase { InvNo=103, ItemId = 4,  PurQty = 700 },
            new Purchase { InvNo=104, ItemId = 3,  PurQty = 900 },
            new Purchase { InvNo=105, ItemId = 4,  PurQty = 650 },
            new Purchase { InvNo=106, ItemId = 1,  PurQty = 458 }
            ];
            Console.WriteLine("--------------");
            JoinMethod(itemlist, purchlist);
            LeftJoinMethod(itemlist, purchlist);
            RightJoinMethod(itemlist, purchlist);
            //Use LINQ to perform operations like filtering products by category,
            //finding products withlow stock,
            //and calculating the total value of inventory.
            GroupByCategoryFindLowStockAndTotalInventory();
            Console.ReadKey();
        }

        private static void ThreeArrayCrossJoin()
        {
            char[] letter = ['X', 'Y'];
            int[] numbers = [1, 2, 3];
            string[] color = ["Green", "Orange"];
            var join = from i in letter from j in numbers from k in color select new { letter = i, number = j, color = k };
            foreach (var i in join) { Console.WriteLine(i); }
        }

        private static void JoinMethod(List<Item_mast> itemlist, List<Purchase> purchlist)
        {
            var InnerJoin = from i in itemlist
                            join j in purchlist on i.Id equals j.ItemId
                            select new
                            {
                                Id = i.Id,
                                Name = i.ItemDes,
                                Qty = j.PurQty
                            };
            Console.WriteLine("Id\tName    \tQty");
            foreach (var i in InnerJoin)
            {
                Console.WriteLine($"{i.Id}\t{i.Name}\t{i.Qty}");
            }
        }

        private static void LeftJoinMethod(List<Item_mast> itemlist, List<Purchase> purchlist)
        {
            var LeftJoin = from i in itemlist
                           join j in purchlist on i.Id equals j.ItemId
                           into it
                           from pu in it.DefaultIfEmpty(new Purchase())
                           select new
                           {
                               Id = i.Id,
                               Name = i.ItemDes,
                               Qty = pu.PurQty
                           };
            Console.WriteLine("Id\tName    \tQty");
            foreach (var i in LeftJoin)
            {
                Console.WriteLine($"{i.Id}\t{i.Name}\t{i.Qty}");
            }
        }

        static void RightJoinMethod(List<Item_mast> itemlist, List<Purchase> purchlist)
        {
            var RightJoin = from i in purchlist
                            join j in itemlist on i.ItemId equals j.Id
                            into pu
                            from it in pu.DefaultIfEmpty(new Item_mast())
                            select new
                            {
                                Id = it.Id,
                                Name = it.ItemDes,
                                Qty = i.PurQty
                            };
            Console.WriteLine("Id\tName    \tQty");
            foreach (var i in RightJoin)
            {
                Console.WriteLine($"{i.Id}\t{i.Name}\t{i.Qty}");
            }
        }

        private static void GroupByCategoryFindLowStockAndTotalInventory()
        {
            List<Category> categories =
                        [
                        new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Clothing" }
                        ];

            List<Supplier> suppliers =
            [
            new Supplier { Id = 1, Name = "Supplier A" },
            new Supplier { Id = 2, Name = "Supplier B" }
            ];

            List<Product> products =
            [
            new Product { Id = 1, Name = "Laptop", Price = 999.99, StockQuantity = 10, Category = categories[0], Supplier = suppliers[0] },
            new Product { Id = 2, Name = "T-Shirt", Price = 19.99, StockQuantity = 20, Category = categories[1], Supplier = suppliers[1] },
            new Product { Id = 3, Name = "Smartphone", Price = 699.99, StockQuantity = 5, Category = categories[0], Supplier = suppliers[0] }
            ];
            var filter = from i in products group i by i.Category into j select j;
            var lowstock = filter.Select(x => new { min = x.Select(y => y.StockQuantity).Min(), total = x.Select(y => y.Price * y.StockQuantity).Sum() });
            foreach (var i in lowstock) Console.WriteLine(i.min + " " + Math.Round(i.total, 2));
        }
    }
    class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public Category? Category { get; set; }
        public Supplier? Supplier { get; set; }
    }
    class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    class Supplier
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class Purchase
    {
        public int InvNo { get; set; }
        public int ItemId { get; set; }
        public int PurQty { get; set; }
    }
    public class Item_mast
    {
        public int Id { get; set; }
        public string? ItemDes { get; set; }
    }
    public class ManageStudent
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public double[]? Marks { get; set; }
    }
}