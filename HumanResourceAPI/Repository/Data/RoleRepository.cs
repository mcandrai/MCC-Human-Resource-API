using HumanResourceAPI.Context;
using HumanResourceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext,Role, int>
    {
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            
        }
    }
}
