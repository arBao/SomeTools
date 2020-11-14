using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAI
{
	public class ActionRandomRotate : BNodeAction
	{
		public ActionRandomRotate() : base()
		{
			this.m_strName = "随机旋转";
			this.m_description = "随机旋转一个角度。(叶子节点不能添加子节点)";
		}

	}
}
