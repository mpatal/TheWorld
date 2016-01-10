﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<StopController> _logger;

        public StopController(IWorldRepository repository, ILogger<StopController> logger)
        {
            _repository = repository;
            _logger = logger;
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
    }
}
