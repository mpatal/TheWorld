using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<StopController> _logger;
        private ICoordService _coordService;

        public StopController(IWorldRepository repository, ILogger<StopController> logger, ICoordService coordService)
        {
            _repository = repository;
            _logger = logger;
            _coordService = coordService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = _repository.GetTripByName(tripName);

                if (results == null)
                {
                    return Json(null);
                }
                Response.StatusCode = (int)HttpStatusCode.OK;

                var orderedStops = results.Stops.OrderBy(s => s.Order);
                var stopViewModel = Mapper.Map<IEnumerable<StopViewModel>>(orderedStops);
                return Json(stopViewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get stop information for trip {tripName}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { MessageBody = ex.Message });
            }           
        }

        [HttpPost("")]
        public async Task<JsonResult> Post(string tripName, [FromBody]StopViewModel stopViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //map the view model
                    var stop = Mapper.Map<Stop>(stopViewModel);

                    //look up geo coordinates
                    var coordResult = await _coordService.Lookup(stop.Name);

                    if (!coordResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(coordResult.Message);
                    }

                    stop.Latitude = coordResult.Latitude;
                    stop.Longitude = coordResult.Longitude;

                    //save to the database

                    _repository.AddStop(tripName, stop);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        var savedStop = Mapper.Map<StopViewModel>(stop);
                        return Json(savedStop);
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save stop for tripname {tripName}", ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json("Failed to save stop");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed on new stop");
        }

    }
}
