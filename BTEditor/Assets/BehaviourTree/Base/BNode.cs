using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameAI
{
    public enum ActionResult
    {
        None,
        Running,
        Failure,
        Success
    }

    public class BNode
    {
        protected string m_strType;
        protected string m_strName = "node";
        protected string m_description = "it's a node.";
        public int m_nodeId = 0;

        protected BNode m_parent;
        protected List<BNode> m_listChildren = new List<BNode>();

        public string NodeName
        {
            get { return m_strName; }
            set { m_strName = value; }
        }

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        public BNode ParentNode
        {
            get { return m_parent; }
        }

        public List<BNode> ListChildren
        {
            get
            {
                return m_listChildren;
            }
        }

        public int NodeID
        {
            get { return m_nodeId; }
            set { m_nodeId = value; }
        }

        public BNode()
        {
            m_strType = this.GetType().FullName;
            m_strName = this.GetType().Name;

        }

        #region 行为树逻辑部分

        public virtual void OnEnter(BTInput input)
        {

        }

        public virtual ActionResult Excute(BTInput input)
        {
            //if (input.GetCharacter().m_playerCamp == BattleCommon.ePlayerCamp.Enemy)
            //{
            //    //Debug.LogError("-----------Excute  " + m_strName);
            //}
            
            return ActionResult.Success;
        }

        public virtual void OnExit(BTInput input)
        {

        }
        private ActionResult m_eState;
        public ActionResult RunNode(BTInput input)
        {
            if (this.m_eState == ActionResult.None)
            {
                this.OnEnter(input);
                this.m_eState = ActionResult.Running;
            }
            ActionResult res = this.Excute(input);

            if (res != ActionResult.Running)
            {
                this.OnExit(input);
                this.m_eState = ActionResult.None;
            }
            return res;
        }

        public virtual void AddChild(BNode child)
        {
            child.m_parent = this;
            this.m_listChildren.Add(child);
        }

        public virtual void InsertChild(BNode child,int index)
        {
            child.m_parent = this;
            this.m_listChildren.Insert(index, child);
        }

        #endregion

        #region 编辑器部分

        public string GetTypeName()
        {
            return this.m_strType;
        }

        public void SetTypeName(string type)
        {
            this.m_strType = type;
        }
        public string GetName()
        {
            return m_strName;
        }

        public virtual void DeleteSelf()
        {
            m_parent.m_listChildren.Remove(this);
        }

        public virtual void CopySelf()
        {

        }

        public virtual void RemoveChild(BNode child)
        {
            this.m_listChildren.Remove(child);
        }

        public virtual void InsertChild(BNode prenode, BNode node)
        {
            int index = this.m_listChildren.FindIndex((a) => { return a == prenode; });
            this.m_listChildren.Insert(index, node);
        }
        public virtual void ReplaceChild(BNode prenode, BNode node)
        {
            int index = this.m_listChildren.FindIndex((a) => { return a == prenode; });
            this.m_listChildren[index] = node;
        }

        #endregion
    }
}

