using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatAndVote.Controllers
{
    public class SignalRChatController : Controller
    {
        //
        // GET: /SignalRChat/

        public ActionResult Index()
        {
            return View();
        }

    }

    public class Messages : Hub
    {
        public void Send(string message)
        {
            Clients.All.addMessage(message);
        }
    }
}
