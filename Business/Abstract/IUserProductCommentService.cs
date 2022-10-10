using Core.Utilities.Result;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserProductCommentService
    {
        IDataResult<List<UserProductComment>> GetAll();

        IResult Add(UserProductComment userProductComment);
        IResult Update(UserProductComment userProductComment);

        IResult Delete(UserProductComment userProductComment);
    }
        
       
}
