using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SC.DevChallenge.Api.Config;
using SC.DevChallenge.Api.Dto;
using SC.DevChallenge.DataLayer;
using SC.DevChallenge.DataLayer.Db;
using System;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using SC.DevChallenge.DataLayer.Tables;
using SC.DevChallenge.DataLayer.Helpers;
using SC.DevChallenge.DataLayer.Statistics;
using System.Collections.Generic;

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
        private readonly ApplicationDbContext _context;
        private readonly string csvPath;

        public PricesController(IDataReaderService dataReaderService, 
            IDataQueryService dataQueryService,
            IOptions<CsvConfig> csvConfig, 
            IWebHostEnvironment env,
            ApplicationDbContext context)
        {
            _dataReaderService = dataReaderService;
            _dataQueryService = dataQueryService;
            _csvConfig = csvConfig;
            _env = env;
            _context = context;


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

        [HttpGet("benchmark")]
        public async Task<IActionResult> GetBenchmark(string portfolio, string date)
        {
            var dateTime = DateTime.ParseExact(date, _csvConfig.Value.DateFormat, CultureInfo.InvariantCulture);
            var timeSlot = TimeIntervalService.DateToTimeSlot(dateTime);
            
            var selection = _context.FinanceInstruments
                .Where(fi => fi.Portfolio == portfolio && fi.TimeSlot == timeSlot);

            if (!selection.Any())
            {
                return NotFound();
            }

            var benchmark = MathStats
                .RemoveOutliers(selection.Select(s => s.Price))
                .Average();

            var response = new Result { date = TimeIntervalService.TimeSlotToDate(timeSlot), price = benchmark };
            return Ok(response);
        }

        [HttpGet("aggregate")]
        public async Task<IActionResult> GetAggregated(string portfolio, string startdate, string enddate, int intervals)
        {
            var startDateTime = DateTime.ParseExact(startdate, _csvConfig.Value.DateFormat, CultureInfo.InvariantCulture);
            var startTimeSlot = TimeIntervalService.DateToTimeSlot(startDateTime);

            var endDateTime = DateTime.ParseExact(enddate, _csvConfig.Value.DateFormat, CultureInfo.InvariantCulture);
            var endTimeSlot = TimeIntervalService.DateToTimeSlot(endDateTime);

            // all timeslots
            var timeSlots = endTimeSlot - startTimeSlot;

            // timeslots in each group
            var evenGroups = timeSlots / intervals;
            
            // incomplete timeslots group
            var rest = timeSlots % intervals;


            var timeSlotGroups = Enumerable.Range(startTimeSlot, timeSlots)
                .Select((ts, i) => new { Index = i, Value = ts })
                .GroupBy(x => x.Index / evenGroups)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();

            var completeGroups = timeSlotGroups.Take(intervals).ToList();
            var incompleteGroupTimeSlots = timeSlotGroups.Skip(intervals)
                .SelectMany(ts => ts)
                .ToList();

            // share incomplere group between complete groups
            if (rest > 0)
            {
                for (int i = 0; i < incompleteGroupTimeSlots.Count; i++)
                {
                    var idx = i % completeGroups.Count;

                    completeGroups[i].Add(incompleteGroupTimeSlots[i]);
                }
            }

            var results = new List<Result>();

            foreach (var group in completeGroups)
            {
                var groupData = _context.FinanceInstruments.Where(f => group.Contains(f.TimeSlot));
                    
                var benchmark = MathStats
                            .RemoveOutliers(groupData.Select(s => s.Price))
                            .Average();

                var result = new Result
                {
                    date = TimeIntervalService.TimeSlotToDate(groupData.Max(d => d.TimeSlot)),
                    price = benchmark
                };

                results.Add(result);
            }

            return Ok(results);
        }





        [HttpGet("initdb")]
        public async Task InitDb()
        {
            var data = _dataReaderService.GetAll(csvPath, _csvConfig.Value.DateFormat);

            var tableData = data.Select(d => new FinanceInstrument
            {
                Id = Guid.NewGuid(),
                Portfolio = d.Portfolio,
                Owner = d.Owner,
                Instrument = d.Instrument,
                Date = d.Date,
                Price = d.Price,
                TimeSlot = d.TimeSlot
            });


            _context.FinanceInstruments.AddRange(tableData);
            _context.SaveChanges();
        }
    }
}
