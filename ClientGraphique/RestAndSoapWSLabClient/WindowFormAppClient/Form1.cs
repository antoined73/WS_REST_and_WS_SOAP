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

        public Form1()
        {
            client = new WindowFormAppClient.ServiceReference1.Service1Client();
            InitializeComponent();
            contractsNamesComboBox.DataSource = client.getContractsNames();
        }

        private void errorHandler()
        {

            label1.Text = "Une erreur est survenue !\n Ressayez avec au moins une ville.";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CompositeStation station = client.getStationsInfos(contractsNamesComboBox.SelectedItem.ToString(), listBox1.SelectedItem.ToString());
            if (station != null) { 
                label1.Text =
                "Nom :" + station.Name + "\n"
                + "Adresse :" + station.Address + "\n"
                 + "Vélos disponibles :" + station.Available_Bikes + "\n";
            }
            else {
                label1.Text = "Aucune information trouvée.";
                return;
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
