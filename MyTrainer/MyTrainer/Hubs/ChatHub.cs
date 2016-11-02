using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MyTrainer.Models;
using Microsoft.AspNet.Identity;

namespace MyTrainer
{
    public class ChatHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Send(string name, string message)
        {

            string userId = Context.User.Identity.GetUserId();
            User currentUser = db.UserDb.FirstOrDefault(x => x.LoginId == userId);
            name = currentUser.Username;
            Clients.All.broadcastMessage(name, message);
        }
    }
}