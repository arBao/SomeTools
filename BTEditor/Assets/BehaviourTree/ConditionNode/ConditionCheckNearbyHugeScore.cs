using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAI
{
	public class ConditionCheckNearbyHugeScore: BNodeCondition
	{
		public ConditionCheckNearbyHugeScore():base()
		{
			this.m_strName = "检测附近大分值";
			this.m_description = "检测蛇头直径 * radiusMultiple 范围内是否有大分值 (叶子节点不能添加子节点)";
		}

		private float m_radiusMultiple = 5;
		[ShowInEditorUI]
		public float radiusMultiple
		{
			get
			{
				return m_radiusMultiple;
			}
			set
			{
				m_radiusMultiple = value;
			}
		}

	}
}

