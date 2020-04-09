using socialNetworkApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialNetworkApi.Repository
{
    interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetAll(int pid);
        Comment Get(int pid, int id);
        void Delete(int pid, int id);
        void DeleteAll(int pid);
    }
}
