using HumanResourceAPI.Context;
using HumanResourceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        public UniversityRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
