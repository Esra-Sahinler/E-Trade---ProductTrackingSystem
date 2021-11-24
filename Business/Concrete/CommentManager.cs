using Business.Abstract;
using DataAccess.Abstract;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }
        public void Add(Comment comment)
        {
            _commentDal.Add(comment);
        }

        public void Delete(Comment comment)
        {
            _commentDal.Delete(comment);
        }

        public List<Comment> GetAll()
        {
            return _commentDal.GetAll();
        }

        public List<Comment> GetAllByProductId(int id)
        {
            return _commentDal.GetAll(x => x.ProductId == id);
        }

        public List<Comment> GetAllByUser(int id)
        {
            return _commentDal.GetAll(x => x.UserId == id);
        }

        //public List<Comment> GetAllSearch(string p)
        //{
        //    return _commentDal.GetAll(x => x.Description.Contains(p));
        //}

        public Comment GetById(int commentId)
        {
            return _commentDal.Get(co => co.CommentId == commentId);
        }

        public void Update(Comment comment)
        {
            _commentDal.Update(comment);
        }
    }
}
