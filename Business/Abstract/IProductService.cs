using Core.Utilities.Result;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);
    }
       
}
