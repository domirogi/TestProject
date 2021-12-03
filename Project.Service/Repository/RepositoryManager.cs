using Project.Service.DataAccess.Data;
using Project.Service.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private VehicleDbContext _db;
        private IMakeRepository _makeRepository;
        private IModelRepository _modelRepository;
        public RepositoryManager(VehicleDbContext db)
        {
            _db = db;
        }
        public IMakeRepository Make
        {
            get
            {
                if (_makeRepository == null)
                {
                    _makeRepository = new MakeRepository(_db);
                }
                return _makeRepository;
            }
        }
        public IModelRepository Model
        {
            get
            {
                if (_modelRepository == null)
                {
                    _modelRepository = new ModelRepository(_db);
                }
                return _modelRepository;
            }
        }
        public Task SaveAsync() => _db.SaveChangesAsync();
    }
}
