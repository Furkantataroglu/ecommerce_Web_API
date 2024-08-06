using DAL_DataAccessLayer.Abstarct;
using DAL_DataAccessLayer.EntityFramework.Contexts;
using DAL_DataAccessLayer.EntityFramework.InterfaceRepositories;
using DAL_DataAccessLayer.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _context;

        public UnitOfWork(MyDbContext context)
        {
            _context = context;
        }

        private UserRepository _userRepository;
        //private RoleRepository _roleRepository;
        //private HospitalRepository _hospitalRepository gibi

                                                        //?? değer null ise alternatif kodu çalıştırır
        public IUserRepository Users => _userRepository ?? new UserRepository(_context);
        //public IRoleRepository Roles => _roleRepository ?? new RoleRepository(_context);
        //public IHospitalRepository Hospitals => _hospitalRepository ?? new UserRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        //Kaldırma
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
