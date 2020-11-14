using UnityEngine;
using System.Collections;

namespace GameAI
{
	public class ConditionCheckNearbyDanger:BNodeCondition
	{
		private float m_radius = 1;
		//检测半径1;
		[ShowInEditorUI]
		public float radius
		{
			get
			{
				return m_radius;
			}
			set
			{
				m_radius = value;
			}
		}
		public ConditionCheckNearbyDanger():base()
		{
			this.m_strName = "检测附近危险";
			this.m_description = "蛇头直径*radius 范围内是否有敌对蛇/地图边界？(叶子节点不能添加子节点)";
		}
	}
}
