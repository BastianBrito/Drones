using Dato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Newtonsoft.Json;
using System.Data;

namespace Negocio
{
    public class NegocioMantenedorDron
    {

        public string ListarDron(string JsonListarDron)
        {
            DatoMantenedorDron datoMantenedorDron = new DatoMantenedorDron();
            DataTable dataTable = new DataTable();
            Dron dron = new Dron();
            string mensaje = string.Empty;

            try
            {
                dron = JsonConvert.DeserializeObject<Dron>(JsonListarDron);
                //validar dron...
                try
                {
                    if (dron.NumeroSerie.ToString().Length >= 100)
                    {
                        mensaje = "Dron no puede tener número de serie mayor a 100 caracteres";
                    }
                }
                catch (Exception)
                {
                    mensaje = "Error al intentar validar número de serie de dron";
                }

                dataTable = datoMantenedorDron.ListarDron(dron);
                return DataTableToJson(dataTable);
            }
            catch (Exception e)
            {

                return e.Message.ToString();
            }
        }

        public string InsertarDron(string JsonInsertarDron)
        {
            DatoMantenedorDron datoMantenedorDron = new DatoMantenedorDron();
            Dron dron = new Dron();
            string mensaje = string.Empty;
            try
            {
                dron = JsonConvert.DeserializeObject<Dron>(JsonInsertarDron);

                //validar los datos....
                if (ValidarDron(dron, ref mensaje))
                {

                    mensaje = datoMantenedorDron.InsertarDron(dron);
                }

            }
            catch (Exception e)
            {
                mensaje = e.Message.ToString();
            }

            return ToJson(mensaje);
        }

        public string InsertarCargarDron(string JsonInsertarCargarDron)
        {
            DatoMantenedorDron datoMantenedorDron = new DatoMantenedorDron();
            Dron dron = new Dron();
            string mensaje = string.Empty;
            DataTable dataTable = new DataTable();
            try
            {
                dron = JsonConvert.DeserializeObject<Dron>(JsonInsertarCargarDron);

                //obtener datos del dron
                dataTable = datoMantenedorDron.ListarDron(dron);

                //Asignar el peso límite del dron seleccionado
                dron.PesoLimite = int.Parse(dataTable.Rows[0]["PesoLimite"].ToString());

                //validar datos de dron y medicamentos
                if (ValidarCargarDron(dron, ref mensaje))
                {


                    mensaje = datoMantenedorDron.InsertarCargarDron(dron);
                }


            }
            catch (Exception e)
            {
                mensaje = e.Message.ToString();
            }

            return ToJson(mensaje);
        }

        public string MostrarDronDisponible()
        {
            DatoMantenedorDron datoMantenedorDron = new DatoMantenedorDron();
            DataTable dataTable = new DataTable();

            try
            {

                dataTable = datoMantenedorDron.MostrarDronDisponible();
                return CEspeciales(DataTableToJson(dataTable));
            }
            catch (Exception e)
            {

                return ToJson(e.Message.ToString());
            }
        }


        #region validaciones

        private bool ValidarDron(Dron dron, ref string mensaje)
        {
            //valida nunero de serie
            try
            {
                if (dron.NumeroSerie.ToString().Length >= 100)
                {
                    mensaje = "Dron no puede tener número de serie mayor a 100 caracteres";
                    return false;
                }
            }
            catch (Exception)
            {
                mensaje = "Error al intentar validar número de serie de dron";
                return false;
            }

            //valida el modelo
            if (dron.Modelo == "peso ligero" || dron.Modelo == "peso medio" || dron.Modelo == "peso crucero" || dron.Modelo == "peso pesado")
            {

            }
            else
            {
                mensaje = "dron solo puede tener un modelo: peso ligero, peso medio, peso crucero, peso pesado";
                return false;
            }

            //valida el peso límite
            try
            {
                if (dron.PesoLimite > 500)
                {
                    mensaje = "dron no puede tener un peso límite mayor a 500 gr.";
                    return false;
                }
            }
            catch (Exception)
            {
                mensaje = "error al intentar leer el peso límite de dron";
                return false;
            }

            //valida el nivel de batería
            if (dron.CapacidadBateria < 0 || dron.CapacidadBateria > 100)
            {
                mensaje = "dron no puede tener una capacidad de batería fuera del rango 0% a 100%";
                return false;
            }

            //valida el nombre de estado
            if (dron.Estado == "INACTIVO" || dron.Estado == "CARGANDO" || dron.Estado == "CARGADO" || dron.Estado == "ENTREGANDO CARGA" || dron.Estado == "CARGA ENTREGADA" || dron.Estado == "REGRESANDO")
            {
                if (dron.CapacidadBateria < 25 && dron.Estado == "CARGANDO")
                {
                    mensaje = "dron no puede estar en estado CARGANDO si su batería está por debajo del 25%";
                    return false;
                }
            }
            else
            {
                mensaje = "dron solo puede tener estados: INACTIVO, CARGANDO, CARGADO, ENTREGANDO CARGA, CARGA ENTREGADA, REGRESANDO";
                return false;
            }


            return true;
        }

        private bool ValidarCargarDron(Dron dron, ref string mensaje)
        {
            int cantidadAcumulada = 0;//peso acumulado de medicamentos

            //valida el numero de serie del dron
            try
            {
                if (dron.NumeroSerie.ToString().Length >= 100)
                {
                    mensaje = "Dron no puede tener número de serie mayor a 100 caracteres";
                    return false;
                }
            }
            catch (Exception)
            {
                mensaje = "Error al intentar validar número de serie de dron";
                return false;
            }

            //valida el peso límite del dron
            try
            {
                if (dron.PesoLimite > 500)
                {
                    mensaje = "dron no puede tener un peso límite mayor a 500 gr.";
                    return false;
                }

            }
            catch (Exception)
            {
                mensaje = "error al intentar leer el peso límite de dron";
                return false;
            }

            //caracteres válidos en una lista de caracteres
            List<char> Validos = new List<char>() { 'A', 'B', 'C', 'D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                                                    'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','v','w','x','y','z',
                                                    '1','2','3','4','5','6','7','8','9','0',
                                                    '-','_' };
            List<char> Validos2 = new List<char>() { 'A', 'B', 'C', 'D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                                                    '1','2','3','4','5','6','7','8','9','0',
                                                    '_' };

            //recorre cada uno de los medicamentos asignados al dron
            foreach (Medicamento medicamentoItem in dron.Medicamentos)
            {
                //valida el nombre del medicamento y sus caracteres
                try
                {
                    foreach (char letraNombre in medicamentoItem.Nombre)
                    {
                        if (!Validos.Contains(letraNombre))
                        {
                            mensaje = "nombre del medicamento solo puede contener: letras, número, - y _";
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    mensaje = "error al leer el nombre del medicamento";
                    return false;
                }

                //valida el peso del medicamento
                try
                {
                    if (medicamentoItem.Peso < 0)
                    {
                        mensaje = "medicamente debe tener un peso meyor a 0 gr";
                        return false;
                    }
                    else
                    {
                        //acumula el peso de cada medicamento
                        cantidadAcumulada += medicamentoItem.Peso;

                        //valida el peso límite del dron vs el peso acumulado de medicamentos
                        if (cantidadAcumulada > dron.PesoLimite)
                        {
                            mensaje = "el peso acumulado de medicamentos no puede superar el peso límite del dron (" + dron.PesoLimite.ToString() + "gr.)";
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    mensaje = "error al leer el peso del medicamento " + medicamentoItem.Nombre;
                    return false;
                }

                //valida el código del medicamento y sus caracteres
                try
                {
                    foreach (char letraCodigo in medicamentoItem.Codigo)
                    {
                        if (!Validos2.Contains(letraCodigo))
                        {
                            mensaje = "código del medicamento solo puede contener: letras mayúsculas, números y _";
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    mensaje = "error al leer el nombre del medicamento";
                    return false;
                }
            }


            return true;
        }

        //Convierte el datatable en un json
        private string DataTableToJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer Jserializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rowsList = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            Jserializer.MaxJsonLength = Int32.MaxValue;

            if ((dt != null))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)

                        row.Add(col.ColumnName, dr[col]);

                    rowsList.Add(row);
                }
            }

            return Jserializer.Serialize(rowsList);
        }

        //convierte un string en JSON
        public string ToJson(string mensaje)
        {

            string json = string.Empty;

            json += "[{\"Mensaje\": " + mensaje + "\"}]";

            return CEspeciales(json);
        }

        //valida los caracteres especiales
        private string CEspeciales(string oString)
        {
            oString = oString.Replace(@"\""[", "[");
            oString = oString.Replace("\"[", "[");
            oString = oString.Replace("]\"", "]");
            oString = oString.Replace(@"]\""", "]");
            oString = oString.Replace(@"\u0026Aacute;", "Á");
            oString = oString.Replace(@"\u0026oacute;", "ó");
            oString = oString.Replace(@"\u0026iacute;", "í");
            oString = oString.Replace("]\",", "],");
            oString = oString.Replace(@"\", ""); // Debe ir al final de la función
            return oString;
        }

        #endregion
    }
}
