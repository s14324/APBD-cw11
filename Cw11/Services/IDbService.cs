using Cw11.DTOs.Requests;
using Cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Services
{
    public interface IDbService
    {
        IEnumerable<Doctor> GetDoctors();
        Doctor AddDoctor(AddDoctorRequest request);
        Doctor UpdateDoctor(UpdateDoctorRequest request);
        public string DeleteDoctor(int id);
    }
}
