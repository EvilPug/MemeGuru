using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/*
public class ExistingDBScript : MonoBehaviour {

	public Text DebugText;
	// Use this for initialization
	void Start () {
		var ds = new DataService ("game_data.db");
		//ds.CreateDB ();
		var units = ds.GetUnits ();
		ToConsole (units);
	}
	
    
	private void ToConsole(IEnumerable<Unit> units){
		foreach (var unit in units) {
			ToConsole(unit.ToString());
		}
	}

    private void ToConsole(string msg){
		DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}

}
*/