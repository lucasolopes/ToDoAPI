﻿using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using OnKanBan.Domain.Entities;
using OnKanBan.Persistence;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal sealed class WhiteBoardRepository : IWhiteBoardRepository
    {
        private readonly RepositoryDbContext _context;

        public WhiteBoardRepository(RepositoryDbContext context) => _context = context;

        public async Task<WhiteBoard> CreateAsync(WhiteBoard whiteBoard)
        {
            await _context.AddAsync(whiteBoard);
            await _context.SaveChangesAsync();
            return await _context.WhiteBoards.FirstOrDefaultAsync(wb=> wb.Name == whiteBoard.Name);
        }

        public async Task<WhiteBoard> GetByIdAsync(string id)
        {
            WhiteBoard whiteBoard = await _context.WhiteBoards.AsNoTracking().FirstOrDefaultAsync(wb => wb.Id == id);

            return whiteBoard;
        }

        public async Task<WhiteBoard> UpdateAsync(string id,WhiteBoard whiteBoard)
        {
            whiteBoard.Id = id;
            _context.Entry(whiteBoard).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return whiteBoard;
        }


        public async Task DeleteAsync(string id)
        {
            WhiteBoard whiteBoard = await GetByIdAsync(id);
            _context.WhiteBoards.Remove(whiteBoard);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(string id)
        {
            return _context.WhiteBoards.AnyAsync(wb => wb.Id == id);
        }

        public async Task UpdateNameAsync(string id, WhiteBoard whiteBoard)
        {
            WhiteBoard whiteBoardOld =  await GetByIdAsync(id);
            whiteBoardOld.Name = whiteBoard.Name;
            whiteBoardOld.LastUpdatedAt = whiteBoard.LastUpdatedAt;

            _context.Entry(whiteBoardOld).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDescriptionAsync(string id, WhiteBoard whiteBoard)
        {
            WhiteBoard whiteBoardOld = await GetByIdAsync(id);
            whiteBoardOld.Description = whiteBoard.Description;
            whiteBoardOld.LastUpdatedAt = whiteBoard.LastUpdatedAt;

            _context.Entry(whiteBoardOld).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
