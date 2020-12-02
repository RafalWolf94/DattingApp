using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AppUser>> GetUserAsync()
        => await _context.Users.Include(p => p.Photos).ToListAsync();

        public async Task<AppUser> GetUserByIdAsync(int id)
            => await _context.Users.FindAsync(id);

        public async Task<AppUser> GetUserByUsername(string username)
        => await _context.Users.Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName.Equals(username));


        public async Task<bool> SaveAllAsync()
        => await _context.SaveChangesAsync() > 0;

        public void Update(AppUser user)
         => _context.Entry(user).State = EntityState.Modified;
    }
}
