using API.Extensions;
using Application.Comments;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Text;


namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendComment(Create.Command command)
        {
            command.Body = command.Body;
            var comment = await _mediator.Send(command);

            await Clients.Group(command.EventId.ToString())
                .SendAsync("ReceiveComment", comment.Value);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var EventId = httpContext.Request.Query["EventId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, EventId);
            var result = await _mediator.Send(new List.Query { EventId = Guid.Parse(EventId) });
            await Clients.Caller.SendAsync("LoadComments", result.Value);
        }

        //public string Encrypt(string plainText)
        //{
        //    try
        //    {
        //        string key = "hahskrpdhtpauthg";
        //        string iv = "kwlsbgodptnjlris";
        //        using (Aes aesAlg = Aes.Create())
        //        {
        //            aesAlg.Key = Encoding.UTF8.GetBytes(key);
        //            aesAlg.IV = Encoding.UTF8.GetBytes(iv);

        //            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //            using (MemoryStream msEncrypt = new MemoryStream())
        //            {
        //                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //                {
        //                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                    {
        //                        swEncrypt.Write(plainText);
        //                    }
        //                }
        //                return Convert.ToBase64String(msEncrypt.ToArray());
        //            }
        //        }
        //    }
        //    catch (CryptographicException ex)
        //    {
        //        Console.WriteLine($"CryptographicException: {ex.Message}");
        //        return null;
        //    }

        //}

    }
}

