using System.Collections.Generic;
using System.Security.Cryptography;
using MusicStore.BusinessLogic.Core;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore2.Domain.Entities.Subscription;

namespace MusicStore.BusinessLogic
{
    public class PlanBl : AdminApi, IPlan
    {
        public List<PlanData> GetAll()
        {
            return GetAllPlans();
        }

        public PlanData GetPlan(int id)
        {
            return GetPlanById(id);
        }

        public void Create(PlanData plan)
        {
            CreatePlan(plan);
        }

        public void Update(PlanData plan)
        {
            UpdatePlan(plan);
        }

        public void Delete(int id)
        {
            DeletePlan(id);
        }
    }
}