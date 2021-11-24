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
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;
        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }
        public void Add(Message message)
        {
            _messageDal.Add(message);
        }

        public void Delete(Message message)
        {
            _messageDal.Delete(message);
        }

        public List<Message> GetAllInbox(string p)
        {
            return _messageDal.GetAll(x=>x.ReceiverMail==p);
        }

        //public List<Message> GetAllSearch(string p)
        //{
        //    return _messageDal.GetAll(x => x.MessageContent.Contains(p));
        //}

        public List<Message> GetAllSendbox(string p)
        {
            return _messageDal.GetAll(x => x.SenderMail == p);
        }

        public Message GetById(int messageId)
        {
            return _messageDal.Get(x => x.MessageId == messageId);
        }

        public void Update(Message message)
        {
            _messageDal.Update(message);
        }
    }
}
