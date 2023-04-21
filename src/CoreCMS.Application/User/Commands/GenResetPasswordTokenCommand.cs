using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class GenResetPasswordTokenCommand : IRequest<string>
    {
        public string UserId { get; set; }
    }
}