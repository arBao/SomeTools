using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionPassed : BNodeCondition
    {
        public ConditionPassed()
            : base()
        {
            this.m_strName = "ConditionPassed";
            this.m_description = "ConditionPassed。(叶子节点不能添加子节点)";
        }
        public override ActionResult Excute(BTInput input)
        {
            base.Excute(input);
            //input.SetActiveEvent(input.GetConditionChecker().GetActiveCfg());
            //if (input.GetActiveEvent() != null)
            //{
            //    return ActionResult.Success;
            //}
            //else
            //{
            //    return ActionResult.Failure;
            //}
			return ActionResult.Success;
        }
       
    }

}
