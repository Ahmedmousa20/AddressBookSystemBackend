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
    public class DepartmentController : BaseApiController
    {
        private readonly IGenericRepo<Department> _departmentRepo;
        private readonly IMapper _mapper;

        public DepartmentController(IGenericRepo<Department> departmentRepo , IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetDepartments()
        {
            var departments = await _departmentRepo.GeAllAsync();

            if (departments == null) return NotFound(new ApiResponse(404));

            var dataToReturn = _mapper.Map<IReadOnlyList<Department>, IReadOnlyList<DepartmentDto>>(departments);

            return Ok(dataToReturn);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
        {
            var department = await _departmentRepo.GetByIdAsync(id);

            if (department == null) return NotFound(new ApiResponse(404));

            var dataToReturn = _mapper.Map<Department, DepartmentDto>(department);

            return Ok(dataToReturn);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentDto departmentDto)
        {

            if (ModelState.IsValid)
            {
                var Department = _mapper.Map<DepartmentDto, Department>(departmentDto);

                var result = await _departmentRepo.Add(Department);

                if (result <= 0 ) return null;

                return Ok(Department);
            }

            return BadRequest(new ApiResponse(400, "Invalid Model State "));

        }


        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(DepartmentDto departmentDto)
        {

            if (ModelState.IsValid)
            {
                var Department = _mapper.Map<DepartmentDto, Department>(departmentDto);

                var result = await _departmentRepo.Update(Department);

                if (result <= 0) return null;

                return Ok(Department);
            }

            return BadRequest(new ApiResponse(400, "Invalid Model State "));

        }



        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(DepartmentDto departmentDto)
        {

            if (ModelState.IsValid)
            {
                var Department = _mapper.Map<DepartmentDto, Department>(departmentDto);

                var result = await _departmentRepo.Delete(Department);

                if (result <= 0) return null;

                return Ok(Department);
            }

            return BadRequest(new ApiResponse(400, "Invalid Model State "));

        }
    }
}
