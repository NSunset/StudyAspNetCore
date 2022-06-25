using Microsoft.Extensions.Logging;
using Nw.Account.MicroService.IService;
using Nw.Account.MicroService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Account.MicroService.Service
{
    public class AccountService : IAccountService
    {
        private ILogger<AccountService> _logger;
        private AccountDbContext _dbContext;

        public AccountService(ILogger<AccountService> logger, AccountDbContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }

        public void UpdateBalance(long? userId, decimal? money)
        {
            _logger.LogInformation("更新{userId}的账号余额", userId);
            Models.Account account = null;
            try
            {
                account = _dbContext.Account.First(u => u.UserId == userId);
            }
            catch (Exception)
            {
                throw new Exception("账号不存在，请核准后输入");
            }

            int result = decimal.Compare((account.Residue - money).Value, 0);
            if (result < 0)
            {
                throw new Exception("用户的余额不足，请充值");
            }
            // 更新余额
            account.Used += money;
            account.Residue -= money;

            _dbContext.SaveChanges();
        }
    }
}
