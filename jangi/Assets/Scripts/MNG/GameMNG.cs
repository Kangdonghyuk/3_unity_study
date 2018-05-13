using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UNIT {
	public int y, x;
	public int team;
	public GameObject unitObject;
	public Unit unitScript;
	public EUNITTYPE unitType;

	public UNIT(GameObject unitObject, int y, int x, EUNITTYPE eType, int team) {
		this.unitObject = unitObject;
		this.y = y;
		this.x = x;
		this.team = team;
		unitType = eType;
		unitScript = unitObject.GetComponent<Unit>();
		unitScript.Create(y, x, eType, team);
	}

	public void SetYX(int y, int x) {
		this.y = y;
		this.x = x;
		unitScript.SetYX(y,x);
	}
};

public class GameMNG : MonoBehaviour {

	public static GameMNG I;
	public int[,] map = new int[10,9];
	UNIT[] units = new UNIT[32];
	GameObject unitPrefab;
	GameObject lightPrefab;
	GameObject[,] lights = new GameObject[10,9];
	int[,] canWay = new int[10,9];
	int clickX, clickY, clickIndex;

	void Awake() {
		I = this;
		unitPrefab = (GameObject)Resources.Load("Prefabs/Unit/Unit");
		lightPrefab = (GameObject)Resources.Load("Prefabs/Unit/Light");
	}
	// Use this for initialization
	void Start () {
		for(int i=0; i<10; i++) {
			for(int j=0; j<9; j++)
				map[i,j] = 0;
		}

		int count = 0;
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 0, EUNITTYPE.E_CAR, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 1, EUNITTYPE.E_HOR, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 2, EUNITTYPE.E_COW, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 3, EUNITTYPE.E_SA, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 5, EUNITTYPE.E_SA, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 6, EUNITTYPE.E_COW, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 7, EUNITTYPE.E_HOR, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 0, 8, EUNITTYPE.E_CAR, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 1, 4, EUNITTYPE.E_KING, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 2, 1, EUNITTYPE.E_TNK, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 2, 8, EUNITTYPE.E_TNK, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 3, 0, EUNITTYPE.E_SOL, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 3, 2, EUNITTYPE.E_SOL, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 3, 4, EUNITTYPE.E_SOL, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 3, 6, EUNITTYPE.E_SOL, -1);
		units[count++] = new UNIT((GameObject)Instantiate(unitPrefab), 3, 8, EUNITTYPE.E_SOL, -1);

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
			map[units[i].y, units[i].x] = ((int)units[i].unitType * units[i].team);
			units[i].unitObject.transform.parent = transform;
		}
	}

	void CreateLight(int y, int x) {
		lights[y,x] = (GameObject)Instantiate(lightPrefab);
		lights[y,x].transform.position = new Vector3(
			(-4f * 1.31f) + (x * 1.31f), (3.81f) + (0.85f * -y), 0.0f
		);
	}
	
	public void ClickUnit(int y, int x, GameObject cUnit) {
		ClearLight();
			
		canWay = UnitWay.I.GetWay(y,x,EUNITTYPE.E_SOL);
		//canWay = UnitWay.I.GetWay(y,x,(EUNITTYPE)Mathf.Abs(map[y,x]));
		for(int i=0; i<10; i++) {
			for(int j=0; j<9; j++) {
				if(Mathf.Abs(canWay[i,j]) == 9) 
					CreateLight(i, j);
			}
		}
		for(int i=0; i<32; i++) {
			if(cUnit == units[i].unitObject)
			{
				clickIndex = i;
				break;
			}
		}
		clickY = y;
		clickX = x;
	}

	public void ClickLight(GameObject light) {
		map[clickY,clickX] = 0;
		for(int y=0; y<10; y++) {
			for(int x=0; x<9; x++) {
				if(lights[y,x] == light) {
					map[y,x] = ((int)units[clickIndex].unitType * units[clickIndex].team);
					units[clickIndex].SetYX(y,x);
					break;
				}
			}
		}
		ClearLight();
	}

	void ClearLight() {
		for(int i=0; i<10; i++) {
			for(int j=0; j<9; j++) {
				Destroy(lights[i,j]);
				lights[i,j] = null;
			}
		}
	}
}
