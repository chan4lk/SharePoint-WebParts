using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketSystem.Repository
{
    public interface IRepository<TEntity>
    {
        int Create(TEntity item);

        int Update(TEntity item);

        int Delete(int id);

        TEntity GetById(int id);

        List<TEntity> GetAll();
    }
}
