using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using EmitEvent;
using TechTestApi.Models;
using TechTestApi.Controllers;
using System.Net;

namespace Retailer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RetailerController : BaseController
    {
        private readonly IRetailerRepository _retailRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IEmitEventLog _emitEventLog;

        public RetailerController(
            IRetailerRepository retailRepository,
            IGroupRepository groupRepository,
            IEmitEventLog emitEventLog)
        {
            _retailRepository = retailRepository;
            _groupRepository = groupRepository;
            _emitEventLog = emitEventLog;
        }

        
        [HttpGet]
        public async Task<IEnumerable<RetailerModel>> GetAllRetailers()
        {
            return await _retailRepository.GetAllRetailers();
        }

        [HttpGet("{retailerId}")]
        public async Task<RetailerModel> GetRetailer(string retailerId)
        {
            return await _retailRepository.GetRetailer(retailerId) ?? new RetailerModel();
        }


        /// <summary>
        ///     Create retailer
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateRetailer([FromForm] CreateUpdateRetailerModel model)
        {
            try
            {
                if (_groupRepository.GetGroup(model.GroupId) != null)
                {
                    var newRetailer = new RetailerModel()
                    {
                        Name = model.Name,
                        GroupId = model.GroupId,
                        CreateDate = DateTime.Now,
                        ModificationDate = DateTime.Now
                    };

                    var newRetailerId = await _retailRepository.AddRetailer(newRetailer);

                    _emitEventLog.EventPublish("created", $"{newRetailerId} at {newRetailer.CreateDate}");

                    return Ok(newRetailerId);
                }

                return Error((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }
                
        }

        /// <summary>
        ///     Edit retailer`s name or group
        /// </summary>
        [HttpPatch]
        public async Task<IActionResult> EditRetailer([FromForm] CreateUpdateRetailerModel model)
        {
            try
            {
                if (_groupRepository.GetGroup(model.GroupId) != null)
                {
                    await _retailRepository.UpdateRetailer(model.RetailerId, model.Name, model.GroupId);

                    _emitEventLog.EventPublish("edited", $"{model.RetailerId} : {model.Name} at {DateTime.Now}");

                    return Ok();
                }

                return Error();
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }             
        }

        /// <summary>
        ///     Delete retailer
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteRetailer([FromBody] string retailerId)
        {
            try
            {
                await _retailRepository.RemoveRetailer(retailerId);

                _emitEventLog.EventPublish("deleted", $"{retailerId} at {DateTime.Now}");

                return Ok();
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
