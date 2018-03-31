using Newtonsoft.Json.Linq;
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
using WindowFormAppClient.ServiceReference1;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        WindowFormAppClient.ServiceReference1.Service1Client client = null;
        private string API_KEY = "AIzaSyDYRIvR4K-q_XbLEH8yZfNmq8ffuXdq_Bo";

        public Form1()
        {
            client = new WindowFormAppClient.ServiceReference1.Service1Client();
            InitializeComponent();
            contractsNamesComboBox.DataSource = client.getContractsNames();            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CompositeStation station = client.getStationsInfos(contractsNamesComboBox.SelectedItem.ToString(), listBox1.SelectedItem.ToString());
            if (station != null) { 
                pictureBox1.Load("https://maps.googleapis.com/maps/api/staticmap?&markers=size:mid|color:red|" + station.Position.Lat+","+station.Position.Lng+ "&zoom=13&size=600x300&maptype=roadmap&key=" + API_KEY);
                adressLabel.Text = station.Name + "\n" + station.Contract_Name + ", "+ station.Address;
                label6.Text = station.Available_Bikes+"";
                label11.Text = station.Status;
            }       
        }

        private void contractsNamesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.DataSource = client.getStationsNames(contractsNamesComboBox.SelectedItem.ToString(), stationNameTB.Text);
        }

        private void stationNameTB_TextChanged(object sender, EventArgs e)
        {
            listBox1.DataSource = client.getStationsNames(contractsNamesComboBox.SelectedItem.ToString(), stationNameTB.Text);
        }
    }


}
