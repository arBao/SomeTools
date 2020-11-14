using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class BNodeAction:BNode
    {
        public BNodeAction():base()
        {
            this.m_strName = "行为节点(叶子)";
            this.m_description = "行为节点，用来实现AI行为（叶子节点无法添加子节点）。";
        }

        public override ActionResult Excute(BTInput input)
        {
            return base.Excute(input);
        }
    }
}