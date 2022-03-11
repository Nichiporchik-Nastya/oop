using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;


namespace Bank2lab
{
    [Serializable]
    public class Owner
    {
        [XmlElement]
        public string surName { get; set; }
        [XmlElement]
        public string name { get; set; }
        [XmlElement]
        public string patronymic { get; set; }
        [XmlElement]
        public DateTime bDay { get; set; }
        [XmlElement]
        public string passportNumber { get; set; }
        [XmlElement]
        public Score score;
        public Owner(Score someScore) //объект создан вне класса, конструктор принимает ссылку на объект -- это агрегация
        {
            this.score = someScore;
        }
        public Owner() { }
        public override string ToString()
        {
            Owner owner = this;
            string strForRes ="";
            strForRes += "Владелец " + ":" + " " + owner.surName + " " + owner.name + " " + owner.patronymic;
            strForRes += "имеет счёт с номером" + " " + owner.score.scoreNumber + ", открытый " + owner.score.openingDate.ToShortDateString();
            strForRes += "тип вклада: " + " " + owner.score.typeOfDiposit + ", баланс: " + owner.score.balance;
            strForRes = "дополнительно:";
            strForRes = "дата рождения: " + owner.bDay.ToShortDateString() + ", паспорт:" + " " + owner.passportNumber;

            if (owner.score.banking)
            {
                strForRes += "подключен интернет-банкинг";
            }
            if (owner.score.cmc)
            {
                strForRes += "подключено смс оповещение";
            }
            return strForRes;
        }
    }

    [Serializable]
    public class Score
    {
        [XmlElement]
        public decimal scoreNumber { get; set; }
        [XmlElement]
        public DateTime openingDate { get; set; }
        [XmlElement]
        public string typeOfDiposit { get; set; }
        [XmlElement]
        public int balance { get; set; }
        [XmlElement]
        public bool banking { get; set; }
        [XmlElement]
        public bool cmc { get; set; }
        public Score() { }
    }

    [Serializable]
    [XmlRoot]
    public class Bank
    {
        [XmlElement("Owner")]
        public List<Owner> owners = new List<Owner>();

        [XmlIgnore]
        public Score someScore { get; set; }
        public Bank()
        {
            owners.Add(new Owner(someScore));
            someScore = new Score();
        }
    }

    public static class XmlSerializeWrapper
    {
        public static void Serialize<T>(T obj, string filename)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, obj);
            }
        }

        public static T Deserialize<T>(string filename)
        {
            T obj;
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                obj = (T)formatter.Deserialize(fs);
            }

            return obj;
        }
    }
}
