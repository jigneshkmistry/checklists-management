﻿using ChecklistsManagement.DTO;
using ChecklistsManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChecklistsManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistsController : ControllerBase
    {

        #region PRIVATE MEMBERS

        private readonly IChecklistsService _checklistsService;
        private readonly ILogger<ChecklistsController> _logger;

        #endregion


        #region CONSTRUCTOR

        public ChecklistsController(ILogger<ChecklistsController> logger,
            IChecklistsService checklistsService)
        {
            _checklistsService = checklistsService ?? throw new ArgumentNullException(nameof(checklistsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion


        #region HTTPGET


        [HttpGet(Name = "GetChecklists")]
        public ActionResult<List<ChecklistsDTO>> GetChecklists()
        {
            _logger.LogInformation("GetChecklists called:");

            List<ChecklistsDTO> checklists = _checklistsService.GetChecklists();
            return Ok(checklists);
        }

        #endregion


        #region HTTPPOST


        #endregion


        #region HTTPUT


        #endregion


        #region HTTPPATCH


        #endregion


        #region HTTPDELETE


        #endregion


        #region PRIVATE METHODS


        #endregion


    }
}
