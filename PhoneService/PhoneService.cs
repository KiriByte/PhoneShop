using Newtonsoft.Json;
using PhoneShop.Models;

namespace PhoneShop.Service
{
    public class PhoneService
    {
        private readonly string pathToFile = @".\wwwroot\db\phones.json";
        List<PhoneModel> phones = new List<PhoneModel>();

        public void AddPhoneToList(PhoneModel phone)
        {
            DeserializeList();
            phone.Id = FindFirstFreeId();
            phones.Add(phone);
            SerializeList();
        }

        public List<PhoneModel> ShowPhones()
        {
            DeserializeList();
            return phones;
        }


        public PhoneModel ShowPhoneInfoById(int id)
        {
            DeserializeList();
            return phones.Find((phone) => phone.Id == id);
        }

        public void DeletePhoneById(int id)
        {
            DeserializeList();
            var phone = phones.Find((phone) => phone.Id == id);
            phones.Remove(phone);
            SerializeList();
        }

        private int FindFirstFreeId()
        {
            DeserializeList();
            var orderedId = from phone in phones
                orderby phone.Id
                select phone.Id;
            var result = Enumerable.Range(1, int.MaxValue).Except(orderedId).First();
            return result;
        }

        private void SerializeList()
        {
            string json = JsonConvert.SerializeObject(phones,Formatting.Indented);
            File.WriteAllText(pathToFile, json);
        }

        private void DeserializeList()
        {
            string json = ReadFileToString();
            phones = JsonConvert.DeserializeObject<List<PhoneModel>>(json) ?? new List<PhoneModel>();
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