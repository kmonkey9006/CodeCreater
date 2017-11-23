using System;
using System.Collections.Generic;
using RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmModules.Models;
namespace RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmModules.Domain
{
    public interface IBuildUnitService
    {
        BuildUnitModel Get(Guid id);
        int Add(BuildUnitModel model);
        int Edit(BuildUnitModel model);
        int Delete(Guid id);
        List<BuildUnitModel> GetAllPage(int index, int size, Guid? lineID,Guid? segmentID, DateTime? sd, DateTime? ed, out int total);
    }
}
