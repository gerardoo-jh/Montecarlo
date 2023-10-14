using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class AlgoritmoGenerador
    {
        private int[] rangoVidaUtil; // Rango de vida útil [mínimo, máximo]
        private double valorPromedioVidaUtil; // Valor promedio de vida útil

        private Random random;

        public AlgoritmoGenerador(int[] rangoVidaUtil, double valorPromedioVidaUtil)
        {
            this.rangoVidaUtil = rangoVidaUtil;
            this.valorPromedioVidaUtil = valorPromedioVidaUtil;
            this.random = new Random();
        }

        public double GenerarTiempoFallaPanel()
        {
            // Genera un tiempo de falla aleatorio basado en una distribución alrededor del valor promedio
            double desviacionEstandar = (rangoVidaUtil[1] - rangoVidaUtil[0]) / 6; // Asumiendo una distribución normal
            double tiempoFalla = random.NextDouble() * (rangoVidaUtil[1] - rangoVidaUtil[0]) + rangoVidaUtil[0];
            tiempoFalla = Math.Max(rangoVidaUtil[0], tiempoFalla); // Asegura que esté en el rango mínimo
            tiempoFalla = Math.Min(rangoVidaUtil[1], tiempoFalla); // Asegura que esté en el rango máximo

            // Aplica la desviación estándar al tiempo de falla
            tiempoFalla = tiempoFalla + (random.NextDouble() * 2 - 1) * desviacionEstandar;

            return tiempoFalla;
        }
    }

}
