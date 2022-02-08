using Api_Pcto.Data;
using Api_Pcto.Models;
using Api_Pcto.Models.DTOS.Requests;
using Api_Pcto.Models.DTOS.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api_Pcto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TelecamereController : ControllerBase
    {
        private readonly ITelecamereService _telecamere;

        public TelecamereController(ITelecamereService telecamere)
        {
            _telecamere = telecamere;
        }

        /// <summary>
        /// Metodo API che restituisce l'intero contenuto del Database.
        /// </summary>
        /// <returns>IEnumerable<Telecamera_Data></returns>
        [HttpGet]
        public async Task<IEnumerable<Telecamera_Data>> GetAll()
        {
            var result = await _telecamere.GetAll();

            return result;
        }


        /// <summary>
        /// Metodo API che restituisce un oggetto DTO GetTelecameraPerIdResponse dato un id.
        /// </summary>
        /// <param name="id">Id della telecamera interessata.</param>
        /// <returns></returns>
        [HttpGet("id {id}")]
        public async Task<ActionResult<GetTelecameraPerIdResponse>> GetById(int id)
        {
            var result = await _telecamere.GetById(id);
            if (result == null)
                return NotFound();
            return result;
        }


        /// <summary>
        /// Metodo API che restituisce un oggetto DTO GetTelecameraPerNomeResponse dato un nome.
        /// </summary>
        /// <param name="nome">Nome della telecamera interessata.</param>
        /// <returns></returns>
        [HttpGet("nome {nome}")]
        public async Task<ActionResult<GetTelecameraPerNomeResponse>> GetByName(string nome)
        {
            var result = await _telecamere.GetByName(nome);
            if (result == null)
                return NotFound();
            return result;
        }

        /// <summary>
        /// Metodo API che restituisce un oggetto DTO GetTelecameraRandomResponse.
        /// </summary>
        /// <returns></returns>
        [HttpGet("random")]
        public async Task<ActionResult<GetTelecameraRandomResponse>> GetRandom()
        {
            var result = await _telecamere.GetRandom();
            if (result == null)
                return NotFound();
            return result;
        }

        /// <summary>
        /// Metodo API che crea un oggetto DTO CreaTelecameraResponse e crea una telecamera sul database dato un DTO CreaTelecameraRequest.
        /// </summary>
        /// <param name="request">Oggetto CreaTelecameraRequest, maggiori informazioni reperibili tramite schema su swagger</param>
        /// <returns></returns>
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
        /// <summary>
        /// Metodo API che crea un oggetto DTO ModificaTelecameraResponse e modifica una telecamera sul database dato un DTO ModificaTelecameraRequest.
        /// </summary>
        /// <param name="request">Oggetto ModificaTelecameraRequest, maggiori informazioni reperibili tramite schema su swagger</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ModificaTelecameraResponse>> Put(ModificaTelecameraRequest request)
        {
            var result = await _telecamere.Put(request);
            if (result == null)
                return NotFound();
            return result;
        }

        // DELETE api/<ValuesController>/5
        /// <summary>
        /// Metodo API che restituisce un oggetto DTO EliminaTelecameraResponse ed elimina dal database una telecamera dato il suo id.
        /// </summary>
        /// <param name="id">Id della telecamera interessata.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EliminaTelecameraResponse>> Delete(int id)
        {
            var result = await _telecamere.Delete(id);
            if (result == null)
                return NotFound();
            return result;
        }
        //
    }
}
