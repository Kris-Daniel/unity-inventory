using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class Inventory : MonoBehaviour
	{
		[SerializeField] bool hasEmptyStacks;
		[SerializeField] List<Stack> stacks;

		public void AddStack(Stack stack)
		{
			
		}
	}
}