using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionCheckTailAndHeadCross : BNodeCondition
    {
        public ConditionCheckTailAndHeadCross()
            : base()
        {
            this.m_strName = "判断自后须是否与敌前须相交";
            this.m_description = "判断自身后须是否与敌蛇前须相交。(叶子节点不能添加子节点)";
        }
    }

}
