using UnityEngine;
using System.Collections;
//using battle;

namespace GameAI
{
    public class ActionUniqueSkill : BNodeAction
    {
        public ActionUniqueSkill()
            : base()
        {
            this.m_strName = "ActionUniqueSkill";
            this.m_description = "ActionUniqueSkill。(叶子节点不能添加子节点)";
        }
        public override ActionResult Excute(BTInput input)
        {
            base.Excute(input);
            //if (input.GetActiveEvent().cfg.actionTypeA == (int)BattleEnums.eAIActionType.UniqueAttack)
            //{
            //    input.GetActiveEvent().resetCdCount();
            //    UniqueSkillCommand command = new UniqueSkillCommand(input.GetCharacter());
            //    input.GetCharacter().ExecuteBehaviourCommand(command);
            //    float eventTime = (float)input.GetCharacter().skillControl.GetSkillWithType(SkillType.UniqueSkill).GetEffectCfg().TotalTime;
            //    input.SetEventTriggerEventTime(eventTime);
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

