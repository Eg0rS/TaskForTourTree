using Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tour_3
{
    public partial class AddDrone : Form
    {
        public OpenType OpenType { get; set; }
        public DroneModel Model { get; set; }
        public Analytics Analytics { get; set; }

        public AddDrone()
        {
            InitializeComponent();
        }

        public AddDrone(OpenType type, Analytics analytics, DroneModel model = null)
        {
            OpenType = type;
            Model = model == null ? new DroneModel() : model;
            Analytics = analytics;
            InitializeComponent();
            switch (type)
            {
                case OpenType.create:
                    ActiveForm.Text = "Добавить беспилотник";
                    button1.Text = "Добавить";
                    break;
                case OpenType.update:
                    ActiveForm.Text = "Редактировать беспилотник";
                    button1.Text = "Редактировать";

                    textBox1.Text = model.Name;
                    textBox2.Text = model.Model;
                    textBox3.Text = model.Engine;
                    textBox4.Text = model.Weight.ToString();
                    textBox5.Text = model.MaxSpeed.ToString();
                    textBox6.Text = model.MaxFlightTime.ToString();
                    textBox7.Text = model.MaxFlightDistance.ToString();

                    break;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DroneModel model = Model;
            model.Name = textBox1.Text;
            model.Model = textBox2.Text;
            model.Engine = textBox3.Text;
            model.Weight = Convert.ToInt32(textBox4.Text);
            model.MaxSpeed = Convert.ToInt32(textBox5.Text);
            model.MaxFlightTime = Convert.ToInt32(textBox6.Text);
            model.MaxFlightDistance = Convert.ToInt32(textBox7.Text);
            switch (OpenType)
            {
                case OpenType.create:
                    Program.droneService.AddDrone(model);
                    break;
                case OpenType.update:
                    Program.droneService.UpdateDrone(model);
                    break;
            }
            Analytics?.Refreshgrid();

            this.Close();
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

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Валидация на ввод только цифр и клавиш удаления символов
            if (((e.KeyChar >= 47) && (e.KeyChar <= 57)) || (e.KeyChar == 8) || (e.KeyChar == 127))
                return;
            e.Handled = true;
        }
    }
}