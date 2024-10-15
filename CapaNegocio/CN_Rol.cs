using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Rol
    {

        private CD_ROL objcd_rol = new CD_ROL();

        public List<Rol> Listar()
        {

            return objcd_rol.Listar();

        }

    }
}
