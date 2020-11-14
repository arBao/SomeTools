using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAI
{
	public class ActionRotateReverse:BNodeAction
	{
		public ActionRotateReverse(): base()
		{
			this.m_strName = "移动到相反方向";
			this.m_description = "转向当前朝向的相反方向。(叶子节点不能添加子节点)";
		}
	}
}

