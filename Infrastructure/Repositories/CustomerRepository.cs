using Application.Exceptions;
using Core.Entities;
using Core.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Net;
using System.Xml;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task CreateCustomerAsync(Customer customer)
        {
            try
            {
                using var connection = new OracleConnection(_connectionString);
                using var command = new OracleCommand("SP_CREATE_CUSTOMER", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = customer.lc_tipocliente;
                command.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = customer.lc_tipocliente;

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw ;
            }
                              
        }
  
        public async Task<Customer> GetCustomerByIdAsync(string identificacion)
        {
            string emi = "";
            string solCred ="" ;
            string tipocliente ="" ;
            string puntajeScore ="" ;
            string perfilCliente ="" ;
            string dueDay ="";

            try
            {
                using var connection = new OracleConnection(_connectionString);
                using var command = new OracleCommand("PKG_ECOMMERCE.SP_VALIDATE_SCANANDGO", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                string infoInput = $"<datos><row><cedula>{identificacion}</cedula></row></datos>";

              
                command.Parameters.Add("infoInput", OracleDbType.Varchar2).Value = infoInput;
                command.Parameters["infoInput"].Direction = ParameterDirection.Input;
                command.Parameters.Add("resultado_Oracle", OracleDbType.RefCursor);
                command.Parameters["resultado_Oracle"].Direction = ParameterDirection.Output;
             
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                OracleDataAdapter da = new OracleDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    XmlDocument resultXml = new XmlDocument();
                    resultXml.LoadXml($"" + dt.Rows[0][0].ToString());

                    string codeResult = resultXml.GetElementsByTagName("tipo")[0].InnerText;

                    if (codeResult == "C")
                    {

                        throw new DataAccessException(resultXml.SelectSingleNode("/respuesta/mensaje").InnerText + " Cedula: " + identificacion);
                    }


                     emi = resultXml.SelectSingleNode("/respuesta/mensaje/codemi").InnerText;
                     solCred = resultXml.SelectSingleNode("/respuesta/mensaje/solcredito").InnerText;
                     tipocliente = resultXml.SelectSingleNode("/respuesta/mensaje/tipocliente").InnerText;
                     puntajeScore = resultXml.SelectSingleNode("/respuesta/mensaje/score").InnerText;
                     perfilCliente = resultXml.SelectSingleNode("/respuesta/mensaje/perfilcliente").InnerText;
                     dueDay = resultXml.SelectSingleNode("/respuesta/mensaje/diapago").InnerText;


                    if (String.IsNullOrEmpty(tipocliente))
                    {
                        throw new DataAccessException("No se encontro informacion tipo cliente." + "Cedula: " + identificacion, HttpStatusCode.NotFound);

                    }
                    if (String.IsNullOrEmpty(puntajeScore))
                    {
                        throw new DataAccessException("No se encontro informacion puntaje score." + "Cedula: " + identificacion, HttpStatusCode.NotFound);

                    }
                    if (String.IsNullOrEmpty(perfilCliente))
                    {
                        throw new DataAccessException("No se encontro informacion perfil cliente." + "Cedula: " + identificacion, HttpStatusCode.NotFound);

                    }
                    if (String.IsNullOrEmpty(dueDay))
                    {
                        throw new DataAccessException("No se encontro informacion dia de pago." + "Cedula: " + identificacion, HttpStatusCode.NotFound);

                    }
                 
                }
                else
                {

                    throw new DataAccessException("No se encontro informacion del Dia de pago." + "Cedula: " + identificacion, HttpStatusCode.NotFound);

                }


                    var customer = new Customer
                {
                    lc_emisor = emi,
                    lc_solcre = solCred,
                    lc_tipocliente = tipocliente,
                    lc_perfilcli= perfilCliente,
                        lc_score = puntajeScore,
                    lc_diapago = dueDay
                    };

                return customer;


            }
            catch (Exception ex)
            {
                throw;
                 
            }
                              
        }
    }
}
