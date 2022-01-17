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
        public async Task<IEnumerable<Telecamera>> GetAll()
        {
            return await _context.eletelecamere.ToListAsync();
        }

        public async Task<Telecamera> GetById(int id)
        {
            Telecamera telecamera = await _context.eletelecamere.FirstOrDefaultAsync(telecamera => telecamera.id == id);
            if (telecamera == null)
                return null;
            return telecamera;
        }

        public async Task<CreaTelecameraResponse> Post(CreaTelecameraRequest request)
        {            
            await _context.AddAsync(request.telecamera);
            await _context.SaveChangesAsync();
            return new CreaTelecameraResponse()
            {
                Success = true,
                Created_telecamera = request.telecamera,
                Errors = null
            };
        }

        public async Task<ModificaTelecameraResponse> Put(ModificaTelecameraRequest request)
        {
            _context.Attach(request.telecamera).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ModificaTelecameraResponse()
            {
                Success = true,
                Edited_telecamera = request.telecamera,
                Errors = null
            };

        }

        public async Task<Telecamera> Delete(int id)
        {
            Telecamera telecamera;
            telecamera = await _context.eletelecamere.FirstOrDefaultAsync(telecamera => telecamera.id == id);
            if (telecamera == null)
                return null;
            _context.eletelecamere.Remove(telecamera);
            await _context.SaveChangesAsync();
            return telecamera;
        }
    }
}
