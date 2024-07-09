﻿using Microsoft.AspNetCore.Mvc;

using Server.Interfaces;
using Server.Models;
using Server.Repository;

namespace Server.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class StaffController : Controller
    {
        private readonly IStaffRepository _staffRepository;

        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<StaffRepository>))]
        public IActionResult GetStaff()
        {
            var staff = _staffRepository.Get();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(staff);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(StaffRepository))]
        public IActionResult GetPerson(int id)
        {
            var staff = _staffRepository.GetByID(id);

            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

        //[HttpGet("{id}/classes")]
        //[ProducesResponseType(200)]
        //public IActionResult GetPersonClass(int id)
        //{
        //    var person = _staffRepository.GetClasses(id);

        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(person);
        //}

        [HttpPost("{id}&{state}")]
        [ProducesResponseType(200)]
        public IActionResult UpdatePerson(int id, bool state)
        {
            Staff staff = _staffRepository.GetByID(id);

            if (id != staff.ID)
            {
                return BadRequest();
            }

            staff.Present = state;

            _staffRepository.Save();

            return NoContent();
        }

        [HttpPost("SignIn/{email}&{password}")]
        [ProducesResponseType(200, Type=typeof(string))]
        public IActionResult SignIn(string email, string password)
        {
            string sessionID = _staffRepository.SignIn(email, password);

            if (sessionID != null)
            {
                return Ok(sessionID);
            }

            return Unauthorized();
        }
    }
}
