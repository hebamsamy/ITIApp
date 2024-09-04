using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class SubjectManager : MainManager<Subject>
    {
        public SubjectManager(ProjectContext context) : base(context)
        {
        }
    }
}
