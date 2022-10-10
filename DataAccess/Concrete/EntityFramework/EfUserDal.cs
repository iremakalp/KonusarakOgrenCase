using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ECommerceDbContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context=new ECommerceDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOpClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOpClaim.OperationClaimId
                             where userOpClaim.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return result.ToList();
            }
        }
    }
}
