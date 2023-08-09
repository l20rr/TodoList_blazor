﻿using Microsoft.EntityFrameworkCore;
using TodoList_blazor.Shared;

namespace TodoList_blazor.API.Model
{
    public class DoModel : IDoModel
    {
        private readonly AppDbContext _appDbContext;

        public DoModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Do> AddDo(Do dos)
        {
           var result = await _appDbContext.Dos.AddAsync(dos);  
            await _appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteAll()
        {
            var foundDos = _appDbContext.Dos.OrderByDescending(d => d.DoId).ToList();

            foreach (var dos in foundDos)
            {
                _appDbContext.Dos.Remove(dos);
            }

            await _appDbContext.SaveChangesAsync();
        }
        public void DeleteDo(int id)
        {
           var foundDo = _appDbContext.Dos.FirstOrDefault(d => d.DoId == id);   
            if (foundDo != null)
            {
                _appDbContext.Dos.Remove(foundDo);
                _appDbContext.SaveChanges();
            }
        }

        public Do GetDo(int id)
        {
            return _appDbContext.Dos.FirstOrDefault(x => x.DoId == id);
        }

        public IEnumerable<Do> GetDos()
        {
            return _appDbContext.Dos;
        }

        public Do UpdateDo(Do dos)
        {
            var foundDo = _appDbContext.Dos.FirstOrDefault(d => d.DoId == dos.DoId);
            if (foundDo != null) {
                foundDo.Completed = dos.Completed;

                _appDbContext.SaveChangesAsync();

                return foundDo; 
            }
            return null;

        }
    }
}
