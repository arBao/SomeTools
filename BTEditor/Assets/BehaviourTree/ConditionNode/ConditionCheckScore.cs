using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionCheckScore : BNodeCondition
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

        public ConditionCheckScore()
            : base()
        {
            this.m_strName = "判断自分数和敌分数";
            this.m_description = "判断自分数是否小于敌分数。(叶子节点不能添加子节点)";
        }
    }

}
