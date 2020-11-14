using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAI
{
	public class ActionMoveToTargetPos:BNodeAction
	{
		public ActionMoveToTargetPos(): base()
		{
			this.m_strName = "移动到目标位置";
			this.m_description = "移动到目标位置。(叶子节点不能添加子节点)";
		}
	}
}

