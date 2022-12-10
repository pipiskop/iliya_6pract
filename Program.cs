using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Text_converter
{
    internal class converter
    {
        public static void txtconverter()
        {
            //C:\Users\Tachk\Desktop\mytxt.txt
            //C:\Users\Tachk\Desktop\myjson.json
            Console.WriteLine("Введите путь до файла");
            Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
            string path = Console.ReadLine();
            string[] lines;
            List<Mod> modelList = new List<Mod>();

            if (path.Contains(".txt"))
            {
                Console.Clear();
                Print();
                lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }
                for (int i = 0; i < lines.Length; i += 3)
                {
                    Mod model = new Mod();
                    model.name = lines[i];
                    model.width = lines[i + 1];
                    model.height = lines[i + 2];
                    modelList.Add(model);
                }
            }
            else if (path.Contains(".json"))
            {
                Console.Clear();
                Print();
                List<Mod> result = JsonConvert.DeserializeObject<List<Mod>>(File.ReadAllText(path));
                foreach (var i in result)
                {
                    Console.WriteLine(i.name);
                    Console.WriteLine(i.width);
                    Console.WriteLine(i.height);
                    modelList.Add(i);
                }
            }
            else if (path.Contains(".xml"))
            {
                Console.Clear();
                Print();
                List<Mod> result;
                XmlSerializer xml = new XmlSerializer(typeof(List<Mod>));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    result = (List<Mod>)xml.Deserialize(fs);
                }
                foreach (var i in result)
                {
                    Console.WriteLine(i.name);
                    Console.WriteLine(i.width);
                    Console.WriteLine(i.height);
                    modelList.Add(i);
                }
            }

            ConsoleKeyInfo knopka = Console.ReadKey();
            switch (knopka.Key)
            {
                case ConsoleKey.F1:
                    {
                        Console.Clear();
                        Console.WriteLine("Введите путь до файла");
                        Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
                        path = Console.ReadLine();
                        if (path.Contains(".json"))
                        {
                            string json = JsonConvert.SerializeObject(modelList);
                            File.WriteAllText(path, json);
                            Console.WriteLine("фай сохранен");
                            break;
                        }
                        else if (path.Contains(".txt"))
                        {
                            foreach (var line in modelList)
                            {
                                File.AppendAllText(path, line.name + "\n");
                                File.AppendAllText(path, line.width + "\n");
                                File.AppendAllText(path, line.height + "\n");
                            }
                            Console.WriteLine("фай сохранен");
                            break;
                        }
                        else if (path.Contains(".xml"))
                        {
                            XmlSerializer xml = new XmlSerializer(typeof(List<Mod>));
                            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                            {
                                xml.Serialize(fs, modelList);
                            }
                            Console.WriteLine("Файл успешно сохранен!");
                            break;
                        }
                        break;
                    }
                case ConsoleKey.Escape:
                    {
                        Environment.Exit(0);
                        break;
                    }
            }
        }
        public static void Print()
        {
            Console.WriteLine("Сохранить файл в одном из форматов(txt, json, xml) - F1. Выйти из программы - Escape");
            Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
            Console.SetCursorPosition(0, 2);
        }
    }
}