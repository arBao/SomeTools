using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class BNodeDecorator : BNode
    {
        public BNodeDecorator()
            : base()
        {
            this.m_strName = "装饰节点(叶子)";
            this.m_description = "装饰节点，用来处理流经此节点数据（叶子节点无法添加子节点）。";
        }

        public override ActionResult Excute(BTInput input)
        {
            return base.Excute(input);
        }
    }
}
