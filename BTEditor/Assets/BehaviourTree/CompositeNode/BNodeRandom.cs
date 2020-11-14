using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;


namespace GameAI
{
    public sealed class BNodeRandom : BNodeComposite
    {
        private int m_iRunningIndex;

        public BNodeRandom()
            : base()
        {
            this.NodeName = "随机";
            this.m_description = "随机执行子节点的其中一个，返回被执行的子节点的执行结果。";
        }

        public override void OnEnter(BTInput input)
        {
            this.m_iRunningIndex = Random.Range(0, this.m_listChildren.Count);
            base.OnEnter(input);
        }

        //excute
        public override ActionResult Excute(BTInput input)
        {
            base.Excute(input);
            return this.m_listChildren[this.m_iRunningIndex].RunNode(input);
        }
    }

}