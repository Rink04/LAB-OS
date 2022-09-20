using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.IO.Compression;

namespace Hiko
{
    class Program
    {
        // задание 1
        static void Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");
                Console.WriteLine($"Объем диска: {drive.TotalSize}");
                Console.WriteLine($"Метка: {drive.VolumeLabel}");
                Console.WriteLine();
            }

            // задание 2 
            string path = "G:\\";
            DirectoryInfo dirinfo = new DirectoryInfo(path);
            if (!dirinfo.Exists)
            {
                dirinfo.Create();
            }

            Console.WriteLine("введите строку для записи в файл:");
            string text = Console.ReadLine();

            // запись в файл
            using (FileStream fstream = new FileStream($"{path}/note.txt", FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("текст записан в файл");
            }

            // считка
            using (FileStream fstream = File.OpenRead($"{path}/note.txt"))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine($"текст из файла: {textFromFile}");

            }

            string surfile = @"G:\note.txt";
            Console.WriteLine(File.Exists(surfile) ? "Найден" : " Такого нет ");
            {
                File.Delete(surfile);
                Console.WriteLine("Файл удален");
            }
            //5

            string sourceFolder = "G://kar/"; // исходная папка
            string zipFile = "G://hh.zip"; // сжатый файл
            string targetFolder = "G://qwe"; // папка, куда распаковывается файл

            ZipFile.CreateFromDirectory(sourceFolder, zipFile);
            Console.WriteLine($"Папка {sourceFolder} архивирована в файл {zipFile}");
            ZipFile.ExtractToDirectory(zipFile, targetFolder);

            Console.WriteLine($"Файл {zipFile} распакован в папку {targetFolder}");
            Console.ReadLine();

            string curfile = @"G://qwe";
            Console.WriteLine(File.Exists(curfile) ? "Найден" : " Такого нет ");
            {
                File.Delete(curfile);
                Console.WriteLine("Файл удален");


            }

        }
        //3
        class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Company { get; set; }

            static async Task Main(string[] args)
            {
                Student tom = new Student { Name = "Nastasia", Age = "19" };
                string fileName = "Student.json";
                string json = JsonSerializer.Serialize<Student>(tom);
                File.WriteAllText(fileName, json);
                Console.WriteLine(json);
                Student restoredStudent = JsonSerializer.Deserialize<Student>(json);

                string jsfile = @"G:\Student.json";
                Console.WriteLine(File.Exists(jsfile) ? "Файл существует" : "Файл не существует");
                {
                    File.Delete(jsfile);
                    Console.WriteLine("Файл удалён");

                }
            }
        }

        
    }
}
            // недо 4
//            List<Student> users = new List<Student>();

//            XmlDocument xDoc = new XmlDocument();
//            xDoc.Load("D://users.xml");
//                    XmlElement xRoot = xDoc.DocumentElement;
//                    foreach (XmlElement xnode in xRoot)
//                    {
//                        User user = new User();
//            XmlNode attr = xnode.Attributes.GetNamedItem("name");
//                        if (attr != null)
//                            user.Name = attr.Value;

//                        foreach (XmlNode childnode in xnode.ChildNodes)
//                        {
//                            if (childnode.Name == "company")
//                                user.Company = childnode.InnerText;

//                            if (childnode.Name == "age")
//                                user.Age = Int32.Parse(childnode.InnerText);
//                        }
//        users.Add(user);
//                    }
//                    foreach (User u in users)
//                        Console.WriteLine($"{u.Name} ({u.Company}) - {u.Age}");
//                    Console.Read();
//                }
//XmlDocument xDoc = new XmlDocument();
//xDoc.Load("D://users.xml");
//XmlElement xRoot = xDoc.DocumentElement;

//XmlNode firstNode = xRoot.FirstChild;
//xRoot.RemoveChild(firstNode);
//xDoc.Save("D://users.xml");
//                    //    }
//     // 4      
//class Phone
//        {
//            public string Name { get; set; }
//            public string Price { get; set; }
//            static async Task Main(string[] args)
//            { 
//                XDocument xdoc = XDocument.Load("phones.xml");
//                var items = from xe in xdoc.Element("phones").Elements("phone")
//                    where xe.Element("company").Value == "Samsung"
//                    select new Phone
//                    {
//                        Name = xe.Attribute("name").Value,
//                        Price = xe.Element("price").Value
//                    };
 
//                foreach (var item in items)
//                Console.WriteLine($"{item.Name} - {item.Price}");}
//        }
        

// }
        

   
    


