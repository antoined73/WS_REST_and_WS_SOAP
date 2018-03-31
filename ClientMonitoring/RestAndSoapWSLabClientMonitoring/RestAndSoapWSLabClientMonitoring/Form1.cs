using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;

namespace RestAndSoapWSLabClientMonitoring
{
    public partial class Form1 : Form, IObserver<MetricsData>
    {
        MetricsData metrics = null;
        public Form1()
        {
            metrics = new MetricsData();
            metrics.Subscribe(this);
            InitializeComponent();
            this.timer1.Start();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            label1.Text = "error";
        }

        public void OnNext(MetricsData value)
        {
            label1.Text = metrics.GetNumberOfRequest().ToString();
            chart1.Series["Requests"].Points.AddY(metrics.GetNumberOfRequest());

            label3.Text = metrics.GetNumberOfDataInCache().ToString();
            chart5.Series["Requests"].Points.AddY(metrics.GetNumberOfDataInCache());

            string average = metrics.GetAverageResponseTime().ToString();
            if (average != "NaN")
            {
                average = average.Substring(0, 4);
                chart3.Series["Requests"].Points.AddY((metrics.GetAverageResponseTime()));
            }
            else
            {
                chart3.Series["Requests"].Points.AddY(0);
            }
            label5.Text = average + " ms";
            

            label7.Text = metrics.GetNumberOfJCDecauxRequest().ToString();
            chart6.Series["Requests"].Points.AddY(metrics.GetNumberOfJCDecauxRequest());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            metrics.RefreshData();
        }
    }
}
