﻿using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuario
    {

        private CD_Usuario objcd_usuario = new CD_Usuario();

        public List<Usuario> Listar()
        {
            
            return objcd_usuario.Listar();

        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "El campo documento es obligatorio\n";

            }

            if (obj.NombreCompleto == "" )
            {
                Mensaje += "El campo Nombre Completo es obligatorio\n";
                
            }

            if (obj.Clave == "")
            {
                Mensaje += "El campo clave es obligatorio\n";

            }

            if ( Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_usuario.Registrar(obj, out Mensaje);
            }

            

        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "El campo documento es obligatorio\n";

            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "El campo Nombre Completo es obligatorio\n";

            }

            if (obj.Clave == "")
            {
                Mensaje += "El campo clave es obligatorio\n";

            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_usuario.Editar(obj, out Mensaje);
            }

            

        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {

            return objcd_usuario.Eliminar(obj, out Mensaje);

        }
    }
}
