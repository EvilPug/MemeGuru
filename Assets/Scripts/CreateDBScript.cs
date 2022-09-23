using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreateDBScript : MonoBehaviour {

	public Text DebugText;

	// Use this for initialization
	void Start () {
		StartSync();
	}

    private void StartSync()
    {
        var ds = new DataService("tempDatabase.db");
        ds.CreateDB();
        
        var units = ds.GetUnits ();
        ToConsole (units);
        units = ds.GetUnitsNamedTest ();
        ToConsole("Searching for Test ...");
        ToConsole (units); 
    }
	
	private void ToConsole(IEnumerable<Unit> units){
		foreach (var person in units) {
			ToConsole(person.ToString());
		}
	}
	
	private void ToConsole(string msg){
		DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}
}
