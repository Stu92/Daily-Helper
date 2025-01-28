using DailyHelpers.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Reporting.WebForms;
using System.Net;

namespace DailyHelpers.ReportingService
{
    /// <summary>
    /// Helper para configurar y manejar servicios de generación de reportes en un servidor de Reporting Services.
    /// </summary>
    public static class ReportingServiceHelper
    {
        private static string _environment;
        private static string _targetServerUrl;
        private static string _targetReportFolder;
        private static string _username;
        private static string _password;
        private static string _domain;

        /// <summary>
        /// Configura la clase con los valores necesarios para conectarse al servidor de Reporting Services.
        /// </summary>
        /// <param name="environment">Nombre del entorno (por ejemplo, Desarrollo, Producción).</param>
        /// <param name="targetServerUrl">URL del servidor de Reporting Services.</param>
        /// <param name="targetReportFolder">Carpeta en el servidor donde están los reportes.</param>
        /// <param name="username">Usuario para autenticarse en el servidor.</param>
        /// <param name="password">Contraseña para autenticarse en el servidor.</param>
        /// <param name="domain">Dominio para autenticarse en el servidor.</param>
        /// <exception cref="ArgumentNullException">Se lanza si algún parámetro es null.</exception>
        public static void Configure(string environment, string targetServerUrl, string targetReportFolder, string username, string password, string domain)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _targetServerUrl = targetServerUrl ?? throw new ArgumentNullException(nameof(targetServerUrl));
            _targetReportFolder = targetReportFolder ?? throw new ArgumentNullException(nameof(targetReportFolder));
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
            _domain = domain ?? throw new ArgumentNullException(nameof(domain));
        }

        /// <summary>
        /// Configura la clase utilizando un objeto de configuración (IConfiguration)(opcional).
        /// </summary>
        /// <param name="configuration">Instancia de IConfiguration que contiene las claves y valores de configuración.</param>
        public static void Configure(IConfiguration configuration)
        {
            Configure(
                environment: configuration["Environment"],
                targetServerUrl: configuration[$"TargetServerURL{configuration["Environment"]}"],
                targetReportFolder: configuration["TargetReportFolder"],
                username: configuration["userServer"],
                password: configuration["passServer"],
                domain: configuration["Domain"]
            );
        }


        /// <summary>
        /// Crea una nueva instancia de <see cref="ServerReport"/> con la configuración proporcionada.
        /// </summary>
        /// <param name="rptName">Nombre del reporte que se desea generar.</param>
        /// <returns>Una instancia configurada de <see cref="ServerReport"/>.</returns>
        /// <exception cref="InvalidOperationException">Se lanza si la clase no ha sido configurada previamente.</exception>
        public static ServerReport GetNewServerReportInstance(string rptName)
        {
            if (string.IsNullOrEmpty(_targetServerUrl) || string.IsNullOrEmpty(_targetReportFolder))
            {
                throw new InvalidOperationException("ReportingServiceHelper must be configured before use.");
            }

            return new ServerReport()
            {
                ReportServerUrl = new Uri(_targetServerUrl),
                ReportPath = $"{_targetReportFolder}{rptName}",
                ReportServerCredentials = new CustomReportServerCredentials(_username, _password, _domain)
            };
        }

        /// <summary>
        /// Renderiza un reporte en el formato especificado y genera un archivo como resultado.
        /// </summary>
        /// <param name="serverReport">Instancia de <see cref="ServerReport"/> configurada.</param>
        /// <param name="deviceInfo">Información del dispositivo utilizada para personalizar el reporte (XML).</param>
        /// <param name="reportType">Tipo de reporte a generar (por ejemplo, PDF, Excel).</param>
        /// <param name="mimeType">El tipo MIME del reporte generado.</param>
        /// <returns>Un objeto <see cref="ActionResult"/> con el reporte renderizado como archivo.</returns>
        public static ActionResult RenderReport(ServerReport serverReport, string deviceInfo, string reportType, string mimeType)
        {
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = serverReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings
            );

            return FileHelper.GenerateFile(renderedBytes, mimeType);
        }
    }

    /// <summary>
    /// Credenciales personalizadas para conectarse a un servidor de Reporting Services.
    /// </summary>
    public class CustomReportServerCredentials : IReportServerCredentials
    {
        private string _username;
        private string _password;
        private string _domain;

        /// <summary>
        /// Crea una nueva instancia de <see cref="CustomReportServerCredentials"/>.
        /// </summary>
        /// <param name="username">Usuario para autenticarse.</param>
        /// <param name="password">Contraseña para autenticarse.</param>
        /// <param name="domain">Dominio para autenticarse.</param>
        public CustomReportServerCredentials(string username, string password, string domain)
        {
            _username = username;
            _password = password;
            _domain = domain;
        }

        /// <summary>
        /// Obtiene la identidad de usuario para la suplantación (no implementada).
        /// </summary>
        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        /// <summary>
        /// Obtiene las credenciales de red para conectarse al servidor de reportes.
        /// </summary>
        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_username, _password, _domain); }
        }

        /// <summary>
        /// Configuración para autenticación por formularios (no implementada).
        /// </summary>
        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            return false;
        }
    }

    //    // Configuración manual
    //    ReportingServiceHelper.Configure(
    //        environment: "Development",
    //    targetServerUrl: "http://localhost/reportserver",
    //    targetReportFolder: "/Reports/",
    //    username: "admin",
    //    password: "password",
    //    domain: "MYDOMAIN"
    //);

    //// O usando IConfiguration
    //ReportingServiceHelper.Configure(configuration);

    //var report = ReportingServiceHelper.GetNewServerReportInstance("MyReport");
    //    var result = ReportingServiceHelper.RenderReport(report, "<DeviceInfo>", "PDF", out var mimeType);

}
