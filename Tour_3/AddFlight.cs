using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models.Model;

namespace Tour_3
{
    public partial class AddFlight : Form
    {
        public Analytics Analytics { get; set; }

        public AddFlight()
        {
            InitializeComponent();
        }

        public AddFlight(Analytics analytics)
        {
            Analytics = analytics;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SortieModel model = new SortieModel();
            model.DroneId = Convert.ToInt32(textBox1.Text);
            model.FlightTime = Convert.ToInt32(textBox2.Text);
            model.Distance = Convert.ToInt32(textBox3.Text);
            model.Height = Convert.ToInt32(textBox4.Text);
            model.MissionCompleted = Convert.ToInt32(textBox5.Text);
            model.IsShotSown = checkBox1.Checked;
            model.ConsumptionFuel = Convert.ToInt32(textBox6.Text);
            Program.flightService.AddFlight(model);
            Analytics?.Refreshgrid();
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Валидация на ввод только цифр и клавиш удаления символов
            if (((e.KeyChar >= 47) && (e.KeyChar <= 57)) || (e.KeyChar == 8) || (e.KeyChar == 127))
                return;
            e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Валидация на ввод только цифр и клавиш удаления символов
            if (((e.KeyChar >= 47) && (e.KeyChar <= 57)) || (e.KeyChar == 8) || (e.KeyChar == 127))
                return;
            e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Валидация на ввод только цифр и клавиш удаления символов
            if (((e.KeyChar >= 47) && (e.KeyChar <= 57)) || (e.KeyChar == 8) || (e.KeyChar == 127))
                return;
            e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Валидация на ввод только цифр и клавиш удаления символов
            if (((e.KeyChar >= 47) && (e.KeyChar <= 57)) || (e.KeyChar == 8) || (e.KeyChar == 127))
                return;
            e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Валидация на ввод только цифр и клавиш удаления символов
            if (((e.KeyChar >= 47) && (e.KeyChar <= 57)) || (e.KeyChar == 8) || (e.KeyChar == 127))
                return;
            e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Валидация на ввод только цифр и клавиш удаления символов
            if (((e.KeyChar >= 47) && (e.KeyChar <= 57)) || (e.KeyChar == 8) || (e.KeyChar == 127))
                return;
            e.Handled = true;
        }
    }
}