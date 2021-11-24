using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICommentService
    {
        //List<Comment> GetAllSearch(string p);
        List<Comment> GetAll();
        List<Comment> GetAllByUser(int id);
        List<Comment> GetAllByProductId(int id);
        void Add(Comment comment);
        Comment GetById(int commentId);
        void Delete(Comment comment);
        void Update(Comment comment);
    }
}
