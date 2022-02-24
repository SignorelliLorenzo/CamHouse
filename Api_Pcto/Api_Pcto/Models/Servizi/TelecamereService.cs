using Api_Pcto.Data;
using Api_Pcto.Models.DTOS.Requests;
using Api_Pcto.Models.DTOS.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models
{
    public class TelecamereService : ITelecamereService
    {
        private readonly MyDbContext _context;
        public TelecamereService(MyDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Metodo che restituisce l' intera lista di telecamere dal DB.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Telecamera_Data>> GetAll()
        {
            return await _context.eletelecamere.ToListAsync();
        }

        /// <summary>
        /// Metodo che restituisce un oggetto DTO GetTelecameraPerIdResponse dato un id.
        /// </summary>
        /// <param name="id">Id della telecamera interessata.</param>
        /// <returns></returns>
        public async Task<GetTelecameraPerIdResponse> GetById(int id)
        {
            Telecamera_Data telecamera = await _context.eletelecamere.FirstOrDefaultAsync(telecamera => telecamera.id == id);
            if (telecamera == null)
            {
                return new GetTelecameraPerIdResponse()
                {
                    Success = false,
                    Found_telecamera = null,
                    Errors = new List<string>() { "Not Found" }
                };
            }
            return new GetTelecameraPerIdResponse()
            {
                Success = true,
                Found_telecamera = telecamera,
                Errors = null
            };
        }

        /// <summary>
        /// Metodo che restituisce un oggetto DTO GetTelecameraPerNome Response dato un nome.
        /// </summary>
        /// <param name="name">nome delle telecamere interessate.</param>
        /// <returns></returns>
        public async Task<GetTelecameraPerNomeResponse> GetByName(string name)
        {
            var telecamere = await _context.eletelecamere.Where(t => t.nome.ToLower().Contains(name.ToLower().Trim())).ToListAsync();
            if(telecamere.Count == 0)
            {
                return new GetTelecameraPerNomeResponse()
                {
                    Success = false,
                    Found_telecameras = null,
                    Errors = new List<string>() { "Not Found" }
                };
            }
            return new GetTelecameraPerNomeResponse()
            {
                Success = true,
                Found_telecameras = telecamere,
                Errors = null
            };
        }

        /// <summary>
        /// Metodo che restituisce un oggetto DTO GetTelecameraPerNome Response dato un nome.
        /// </summary>
        /// <param name="name">nome delle telecamere interessate.</param>
        /// <returns></returns>
        public async Task<GetTelecameraRandomResponse> GetRandom()
        {
            Random random = new Random();
            var num = await _context.eletelecamere.CountAsync();
            var num_generato = random.Next(1, num);
            var telecamera = (await _context.eletelecamere.ToListAsync())[num_generato];
            if (telecamera == null)
            {
                return new GetTelecameraRandomResponse()
                {
                    Success = false,
                    Found_telecamera = null,
                    Errors = new List<string>() { "Errore inaspettato" }
                };
            }
            return new GetTelecameraRandomResponse()
            {
                Success = true,
                Found_telecamera = telecamera,
                Errors = null
            };
        }

        /// <summary>
        /// Metodo che crea un oggetto DTO CreaTelecameraResponse e crea una telecamera sul database dato un DTO CreaTelecameraRequest.
        /// </summary>
        /// <param name="request">Oggetto CreaTelecameraRequest, maggiori informazioni reperibili tramite schema su swagger</param>
        /// <returns></returns>
        public async Task<CreaTelecameraResponse> Post(CreaTelecameraRequest request)
        {
            Telecamera_Data telecamera = new Telecamera_Data(request.nome, request.link, request.num_like, request.num_salvati);
            try
            {
                await _context.AddAsync(telecamera);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return new CreaTelecameraResponse()
                {
                    Success = false,
                    Created_telecamera = null,
                    Errors = new List<string> { ex.Message }
                };
            }
            return new CreaTelecameraResponse()
            {
                Success = true,
                Created_telecamera = telecamera,
                Errors = null
            };
        }

        /// <summary>
        /// Metodo che crea un oggetto DTO ModificaTelecameraResponse e modifica una telecamera sul database dato un DTO ModificaTelecameraRequest.
        /// </summary>
        /// <param name="request">Oggetto ModificaTelecameraRequest, maggiori informazioni reperibili tramite schema su swagger</param>
        /// <returns></returns>
        public async Task<ModificaTelecameraResponse> Put(ModificaTelecameraRequest request)
        {

            if (await _context.eletelecamere.FirstOrDefaultAsync(t => t.id == request.telecamera.id) == null)
            {
                return new ModificaTelecameraResponse()
                {
                    Success = false,
                    Edited_telecamera = null,
                    Errors = new List<string> { "Not Found" }
                };
            }
            Telecamera_Data telecamera;
            try
            {
                telecamera = await _context.eletelecamere.FirstOrDefaultAsync(t => t.id == request.telecamera.id);
                telecamera.nome = request.telecamera.nome;
                telecamera.link = request.telecamera.link;
                telecamera.num_like = request.telecamera.num_like;
                telecamera.num_salvati = request.telecamera.num_salvati;
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return new ModificaTelecameraResponse()
                {
                    Success = false,
                    Edited_telecamera = null,
                    Errors = new List<string> { ex.Message }
                };
            }
            return new ModificaTelecameraResponse()
            {
                Success = true,
                Edited_telecamera = telecamera,
                Errors = null
            };

        }

        /// <summary>
        /// Metodo che restituisce un oggetto DTO EliminaTelecameraResponse ed elimina dal database una telecamera dato il suo id.
        /// </summary>
        /// <param name="id">Id della telecamera interessata.</param>
        /// <returns></returns>
        public async Task<EliminaTelecameraResponse> Delete(int id)
        {
            Telecamera_Data telecamera = await _context.eletelecamere.FirstOrDefaultAsync(telecamera => telecamera.id == id);
            if (telecamera == null)
            {
                return new EliminaTelecameraResponse()
                {
                    Success = false,
                    Deleted_telecamera = null,
                    Errors = new List<string>() { "Not Found" }
                };
            }
            try
            {
                _context.eletelecamere.Remove(telecamera);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new EliminaTelecameraResponse()
                {
                    Success = false,
                    Deleted_telecamera = null,
                    Errors = new List<string> { ex.Message }
                };
            }
            return new EliminaTelecameraResponse()
            {
                Success = true,
                Deleted_telecamera = telecamera,
                Errors = null
            };
        }
    }
}
