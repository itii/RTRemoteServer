using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Owin;
using Microsoft.Owin;
using RTServer.Controllers;

[assembly: OwinStartup(typeof(RTServer.Startup))]
namespace RTServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }


    public class ChatHub : Hub
    {
        static List<User> Users = new List<User>();
         
        // Отправка сообщений
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
 
        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
 
 
            if (Users.Count(x => x.ConnectionId == id) == 0)
            {
                Users.Add(new User { ConnectionId = id, Name = userName });
 
                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);
 
                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }
 
        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected()
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }
 
            return base.OnDisconnected();
        }
    }



    [HubName("RTServerHub")]
    public class RTServerHub : Hub
    {
        static List<string> Users = new List<string>();

        //public void Send(string name, Record record)
        //{
        //    Clients.All.addMessage(name, record);
        //}

        public void Send(Customer record)
        {
            Clients.All.addMessage(record);
        }
        

        public void Registration(string userName)
        {
            var id = Context.ConnectionId;


            Users.Add(userName);

            //Clients.Caller.onConnected(id, userName, Users);

            //Clients.AllExcept(id).onNewUserConnected(id, userName);
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            return base.OnDisconnected();
        }

        public void ActivateRandomizeData()
        {
            var radnom = new Random();
            var record = new Customer()
            {
                
            };
            Clients.All.addMessage(record);
        }

    }

}
