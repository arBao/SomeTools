using UnityEngine;
using System.Collections;

namespace GameAI
{
    public sealed class BNodeIterator : BNodeComposite
    {
        private int m_CycleTime = 1;//默认为1
        [ShowInEditorUI]
        public int CycleTime
        {
            set { m_CycleTime = value; }
            get { return m_CycleTime; }
        }

        private int m_iRunningIndex;
        private int m_iRunningNum;

        public BNodeIterator()
            : base()
        {
            this.m_strName = "循环";
            this.m_description = "循环节点，把子节点执行 Num 次，当执行次数大于 Num返回成功并停止循环。如果遇到子节点返回失败，停止循环。";
        }

        //onenter
        public override void OnEnter(BTInput input)
        {
            this.m_iRunningIndex = 0;
            this.m_iRunningNum = 0;
        }

        //exceute
        public override ActionResult Excute(BTInput input)
        {
            base.Excute(input);

            for (int i = m_iRunningNum; i < m_CycleTime; i++)
            {
                for (int j = m_iRunningIndex; j < this.m_listChildren.Count; j++)
                {
                    ActionResult res = this.m_listChildren[j].RunNode(input);
                    if (res == ActionResult.Failure)
                        return ActionResult.Failure;
                    if (res == ActionResult.Success)
                    {
                        
                    }
                    else if(res == ActionResult.Running)
                    {
                        this.m_iRunningIndex = j;
                        return ActionResult.Running;
                    }
                }
                m_iRunningNum++;
            }
            return ActionResult.Success;

            //if (this.m_iRunningIndex >= this.m_listChildren.Count)
            //{
            //    return ActionResult.Failure;
            //}

            //ActionResult res = this.m_listChildren[this.m_iRunningIndex].RunNode(input);

            //if (res == ActionResult.Failure)
            //    return ActionResult.Failure;

            //if (res == ActionResult.Success)
            //{
            //    this.m_iRunningIndex++;
            //    this.m_iRunningNum++;
            //}

            //if (this.m_iRunningNum >= this.m_CycleTime)
            //    return ActionResult.Success;

            //return ActionResult.Running;
        }

    }
}
