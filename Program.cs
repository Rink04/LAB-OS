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
                    string path = "G://";
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
                    Console.WriteLine("удалить ?");
                    string f = Console.ReadLine();
                    switch (f)
                    {
                        case "Y":
                            string surfile = @"G:\note.txt";
                            Console.WriteLine(File.Exists(surfile) ? "Найден" : " Такого нет ");
                            {
                                File.Delete(surfile);
                                Console.WriteLine("Файл удален");  
                            }
                            break;

                        case "N":
                            Console.WriteLine("Ладно");
                            break;
                    }
                    break;


                case "2":

                    //3
                    Console.WriteLine("Введите имя и возраст");
                    Program tom = new Program { Name = Console.ReadLine(), Age = Console.ReadLine() };
                    string json = JsonSerializer.Serialize<Program>(tom);
                    string fileName = "n.json";
                    File.WriteAllText(fileName, json);
                    Console.WriteLine(json);
                    Program restoredStudent = JsonSerializer.Deserialize<Program>(json);

                    Console.WriteLine("удалить ?");
                    string u = Console.ReadLine();
                    switch (u)
                    {
                        case "Y":
                            string jsfile = @"G:\n.json";
                            Console.WriteLine(File.Exists(jsfile) ? "Файл не существует" : "Файл существует");
                            Console.WriteLine(" ");
                            {
                                File.Delete(jsfile);
                                Console.WriteLine("Файл удалён");
                            }
                            break;

                        case "N":
                            Console.WriteLine("ладно");
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
                    Console.WriteLine($"вы ввели: {chel.Names}, {chel.Ages}, ");

                    List<Program> users = new List<Program>();

                    StreamWriter writer = new StreamWriter("G:\\text.xml");
                    XmlSerializer serializer = new XmlSerializer(typeof(Program));
                    serializer.Serialize(writer, chel);
                    writer.Close();

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("G://text.xml");
                    XmlElement xRoot = xDoc.DocumentElement;
                    XmlElement userElem = xDoc.CreateElement("user");
                    XmlAttribute nameAttr = xDoc.CreateAttribute("name");
                    XmlElement companyElem = xDoc.CreateElement("company");
                    XmlElement ageElem = xDoc.CreateElement("age");

                    // создаем текстовые значения для элементов и атрибута
                    XmlText companyText = xDoc.CreateTextNode(chel.Company);
                    XmlText nameText = xDoc.CreateTextNode(chel.Names);
                    XmlText ageText = xDoc.CreateTextNode(chel.Ages);

                    //добавляем узлы
                    companyElem.AppendChild(companyText);
                    nameAttr.AppendChild(nameText);
                    ageElem.AppendChild(ageText);

                    userElem.AppendChild(companyElem);
                    userElem.Attributes.Append(nameAttr);
                    userElem.AppendChild(ageElem);

                    xRoot.AppendChild(userElem);
                    xDoc.Save("G://text.xml");

                    Console.WriteLine("удалить ?");
                    string o = Console.ReadLine();
                    switch (o)
                    {
                        case "Y":
                            string xmlfile = @"G://text.xml";
                            Console.WriteLine(File.Exists(xmlfile) ? "Файл существует" : "Файл не существует");
                            {
                                File.Delete(xmlfile);
                                Console.WriteLine("Файл удалён");
                                Console.WriteLine(" ");
                            }
                            break;

                        case "N":
                            Console.WriteLine("ладно");
                            break;
                    }
                    break;

                case "4":

                    //5
                    string sourceFolder = "G://test/"; // исходная папка
                    string zipFile = "G://test.zip"; // сжатый файл
                    string targetFolder = "G://newtest"; // папка, куда распаковывается файл

                    ZipFile.CreateFromDirectory(sourceFolder, zipFile);
                    Console.WriteLine($"Папка {sourceFolder} архивирована в файл {zipFile}");
                    ZipFile.ExtractToDirectory(zipFile, targetFolder);

                    Console.WriteLine($"Файл {zipFile} распакован в папку {targetFolder}");
                    Console.ReadLine();

                    Console.WriteLine("удалить ?");
                    string p = Console.ReadLine();
                    switch (p)
                    {
                        case "Y":
                            string curfile = @"G:\test.zip";
                            Console.WriteLine(File.Exists(curfile) ? "Найден" : " Такого нет ");
                            {
                                File.Delete(curfile);
                                Console.WriteLine("Файл удален");
                            }
                            break;

                        case "N":
                            Console.WriteLine("ладно");
                            break;
                    }
                    break;
            }
        }
    }
}
