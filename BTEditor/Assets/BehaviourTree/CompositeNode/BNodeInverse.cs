using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class BNodeInverse : BNodeComposite
    {
        public BNodeInverse()
            : base()
        {
            this.m_strName = "反转节点";
            this.m_description = "反转节点，返回子结果相反,最多只能添加一个子节点。";
        }
        
        //excute
        public override ActionResult Excute(BTInput input)
        {
            base.Excute(input);
            ActionResult reslut = this.m_listChildren[0].RunNode(input);
            if(reslut == ActionResult.Success)
            {
                reslut = ActionResult.Failure;
            }
            else if(reslut == ActionResult.Failure)
            {
                reslut = ActionResult.Success;
            }
            return reslut;
        }

    }
}

