using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Telerik.Reporting.Processing;
using Telerik.Reporting;
using System.Collections;
using Newtonsoft.Json;
using System.Text.Json;
//using Telerik.Reporting.JsonSerialization;
using Telerik.Reporting.XmlSerialization;
using System.Text;
//using Telerik.Reporting.Json;

//using Telerik.Reporting.DataSource.Json;



namespace API_01.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly MemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions());

        private readonly ILogger<WeatherForecastController> _logger;
        private object reportPath;
        private string jsonData;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        //======================================== V3.1=====================================================

       /* [HttpGet]
        [Route("api/reports/print")]
        public async Task<IActionResult> PrintReport()
        {
            var reportProcessor = new ReportProcessor();
            var reportPackager = new ReportPackager();
            var instanceReportSource = new InstanceReportSource();

            // Load JSON data from file
            string jsonData = System.IO.File.ReadAllText(@"C:\Users\Administrator\Desktop\data\20 records.json");

            var reportUri = @"C:\Users\Administrator\Desktop\template 02\Telerik sub totel.trdp";

            // Load report from file and update data source
            using (var sourceStream = System.IO.File.OpenRead(reportUri))
            {
                var report = (Telerik.Reporting.Report)reportPackager.UnpackageDocument(sourceStream);
                var jsonDataSource = new JsonDataSource();
                jsonDataSource.Source = jsonData;
                report.DataSource = jsonDataSource;
                instanceReportSource.ReportDocument = report;
            }

            // Render report to PDF
            var deviceInfo = new Hashtable();
            var renderingResult = await Task.Run(() => reportProcessor.RenderReport("PDF", instanceReportSource, deviceInfo));

            // Save PDF file to disk
            var fileName = reportUri + ".pdf";
            var folderPath = @"C:\Users\Administrator\Desktop\PDF files\Report PDF files";
            var filePath = Path.Combine(folderPath, fileName);

            int i = 1;
            while (System.IO.File.Exists(filePath))
            {
                fileName = Path.GetFileNameWithoutExtension(reportUri) + "_" + i + ".pdf";
                filePath = Path.Combine(folderPath, fileName);
                i++;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.WriteAsync(renderingResult.DocumentBytes, 0, renderingResult.DocumentBytes.Length);
                await fileStream.FlushAsync();
            }

            // Return PDF file to client
            var file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return File(file, "application/pdf", fileName);
        }
*/




        //======================================== V3.2=====================================================

        [HttpGet]
        [Route("api/reports/print/{jsonFileName}")]
        public async Task<IActionResult> PrintReport3(string jsonFileName)
        {
            var reportProcessor = new ReportProcessor();
            var reportPackager = new ReportPackager();
            var instanceReportSource = new InstanceReportSource();

            // Load JSON data from file
            var jsonFilePath = Path.Combine(@"C:\Users\Administrator\Desktop\template 02\Subtotel data", jsonFileName + ".json");
            if (!System.IO.File.Exists(jsonFilePath))
            {
                return NotFound();
            }

            string jsonData = System.IO.File.ReadAllText(jsonFilePath);

            var reportUri = @"C:\Users\Administrator\Desktop\template 02\Telerik sub totel.trdp";

            // Load report from file and update data source
            using (var sourceStream = System.IO.File.OpenRead(reportUri))
            {
                var report = (Telerik.Reporting.Report)reportPackager.UnpackageDocument(sourceStream);

                var jsonDataSource = new JsonDataSource();
                jsonDataSource.Source = jsonData;

                report.DataSource = jsonDataSource;
                instanceReportSource.ReportDocument = report;
            }

            // Render report to PDF
            var deviceInfo = new Hashtable();
            var renderingResult = await Task.Run(() => reportProcessor.RenderReport("PDF", instanceReportSource, deviceInfo));

          
            // Save PDF file to disk
            var fileName = reportUri + ".pdf";
            var folderPath = @"C:\Users\Administrator\Desktop\PDF files\Report PDF files";
            var filePath = Path.Combine(folderPath, fileName);

            int i = 1;
            while (System.IO.File.Exists(filePath))
            {
                fileName = Path.GetFileNameWithoutExtension(reportUri) + "_" + i + ".pdf";
                filePath = Path.Combine(folderPath, fileName);
                i++;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.WriteAsync(renderingResult.DocumentBytes, 0, renderingResult.DocumentBytes.Length);
                await fileStream.FlushAsync();
            }

            // Return PDF file to client
            var file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return File(file, "application/pdf", fileName);
        }
    }

}




















