using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGenerator
{
	public static class Generation
	{
		public static string[] _maleSurnames { get; private set; }
		public static string[] _maleFirstnames { get; private set; }
		public static string[] _maleLastnames { get; private set; }
		public static string[] _femaleSurnames { get; private set; }
		public static string[] _femaleFirstnames { get; private set; }
		public static string[] _femaleLastnames { get; private set; }
		public static string[] _cities { get; private set; }
		public static string[] _typesOfInsurance { get; private set; }
		public static string[] _addresses { get; private set; }
		public static string[] _propertyTypes { get; private set; }
		public static string[] _companyNames { get; private set; }
		public static string[] _contractTexts { get; private set; }
		public static string[] _socialStatusOfClients { get; private set; }

		static Random random = new Random();
		static bool isFilesLoaded = false; // загружены ли все файлы для генерации

		static Generation()
		{
			try
			{
				_maleSurnames = File.ReadAllLines(@"FilesForGeneration\М_фамилия.txt", Encoding.GetEncoding(1251));
				_maleFirstnames = File.ReadAllLines(@"FilesForGeneration\М_имя.txt", Encoding.GetEncoding(1251));
				_maleLastnames = File.ReadAllLines(@"FilesForGeneration\М_Отчество.txt", Encoding.GetEncoding(1251));
				_femaleSurnames = File.ReadAllLines(@"FilesForGeneration\Ж_фамилия.txt", Encoding.GetEncoding(1251));
				_femaleFirstnames = File.ReadAllLines(@"FilesForGeneration\Ж_имя.txt", Encoding.GetEncoding(1251));
				_femaleLastnames = File.ReadAllLines(@"FilesForGeneration\Ж_Отчество.txt", Encoding.GetEncoding(1251));
				_cities = File.ReadAllLines(@"FilesForGeneration\Города.txt", Encoding.GetEncoding(1251));
				_typesOfInsurance = File.ReadAllLines(@"FilesForGeneration\Виды страхования.txt", Encoding.GetEncoding(1251));
				_addresses = File.ReadAllLines(@"FilesForGeneration\Адреса.txt", Encoding.GetEncoding(1251));
				_propertyTypes = File.ReadAllLines(@"FilesForGeneration\Типы собственности.txt", Encoding.GetEncoding(1251));
				_companyNames = File.ReadAllLines(@"FilesForGeneration\Названия_компаний.txt", Encoding.GetEncoding(1251));
				_contractTexts = File.ReadAllLines(@"FilesForGeneration\Тексты_договоров.txt", Encoding.GetEncoding(1251));
				_socialStatusOfClients = File.ReadAllLines(@"FilesForGeneration\Социальные положения клиентов.txt", Encoding.GetEncoding(1251));
				isFilesLoaded = true;
			}
			catch(Exception ex)
            {
				MessageBox.Show(ex.ToString());
            }
		}

		public static string[] GenerateFullName(bool sex)
		{
			if (!isFilesLoaded) return null;

			string[] fullName = new string[3];
			if (!sex) // Женщина
			{
				fullName[0] = _femaleSurnames[random.Next(0, _femaleSurnames.Count())];
				fullName[1] = _femaleFirstnames[random.Next(0, _femaleFirstnames.Count())];
				fullName[2] = _femaleLastnames[random.Next(0, _femaleLastnames.Count())];

				return fullName;
			}

			//мужчина
			fullName[0] = _maleSurnames[random.Next(0, _maleSurnames.Count())];
			fullName[1] = _maleFirstnames[random.Next(0, _maleFirstnames.Count())];
			fullName[2] = _maleLastnames[random.Next(0, _maleLastnames.Count())];

			return fullName;
		}

		public static string GenerateLine(string lineName)
		{
			if (!isFilesLoaded) return null;

			string generatedLine;
			switch (lineName)
			{
				case "city":
					generatedLine = _cities[random.Next(0, _cities.Count())];
					break;
				case "typeOfInsurance":
					generatedLine = _typesOfInsurance[random.Next(0, _typesOfInsurance.Count())];
					break;
				case "address":
					generatedLine = _addresses[random.Next(0, _addresses.Count())];
					break;
				case "propertyType":
					generatedLine = _propertyTypes[random.Next(0, _propertyTypes.Count())];
					break;
				case "companyName":
					generatedLine = _companyNames[random.Next(0, _companyNames.Count())];
					break;
				case "contractText":
					generatedLine = _contractTexts[random.Next(0, _contractTexts.Count())];
					break;
				case "socialStatusOfClient":
					generatedLine = _socialStatusOfClients[random.Next(0, _socialStatusOfClients.Count())];
					break;

				default:
					generatedLine = null;
					break;
			}
			return generatedLine;
		}

		public static string[] GenerateFullDirectory(string directoryName)
		{
			if (!isFilesLoaded) return null;

			List<string> directory = new List<string>();
			switch (directoryName)
			{
				case "cities":
					foreach (var line in _cities)
					{
						directory.Add(line);
					}
					break;
				case "typesOfInsurance":
					foreach (var line in _typesOfInsurance)
					{
						directory.Add(line);
					}
					break;
				case "propertyTypes":
					foreach (var line in _propertyTypes)
					{
						directory.Add(line);
					}
					break;
				case "socialStatusOfClients":
					foreach (var line in _socialStatusOfClients)
					{
						directory.Add(line);
					}
					break;

				default:
					directory = null;
					break;
			}
			return directory.ToArray();
		}

		public static string GeneratePhoneNumber()
		{
			if (!isFilesLoaded) return null;

			string number = random.Next(10000, 100000).ToString();
			number += random.Next(10000, 100000).ToString();
			return number;
		}
	}
}
