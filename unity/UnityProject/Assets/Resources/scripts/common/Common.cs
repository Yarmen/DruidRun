using UnityEngine;
using System.Collections;


public class Common : Singleton<Common> {
	protected Common () {} // guarantee this will be always a singleton only - can't use the constructor!
 	
	public int getScreenHeight() {
		
		return Screen.height;
		
		
	}
	
	public int getScreenWidth() {
		
		return Screen.width;
		
		
	}
	
}


