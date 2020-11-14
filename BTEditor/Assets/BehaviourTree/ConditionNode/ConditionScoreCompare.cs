using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionScoreCompare : BNodeCondition
    {
        private Operate m_Operate;
        private int m_CompareValue;

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

        [ShowInEditorUI]
        public int CompareValue
        {
            get
            {
                return m_CompareValue;
            }
            set
            {
                m_CompareValue = value;
            }
        }

        public ConditionScoreCompare()
            : base()
        {
            this.m_strName = "与指定值进行比较";
            this.m_description = "自身分数值与指定值进行对比。(叶子节点不能添加子节点)";
        }
    }

}
