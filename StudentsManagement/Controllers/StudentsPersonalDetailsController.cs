using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Models;
using StudentsManagement.Models.Dtos;
using StudentsManagement.Repository.IRepository;
using System.Collections.Generic;

namespace StudentsManagement.Controllers
{
    [Route("api/v{version:apiVersion}/studentsPersonalDetails")]
    [ApiVersion("2.0")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class StudentsPersonalDetailsController : ControllerBase
    {
        private readonly IStudentsPersonalDetailsRepository _stuRepo;
        private readonly IMapper _mapper;

        public StudentsPersonalDetailsController(IStudentsPersonalDetailsRepository stuRepo, IMapper mapper)
        {
            _stuRepo = stuRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of students details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStudentsPersonalDetails()
        {
            var objList = _stuRepo.GetStudentsPersonalDetails();


            var objDto = new List<StudentsPersonalDetailsDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<StudentsPersonalDetailsDto>(obj));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Get individual student detail
        /// </summary>
        /// <param name="StudentPersonalDetailId"></param>
        /// <returns></returns>
        [HttpGet("{StudentPersonalDetailId:int}", Name = "GetStudentDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStudentPersonalDetails(int StudentPersonalDetailId)
        {
            var obj = _stuRepo.GetStudentPersonalDetails(StudentPersonalDetailId);

            if (obj == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StudentsPersonalDetailsDto>(obj));
        }

        /// <summary>
        /// Create student detail
        /// </summary>
        /// <param name="createStudentDetailsDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateStudent([FromBody] CreateStudentsPersonalDetailsDto createStudentDetailsDto)
        {
            if (createStudentDetailsDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_stuRepo.StudentPersonalDetailsExists(createStudentDetailsDto.PhoneNumber))
            {
                ModelState.AddModelError("", "Student details already exists");
                return StatusCode(404, ModelState);
            }

            var createStudentDetail = _mapper.Map<StudentsPersonalDetails>(createStudentDetailsDto);

            if (!_stuRepo.CreateStudentPersonalDetails(createStudentDetail))
            {
                ModelState.AddModelError("", "Something went wrong when create student details please try again later...");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetStudentDetail", new { StudentPersonalDetailId = createStudentDetail.Id }, createStudentDetail);

        }

        /// <summary>
        /// Update Student detail by id
        /// </summary>
        /// <param name="StudentPersonalDetailId"></param>
        /// <param name="updateStudentDetailsDto"></param>
        /// <returns></returns>
        [HttpPatch("{StudentPersonalDetailId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateStudent(int StudentPersonalDetailId, [FromBody] UpdateStudentsPersonalDetailsDto updateStudentDetailsDto)
        {

            if (updateStudentDetailsDto == null || StudentPersonalDetailId != updateStudentDetailsDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_stuRepo.StudentPersonalDetailsExists(updateStudentDetailsDto.Id))
            {
                ModelState.AddModelError("", "Student details not found please enter correct Id...");
                return StatusCode(404, ModelState);
            }

            var studentDetailObj = _mapper.Map<StudentsPersonalDetails>(updateStudentDetailsDto);
            if (!_stuRepo.UpdateStudentPersonalDetails(studentDetailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating student details please try again later... {studentDetailObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete student detail by id
        /// </summary>
        /// <param name="StudentPersonalDetailId"></param>
        /// <returns></returns>
        [HttpDelete("{StudentPersonalDetailId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteStudent(int StudentPersonalDetailId)
        {

            if (!_stuRepo.StudentPersonalDetailsExists(StudentPersonalDetailId))
            {
                return NotFound();
            }

            //get student
            var studentDetail = _stuRepo.GetStudentPersonalDetails(StudentPersonalDetailId);

            if (!_stuRepo.DeleteStudentPersonalDetails(studentDetail))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting student details please try again later... {studentDetail.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
