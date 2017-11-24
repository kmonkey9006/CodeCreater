using System;
using System.Linq;
using System.Collections.Generic;
using RTSafe.HiddenTroubleTreatm.BusinessModules.DataAccess;
using RTSafe.HiddenTroubleTreatm.BusinessModules.DataAccess.Entities;

using RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmModules.Models;
namespace RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmModules.Domain
{

    public class BuildUnitService : IBuildUnitService
    {
        HiddenTroubleTreatmDataContext dbContext = new HiddenTroubleTreatmDataContext();
        public BuildUnitService()
        {
 
        }

        public BuildUnitModel Get(Guid id)        {

            var entity = dbContext.BuildUnit.First(p => p.BID == id);
            return entity.ToViewModel<BuildUnitModel>();
        }
        public int Add(BuildUnitModel model)        {
            BuildUnit ent = model.ToORMEntity<BuildUnit>();
            int re = dbContext.BuildUnit.Insert(ent);
            return re;
        }
        public int Edit(BuildUnitModel model)
        {
            var ent = model.ToORMEntity<BuildUnit>();
            int re = dbContext.BuildUnit.Update(ent);
            return re;
        }
        public int Delete(Guid id)
        {
            return dbContext.BuildUnit.Delete(id);
        }

        public List<BuildUnitModel> GetAllPage(int index, int size, out int total)
        {
            string sql = @"select * from BuildUnit  where 1=1 ";

            try
            {
                var query = dbContext.CustomSql(sql).SetSelectRange(size * (index - 1), size);
                total = query.GetTotalForPaging();
                return query.OrderBy(" CreateTime Desc").ToList<BuildUnitModel>();
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }
        }
    }
}
