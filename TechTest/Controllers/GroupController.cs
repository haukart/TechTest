using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interface;
using EmitEvent;
using Microsoft.AspNetCore.Mvc;
using TechTestApi.Models;

namespace TechTestApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GroupController : BaseController
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IEmitEventLog _emitEventLog;

        public GroupController(
            IGroupRepository groupRepository,
            IEmitEventLog emitEventLog)
        {
            _groupRepository = groupRepository;
            _emitEventLog = emitEventLog;
        }


        [HttpGet]
        public async Task<IEnumerable<GroupModel>> GetAllGroups()
        {
            return await _groupRepository.GetAllGroups();
        }

        [HttpGet("{groupId}")]
        public async Task<GroupModel> GetGroup(string groupId)
        {
            return await _groupRepository.GetGroup(groupId) ?? new GroupModel();
        }


        /// <summary>
        ///     Create Group
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromForm] CreateUpdateGroupModel model)
        {
            try
            {
                var newGroup = new GroupModel()
                {
                    Name = model.Name,
                    CreateDate = DateTime.Now,
                    ModificationDate = DateTime.Now
                };

                var groupId = await _groupRepository.CreateGroup(newGroup);

                return Ok(groupId);
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }

        }

        /// <summary>
        ///     Edite Group`s name
        /// </summary>
        [HttpPatch]
        public async Task<IActionResult> EditGroup([FromForm]CreateUpdateGroupModel model)
        {
            try
            {
                await _groupRepository.UpdateGroup(model.GroupId, model.Name);

                return Ok();
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        ///     Delete Group
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteGroup([FromBody] string retailerId)
        {
            try
            {
                await _groupRepository.RemoveGroup(retailerId);

                return Ok();
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }
        }
    }
}