using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionCheckLineSegmentCross : BNodeCondition
    {
        public ConditionCheckLineSegmentCross()
            : base()
        {
            this.m_strName = "判断前触须是否相交";
            this.m_description = "ConditionCheckLineSegmentCross。(叶子节点不能添加子节点)";
        }
    }

}
