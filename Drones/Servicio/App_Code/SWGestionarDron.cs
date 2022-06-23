using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidad;
using Negocio;

/// <summary>
/// Descripción breve de SWGestionarDron
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class SWGestionarDron : System.Web.Services.WebService
{

    /// <summary>
    /// Lista los atributos de un dorn dado
    /// </summary>
    /// <param name="JsonListarDron"></param>
    /// <returns></returns>
    [WebMethod]
    public string ListarDron(string JsonListarDron)
    {
        NegocioMantenedorDron negocioMantenedorDron = new NegocioMantenedorDron();
        return negocioMantenedorDron.ListarDron(JsonListarDron);
    }

    /// <summary>
    /// Inserta un nuevo dron a la base de datos
    /// </summary>
    /// <param name="JsonInsertarDron"></param>
    /// <returns></returns>
    [WebMethod]
    public string InsertarDron(string JsonInsertarDron)
    {
        NegocioMantenedorDron negocioMantenedorDron = new NegocioMantenedorDron();
        return negocioMantenedorDron.InsertarDron(JsonInsertarDron);

    }

    /// <summary>
    /// Carga un dron con medicamentos
    /// </summary>
    /// <param name="JsonInsertarCargarDron"></param>
    /// <returns></returns>
    [WebMethod]
    public string InsertarCargarDron(string JsonInsertarCargarDron)
    {
        NegocioMantenedorDron negocioMantenedorDron = new NegocioMantenedorDron();
        return negocioMantenedorDron.InsertarCargarDron(JsonInsertarCargarDron);
    }

    /// <summary>
    /// Muestra un listado de drones disponibles
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string MostrarDronDisponible()
    {
        NegocioMantenedorDron negocioMantenedorDron = new NegocioMantenedorDron();
        return negocioMantenedorDron.MostrarDronDisponible();
    }
}
