using AutoMapper;
using ChecklistsManagement.Domain;
using ChecklistsManagement.DTO;
using Microsoft.Extensions.Logging;

namespace ChecklistsManagement.Service
{
    public class ChecklistsService : IChecklistsService
    {

        #region PRIVATE MEMBERS

        private readonly IChecklistsRepository _checklistsRepository;
        private readonly ILogger<ChecklistsService> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region CONSTRUCTOR

        public ChecklistsService(ILogger<ChecklistsService> logger,
            IMapper mapper,
            IChecklistsRepository checklistsRepository) 
        {
            _checklistsRepository = checklistsRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region PUBLIC MEMBERS   

        public async Task<List<ChecklistsDTO>> GetChecklists()
        {
            _logger.LogInformation("ChecklistsService.GetChecklists called:");
          
            return _mapper.Map<List<ChecklistsDTO>>(await _checklistsRepository.GetChecklists());
        }

        #endregion

        #region VIRTUAL METHODS


        #endregion

    }
}
