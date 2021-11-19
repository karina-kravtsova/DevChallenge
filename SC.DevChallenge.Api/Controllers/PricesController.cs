using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SC.DevChallenge.Api.Config;
using SC.DevChallenge.Api.Dto;
using SC.DevChallenge.DataLayer;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricesController : ControllerBase
    {
        private readonly IDataReaderService _dataReaderService;
        private readonly IDataQueryService _dataQueryService;
        private readonly IOptions<CsvConfig> _csvConfig;
        private readonly IWebHostEnvironment _env;
        private readonly string csvPath;

        public PricesController(IDataReaderService dataReaderService, 
            IDataQueryService dataQueryService,
            IOptions<CsvConfig> csvConfig, 
            IWebHostEnvironment env)
        {
            _dataReaderService = dataReaderService;
            _dataQueryService = dataQueryService;
            _csvConfig = csvConfig;
            _env = env;
            
            csvPath =  $"{_env.ContentRootPath}/{_csvConfig.Value.FilePath}";
        }

        [HttpGet("average")]
        public async Task<IActionResult> GetAvg(string portfolio, string owner, string instrument, string date)
        {
            var dateTime = DateTime.ParseExact(date, _csvConfig.Value.DateFormat, CultureInfo.InvariantCulture);
            var data = _dataReaderService.GetAll(csvPath, _csvConfig.Value.DateFormat);

            var result = _dataQueryService.GetAvgData(data, portfolio, owner, instrument, dateTime);
            
            if(result == null)
            {
                return NotFound();
            }

            var response = new Result { date = result.TimeSlotDate, price = result.AvgPrice };
            return Ok(response);
        }
    }
}
