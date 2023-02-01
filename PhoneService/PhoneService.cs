using Newtonsoft.Json;
using PhoneShop.Models;

namespace PhoneShop.Service
{
    public class PhoneService
    {
        private readonly string pathToFile = @".\wwwroot\db\phones.json";
        List<Phone> phones= new List<Phone>();

        public void AddPhoneToList(Phone phone)
        {
            DeserializeList();
            phones.Add(phone);
            SerializeList();
        }

        public List<Phone> ShowPhones()
        {
            DeserializeList();
            return phones;
        }

        private void SerializeList()
        {
            string json = JsonConvert.SerializeObject(phones);
            File.WriteAllText(pathToFile, json);
        }

        private void DeserializeList()
        {
            string json = ReadFileToString();
            phones = JsonConvert.DeserializeObject<List<Phone>>(json) ?? new List<Phone>();
        }

        private string ReadFileToString()
        {
            if (!File.Exists(pathToFile))
            {
                File.WriteAllText(pathToFile, "");
            }

            return File.ReadAllText(pathToFile);
        }

    }
}
