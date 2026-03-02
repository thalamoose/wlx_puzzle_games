using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtendedHowToPlayPanel : HowToPlayPanel 
{
	//public Text howToPlayDescriptionTextLine2;
	//public List<string> howToPlayLine1Text;
	//public List<string> howToPlayLine2Text;

	public override void NextHowToPlayPage ()
	{
		SoundManager.Instance.PlaySoundEffect (SoundManager.GENERIC_BUTTON, false);
		base.NextHowToPlayPage ();
	}

	public override void PrevHowToPlayPage ()
	{
		SoundManager.Instance.PlaySoundEffect (SoundManager.GENERIC_BUTTON, false);
		base.PrevHowToPlayPage ();
	}

	protected override void SetHowToPlayPageText ()
	{
		//howToPlayPageDescriptionText.text = howToPlayLine1Text [currentHowToPlayPageIndex];
		//howToPlayDescriptionTextLine2.text = howToPlayLine2Text [currentHowToPlayPageIndex];
	}
}
