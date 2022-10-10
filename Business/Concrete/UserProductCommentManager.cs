using Business.Abstract;
using DataAccess.Abstract;
using Core.Utilities.Result;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserProductCommentManager:IUserProductCommentService
    {
        IUserProductCommentDal _userProductCommentDal;
        public UserProductCommentManager(IUserProductCommentDal userProductCommentDal)
        {
            _userProductCommentDal = userProductCommentDal;
        }
        public IResult Add(UserProductComment userProductComment)
        {
            _userProductCommentDal.Add(userProductComment);
            return new SuccessResult();
        }
        public IResult Delete(UserProductComment userProductComment)
        {
            _userProductCommentDal.Delete(userProductComment);
            return new SuccessResult();
        }
        public IDataResult<List<UserProductComment>> GetAll()
        {
            return new SuccessDataResult<List<UserProductComment>>(_userProductCommentDal.GetAll());
        }
        public IResult Update(UserProductComment userProductComment)
        {
            _userProductCommentDal.Update(userProductComment);
            return new SuccessResult();
        }
    }
}