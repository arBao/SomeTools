using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class ConditionCheckFrontTouchWall : BNodeCondition
    {
        public ConditionCheckFrontTouchWall()
            : base()
        {
            this.m_strName = "判断前触须是否与墙体碰撞";
            this.m_description = "判断自身前触须是否与墙体发生碰撞。(叶子节点不能添加子节点)";
        }
    }

}
