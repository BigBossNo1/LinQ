using System;
using System.Collections.Generic;
using System.Linq;

namespace LinQ
{
    public class Product
    {
        public int ID { set; get; }
        public string Name { set; get; }         // tên
        public double Price { set; get; }        // giá
        public string[] Colors { set; get; }     // các màu sắc
        public int Brand { set; get; }           // ID Nhãn hiệu, hãng

        public Product(int id, string name, double price, string[] colors, int brand)
        {
            ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
        }

        // Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
        public override string ToString()
           => $"{ID,3} {Name,12} {Price,5} {Brand,2} {string.Join(",", Colors)}";
    }

    public class Brand
    {
        public string Name { set; get; }
        public int ID { set; get; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Thiết lập Encoding cho Console
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var brands = new List<Brand>() {
            new Brand{ID = 1, Name = "Công ty AAA"},
            new Brand{ID = 2, Name = "Công ty BBB"},
            new Brand{ID = 3, Name = "Công ty CCC"},
            };

            var products = new List<Product>()
            {
            new Product(1, "Bàn trà",    350, new string[] {"Xám", "Xanh"},         2),
            new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
            new Product(3, "Đèn trùm",   500, new string[] {"Trắng" , "Xanh"},      3),
            new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
            new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   1),
            new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               3),
            new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
            new Product(8, "Áo khoác",      450,new string[] {"Đỏ", "Đen", "Vàng"},   2),
            new Product(9, "Ghế ngồi",      500, new string[] {"Đỏ", "Đen", "Vàng"},   2),
            new Product(10, "Đèn cây",      999,new string[] {"Đỏ", "Đen", "Vàng"},   1),
            };

            // TRUY VẤN DẠNG GẦN GIỐNG SQL 
            var query = from p in products where p.Price == 400 select p;
            foreach (var item in query)
            {
                //Console.WriteLine(item);
            }
            //--------------------------------------------------------------

            // WHERE + ORDERBY
            //1. Where
            var query1 = products.Where(x => x.Name.Contains("tr") && x.Brand == 2).Select(y => y.Name);
            foreach (var item in query1)
            {
                //Console.WriteLine(item);
            }
            //2. Where + OrderBy 
            var query2 = products.Where(x => x.Price > 400).OrderBy(y => y.Price);
            foreach (var item in query2)
            {
                //Console.WriteLine(item);
            }
            //------------------------------------------------------------------

            // WHERE + SELECT
            //1. Where
            var query3 = products.Where(x => x.Name.Contains('T')).Select(y => y.Name + "Price = " + y.Price);
            foreach (var item in query3)
            {
                //Console.WriteLine(item);
            }
            //2.Select
            var query4 = products.Select(y => y.Name + " : Have Price = " + y.Price);
            foreach (var item in query4)
            {
                //Console.WriteLine(item);
            }
            //------------------------------------------------------------------------
            //MIN , MAX , SUM , AVG 
            //1. Max
            var query5 = products.Select(y => y.Price).Max();
            //Console.WriteLine("Price have max value is : " + query5);
            //2. Min
            var query6 = products.Where(x => x.Brand == 2 && x.Price > 100).OrderByDescending(y => y.Price).Select(z => z.Price).FirstOrDefault();
            //Console.WriteLine("Price have max value is : " + query6);
            //3. Sum
            var query7 = products.Where(x => x.Brand == 1).Select(x => x.Price).Sum();
            //Console.WriteLine("Summary of price : " + query7);
            //4. Avg
            var query8 = products.Where(x => x.Price % 2 == 0 && x.Price > 500).Select(x => x.Price).Average();
            //Console.WriteLine("Avg of price is : " + query8);
            //--------------------------------------------------------------------------

            //JOIN
            // Option 1 
            var query9 = products.Join(brands, p => p.Brand, b => b.ID, (p, b) => "Tên sản phẩm : " + p.Name + " Sản xuất tại : " + b.Name);
            foreach (var item in query9)
            {
                //Console.WriteLine(item);
            }
            // Option 2
            var query10 = products.Join(brands, p => p.Brand, b => b.ID, (p, b) =>
            {
                return new
                {
                    Ten = p.Name,
                    SanXuat = b.Name
                };
            });
            foreach (var item in query10)
            {
                //Console.WriteLine(item);
            }
            //----------------------------------------------------------------------------------

            //TAKE (Lấy N phần tử đầu tiên)
            var query11 = products.Take(3);
            foreach (var item in query11)
            {
                //Console.WriteLine(item);
            }

            //------------------------------------------------------------------------------------
            //SKIP (Bỏ N phần tử đầu tiên)
            var query12 = products.Skip(3);
            foreach (var item in query12)
            {
                //Console.WriteLine(item);
            }

            //------------------------------------------------------------------------------------
            //ORDERBY - ORDERBYDESCENDING ()
            // 1. OrderBy (Xắp xếp tăng dần)
            var query13 = products.OrderBy(x => x.Price);
            foreach (var item in query13)
            {
                //Console.WriteLine(item);
            }
            // 2. OrderByDescending (Xắp xếp giảm dần)
            var query14 = products.OrderByDescending(x => x.Price);
            foreach (var item in query14)
            {
                //Console.WriteLine(item);
            }

            // products.OrderByDescending(x => x.Price).ToList().ForEach(p=> Console.WriteLine(p));

            //------------------------------------------------------------------------------------
            //REVSER (Đảo ngược thứ tự in ra )
            var query15 = products.Select(x => x.ID).Reverse();
            foreach (var item in query15)
            {
                //Console.WriteLine(item);
            }

            //------------------------------------------------------------------------------------
            //GROUPBY (Tập hợp)
            var query16 = products.Where(x => x.Price == 500).GroupBy(x => x.Price);
            foreach (var group in query16)
            {
                // Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    // Console.WriteLine(item.Name);
                }
            }

            //------------------------------------------------------------------------------------
            //DISTINCT (Loại bỏ phần tử có cùng giá trị)

            //-- products.SelectMany(x => x.Colors).Distinct().ToList().ForEach(color => Console.WriteLine(color));

            //------------------------------------------------------------------------------------
            //SINGLE - SINGLEORDEFAULT (lấy ra duy nhất một phần tử thõa mãn Logic)

            /*
             -Lấy ra phần tử có một gí trị duy nhất nếu có hai giá trị giống nhau sẽ báo lỗi hoặc không tìm thấy phần tử nào nó cũng báo lỗi
             - SingleOrdefault : là nếu không tìm thấy phần tử nào thì sẽ trả về Null còn nếu nhiều hơn 1 phần tử thì vẫn phát sing lỗi
             */
            var query17 = products.SingleOrDefault(x => x.Price == 999);
            if (query17 != null)
            {
                //Console.WriteLine("Number is " + query17);
            }
            else
            {
                //Console.WriteLine("Can't find number");
            }

            //------------------------------------------------------------------------------------
            //ANY (Trả về true nếu thỏa mãn 1 điều kiện Logic)
            var query18 = products.Any(x => x.Price == 600);
            if (query18 == true)
            {
                //Console.WriteLine("Success");
            }
            else
            {
                //Console.WriteLine("Faild");
            }

            //------------------------------------------------------------------------------------
            //ANY (Trả về true nếu tất cả các sản phẩm đều thõa mãn điều kiện Logic)
            var query19 = products.All(x => x.Price > 500);
            if (query19 == true)
            {
                //Console.WriteLine("Success");
            }
            else
            {
                //Console.WriteLine("Faild");
            }


            //------------------------------------------------------------------------------------
            //COUNT (Đếm số lượng)
            var count = products.Count(x => x.Price > 500);
            //Console.WriteLine(count);

            //------------------------------------------------------------------------------------
            //TRUY VAN LINQ 
            /*
             - 1, Xác định nguồn : IEnumerable , Array ....
             - 2, Câu truy vấn : From tenPhanTu in nguồn ... where , orderby 
             - 3, Lấy dữ liệu : select , group by ....
             */
            // Select
            var query20 = from p in products
                          from color in p.Colors
                          where p.Price > 100 && color == "Xanh"
                          orderby p.Price
                          select new
                          {
                              Ten = p.Name,
                              Gia = p.Price,
                              Mau = color,
                          };
            //query20.ToList().ForEach(x => Console.WriteLine(x));
            // GroupBy 
            var query21 = from p in products
                          group p by p.Price;
            query21.ToList().ForEach(group =>
            {
                //Console.WriteLine(group.Key);
                //group.ToList().ForEach(p => Console.WriteLine(p));
            });
            // Join
            var query22 = from product in products
                          join brand in brands on
                          product.Brand equals brand.ID
                          where product.Price > 500
                          select new
                          {
                              Name = product.Name,
                              Price = product.Price,
                              NameBrand = brand.Name
                          };
            // query22.ToList().ForEach(x => Console.WriteLine(x));

            var query23 = from product in products
                          join brand in brands on
                          product.Brand equals brand.ID
                          where product.ID > 5 && product.Price > 400
                          select new
                          {
                              MaSanPham = product.ID,
                              TenSanPham = product.Name,
                              MaSanXuat = product.Brand,
                              NhaSanXuat = brand.Name,
                              Gia = product.Price,
                          };
            query23.ToList().ForEach(x =>
            {
                //Console.WriteLine($"Ten san pham : {x.TenSanPham} : Ma san xuat  {x.MaSanXuat} : Nha san xuat {x.NhaSanXuat}  : Gia {x.Gia} : Ma san pham {x.MaSanPham}");
            });

            //-------------------------------------------------------------------------------------
            // PA 
            var query24 = products.Where(x => x.Price > 300 && x.Price < 500).OrderByDescending(x => x.Price).Join(brands, p => p.Brand, b => b.ID, (p, b) => "Name : " + p.Name + "-> Thuong Hieu : " + b.Name);
            foreach (var item in query24)
            {
                //Console.WriteLine(item);
            }

            var query25 = from product in products
                          join brand in brands on
                          product.Brand equals brand.ID
                          where product.Price > 500 && product.Brand ==1
                          select new
                          {
                              Ten = product.Name,
                              Gia = product.Price,
                              NhaSX = brand.Name
                          };
            query25.ToList().ForEach(x =>
            {
                Console.WriteLine("Ten : " + x.Ten);
                Console.WriteLine("Gia Ban : " + x.Gia);
                Console.WriteLine("Thuong Hieu : " + x.NhaSX);
                Console.WriteLine("---------_-_-------" );
            });

        }
    }
}