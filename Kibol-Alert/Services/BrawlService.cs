﻿using Kibol_Alert.Database;
using Kibol_Alert.Models;
using Kibol_Alert.Requests;
using Kibol_Alert.Responses;
using Kibol_Alert.Services.Interfaces;
using Kibol_Alert.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kibol_Alert.Services
{
    public class BrawlService : BaseService, IBrawlService
    {
        public BrawlService(Kibol_AlertContext context, ILoggerService logger) : base(context, logger)
        {
        }

        public async Task<Response> AddBrawl(BrawlRequest request)
        {
            var brawl = new Brawl()
            {
                FirstClubName = request.FirstClubName,
                SecondClubName = request.SecondClubName,
                Date = request.Date,
                Longitude = request.Longitude,
                Latitude = request.Latitude
            };

            await Context.Brawls.AddAsync(brawl);
            await Context.SaveChangesAsync();
            AddLog($"Stworzono ustawkę {request.FirstClubName} vs {request.SecondClubName}");
            return new SuccessResponse<bool>(true);
        }

        public async Task<Response> DeleteBrawl(int id)
        {
            var brawl = await Context.Brawls.FirstOrDefaultAsync(i => i.Id == id);
            if (brawl == null)
            {
                return new ErrorResponse("Ustawki nie znaleziono!");
            }
            Context.Brawls.Remove(brawl);
            await Context.SaveChangesAsync();
            AddLog($"Usunięto ustawkę {brawl.FirstClubName} vs {brawl.SecondClubName}");
            return new SuccessResponse<bool>(true);
        }

        public async Task<Response> EditBrawl(int id, BrawlRequest request)
        {
            var brawl = await Context.Brawls.FirstOrDefaultAsync(i => i.Id == id);
            if (brawl == null)
            {
                return new ErrorResponse("Ustawki nie znaleziono!");
            }

            brawl.FirstClubName = request.FirstClubName;
            brawl.SecondClubName = request.SecondClubName;
            brawl.Date = request.Date;
            brawl.Longitude = request.Longitude;
            brawl.Latitude = request.Latitude;


            Context.Brawls.Update(brawl);
            await Context.SaveChangesAsync();
            AddLog($"Edytowane ustawkę {brawl.Id}");
            return new SuccessResponse<bool>(true);
        }

        public async Task<Response> GetBrawl(int id)
        {
            var brawl = await Context.Brawls
                .FirstOrDefaultAsync(i => i.Id == id);

            var brawlDto = new BrawlVM()
            {
                Id = brawl.Id,
                FirstClubName = brawl.FirstClubName,
                SecondClubName = brawl.SecondClubName,
                Date = brawl.Date,
                Longitude = brawl.Longitude,
                Latitude = brawl.Latitude
            };

            return new SuccessResponse<BrawlVM>(brawlDto);
        }

        public async Task<Response> GetBrawls(int skip, int take)
        {
            var brawls = await Context.Brawls
                .OrderByDescending(row => row)
                .Skip(skip)
                .Take(take)
                .Select(row => new BrawlVM()
                {
                    Id = row.Id,
                    FirstClubName = row.FirstClubName,
                    SecondClubName = row.SecondClubName,
                    Date = row.Date,
                    Longitude = row.Longitude,
                    Latitude = row.Latitude
                })
                .ToListAsync();

            var brawlsDto = brawls.Where(row => DateTime.Parse(row.Date) > DateTime.Now).ToList();

            return new SuccessResponse<List<BrawlVM>>(brawlsDto);
        }
    }
}