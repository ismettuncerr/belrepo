using Bel.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Controllers
{
    public class BaseController : Controller
    {
        protected DataClient dataClient = new DataClient();        

    }
}