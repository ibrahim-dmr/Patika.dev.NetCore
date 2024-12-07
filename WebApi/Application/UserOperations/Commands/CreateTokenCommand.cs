using AutoMapper;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;
using WebApi.DBOperations;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;


namespace WebApi.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _context = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.ExpireDate.AddMinutes(5);
                if (user.RefreshToken == null)
                {
                    throw new InvalidOperationException("RefreshToken cannot be null.");
                }
                _context.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Kullanıcı adı - şifre hatalı!");

        }

        public class CreateTokenModel
        {
            public string Email{ get; set; }
            public string Password { get; set; }

        }
    }
}
