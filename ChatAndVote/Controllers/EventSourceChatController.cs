using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChatAndVote.Controllers
{
    public class EventSourceChatController : Controller
    {

        // static, so that this is shared with everyone who accesses this controller
        static TaskCompletionSource<string> nextMessage = new TaskCompletionSource<string>();
        //
        // GET: /EventSourceChat/

        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> GetNextMessage()
        {

            //var message = await nextMessage.Task;
            return await nextMessage.Task;
        }

        public void PostMessage(string message)
        {
            #region stage1
            //nextMessage.SetResult(message);
            // what if new message already comes in GetNextMessage()
            // then you cannot await nextMessage.Task because it has already completed => SetResult
            //nextMessage = new TaskCompletionSource<string>();
            #endregion


            var oldNextMessage = nextMessage;
            nextMessage = new TaskCompletionSource<string>();
            oldNextMessage.SetResult(message);
        }

        public async Task GetMessages()
        {
            Response.ContentType = "text/event-stream";

            var message = await nextMessage.Task;
            Response.Write("data: " + message + "\n\n");


            Response.Flush();

        }

    }
}
