        using MediatR;
        using Microsoft.AspNetCore.Identity;
        using WhisperNet.Domain.Entities;
        using WhisperNet.Infrastructure;

        namespace WhisperNet.Application.Chat.Admin.GetAllUsers;

        public class GetUserResponse(List<ApplicationUser> Users);
        
        public sealed record GetUsersQuery() : IRequest<GetUserResponse>;
        
        

        public sealed class GetAllUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUserResponse>
        {
            
            private readonly ApplicationDbContext _dbContext;
            private readonly UserManager<ApplicationUser> _userManager;
            public GetAllUsersQueryHandler(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
            {
                _dbContext = dbContext;
                _userManager = userManager;
            }

            public Task<GetUserResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                var userList = _userManager.Users.ToList();
                
                
                return Task.FromResult(new GetUserResponse(userList));
            }
        }