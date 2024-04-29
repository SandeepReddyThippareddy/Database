using System.Text;
using Application.Comments;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class Details
    {
        public class Query : IRequest<Result<EventDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<EventDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<EventDto>> Handle(Query request, CancellationToken cancellationToken)
            {

                var Event = await _context.Events
                    .ProjectTo<EventDto>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                ICollection<CommentDto> commentsDto = new List<CommentDto>();

                foreach(var item in Event.Comments){
                    item.Body = item.Body;
                   commentsDto.Add(item);
                }

                Event.Comments = commentsDto;

                return Result<EventDto>.Success(Event);
            }

            //public string Decrypt(string cipherText)
            //{
            //    try
            //    {
            //        string key = "hahskrpdhtpauthg";
            //        string iv = "kwlsbgodptnjlris";

            //        using (Aes aesAlg = Aes.Create())
            //        {
            //            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            //            aesAlg.IV = Encoding.UTF8.GetBytes(iv);

            //            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            //            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            //            {
            //                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            //                {
            //                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            //                    {
            //                        return srDecrypt.ReadToEnd();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (CryptographicException ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        return null;
            //    }
            //}
        }
    }
}

