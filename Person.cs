using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RIckAndMorty
{
    public partial class Person : Form
    {
        public Person(Result person)
        {
            InitializeComponent();
            string url = "https://rickandmortyapi.com/api/location/" + person.id;

            //открытие поток передачи данных по протоколу
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url); //запрос
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse(); //ответ

            RootLoc root = null;

            //открывает поток для чтения данных по GET ЗАПРОСУ
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string buf = streamReader.ReadToEnd();

                //преобразование строки в экземпляр об. класса root
                root = JsonConvert.DeserializeObject<RootLoc>(buf.ToString());
            }

            httpResponse.Close();

            pictureBox1.ImageLocation = person.image;

            _txtName.Text = root.name;
            _txtType.Text = root.type;
            _txtDimen.Text = root.dimension;
            _txtCreate.Text = root.created.ToString();

            textBox1.Text = person.name;
            textBox2.Text = person.status;
            textBox3.Text = person.species;
            textBox4.Text = person.gender;

        }
    }

    public class RootLoc
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string dimension { get; set; }
        public List<string> residents { get; set; }
        public string url { get; set; }
        public DateTime created { get; set; }
    }
}
