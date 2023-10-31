using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidelcoCapacitacionDiApi
{
    class ApplicationContext : IDisposable
    {
        //clase recomendada para API's
        public Company oCompany = new Company();

        public ApplicationContext()
        {
            conectar();
        }

        public void conectar()
        {
            //metodo de conexion.
        }
        public void desconectar()
        {
            //metodo de desconexion.
        }

        public void Dispose()
        {
            desconectar();
        }
    }
}
