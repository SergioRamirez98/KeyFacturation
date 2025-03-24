using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Servicios
{
    public static class CServ_MsjUsuario
    {
        public static void MensajesDeError(string mensaje)
        {
            MessageBox.Show(mensaje,
                                    "¡ERROR!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
        }
        public static bool Preguntar(string mensaje)
        {
            bool RespuestaUsuario = false;
            DialogResult ConsultarPrimero = MessageBox.Show(mensaje,
            "¡Atención!",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Asterisk,
            MessageBoxDefaultButton.Button2);
            if (ConsultarPrimero == DialogResult.Yes)
            {
                RespuestaUsuario = true;
            }
            return RespuestaUsuario;
        }




        public static int Preguntar2(string mensaje)
        {
            int RespuestaUsuario = 0;
            DialogResult ConsultarPrimero = MessageBox.Show(mensaje,
            "¡Atención!",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Asterisk,
            MessageBoxDefaultButton.Button2);
            if (ConsultarPrimero == DialogResult.Yes)
            {
                RespuestaUsuario = 1;
            }
            else if (ConsultarPrimero == DialogResult.No)
            {
                RespuestaUsuario = 2;
            }
            else RespuestaUsuario = 3;
            return RespuestaUsuario;
        }




        public static void Exito(string msj)
        {
            MessageBox.Show(msj,
                                    "Operación Exitosa",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
        }
    }
}

