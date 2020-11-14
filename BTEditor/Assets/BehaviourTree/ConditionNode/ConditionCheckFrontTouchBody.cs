using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionCheckFrontTouchBody : BNodeCondition
    {
        public ConditionCheckFrontTouchBody()
            : base()
        {
            this.m_strName = "判断前触须是否与蛇体碰撞";
            this.m_description = "判断自身前触须是否与敌蛇的蛇体发生碰撞。(叶子节点不能添加子节点)";
        }
    }

}
