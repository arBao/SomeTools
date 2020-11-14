using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAI
{
    public class ActionRotate : BNodeAction
    {
        private int m_rotation;

        [ShowInEditorUI]
        public int rotation
        {
            get
            {
                return m_rotation;
            }
            set
            {
                m_rotation = value;
            }
        }

        public ActionRotate() : base()
        {
            this.m_strName = "旋转";
            this.m_description = "旋转一个角度。(叶子节点不能添加子节点)";
        }

    }
}
