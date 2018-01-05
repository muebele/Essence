using UnityEngine;
using System.Collections;

public class buttons : MonoBehaviour {

public void PlayButton (int scene)
	{
		
		Application.LoadLevel (scene);

	}

public void ExitButton ()
		
	{
		
		Application.Quit ();
	}

}
