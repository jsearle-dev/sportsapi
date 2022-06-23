using SportsApi.Models;
using SportsApi.Contracts;
using SportsApi.Exceptions;
using Microsoft.EntityFrameworkCore;
using SportsApi.Contexts;

namespace SportsApi.Utilities
{
    public class EfService<T> : IDataService<T>
    where T : DbItem {
        private readonly SportsContext _context;
        public EfService(SportsContext context) {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync<T>();
        }

        public async Task<T?> FindByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> Filter(Func<T, bool> filter) 
        {
            var filtered = _context.Set<T>().Where(filter); 
            // Need to enumerate the query to ensure no errant queries blow up in DB
            return filtered.ToList();
        }

        public async Task<T> PersistAsync(long id, T item)
        {
            try
            {
                if (!Exists(i => i.Id == item.Id)) {
                    _context.Set<T>().Add(item);
                }
                else {
                    _context.Entry(item).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                return item;
            }
            catch (DbUpdateConcurrencyException)
            {
                Thread.Sleep(500);
                return await PersistAsync(id, item);
            }
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            if (item == null)
            {
                throw new NotFoundException($"{id} does not exist");
            }

            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }

        private bool Exists(Func<T, bool> filter)
        {
            return _context.Set<T>().Any(filter);
        }
    }
}