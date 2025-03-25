using ChecklistsManagement.DTO;
using ChecklistsManagement.DTO.Checklists;
using ChecklistsManagement.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace ChecklistsManagement.API.Controllers
{
    /// <summary>
    /// Checklist Endpoint
    /// </summary>
    [Route("api/checklists")]
    [ApiController]
    public class ChecklistsController : ControllerBase
    {

        #region PRIVATE MEMBERS

        private readonly IChecklistsService _checklistsService;
        private readonly ILogger<ChecklistsController> _logger;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// ChecklistsController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="checklistsService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ChecklistsController(ILogger<ChecklistsController> logger,
            IChecklistsService checklistsService)
        {
            _checklistsService = checklistsService ?? throw new ArgumentNullException(nameof(checklistsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion


        #region HTTPGET

        /// <summary>
        /// List all checklists with pagination.
        /// </summary>
        /// <param name="page">Page No</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>List of checklist</returns>
        [HttpGet(Name = "GetChecklists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ChecklistDetailsDTO>>> GetChecklists([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("GetChecklists called with Page No : {page} and Page Size : {pageSize}", page, pageSize);

            return Ok(await _checklistsService.GetPagedDataAsync<ChecklistDetailsDTO>(page, pageSize));
        }

        /// <summary>
        /// Get a checklist by ID.
        /// </summary>
        /// <param name="id">Identifier for checklist</param>
        /// <returns>Checklist</returns>
        [HttpGet]
        [Route("{id}", Name = "GetChecklistById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ChecklistDetailsDTO>> GetChecklistById(string id)
        {
            _logger.LogInformation("GetChecklistById called with id : {id}", id);

            return Ok(await _checklistsService.GetByIdAsync<ChecklistDetailsDTO>(new ObjectId(id)));
        }

        #endregion


        #region HTTPPOST

        /// <summary>
        /// Create a new checklist.
        /// </summary>
        /// <param name="checklistsDTO">Checklist details</param>
        /// <returns>Checklist details</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ChecklistsDTO>> CreateChecklist(ChecklistsForCreationDTO checklistsDTO)
        {
            _logger.LogInformation("CreateChecklist called with : {checklistsDTO}", JsonConvert.SerializeObject(checklistsDTO));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ChecklistsDTO checkList = await _checklistsService.AddAsync<ChecklistsDTO, ChecklistsForCreationDTO>(checklistsDTO);

            return CreatedAtAction("GetChecklistById", new { id = "result" }, checkList);
        }

        /// <summary>
        /// Add an item to a checklist.
        /// </summary>
        /// <param name="id">Checklist Id</param>
        /// <param name="item">Checklist Item Model</param>
        /// <returns>Checklist Item</returns>
        [HttpPost("{id}/items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddChecklistItem(string id, [FromBody] ChecklistItemCreationDTO item)
        {
            _logger.LogInformation("AddChecklistItem called with : {item}", JsonConvert.SerializeObject(item));

            ChecklistItemDTO checklistItem = await _checklistsService.AddChecklistItem(new ObjectId(id), item);

            return CreatedAtAction("GetChecklistById", new { id = id }, checklistItem);
        }

        #endregion


        #region HTTPUT

        /// <summary>
        /// Update a checklist.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="checklistForUpdateDTO">Checklist Update Model</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}", Name = "UpdateChecklist")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateChecklist(string id, ChecklistsForUpdateDTO checklistForUpdateDTO)
        {
            _logger.LogInformation("UpdateChecklist called with {checklistForUpdateDTO}:", JsonConvert.SerializeObject(checklistForUpdateDTO));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _checklistsService.UpdateAsync(new ObjectId(id), checklistForUpdateDTO);

            return NoContent();
        }


        /// <summary>
        /// Update checklist item
        /// </summary>
        /// <param name="id">ChecklistId</param>
        /// <param name="itemId">Item Id</param>
        /// <param name="checklistItemForUpdateDTO">ChecklistItemUpdate Model</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/items/{itemId}", Name = "UpdateChecklitem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateChecklitem(string id, string itemId, ChecklistItemForUpdateDTO checklistItemForUpdateDTO)
        {
            _logger.LogInformation("UpdateChecklitem called with {checklistItemForUpdateDTO}:", JsonConvert.SerializeObject(checklistItemForUpdateDTO));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _checklistsService.UpdateChecklitem(new ObjectId(id), new ObjectId(itemId), checklistItemForUpdateDTO);

            return NoContent();
        }

        #endregion


        #region HTTPATCH

        /// <summary>
        /// Publish/Unpublish a checklist.
        /// </summary>
        /// <param name="id">Checklist ID</param>
        /// <param name="publish">publish/unpublish flag</param>
        /// <returns></returns>
        [HttpPatch("{id}/publish")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PublishChecklist(string id, [FromQuery] bool publish)
        {
            _logger.LogInformation("PublishChecklist called with Id : {Id} and publish : {publish}:", id, publish);

            await _checklistsService.PublishChecklistAsync(new ObjectId(id), publish);

            return NoContent();
        }

        #endregion


        #region HTTPDELETE

        /// <summary>
        ///  Delete a checklist.
        /// </summary>
        /// <param name="id">Checklist Id</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "DeleteChecklist")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteChecklist(string id)
        {
            _logger.LogInformation("Deleting Checklists with ID {Id}.", id);

            await _checklistsService.DeleteAsync(new ObjectId(id));

            return NoContent();
        }

        /// <summary>
        /// Delete an item from a checklist.
        /// </summary>
        /// <param name="id">Checklist Id</param>
        /// <param name="itemId">Item Id</param>
        /// <returns></returns>
        [HttpDelete("{id}/items/{itemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteChecklistItem(string id, string itemId)
        {
            _logger.LogInformation("DeleteChecklistItem called with ChecklistId {id} and ItemId {itemId}", id, itemId);

            await _checklistsService.DeleteChecklistItemAsync(new ObjectId(id),new ObjectId(itemId));
            
            return NoContent();
        }

        #endregion

    }
}
