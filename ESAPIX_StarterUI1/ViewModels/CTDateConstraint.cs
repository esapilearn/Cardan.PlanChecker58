using ESAPIX.Constraints;
using ESAPIX.Extensions;
using System;
using VMS.TPS.Common.Model.API;

namespace ESAPX_StarterUI.ViewModels
{
    public class CTDateConstraint : IConstraint
    {
        public string Name => "CT Date Constraint";

        public string FullName => "CT Date < 60 days old";

        public ConstraintResult CanConstrain(PlanningItem pi)
        {
            var pqa = new PQAsserter(pi);
            return pqa.HasImage()
                .CumulativeResult;
        }

        public ConstraintResult Constrain(PlanningItem pi)
        {
            var ctDate = pi.StructureSet.Image.CreationDateTime.Value;
            return ConstrainCTDate(ctDate);
        }

        public ConstraintResult ConstrainCTDate(DateTime ctDate)
        {
            var ctAge = (DateTime.Now - ctDate).TotalDays;
            if (ctAge > 60)
            {
                return new ConstraintResult(this, ResultType.ACTION_LEVEL_3, $"CT is {ctAge} days old");
            }
            else
            {
                return new ConstraintResult(this, ResultType.PASSED, $"CT is {ctAge} days old");
            }
        }
    }
}