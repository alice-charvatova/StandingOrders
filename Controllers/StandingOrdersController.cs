using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StandingOrders.API.Repositories;
using StandingOrders.API.Models;
using System.Collections.Generic;
using System.Linq;
using StandingOrders.API.Entities;
using StandingOrders.API.Filters;
using StandingOrders.API.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace StandingOrders.API.Controllers
{
    [ApiController]
    [Route("api/standingOrder")]
    public class StandingOrdersController : ControllerBase
    {
        private readonly IRepository<StandingOrder> _repository;
        private readonly IRepository<Interval> _intervalRepository;
        private readonly IMapper _mapper;

        public StandingOrdersController(
            IRepository<StandingOrder> repository,
            IRepository<Interval> intervalRepository,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _repository = repository;
            _intervalRepository = intervalRepository;
        }

        /// <summary>
        /// Reads all standing orders from the database.
        /// </summary> 
        /// <response code="200">Success.</response> 
        [HttpGet]
        [ProducesResponseType(typeof(StandingOrderBrowseDto), StatusCodes.Status200OK)]
        public IActionResult GetStandingOrders()
        {
            var standingOrderEntities = (from s in _repository.GetAll()
                                         join i in _intervalRepository.GetAll() on s.IntervalId equals i.IntervalId
                                         select new StandingOrder
                                         {
                                             StandingOrderId = s.StandingOrderId,
                                             IntervalId = s.IntervalId,
                                             IntervalSpecification = s.IntervalSpecification,
                                             AccountNumber = s.AccountNumber,
                                             Amount = s.Amount,
                                             Name = s.Name,
                                             ValidFrom = s.ValidFrom,
                                             Interval = i
                                         }).ToList();

            if (standingOrderEntities.Count == 0)
            {
                var message = "There are no existing standing orders in the database.";
                return Ok(message);
            }

            return Ok(_mapper.Map<List<StandingOrderBrowseDto>>(standingOrderEntities));
        }

        /// <summary>
        /// Reads a standing order from the database.
        /// </summary> 
        /// <response code="200">Success.</response> 
        /// <response code="404">If there is no item with such an ID.</response> 
        [HttpGet("{id}", Name = "GetStandingOrder")]
        [ProducesResponseType(typeof(StandingOrderDetailDto), StatusCodes.Status200OK)]
        public IActionResult GetStandingOrder(int id)
        {
            var standingOrderEntity = _repository.GetAll()
            .FirstOrDefault(s => s.StandingOrderId == id);

            if (standingOrderEntity == null)
            {
                var message = $"There is no standing order with id {id}.";
                return NotFound(message);
            }

            return Ok(
                _mapper.Map<StandingOrderDetailDto>(standingOrderEntity)
                );           
        }

        /// <summary>
        /// Creates a standing order.
        /// </summary> 
        /// <returns>A newly created standing order.</returns>
        /// <response code="201">If item succesfully created.</response>  
        /// <response code="401">Unauthorized access.</response>
        /// <response code="400">If there were any validation errors.</response>  
        [ServiceFilter(typeof(SecondFactorAuthorizationFilter))]
        [ProducesResponseType(typeof(StandingOrderDetailDto), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateStandingOrder([FromBody] StandingOrderDetailDto standingOrderDto)
        {
            var standingOrderEntity = _mapper.Map<StandingOrder>(standingOrderDto);

            _repository.Create(standingOrderEntity);

            _repository.Save();

            var createdStandingOrderDto = _mapper.Map<StandingOrderDetailDto>(standingOrderEntity);

            return CreatedAtRoute(
                "GetStandingOrder",
                new { id = createdStandingOrderDto.StandingOrderId },
                createdStandingOrderDto);            
        }

        /// <summary>
        /// Updates a standing order.
        /// </summary> 
        /// <response code="200">Success.</response> 
        /// <response code="400">If there were any validation errors.</response>  
        /// <response code="401">Unauthorized access.</response>
        /// <response code="404">If there is no item with such an ID.</response> 
        [ServiceFilter(typeof(SecondFactorAuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult UpdateStandingOrder(int id,
            [FromBody] Models.StandingOrderDetailDto standingOrderDto)
        {
            var standingOrderEntity = _repository.GetAll()
                .FirstOrDefault(s => s.StandingOrderId == id);

            if (standingOrderEntity == null)
            {
                var message = $"There is no standing order with id {id}.";
                return NotFound(message);
            }

            _mapper.Map(standingOrderDto, standingOrderEntity);
            _repository.Save();

            return Ok($"The standing order with id {id} was successfully changed.");

        }

        /// <summary>
        /// Deletes a standing order.
        /// </summary>
        /// <response code="200">Success.</response> 
        /// <response code="401">Unauthorized access.</response>
        /// <response code="404">If there is no item with such an ID.</response>  
        [ServiceFilter(typeof(SecondFactorAuthorizationFilter))]
        [HttpDelete("{id}")]
        public IActionResult DeleteStandingOrder(int id)
        {            
            var standingOrderEntity = _repository.GetAll()
                .FirstOrDefault(s => s.StandingOrderId == id);

            if (standingOrderEntity == null)
            {
                var message = $"There is no standing order with id {id}.";
                return NotFound(message);
            }

            _repository.Delete(standingOrderEntity);
            _repository.Save();

            return Ok($"The standing order with id {id} was successfully deleted.");
        }        
    }
}
