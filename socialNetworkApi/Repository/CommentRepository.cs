using socialNetworkApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace socialNetworkApi.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public void Delete(int pid, int id)
        {
            context.Set<Comment>().Remove(this.Get(pid,id));
            context.SaveChanges();
        }

        public void DeleteAll(int pid)
        {
            context.Set<Comment>().RemoveRange(this.GetAll(pid));
            context.SaveChanges();

        }

        public Comment Get(int pid, int id)
        {
            return context.Set<Comment>().SingleOrDefault(c => c.id == id && c.postId == pid);
        }

        public IEnumerable<Comment> GetAll(int pid)
        {
            return context.Set<Comment>().Where(p => p.postId == pid).ToList();
        }
    }
}