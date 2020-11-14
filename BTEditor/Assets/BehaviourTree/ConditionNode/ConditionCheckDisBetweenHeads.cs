using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionCheckDisBetweenHeads : BNodeCondition
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

        public ConditionCheckDisBetweenHeads()
            : base()
        {
            this.m_strName = "判断自前须和敌前须距离";
            this.m_description = "判断自身头部到相交点的距离是否小于敌蛇头部到相交点的距离。(叶子节点不能添加子节点)";
        }
    }

}
