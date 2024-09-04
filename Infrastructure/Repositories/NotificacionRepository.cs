using Core.Entities;
using Core.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class NotificacionRepository : INotificacionRepository
    {
        private readonly string _connectionString;

        private readonly string _smtpServer = "smtp.office365.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "hayauca@outlook.es";
        private readonly string _smtpPassword = "Healayce_1976*";

        public NotificacionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task EnviarEmailAsync(NotificacionesClienteConfig cliente, string htmlContent)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPassword);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUser),
                        Subject = "Términos del Contrato",
                        Body = htmlContent,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(cliente.Email);

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            


            //using (var smtpClient = new SmtpClient("smtp.example.com"))
            //{
            //    var mailMessage = new MailMessage
            //    {
            //        From = new MailAddress("hayauca@gmail.com"),
            //        Subject = "Términos del Contrato",
            //        Body = htmlContent,
            //        IsBodyHtml = true
            //    };
            //    mailMessage.To.Add(cliente.Email);
            //    await smtpClient.SendMailAsync(mailMessage);
            //}
        }

        public async Task RegistrarAceptacionAsync(string cedula)
        {

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO ClienteAceptaciones (Cedula, FechaAceptacion) VALUES (:cedula, SYSDATE)";
                    command.Parameters.Add(new OracleParameter("cedula", cedula));
                    await command.ExecuteNonQueryAsync();
                }
            }

           // throw new NotImplementedException();
        }
    }
}
