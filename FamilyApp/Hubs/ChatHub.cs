using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FamilyApp
{
    public class ChatHub : Hub
    {

        public async Task SendMessage(string message)
        {
            if (!Context.User.Identity.IsAuthenticated)
                await Clients.Caller.SendAsync("ReceiveMessage", "Error", "Your are not authenticated");
            User user = DbFunctions.FindUserByEmail(Context.User.Identity.Name);
            await Clients.All.SendAsync("ReceiveMessage", user.Name + " " + user.Surname, message);
        }
    }
}
