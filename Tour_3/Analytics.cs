using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tour_3.Service;

namespace Tour_3
{
    public partial class Analytics : Form
    {
        public OpenType OpenType { get; set; }
        public Form1 MainForm { get; set; }

        public int SelectedId { get; set; }

        public SelectedAnalytics SelectedAnalytics { get; set; }

        public Analytics()
        {
            InitializeComponent();
        }

        public Analytics(OpenType openType, Form1 mainForm, SelectedAnalytics selectAnalytics = null)
        {
            SelectedAnalytics = selectAnalytics;
            MainForm = mainForm;
            OpenType = openType;
            InitializeComponent();
            Refreshgrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OpenType == OpenType.viewDrone)
            {
                SelectedId = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                MainForm.button3.Enabled = true;
                MainForm.button4.Enabled = true;
            }

            if (OpenType == OpenType.viewFlight)
            {
                SelectedId = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                MainForm.button7.Enabled = true;
            }
        }

        private void Analytics_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.button3.Enabled = false;
            MainForm.button4.Enabled = false;
            MainForm.button7.Enabled = false;
        }

        public void Refreshgrid()
        {
            switch (OpenType)
            {
                case OpenType.viewDrone:
                    this.Text = "Данные по беспилотникам";
                    dataGridView1.DataSource = Program.droneService.GetAllDrones();
                    break;
                case OpenType.viewFlight:
                    this.Text = "Данные по полетам";
                    dataGridView1.DataSource = Program.flightService.GetAllFlights();
                    break;
                case OpenType.analyticsProductivity:
                    this.Text = "Аналитика по продуктивности";
                    var productivity = Program.analyticsService.GetDronesWithProductivity(SelectedAnalytics);
                    dataGridView1.DataSource = productivity;
                    break;
                case OpenType.analyticsConsumption:
                    this.Text = "Аналитика по расходу топлива";
                    dataGridView1.DataSource = Program.analyticsService.GetDronesWithConsumption(SelectedAnalytics);
                    break;
                case OpenType.analyticsCondition:
                    this.Text = "Аналитика по состоянию";
                    dataGridView1.DataSource = Program.analyticsService.GetDronesWithCondition(SelectedAnalytics);
                    break;
            }
        }
    }
}