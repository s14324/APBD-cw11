using Cw11.DTOs.Requests;
using Cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Services
{
    public class SqlServerDbService : IDbService
    {
        private readonly CodeFirstContext _db;

        public SqlServerDbService(CodeFirstContext db)
        {
            _db = db;
        }
        
        public IEnumerable<Doctor> GetDoctors()
        {
            return _db.Doctor;
        }
        
        public Doctor AddDoctor(AddDoctorRequest request)
        {
            var d = new Doctor 
            { 
                IdDoctor = request.IdDoctor,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email 
            };
            _db.Add(d);
            _db.SaveChanges();

            return d;
        }
        
        public string DeleteDoctor(int id)
        {
            var d = _db.Doctor.Where(e => e.IdDoctor == id).FirstOrDefault();

            if (d != null)
            {
                _db.Remove(d);
                _db.SaveChanges();

                return "Usunieto doktora";
            }
            else
                return "Nie ma doktora o podanym id";
        }
        
        public Doctor UpdateDoctor(UpdateDoctorRequest request)
        {
            var d = _db.Doctor.Where(e => e.IdDoctor == request.IdDoctor).FirstOrDefault();

            if (d != null)
            {
                d.FirstName = request.FirstName;
                d.LastName = request.LastName;
                d.Email = request.Email;

                _db.Update(d);
                _db.SaveChanges();

                return d;
            }
            else
                return null;
        }
    }
}
