using Api_Pcto.Data;
using Api_Pcto.Models;
using Api_Pcto.Models.DTOS.Requests;
using Api_Pcto.Models.DTOS.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api_Pcto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelecamereController : ControllerBase
    {
        private readonly ITelecamereService _telecamere;

        public TelecamereController(ITelecamereService telecamere)
        {
            _telecamere = telecamere;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Telecamera_Data>> GetAll()
        {
            var result = await _telecamere.GetAll();

            return result;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTelecameraPerIdResponse>> GetById(int id)
        {
            var result = await _telecamere.GetById(id);
            if (result == null)
                return NotFound();
            return result;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<CreaTelecameraResponse>> Post(CreaTelecameraRequest request)
        {
            var result = await _telecamere.Post(request);
            if (result == null)
                return NotFound();
            return result;
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<ActionResult<ModificaTelecameraResponse>> Put(ModificaTelecameraRequest request)
        {
            var result = await _telecamere.Put(request);
            if (result == null)
                return NotFound();
            return result;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EliminaTelecameraResponse>> Delete(int id)
        {
            var result = await _telecamere.Delete(id);
            if (result == null)
                return NotFound();
            return result;
        }
    }
}
