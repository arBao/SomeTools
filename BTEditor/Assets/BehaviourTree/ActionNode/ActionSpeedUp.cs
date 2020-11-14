using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameAI
{
	public class ActionSpeedUp : BNodeAction
	{
		public ActionSpeedUp() : base()
		{
			this.m_strName = "加速";
			this.m_description = "加速。(叶子节点不能添加子节点)";
		}

		private bool m_SpeedUp = true;
		[ShowInEditorUI]
		public bool SpeedUp
		{
			get
			{
				return m_SpeedUp;
			}
			set
			{
				m_SpeedUp = value;
			}
		}
	}
}

