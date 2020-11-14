using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionCheckAngleRangeInTailAndHead : BNodeCondition
    {
        private int m_upperLimitAngle;
        private int m_lowerLimitAngle;

        [ShowInEditorUI]
        public int UpperLimitAngle
        {
            get { return m_upperLimitAngle; }
            set { m_upperLimitAngle = value; }
        }

        [ShowInEditorUI]
        public int LowerLimitAngle
        {
            get { return m_lowerLimitAngle; }
            set { m_lowerLimitAngle = value; }
        }

        public ConditionCheckAngleRangeInTailAndHead()
            : base()
        {
            this.m_strName = "判断自后须和敌前须角度";
            this.m_description = "判断自身后须与敌蛇前须是否在指定的角度范围。(叶子节点不能添加子节点)";
        }
    }

}
