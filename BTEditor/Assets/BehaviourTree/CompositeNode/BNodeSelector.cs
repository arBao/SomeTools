using System.Collections;
using System.Collections.Generic;


namespace GameAI
{
    /// <summary>
    /// 选择器节点
    /// </summary>
    public sealed class BNodeSelector : BNodeComposite
    {
        private int m_iRuningIndex;	//runing index

        public BNodeSelector()
            : base()
        {
            this.m_strName = "选择";
            this.m_description = "选择节点，按顺序（自上而下）执行所有子节点，遇到第一个执行成功的子节点时，停止执行并返回成功给父节点。如果全部子节点返回失败，则返回失败给父节点。";
        }

        //onenter
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

            for (int i = this.m_iRuningIndex; i < this.m_listChildren.Count; i++)
            {
                BNode node = this.m_listChildren[i];
                ActionResult res = node.RunNode(input);
                if (res == ActionResult.Success)
                    return ActionResult.Success;
                if (res == ActionResult.Failure)
                {
                    
                }
                else if(res == ActionResult.Running)
                {
                    this.m_iRuningIndex = i;
                    return ActionResult.Running;
                }
            }

            return ActionResult.Failure;

            //if(this.m_iRuningIndex >= this.m_listChildren.Count)
            //{
            //    return ActionResult.Failure;
            //}

            //BNode node = this.m_listChildren[this.m_iRuningIndex];

            //ActionResult res = node.RunNode(input);

            //if(res == ActionResult.Success)
            //    return ActionResult.Success;

            //if(res == ActionResult.Failure)
            //{
            //    this.m_iRuningIndex++;
            //}
            //return ActionResult.Running;
        }
    }
}
