using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

public void PlayButton (int scene)
	{
		
		Application.LoadLevel (scene);

	}

public void ExitButton ()
		
	{
		
		Application.Quit ();
	}

}
