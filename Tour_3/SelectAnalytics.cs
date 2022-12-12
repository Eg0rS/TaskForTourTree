using System;
using System.Linq;
using System.Windows.Forms;
using Tour_3.Service;

namespace Tour_3
{
    public partial class SelectAnalytics : Form
    {
        public OpenType OpenType { get; set; }
        public Form1 MainForm { get; set; }
        public Analytics Analytics { get; set; }

        public SelectAnalytics()
        {
            InitializeComponent();
        }

        public SelectAnalytics(OpenType openType, Analytics analytics, Form1 form1)
        {
            OpenType = openType;
            Analytics = analytics;
            MainForm = form1;
            InitializeComponent();
            switch (openType)
            {
                case OpenType.analyticsProductivity:
                    this.Text = "Аналитика по продуктивности";
                    break;
                case OpenType.analyticsConsumption:
                    this.Text = "Аналитика по расходу топлива";
                    break;
                case OpenType.analyticsCondition:
                    this.Text = "Аналитика по состоянию беспилотников";
                    break;
            }

            comboBox2.Items.AddRange(Program.droneService.GetAllDrones().Select(d => d.Model).ToArray());
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var analyitics = new SelectedAnalytics()
            {
                acsending = Convert.ToBoolean(comboBox1.SelectedIndex),
                model = comboBox2.SelectedItem.ToString(),
            };
            MainForm.Analytics = new Analytics(OpenType, MainForm, analyitics);
            MainForm.Analytics.Show();
            MainForm.Analytics.Refreshgrid();
            this.Close();
        }
    }
}