using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionCheckDisBetweenTails : BNodeCondition
    {
        private Operate m_Operate;
        [ShowInEditorUI]
        public Operate Operate
        {
            get
            {
                return m_Operate;
            }
            set
            {
                m_Operate = value;
            }
        }

        public ConditionCheckDisBetweenTails()
            : base()
        {
            this.m_strName = "判断自后须和敌后须距离";
            this.m_description = "判断自身尾部到相交点的距离是否小于敌蛇尾部到相交点的距离。(叶子节点不能添加子节点)";
        }
    }

}
