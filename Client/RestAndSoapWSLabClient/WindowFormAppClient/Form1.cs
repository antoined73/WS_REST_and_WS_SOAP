﻿using Newtonsoft.Json.Linq;
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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        WindowFormAppClient.ServiceReference1.Service1Client client = null;

        public Form1()
        {
            client = new WindowFormAppClient.ServiceReference1.Service1Client();
            InitializeComponent();
            contractsNamesComboBox.DataSource = client.GetContractsNames();
            button1.Click += async (o, e) =>
            {
                listBox1.DataSource = client.getStationsNames(contractsNamesComboBox.SelectedItem.ToString(), stationNameTB.Text);
            };
        }

        private void errorHandler()
        {

            label1.Text = "Une erreur est survenue !\n Ressayez avec au moins une ville.";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string theStationstr = client.getStationsInfos(contractsNamesComboBox.SelectedItem.ToString(), listBox1.SelectedItem.ToString());
            try
            {
                JObject theStation = JObject.Parse(theStationstr);
                label1.Text =
                "Nom :" + theStation.GetValue("name") + "\n"
                + "Adresse :" + theStation.GetValue("address") + "\n"
                 + "Vélos disponibles :" + theStation.GetValue("available_bikes") + "\n";
            }
            catch(Newtonsoft.Json.JsonReaderException)
            {
                label1.Text = "Problème api :" + theStationstr;
                return;
            }
           

            
        }
    }


}
