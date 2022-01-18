﻿using Api_Pcto.Data;
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
        public async Task<IEnumerable<Telecamera_Data>> GetAll()
        {
            return await _context.eletelecamere.ToListAsync();
        }

        public async Task<Telecamera_Data> GetById(int id)
        {
            Telecamera_Data telecamera = await _context.eletelecamere.FirstOrDefaultAsync(telecamera => telecamera.id == id);
            if (telecamera == null)
                return null;
            return telecamera;
        }

        public async Task<CreaTelecameraResponse> Post(CreaTelecameraRequest request)
        {
            Telecamera_Data telecamera = new Telecamera_Data(request.telecamera.id, request.telecamera.nome, request.telecamera.link, request.telecamera.num_like, request.telecamera.num_salvati);
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

        public async Task<Telecamera_Data> Delete(int id)
        {
            Telecamera_Data telecamera;
            telecamera = await _context.eletelecamere.FirstOrDefaultAsync(telecamera => telecamera.id == id);
            if (telecamera == null)
                return null;
            _context.eletelecamere.Remove(telecamera);
            await _context.SaveChangesAsync();
            return telecamera;
        }
    }
}
