using System.Collections;
using System.Collections.Generic;

namespace GameAI
{
    /// <summary>
    /// 顺序节点
    /// </summary>
    public sealed class BNodeSequence : BNodeComposite
    {
        private int m_iRuningIndex;

        public BNodeSequence()
            : base()
        {
            this.m_strName = "顺序";
            this.m_description = "顺序节点，按顺序（自上而下）执行子节点，如果遇到执行失败的子节点，则返回失败给父节点。如果全部子节点成功执行，返回成功给父节点。";
        }

        //on enter
        public override void OnEnter(BTInput input)
        {
            this.m_iRuningIndex = 0;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override ActionResult Excute(BTInput input)
        {
            base.Excute(input);
            //if (this.m_iRuningIndex >= this.m_listChildren.Count)
            //{
            //    return ActionResult.Success;
            //}

            for (int i = this.m_iRuningIndex; i < this.m_listChildren.Count; i++)
            {
                BNode node = this.m_listChildren[i];
                ActionResult res = node.RunNode(input);
                if (res == ActionResult.Failure)
                    return ActionResult.Failure;
                if (res == ActionResult.Success)
                {
                    
                }
                else if(res == ActionResult.Running)
                {
                    this.m_iRuningIndex = i;
                    return ActionResult.Running;
                }
            }

            return ActionResult.Success;

            //BNode node = this.m_listChildren[this.m_iRuningIndex];

            //ActionResult res = node.RunNode(input);

            //if (res == ActionResult.Failure)
            //    return ActionResult.Failure;

            //if (res == ActionResult.Success)
            //{
            //    this.m_iRuningIndex++;
            //}

            //return ActionResult.Running;
        }
    }
}
