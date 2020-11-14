using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class BNodeComposite : BNode
    {
        public BNodeComposite()
            : base()
        {
            this.m_strName = "复合节点";
            this.m_description = "复合节点，用来通过子节点，不同种类的复合节点能实现不同的效果。";
        }
        public override ActionResult Excute(BTInput input)
        {
            return base.Excute(input);
        }
    }
}
