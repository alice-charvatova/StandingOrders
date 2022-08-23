using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StandingOrders.API.Models.Dto;
using StandingOrders.API.Models.Entities;
using StandingOrders.API.Repositories;

namespace StandingOrders.API.Controllers
{
    [ApiController]
    [Route("api/code-table")]
    public class CodeTablesController : ControllerBase
    {
        private readonly IRepository<Interval> _intervalRepository;
        private readonly IRepository<ConstantSymbol> _constantSymbolRepository;
        private readonly IMapper _mapper;

        public CodeTablesController(
            IRepository<Interval> intervalRepository,
            IRepository<ConstantSymbol> constantSymbolRepository,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _intervalRepository = intervalRepository;
            _constantSymbolRepository = constantSymbolRepository;
        }

        /// <summary>
        /// Reads intervals from the database.
        /// </summary> 
        /// <response code="200">Success.</response> 
        [HttpGet("intervals")]
        [ProducesResponseType(typeof(CodeTableDto<int>), StatusCodes.Status200OK)]
        public IActionResult GetIntervals()
        {
            var interval = (from s in _intervalRepository.GetAll()
                            select new Interval {
                                IntervalId = s.IntervalId,
                                Value = s.Value
                            }).ToList();

            return Ok(_mapper.Map<List<CodeTableDto<int>>>(interval));
        }

        /// <summary>
        /// Reads constant symbols from the database.
        /// </summary> 
        /// <response code="200">Success.</response>
        [HttpGet("constant-symbols")]
        [ProducesResponseType(typeof(CodeTableDto<string>), StatusCodes.Status200OK)]
        public IActionResult GetConstantSymbols()
        {
            var constantSymbol = (from cs in _constantSymbolRepository.GetAll()
                                select new ConstantSymbol
                                {
                                    ConstantSymbolValue = cs.ConstantSymbolValue,
                                    Description = cs.Description
                                }).ToList();

            return Ok(_mapper.Map<List<CodeTableDto<string>>>(constantSymbol));
        }
    }
}
