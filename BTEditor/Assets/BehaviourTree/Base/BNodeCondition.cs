using UnityEngine;
using System.Collections;

namespace GameAI
{
    public enum Operate
    {
        Less = 1,
        LessThan = 2,
        Equal = 3,
        More = 4,
        MoreThan = 5,
    }

    public class BNodeCondition : BNode
    {
        public BNodeCondition()
            : base()
        {
            this.m_strName = "条件节点(叶子)";
            this.m_description = "条件节点，判断是否执行下一个节点（叶子节点无法添加子节点）。";
        }

        public override ActionResult Excute(BTInput input)
        {
            return base.Excute(input);
        }
    }
}
