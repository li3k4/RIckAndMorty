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
    public partial class RickMorty : Form
    {
        private List<Result> result = null;
        public RickMorty()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //загрузга данных из сети в табл
            string url = "https://rickandmortyapi.com/api/character";

            //открытие поток передачи данных по протоколу
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url); //запрос
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse(); //ответ

            Root root = null;

            //открывает поток для чтения данных по GET ЗАПРОСУ
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string buf = streamReader.ReadToEnd();

                //преобразование строки в экземпляр об. класса root
                root = JsonConvert.DeserializeObject<Root>(buf.ToString());
            }

            httpResponse.Close();

            result = root.results;
            dataGridView1.Rows.Clear();
            foreach(Result res in result)
            {
                dataGridView1.Rows.Add(
                    res.id,
                    res.name,
                    res.status,
                    res.species,
                    res.gender,
                    res.image
                    );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Select rows");
                return;
            } 

            int curr = dataGridView1.SelectedRows[0].Index;

            int index = (-1);

            for(int i = 0; i < result.Count; i++)
            {
                if(result[i].id  == int.Parse(dataGridView1.Rows[curr].Cells[0].Value.ToString()))
                {
                index = i;
                break;
                }
            }

            if(index >= 0)
            {
                (new Person(result[index])).Show();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    public class Info
    {
        public int count { get; set; }
        public int pages { get; set; }
        public string next { get; set; }
        public object prev { get; set; }
    }

    public class Origin
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Result
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string species { get; set; }
        public string type { get; set; }
        public string gender { get; set; }
        public Origin origin { get; set; }
        public Location location { get; set; }
        public string image { get; set; }
        public List<string> episode { get; set; }
        public string url { get; set; }
        public DateTime created { get; set; }
    }
    public class Root
    {
        public Info info { get; set; }
        public List<Result> results { get; set; }
    }

   
}
