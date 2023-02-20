using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAwayState : CardState
{
	public HideAwayState(CardController cardController) : base(cardController)
	{
	}

	public override void EnterState()
	{
		Debug.Log("Hide away");
		cardController.InactivateCard();
		
	}

}
