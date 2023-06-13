using AddressBook.BLL.Interfaces;
using AddressBook.BLL.Specifications;
using AddressBook.DAL.Entities;
using AddressBook.DAL.Entities.Identity;
using AddressBook.PL.DTOs;
using AddressBook.PL.Errors;
using AddressBook.PL.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.PL.Controllers
{
    public class AddressBookController : BaseApiController
    {
        private readonly IGenericRepo<AddressesBook> _addressBookRepo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;

        //private readonly IHttpContextAccessor _httpContextAccessor;

        public AddressBookController(IGenericRepo<AddressesBook> addressBookRepo, IMapper mapper , IWebHostEnvironment webHostEnvironment , UserManager<AppUser> userManager)
        {
            _addressBookRepo = addressBookRepo;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<AddressBookDto>>> GetaddressesBook()
        {
            var spec = new AddressBookWithJobAndDepartmentSpecifcation();

            var addressesBooks = await _addressBookRepo.GeAllWithSpecAsync(spec);

            if (addressesBooks == null) return NotFound(new ApiResponse(404));

            var dataToReturn = _mapper.Map<IReadOnlyList<AddressesBook>, IReadOnlyList<AddressBookDto>>(addressesBooks);

            return Ok(dataToReturn);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AddressBookDto>> GetAddressBookById(int id)
        {
            var spec = new AddressBookWithJobAndDepartmentSpecifcation(id);

            var addressBook = await _addressBookRepo.GetByIdWithSpecAsync(spec);

            if (addressBook == null) return NotFound(new ApiResponse(404));

            var dataToReturn = _mapper.Map<AddressesBook, AddressBookDto>(addressBook);

            return Ok(dataToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddressBook([FromForm] AddressBookDto addressBookDto)
        {
            if (CheckEmailExist(addressBookDto.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { "this email is already in use!" } });

            if (ModelState.IsValid)
            {
                addressBookDto.PhotoUrl = FileUploader.UploadFile(addressBookDto.Photo , _webHostEnvironment);

                var addressBook = _mapper.Map<AddressBookDto, AddressesBook>(addressBookDto);
                var user = new AppUser()
                {
                    Email = addressBookDto.Email,
                    UserName = addressBookDto.Email.Split("@")[0],
                    DisplayName = addressBookDto.FullName,
                    PhoneNumber = addressBookDto.MobileNumber,
                };
                var Userresult = await _userManager.CreateAsync(user, addressBookDto.Password);
                if (!Userresult.Succeeded) return BadRequest(new ApiResponse(400));

                var result = await _addressBookRepo.Add(addressBook);

                if (result <= 0 ) return null;

                return Ok(addressBook);
            }
            return BadRequest(new ApiResponse(400, "Invalid Model State "));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAddressBook([FromForm] AddressBookDto addressBookDto)
        {

            if (ModelState.IsValid)
            {
                int index = addressBookDto.PhotoUrl.IndexOf('/', "https://".Length);

                addressBookDto.PhotoUrl = addressBookDto.PhotoUrl.Substring(index + 1);

                var addressBook = _mapper.Map<AddressBookDto, AddressesBook>(addressBookDto);

                var result = await _addressBookRepo.Update(addressBook);

                if (result <= 0) return null;

                return Ok(addressBook);
            }

            return BadRequest(new ApiResponse(400, "Invalid Model State "));

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAddressBook([FromForm] AddressBookDto addressBookDto)
        {

            if (ModelState.IsValid)
            {
                int index = addressBookDto.PhotoUrl.IndexOf('/', "https://".Length);

                addressBookDto.PhotoUrl = addressBookDto.PhotoUrl.Substring(index + 1);

                FileUploader.DeleteFile(addressBookDto.PhotoUrl, _webHostEnvironment);

                var addressBook = _mapper.Map<AddressBookDto, AddressesBook>(addressBookDto);

                var result = await _addressBookRepo.Delete(addressBook);

                if (result <= 0) return null;

                return Ok(addressBook);
            }

            return BadRequest(new ApiResponse(400, "Invalid Model State "));

        }


        [HttpGet("emailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
