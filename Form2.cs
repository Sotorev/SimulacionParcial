using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionParcial
{
    public partial class Form2 : Form
    {
        List<Datos> datos = new List<Datos>();
        List<Departamento> departamentos = new List<Departamento>();
        List<Temperatura> temperaturas = new List<Temperatura>();
        public Form2()
        {
            InitializeComponent();
        }
        void LeerDatos()
        {
            FileStream fs = new FileStream("Departamentos.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            while (sr.Peek() != -1)
            {
                Departamento departamento = new Departamento()
                {
                    id = Convert.ToInt32(sr.ReadLine()),
                    nombre = sr.ReadLine()
                };
                departamentos.Add(departamento);
            }
            sr.Close();
            fs.Close();
            fs = new FileStream("Temperaturas.txt", FileMode.Open, FileAccess.Read);
            sr = new StreamReader(fs);
            while (sr.Peek() != -1)
            {
                Temperatura temperatura = new Temperatura()
                {
                    id = Convert.ToInt32(sr.ReadLine()),
                    magnitud = Convert.ToDouble(sr.ReadLine()),
                    fecha = Convert.ToDateTime(sr.ReadLine())
                };
                temperaturas.Add(temperatura);
            }
            sr.Close();
            fs.Close();
        }
        private void ActualizarDatos()
        {
            foreach (Temperatura temp in temperaturas)
            {
                Datos dato = new Datos();
                dato.nombre = departamentos.Find(dep => dep.id == temp.id).nombre;
                dato.magnitud = temp.magnitud;
                datos.Add(dato);
            }
            
        }
        void CargarDataGrid(List<Datos> datos)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = datos;
            dataGridView1.Refresh();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            LeerDatos();
            ActualizarDatos();
            CargarDataGrid(datos);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarDataGrid(datos.OrderByDescending(d => d.magnitud).ToList());
        }
    }
}
