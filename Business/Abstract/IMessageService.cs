using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMessageService
    {
        //List<Message> GetAllSearch(string p);
        List<Message> GetAllInbox(string p);
        List<Message> GetAllSendbox(string p);
        void Add(Message message);
        Message GetById(int messageId);
        void Delete(Message message);
        void Update(Message message);
    }
}
