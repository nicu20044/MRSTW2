using System.Collections.Generic;
using MusicStore2.Domain.Entities.Subscription;

namespace MusicStore.BusinessLogic.Interfaces
{
    public interface IPlan
    {
        List<PlanData> GetAll();
        PlanData GetPlan(int id);
        void Create(PlanData plan);
        void Update(PlanData plan);
        void Delete(int id);
    }
}