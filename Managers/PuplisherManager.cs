using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class PuplisherManager : MainManager<Publisher>
    {
        public PuplisherManager(ProjectContext context) : base(context)
        {
        }
    }
}
