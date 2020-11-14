using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionCheckTailCross : BNodeCondition
    {
        public ConditionCheckTailCross()
            : base()
        {
            this.m_strName = "判断后触须是否相交";
            this.m_description = "判断两条蛇的尾触须是否相交。(叶子节点不能添加子节点)";
        }
    }

}
