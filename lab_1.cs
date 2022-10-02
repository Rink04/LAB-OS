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



namespace Hiko
{
   [Serializable]
    class Program
    {
        public string Name { get; set; }
        public string Age { get; set; }

        public string Names;
        public string Ages;


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

            // 2 
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
                Console.WriteLine(" ");

            }

            //3
            Console.WriteLine("Введите имя и возраст");
            Program tom = new Program { Name = Console.ReadLine() , Age = Console.ReadLine() };
            string fileName = "Program.json";
            string json = JsonSerializer.Serialize<Program>(tom);
            File.WriteAllText(fileName, json);
            Console.WriteLine(json);
            Program restoredStudent = JsonSerializer.Deserialize<Program>(json);

            string jsfile = @"G:\Program.json";
            Console.WriteLine(File.Exists(jsfile) ? "Файл не существует" : "Файл существует");
            {
                File.Delete(jsfile);
                Console.WriteLine("Файл удалён");
                Console.WriteLine(" ");
            }

            //4
            Program chel = new Program();
            Console.WriteLine("Введите имя:");
            chel.Names = Console.ReadLine();
            Console.Write("Возраст: ");
            chel.Ages = Console.ReadLine();
            Console.WriteLine($"вы ввели: {chel.Names}, {chel.Ages}");

            //Запись в файл
            StreamWriter writer = new StreamWriter("G:\\text.txt");
            XmlSerializer serializer = new XmlSerializer(typeof(Program));
            serializer.Serialize(writer, chel);
            writer.Close();

            //Чтение из файла
            Stream streamout = new FileStream("G:\\text.txt", FileMode.Open, FileAccess.Read);
            XmlSerializer xml = new XmlSerializer(typeof(Program));
            chel = (Program)xml.Deserialize(streamout);

            streamout.Close();

            string xmlfile = @"G:\\text.txt";
            Console.WriteLine(File.Exists(xmlfile) ? "Файл существует" : "Файл не существует");
            {
                File.Delete(xmlfile);
                Console.WriteLine("Файл удалён");
                Console.WriteLine(" ");
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
                Console.WriteLine(" ");


            }
        }

    }

}
        


