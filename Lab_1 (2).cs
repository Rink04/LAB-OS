using System;
using System.IO;
using System.Text;
using System.Linq;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;



namespace Lab1_OS
{
    [Serializable]
    class Program
    {
        public string Name { get; set; }
        public string Age { get; set; }

        public string Names;
        public string Ages;
        public string Company;

        // 1
        static void Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");
                Console.WriteLine($"Объем диска: {drive.TotalSize}");
                Console.WriteLine($"Метка: {drive.VolumeLabel}");
                Console.WriteLine($"FS: {drive.DriveFormat}");
                Console.WriteLine();
            }
            Console.WriteLine("Ведите start:");
            string i = Console.ReadLine();
            switch (i)
            {
                case "start":

                    Console.Clear();
                    Console.WriteLine("Выберете пункт:");
                    Console.WriteLine("какой файл вы хотите создать ?");
                    Console.WriteLine(" 1. txt ");
                    Console.WriteLine(" 2. json ");
                    Console.WriteLine(" 3. xml ");
                    Console.WriteLine(" 4. zip ");

                    string s = Console.ReadLine();
                    switch (s)
                    {
                        case "1":

                            // 2 
                            string path = "E://";
                            DirectoryInfo dirinfo = new DirectoryInfo(path);
                            if (!dirinfo.Exists)
                            {
                                dirinfo.Create();
                            }
                            Console.WriteLine("введите строку для записи в файл:");
                            string text = Console.ReadLine();
                            using (FileStream fstream = new FileStream($"{path}/note.txt", FileMode.OpenOrCreate))
                            {
                                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                                fstream.Write(array, 0, array.Length);
                                Console.WriteLine("текст записан в файл");
                            }
                            using (FileStream fstream = File.OpenRead($"{path}/note.txt"))
                            {
                                byte[] array = new byte[fstream.Length];
                                fstream.Read(array, 0, array.Length);
                                string textFromFile = System.Text.Encoding.Default.GetString(array);
                                Console.WriteLine($"текст из файла: {textFromFile}");
                            }
                            Console.WriteLine("Хотите удалить ?");
                            string f = Console.ReadLine();
                            switch (f)
                            {
                                case "Y":
                                    string surfile = @"E:\note.txt";
                                    Console.WriteLine(File.Exists(surfile) ? "Найден" : " Не найден ");
                                    {
                                        File.Delete(surfile);
                                        Console.WriteLine("Файл удален");
                                    }
                                    break;

                                case "N":
                                    Console.WriteLine("Файл сохранен");
                                    break;
                            }
                             break;
                           
                        case "2":

                            //3
                            Console.WriteLine("Введите имя и возраст");
                            Program tom = new Program { Name = Console.ReadLine(), Age = Console.ReadLine() };
                            string json = JsonSerializer.Serialize<Program>(tom);
                            string fileName = @"E:\n.json";
                            File.WriteAllText(fileName, json);
                            Console.WriteLine(json);
                            Program restoredStudent = JsonSerializer.Deserialize<Program>(json);

                            Console.WriteLine("Хотите удалить ?");
                            string u = Console.ReadLine();
                            switch (u)
                            {
                                case "Y":
                                    string jsfile = @"E:\n.json";
                                    Console.WriteLine(File.Exists(jsfile) ? "Найден" : "Не найден");
                                    {
                                        File.Delete(jsfile);
                                        Console.WriteLine("Файл удалён");
                                    }
                                    break;

                                case "N":
                                    Console.WriteLine("Файл сохранен");
                                    break;
                            }
                            break;

                        case "3":

                            //4
                            Program chel = new Program();
                            Console.WriteLine("Введите Фамилия:");
                            chel.Company = Console.ReadLine();
                            Console.WriteLine("Введите имя:");
                            chel.Names = Console.ReadLine();
                            Console.Write("Возраст: ");
                            chel.Ages = Console.ReadLine();
                            Console.WriteLine($"вы ввели: {chel.Company},{chel.Names}, {chel.Ages}, ");

                            List<Program> users = new List<Program>();

                            StreamWriter writer = new StreamWriter(@"E:\text.xml");
                            XmlSerializer serializer = new XmlSerializer(typeof(Program));
                            serializer.Serialize(writer, chel);
                            writer.Close();

                            XmlDocument xDoc = new XmlDocument();
                            xDoc.Load(@"E:\text.xml");
                            XmlElement xRoot = xDoc.DocumentElement;
                            XmlElement userElem = xDoc.CreateElement("user");
                            XmlAttribute nameAttr = xDoc.CreateAttribute("name");
                            XmlElement companyElem = xDoc.CreateElement("company");
                            XmlElement ageElem = xDoc.CreateElement("age");

                            XmlText companyText = xDoc.CreateTextNode(chel.Company);
                            XmlText nameText = xDoc.CreateTextNode(chel.Names);
                            XmlText ageText = xDoc.CreateTextNode(chel.Ages);
                           
                            companyElem.AppendChild(companyText);
                            nameAttr.AppendChild(nameText);
                            ageElem.AppendChild(ageText);

                            userElem.AppendChild(companyElem);
                            userElem.Attributes.Append(nameAttr);
                            userElem.AppendChild(ageElem);

                            xRoot.AppendChild(userElem);
                            xDoc.Save(@"E:\text.xml");

                            Console.WriteLine("Хотите удалить ?");
                            string o = Console.ReadLine();
                            switch (o)
                            {
                                case "Y":
                                    string xmlfile = @"E:\text.xml";
                                    Console.WriteLine(File.Exists(xmlfile) ? "Найден" : "Не найден");
                                    {
                                        File.Delete(xmlfile);
                                        Console.WriteLine("Файл удалён");
                                    }
                                    break;

                                case "N":
                                    Console.WriteLine("Файл Сохранен");
                                    break;
                            }
                            break;

                        case "4":

                            //5
                            string sourceFolder = "E://test/"; // исходная папка
                            string zipFile = @"E://test.zip"; // сжатый файл
                            string targetFolder = "E://newtest"; // папка, куда распаковывается файл

                            ZipFile.CreateFromDirectory(sourceFolder, zipFile);
                            Console.WriteLine($"Папка {sourceFolder} архивирована в файл {zipFile}");
                            ZipFile.ExtractToDirectory(zipFile, targetFolder);

                            Console.WriteLine($"Файл {zipFile} распакован в папку {targetFolder}");
                            Console.ReadLine();

                            Console.WriteLine("Хотите удалить ?");
                            string p = Console.ReadLine();
                            switch (p)
                            {
                                case "Y":
                                    string curfile = @"E:\test.zip";
                                    Console.WriteLine(File.Exists(curfile) ? "Найден" : "Не найден ");
                                    {
                                        File.Delete(curfile);
                                        Console.WriteLine("Файл удален");
                                    }
                                    break;

                                case "N":
                                    Console.WriteLine("Файл сохранен");
                                    break;
                            }
                            break;
                    }
                    break;

            }
        }
        
    }
}
