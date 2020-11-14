using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionCheckHeadAndTailCross : BNodeCondition
    {
        public ConditionCheckHeadAndTailCross()
            : base()
        {
            this.m_strName = "判断自前须是否与敌后须相交";
            this.m_description = "判断自身前须是否与敌蛇后须相交。(叶子节点不能添加子节点)";
        }
    }

}
