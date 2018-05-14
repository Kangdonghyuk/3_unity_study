using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUNITTYPE {
	E_SOL = 1,
	E_CAR,
	E_HOR,
	E_COW,
	E_TNK,
	E_SA,
	E_KING
};

public class Unit : MonoBehaviour {

	int y, x;
	public int team;
	Vector3 position;
	public EUNITTYPE unitType;
	TextMesh unitName;

	void Awake() {
		unitName = transform.GetChild(0).GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Create(int y, int x, EUNITTYPE eType, int team) {
		this.y = y;
		this.x = x;
		unitType = eType;

		position.x = (-4f * 1.31f) + (x * 1.31f);
		position.y = (3.81f) + (0.85f * -y);
		transform.position = position;

		this.team = team;
		if(team == -1) 
			unitName.color = Color.red;
		else
			unitName.color = Color.blue;

		SetName(eType);
	}

	void SetName(EUNITTYPE eType) {
		if(eType == EUNITTYPE.E_SOL) 
			unitName.text = "쫄";
		if(eType == EUNITTYPE.E_CAR) 
			unitName.text = "차";
		if(eType == EUNITTYPE.E_HOR) 
			unitName.text = "마";
		if(eType == EUNITTYPE.E_COW) 
			unitName.text = "상";
		if(eType == EUNITTYPE.E_TNK) 
			unitName.text = "포";
		if(eType == EUNITTYPE.E_SA) 
			unitName.text = "사";
		if(eType == EUNITTYPE.E_KING) 
			unitName.text = "왕";
	}

	public void SetYX(int y, int x) {
		this.y = y;
		this.x = x;

		position.x = (-4f * 1.31f) + (x * 1.31f);
		position.y = (3.81f) + (0.85f * -y);
		transform.position = position;
	}

	void OnMouseDown() {
		GameMNG.I.ClickUnit(y, x, gameObject);
	}
}
