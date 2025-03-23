using ChecklistsManagement.DTO;
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

        #endregion


        #region CONSTRUCTOR

        public ChecklistsController(IChecklistsService checklistsService)
        {
            _checklistsService = checklistsService ?? throw new ArgumentNullException(nameof(checklistsService));
        }

        #endregion


        #region HTTPGET


        [HttpGet(Name = "GetChecklists")]
        public ActionResult<List<ChecklistsDTO>> GetChecklists()
        {
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
