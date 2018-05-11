using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UNIT {
	public int y, x;
	public GameObject unitObject;
	public Unit unitScript;
	public EUNITTYPE unitType;

	public UNIT(GameObject unitObject, int y, int x, EUNITTYPE eType, int team) {
		this.unitObject = unitObject;
		this.y = y;
		this.x = x;
		unitType = eType;
		unitScript = unitObject.GetComponent<Unit>();
		unitScript.Create(y, x, eType, team);
	}
};

public class GameMNG : MonoBehaviour {

	public int[,] map = new int[10,9];
	UNIT[] units = new UNIT[32];
	GameObject unitPrefab;

	void Awake() {
		unitPrefab = (GameObject)Resources.Load("Prefabs/Unit/Unit");
	}
	// Use this for initialization
	void Start () {
		for(int i=0; i<10; i++) {
			for(int j=0; j<9; j++)
				map[i,j] = 0;
		}

		int count = 0;
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 0, EUNITTYPE.E_CAR, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 1, EUNITTYPE.E_HOR, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 2, EUNITTYPE.E_COW, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 3, EUNITTYPE.E_SA, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 5, EUNITTYPE.E_SA, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 6, EUNITTYPE.E_COW, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 7, EUNITTYPE.E_HOR, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 8, EUNITTYPE.E_CAR, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 1, 4, EUNITTYPE.E_KING, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 2, 1, EUNITTYPE.E_TNK, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 2, 8, EUNITTYPE.E_TNK, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 3, 0, EUNITTYPE.E_SOL, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 3, 2, EUNITTYPE.E_SOL, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 3, 4, EUNITTYPE.E_SOL, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 3, 6, EUNITTYPE.E_SOL, 0);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 3, 8, EUNITTYPE.E_SOL, 0);

		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 9, 0, EUNITTYPE.E_CAR, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 9, 1, EUNITTYPE.E_HOR, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 9, 2, EUNITTYPE.E_COW, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 9, 3, EUNITTYPE.E_SA, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 9, 5, EUNITTYPE.E_SA, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 9, 6, EUNITTYPE.E_COW, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 9, 7, EUNITTYPE.E_HOR, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 9, 8, EUNITTYPE.E_CAR, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 8, 4, EUNITTYPE.E_KING, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 7, 1, EUNITTYPE.E_TNK, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 7, 8, EUNITTYPE.E_TNK, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 6, 0, EUNITTYPE.E_SOL, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 6, 2, EUNITTYPE.E_SOL, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 6, 4, EUNITTYPE.E_SOL, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 6, 6, EUNITTYPE.E_SOL, 1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 6, 8, EUNITTYPE.E_SOL, 1);

		for(int i=0; i < count; i++) {
			map[units[i].y, units[i].x] = (int)units[i].unitType;
			units[i].unitObject.transform.parent = transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
