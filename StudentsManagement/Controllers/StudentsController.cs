using AutoMapper;
using StudentsManagement.Models;
using StudentsManagement.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using StudentsManagement.Models.Dtos;

namespace StudentsManagement.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/students")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _stuRepo;
        private readonly IMapper _mapper;

        public StudentsController(IStudentsRepository stuRepo, IMapper mapper)
        {
            _stuRepo = stuRepo;
            _mapper = mapper;
        }
            
        /// <summary>
        /// Get list of students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStudents()
        {
            var objList = _stuRepo.GetStudents();

            var objDto = new List<StudentsDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<StudentsDto>(obj));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Get individual student
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet("{studentId:int}", Name = "GetStudent")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStudent(int studentId)
        {
            var obj = _stuRepo.GetStudent(studentId);

            if (obj == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StudentsDto>(obj));
        }

        /// <summary>
        /// Create student
        /// </summary>
        /// <param name="createStudentDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateStudent([FromBody] CreateStudentsDto createStudentDto)
        {
            if (createStudentDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_stuRepo.StudentExists(createStudentDto.Name))
            {
                ModelState.AddModelError("", "Student name already exists");
                return StatusCode(404, ModelState);
            }
            else if(_stuRepo.StudentExists(createStudentDto.RegisterNumber))
            {
                ModelState.AddModelError("", "Student register number already exists");
                return StatusCode(404, ModelState);
            }
            else if (_stuRepo.StudentExistAadhaar(createStudentDto.AadhaarNumber))
            {
                ModelState.AddModelError("", "Student aadhaar number already exists");
                return StatusCode(404, ModelState);
            }

            var createStudent = _mapper.Map<Students>(createStudentDto);

            if (!_stuRepo.CreateStudent(createStudent))
            {
                ModelState.AddModelError("", "Something went wrong when create student please try again later...");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetStudent", new { studentId = createStudent.Id }, createStudent);

        }

        /// <summary>
        /// Update Student by id
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="updateStudentDto"></param>
        /// <returns></returns>
        [HttpPatch("{studentId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateStudent(int studentId, [FromBody] UpdateStudentsDto updateStudentDto)
        {

            if (updateStudentDto == null || studentId != updateStudentDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_stuRepo.StudentExists(updateStudentDto.Id))
            {
                ModelState.AddModelError("", "Student not found please enter correct Id...");
                return StatusCode(404, ModelState);
            }

            var studentObj = _mapper.Map<Students>(updateStudentDto);
            if (!_stuRepo.UpdateStudent(studentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating student please try again later... {studentObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete student by id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpDelete("{studentId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteStudent(int studentId)
        {

            if (!_stuRepo.StudentExists(studentId))
            {
                return NotFound();
            }

            //get student
            var student = _stuRepo.GetStudent(studentId);

            if (!_stuRepo.DeleteStudent(student))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting student please try again later... {student.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpGet("[action]/{personalDetailAndAddressId:int}")]
        [ProducesResponseType(200, Type = typeof(StudentsDto))]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        public IActionResult GetStudentPersonalDetailAndAddress(int personalDetailAndAddressId)
        {
            var objList = _stuRepo.GetStudentPersonalDetailAndAddress(personalDetailAndAddressId);

            if (objList == null)  // if Id is not found in db it return 404 not found error
            {
                return NotFound();
            }
            var objDto = new List<StudentsDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<StudentsDto>(obj));
            }
            return Ok(objDto);
        }
    }
}
