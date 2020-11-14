using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionCheckDisTailAndHead : BNodeCondition
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

        public ConditionCheckDisTailAndHead()
            : base()
        {
            this.m_strName = "判断自后须和敌前须距离";
            this.m_description = "判断自身后须到相交点的距离是否小于敌蛇的前须到相交点的距离。(叶子节点不能添加子节点)";
        }
    }

}
