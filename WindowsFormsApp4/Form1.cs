using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;
using NAudio.Dsp;
using System.Diagnostics;
using System.Threading;

namespace WindowsFormsApp4
{
    public partial class juego : Form
    {
        int contadormovimiento = 5;
        bool volararriba = false;
        int distancia = 0;
        Random posicionrandom = new Random();

        public juego()
        {
            InitializeComponent();
            //teclado espacio
            this.KeyPreview = true;
            //cuando el juego comienze
            iniciarjuego();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Cuando el tubo se mueva aparecere los nuevos tubos.
            if (tuboabajo.Location.X > -140)
            {
                tuboabajo.Location = new Point((tuboabajo.Location.X) - 1, tuboabajo.Location.Y);
                tuboarriba.Location = new Point((tuboarriba.Location.X) - 1, tuboarriba.Location.Y);
            }
            else
            {
                //en la palabra distancia es donde apareceran los nuevos tubos con diferente lugar 
                distancia = posicionrandom.Next(-170, 118);
                tuboabajo.Location = new Point(400, 319 + distancia);
                tuboarriba.Location = new Point(400, -173 + distancia);
            }
        }
        public void iniciarjuego()
        {
            jugador.Location = new Point(19, 225);
            //Aqui le dices a la palabra distancia que siempre sea random 
            distancia = posicionrandom.Next(-160, 118);
            //al poner distancia a los tubos
            tuboarriba.Location = new Point(270, -173 - distancia);
            tuboabajo.Location = new Point(270, 319 - distancia);
            //el numero de puntaje siempre conenzara con 0
            puntios.Text = "0";

        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //Animacion de alas pero no se refleja 
            int cantidadmovimientos = 55;
            if (contadormovimiento <= cantidadmovimientos)
            {
                jugador.Image = Properties.Resources.tenorio;
                contadormovimiento++;
            }

            if ((contadormovimiento > cantidadmovimientos / 2) && (contadormovimiento <= cantidadmovimientos * 2))
            {
                jugador.Image = Properties.Resources.tenorio;
                contadormovimiento++;
            }

            contadormovimiento = (contadormovimiento > cantidadmovimientos * 2) ? 0 : contadormovimiento;

            int ly = jugador.Location.Y;
            int lx = jugador.Location.X;

            //que se va callendo al alumno
            if (volararriba)
            {
                ly = ly - 15;
                volararriba = false;
            }
            else
            {
                ly++;
            }

            jugador.Location = new Point(jugador.Location.X, ly);

            //cuando el alumno choca con un obstaculo se reiniciara el juego
            if ((jugador.Bounds.IntersectsWith(reprobado.Bounds))||(jugador.Bounds.IntersectsWith(tuboarriba.Bounds)) || (jugador.Bounds.IntersectsWith(tuboabajo.Bounds)))
            {
                iniciarjuego();
            }
            //el puntaje seguira al alumno
            puntios.Location = new Point(jugador.Location.X + 30, jugador.Location.Y - 25);
            puntios.Text = (tuboarriba.Location.X == jugador.Location.X) ? Convert.ToString
                ((Convert.ToInt32(puntios.Text) + 1)).ToString() : puntios.Text; 

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        
        }

        private void juego_Keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                volararriba = true;
            }
        }
    }
}
