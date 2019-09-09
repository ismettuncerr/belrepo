
﻿using Bel.DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Interfaces
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        void Edit(Menu menu);
    }
}
