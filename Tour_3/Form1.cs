using System;
using System.Linq;
using System.Windows.Forms;

namespace Tour_3
{
    public partial class Form1 : Form
    {
        private Analytics _analytics;

        public Analytics Analytics
        {
            get { return _analytics; }
            set
            {
                if (_analytics != null)
                {
                    _analytics.Close();
                }

                _analytics = value;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f = new AddDrone(OpenType.create, Analytics);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Analytics = new Analytics(OpenType.viewDrone, this);
            Analytics.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.droneService.DeleteDrone(Analytics.SelectedId);
            Analytics.Refreshgrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form f = new AddDrone(OpenType.update, Analytics, Program.droneService.GetAllDrones().FirstOrDefault(x => x.Id == Analytics.SelectedId));
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form f = new AddFlight(Analytics);
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Analytics = new Analytics(OpenType.viewFlight, this);
            Analytics.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Program.flightService.DeleteFlight(Analytics.SelectedId);
            Analytics.Refreshgrid();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form f = new SelectAnalytics(OpenType.analyticsProductivity, Analytics, this);
            f.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form f = new SelectAnalytics(OpenType.analyticsConsumption, Analytics, this);
            f.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form f = new SelectAnalytics(OpenType.analyticsCondition, Analytics, this);
            f.Show();
        }
    }
}