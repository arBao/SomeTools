using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAI
{
	public class ActionWait : BNodeAction
	{
		public ActionWait() : base()
		{
			this.m_strName = "等待一段时间";
			this.m_description = "等待一段时间。(叶子节点不能添加子节点)";
		}

		private float m_waitTime = 0f;

		[ShowInEditorUI]
		public float waitTime
		{
			get
			{
				return m_waitTime;
			}
			set
			{
				m_waitTime = value;
			}
		}
	}
}
