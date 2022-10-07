using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Models;
using StudentsManagement.Models.Dtos;
using StudentsManagement.Repository.IRepository;
using System.Collections.Generic;

namespace StudentsManagement.Controllers
{
    [Route("api/v{version:apiVersion}/studentsAddress")]
    [ApiVersion("3.0")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class StudentsAddressController : ControllerBase
    {
        private readonly IStudentsAddressRepository _stuRepo;
        private readonly IMapper _mapper;

        public StudentsAddressController(IStudentsAddressRepository stuRepo, IMapper mapper)
        {
            _stuRepo = stuRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of students address
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStudentsAddress()
        {
            var objList = _stuRepo.GetStudentsAddress();


            var objDto = new List<StudentsAddressDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<StudentsAddressDto>(obj));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Get individual student address
        /// </summary>
        /// <param name="StudentAddressId"></param>
        /// <returns></returns>
        [HttpGet("{StudentAddressId:int}", Name = "GetStudentAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStudentsAddress(int StudentAddressId)
        {
            var obj = _stuRepo.GetStudentAddress(StudentAddressId);

            if (obj == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StudentsAddressDto>(obj));
        }

        /// <summary>
        /// Create student address
        /// </summary>
        /// <param name="createAddressDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateStudent([FromBody] CreateStudentsAddressDto createAddressDto)
        {
            if (createAddressDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_stuRepo.StudentAddressExists(createAddressDto.Address))
            {
                ModelState.AddModelError("", "Student address already exists");
                return StatusCode(404, ModelState);
            }

            var createStudentAddress = _mapper.Map<StudentsAddress>(createAddressDto);

            if (!_stuRepo.CreateStudentAddress(createStudentAddress))
            {
                ModelState.AddModelError("", "Something went wrong when create student address please try again later...");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetStudentAddress", new { StudentAddressId = createStudentAddress.Id }, createStudentAddress);

        }

        /// <summary>
        /// Update Student address by id
        /// </summary>
        /// <param name="StudentAddressId"></param>
        /// <param name="updateAddressDto"></param>
        /// <returns></returns>
        [HttpPatch("{StudentAddressId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateStudent(int StudentAddressId, [FromBody] UpdateStudentsAddressDto updateAddressDto)
        {

            if (updateAddressDto == null || StudentAddressId != updateAddressDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_stuRepo.StudentAddressExists(updateAddressDto.Id))
            {
                ModelState.AddModelError("", "Student address not found please enter correct Id...");
                return StatusCode(404, ModelState);
            }

            var studentAddressObj = _mapper.Map<StudentsAddress>(updateAddressDto);
            if (!_stuRepo.UpdateStudentAddress(studentAddressObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating student address please try again later... {studentAddressObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete student details by id
        /// </summary>
        /// <param name="StudentAddressId"></param>
        /// <returns></returns>
        [HttpDelete("{StudentAddressId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteStudent(int StudentAddressId)
        {

            if (!_stuRepo.StudentAddressExists(StudentAddressId))
            {
                return NotFound();
            }

            //get student
            var studentAddress = _stuRepo.GetStudentAddress(StudentAddressId);

            if (!_stuRepo.DeleteStudentAddress(studentAddress))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting student address please try again later... {studentAddress.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
