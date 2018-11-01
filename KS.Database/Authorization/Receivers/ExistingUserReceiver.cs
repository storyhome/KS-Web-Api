using AutoMapper;
using KS.Business.DataContract.Authorization;
using KS.Database.Contexts;
using KS.Database.DataContract.Authorization;
using KS.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KS.Database.Authorization.Receivers
{
    public class ExistingUserReceiver : IExistingUserReceiver
    {
        private readonly KSContext _context;
        private readonly IMapper _mapper;

        public ExistingUserReceiver(KSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReceivedExistingDTO> LoginUser(LoginRAO userRAO)
        {
            var userEntity = await _context.UserTableAccess.FirstOrDefaultAsync(x => x.Username == userRAO.UserName);
            return _mapper.Map<ReceivedExistingDTO>(userEntity);
        }
    }
}
