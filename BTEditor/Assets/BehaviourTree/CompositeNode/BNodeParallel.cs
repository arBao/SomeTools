using UnityEngine;
using System.Collections;

namespace GameAI
{
    public sealed class BNodeParallel : BNodeComposite
    {
        private int m_iRuningIndex;	//runting index

        public BNodeParallel()
            : base()
        {
            this.m_strName = "平行";
            this.m_description = "平行节点，按顺序(自上而下)执行所有节点(无论子节点执行成功或者失败)。执行所有子节点后返回成功。";
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
                if (res == ActionResult.Running)
                {
                    this.m_iRuningIndex = i;
                    return ActionResult.Running;
                }
            }
            return ActionResult.Success;
            //if (this.m_iRuningIndex >= this.m_listChildren.Count)
            //{
            //    return ActionResult.Success;
            //}

            //BNode node = this.m_listChildren[this.m_iRuningIndex];

            //ActionResult res = node.RunNode(input);

            //if (res != ActionResult.Running)
            //{
            //    this.m_iRuningIndex++;
            //}

            //return ActionResult.Running;
        }
    }
}
