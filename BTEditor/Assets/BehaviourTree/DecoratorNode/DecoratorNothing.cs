using UnityEngine;
using System.Collections;

namespace GameAI
{
    public sealed class DecoratorNothing : BNodeDecorator
    {
        public DecoratorNothing()
            : base()
        {
            this.m_strName = "装饰(空)";
            this.m_description = "装饰节点，不做任何事情。(叶子节点不能添加子节点)";
        }

    }
}
