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

namespace tour3
{
    public partial class AddDrone : Form
    {
        public AddDrone()
        {
            InitializeComponent();
        }

        private void AddDrone_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DroneModel model = new DroneModel(){};
            model.Name = textBox1.Text;
            model.Model = textBox2.Text;
            model.Engine = textBox3.Text;
            model.Weight = Convert.ToInt32(textBox4.Text);
            model.MaxSpeed = Convert.ToInt32(textBox5.Text);
            model.MaxFlightTime = Convert.ToInt32(textBox6.Text);
            model.MaxFlightDistance = Convert.ToInt32(textBox7.Text);
            Program._droneService.AddDrone(model);
        }
    }
}
