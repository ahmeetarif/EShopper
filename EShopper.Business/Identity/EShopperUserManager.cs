using EShopper.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EShopper.Business.Identity
{
    public class EShopperUserManager : UserManager<EShopperUser>
    {
        public EShopperUserManager(IUserStore<EShopperUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<EShopperUser> passwordHasher, IEnumerable<IUserValidator<EShopperUser>> userValidators, IEnumerable<IPasswordValidator<EShopperUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<EShopperUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

        }

        #region User details checking

        public virtual bool IsUserDeactivated(EShopperUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            var isUserDeactivated = user.UserDetails.IsActive;
            if (!isUserDeactivated)
            {
                return true;
            }
            return false;
        }

        public virtual bool CheckUserDeletion(EShopperUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            var isUserDeleted = user.UserDetails.IsDeleted;
            if (isUserDeleted)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Get Users

        public virtual EShopperUser GetUserWithUserDetailsByEmail(string email)
        {
            ThrowIfDisposed();
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("Please provide required information!");

            var getUser = Users.Include(x => x.UserDetails).FirstOrDefault(x => x.Email == email);

            return getUser;
        }

        public virtual EShopperUser GetUserWithUserDetailsById(string userId)
        {
            ThrowIfDisposed();
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException("Please provide required information!");

            var getUser = Users.Include(x => x.UserDetails).FirstOrDefault(x => x.Id == userId);

            return getUser;
        }

        #endregion

        #region Permissions



        #endregion

    }
}
