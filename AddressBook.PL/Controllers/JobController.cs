using AddressBook.BLL.Interfaces;
using AddressBook.DAL.Entities;
using AddressBook.PL.DTOs;
using AddressBook.PL.Errors;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.PL.Controllers
{
    public class JobController : BaseApiController
    {
        private readonly IGenericRepo<Job> _jobRepo;
        private readonly IMapper _mapper;

        public JobController(IGenericRepo<Job> JobRepo, IMapper mapper)
        {
            _jobRepo = JobRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<JobDto>>> GetJobs()
        {
            var jobs = await _jobRepo.GeAllAsync();

            if (jobs == null) return NotFound(new ApiResponse(404));

            var dataToReturn = _mapper.Map<IReadOnlyList<Job>, IReadOnlyList<JobDto>>(jobs);

            return Ok(dataToReturn);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JobDto>> GetJobById(int id)
        {
            var job = await _jobRepo.GetByIdAsync(id);

            if (job == null) return NotFound(new ApiResponse(404));

            var dataToReturn = _mapper.Map<Job, JobDto>(job);

            return Ok(dataToReturn);
        }


        [HttpPost]
        public async Task<IActionResult> CreateJob( JobDto jobDto)
        {

            if (ModelState.IsValid)
            {
                var job = _mapper.Map<JobDto, Job>(jobDto);

                var result = await _jobRepo.Add(job);

                if (result <= 0) return null;

                return Ok(job);
            }

            return BadRequest(new ApiResponse(400, "Invalid Model State "));

        }

        [HttpPut]
        public async Task<IActionResult> UpdateJob( JobDto jobDto)
        {

            if (ModelState.IsValid)
            {
                var job = _mapper.Map<JobDto, Job>(jobDto);

                var result = await _jobRepo.Update(job);

                if (result <= 0) return null;

                return Ok(job);
            }

            return BadRequest(new ApiResponse(400, "Invalid Model State "));

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteJob(JobDto jobDto)
        {

            if (ModelState.IsValid)
            {
                var job = _mapper.Map<JobDto, Job>(jobDto);

                var result = await _jobRepo.Delete(job);

                if (result <= 0) return null;

                return Ok(job);
            }

            return BadRequest(new ApiResponse(400, "Invalid Model State "));

        }
    }
}
