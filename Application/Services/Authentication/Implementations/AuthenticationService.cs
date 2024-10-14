using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services.Authentication.Interfaces;
using Application.Settings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Constants;
using Common.Helpers;
using Data.EntityRepositories.Interfaces;
using Data.UnitOfWorks.Interfaces;
using Domain.Models.Authorization;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Authentication.Implementations
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettings> appSettings) : base(
            unitOfWork,
            mapper)
        {
            _appSettings = appSettings.Value;
            _userRepository = unitOfWork.User;
        }

        public async Task<IActionResult> Authenticate(CertificateModel certificate)
        {
            var user = await _userRepository.Where(x => x.Username.Equals(certificate.Username)
                                                        && x.Password.Equals(certificate.Password))
                .Include(x => x.Role)
                .Include(x => x.Department)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return new NotFoundResult();
            }

            if (user.Status.Equals(UserStatuses.Inactive))
            {
                return new BadRequestObjectResult("User is locked");
            }

            var auth = _mapper.Map<UserInformationModel>(user);
            var accessToken = JwtHelper.GenerateJwtToken(user.Id, user.Role.Name, _appSettings.Secret);
            return new OkObjectResult(new AuthenticationModel
            {
                AccessToken = accessToken,
                User = new UserInformationModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    Name = user.Name,
                    Role = user.Role.Name,
                    Department = user.Department.Name,
                    Status = user.Status,
                }
            });
        }

        public async Task<IActionResult> GetUserInformation(Guid id)
        {
            var user = await _userRepository.Where(st => st.Id.Equals(id))
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return user != null ? new ObjectResult(user) : new NotFoundResult();
        }

        public async Task<UserInformationModel?> GetUser(Guid id)
        {
            var user = await _userRepository
                .Where(st => st.Id.Equals(id))
                .ProjectTo<UserInformationModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return user;
        }
    }
}