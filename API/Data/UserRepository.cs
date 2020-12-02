using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                .Where(x => x.UserName.Equals(username))
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<MemberDto>> GetMembersAsync() => await _context.Users.ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();


        public async Task<IEnumerable<AppUser>> GetUserAsync()
        => await _context.Users.Include(p => p.Photos).ToListAsync();

        public async Task<AppUser> GetUserByIdAsync(int id)
            => await _context.Users.FindAsync(id);

        public async Task<AppUser> GetUserByUsernameAsnyc(string username)
        => await _context.Users.Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName.Equals(username));       
        public async Task<bool> SaveAllAsync() => await _context.SaveChangesAsync() > 0;

        public void Update(AppUser user) => _context.Entry(user).State = EntityState.Modified;
    }
}
