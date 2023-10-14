namespace MonteCarlo
{
    public partial class Form1 : Form
    {
        private AlgoritmoGenerador generador; // Instancia de la clase AlgoritmoGenerador

        public Form1()
        {
            InitializeComponent();
            generador = new AlgoritmoGenerador(new int[] { 1000, 5000 }, 3000);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("La caja de texto NO DEBE ESTAR VACIA");
                return;
            }

            int numExperimentos = Convert.ToInt32(textBox1.Text);

            if (numExperimentos <= 0)
            {
                MessageBox.Show("El numero debe ser MAYOR A CERO");
                return;
            }

            // Limpiar el DataGridView
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // Agregar la columna para los números de experimentos (ahora como primera columna)
            dataGridView1.Columns.Add("Experimento", "Experimento");

            // Agregar columnas para los paneles (ahora después de la columna "Experimento")
            for (int i = 0; i < 5; i++)
            {
                dataGridView1.Columns.Add("Panel" + (i + 1), "Panel " + (i + 1));
            }

            // Agregar una columna para mostrar un panel aleatorio (ahora después de las columnas de los paneles)
            dataGridView1.Columns.Add("PanelAleatorio", "Panel Aleatorio");

            // Realizar la simulación de Montecarlo
            double[,] tiemposFalla = new double[numExperimentos, 5];
            Random random = new Random(); // Nuevo objeto Random para la selección aleatoria

            for (int i = 0; i < numExperimentos; i++)
            {
                int panelAleatorio = random.Next(5); // Genera un índice aleatorio de panel (0 a 4)

                for (int j = 0; j < 5; j++)
                {
                    tiemposFalla[i, j] = generador.GenerarTiempoFallaPanel();
                }

                // Mostrar los resultados en el DataGridView
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);

                // Agrega el número de experimento (en la primera columna)
                row.Cells[0].Value = (i + 1).ToString();

                for (int j = 0; j < 5; j++)
                {
                    row.Cells[j + 1].Value = tiemposFalla[i, j].ToString("F2");
                }

                // Agrega el valor del panel aleatorio
                row.Cells[6].Value = tiemposFalla[i, panelAleatorio].ToString("F2");

                dataGridView1.Rows.Add(row);
            }

            // Calcular el promedio ponderado de los valores de la columna "PanelAleatorio"
            double promedioPonderado = 0;

            for (int i = 0; i < numExperimentos; i++)
            {
                double valor = Convert.ToDouble(dataGridView1.Rows[i].Cells["PanelAleatorio"].Value);
                promedioPonderado += valor;
            }

            promedioPonderado /= numExperimentos; // Calcular el promedio

            // Mostrar el promedio ponderado en una nueva fila
            DataGridViewRow promedioRow = new DataGridViewRow();
            promedioRow.CreateCells(dataGridView1);
            promedioRow.Cells[5].Value = "Promedio Ponderado";
            promedioRow.Cells[6].Value = promedioPonderado.ToString("F2");

            // Agregar la fila del promedio ponderado al DataGridView
            dataGridView1.Rows.Add(promedioRow);
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



    }
}