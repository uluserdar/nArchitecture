using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using RentACar.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Feaures.Auths.Rules
{
    public class AuthBusinessRule
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRule(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task EmailCanNotBeDublicatedWhenRegistered(string email)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == email);

            if (user != null) throw new BusinessException("Mail already exists.");
        }
    }
}
