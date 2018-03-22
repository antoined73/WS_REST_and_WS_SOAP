using LiveCharts;
using LiveCharts.Wpf;
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

            metrics.StartConstantRefreshing();

            // Fait buguer le rafraichissement auto du chiffre
            /**
            requestsDoneChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Nombre de requêtes",
                    Values = new ChartValues<int> { 0 }
                }
            };

            requestsDoneChart.AxisX.Add(new Axis
            {
                Title = "Temps (min)",
                Labels = new[] {"0", "1", "2", "3", "4", "5"}
            });

            requestsDoneChart.AxisY.Add(new Axis
            {
                Title = "Requêtes",
                LabelFormatter = value => value.ToString("C")
            });

            requestsDoneChart.LegendLocation = LegendLocation.Right;**/

            

        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            label1.Text = "error";
        }

        public void OnNext(MetricsData met)
        {
            if (label1.InvokeRequired)
            {
                label1.Invoke(new MethodInvoker(delegate { label1.Text = met.GetNumberOfRequest().ToString(); }));
            }

            //requestsDoneChart.Series[0].Values.Add(met.GetNumberOfRequest());

        }
    }
}
