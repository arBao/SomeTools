using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class BNodeRoot : BNode
    {
        [ShowInEditorUI]
        public string TreeName
        {
            get
            {
                return this.m_strName;
            }
            set
            {
                this.m_strName = value;
            }
        }
        public BNodeRoot()
            : base()
        {
            this.m_strName = "Tree";
            this.m_description = "根节点，最多只能添加一个子节点。";
        }
        public override ActionResult Excute(BTInput input)
        {
            base.Excute(input);
            if (this.m_listChildren.Count == 0)
                return ActionResult.Success;

            BNode node = this.m_listChildren[0];
            ActionResult res = node.RunNode(input);
            return res;
        }
        //public override void DeleteSelf()
        //{
        //    Debug.LogError("do nothing");
        //}

    }
}
