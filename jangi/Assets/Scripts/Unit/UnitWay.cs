using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWay : MonoBehaviour {
	public static UnitWay I;
	public int[,] way = new int[16,15];
	public int[,] canWay = new int[10, 9];
	int team, virtualPos = 3;

	void Awake() {
		I = this;
	}

	public int[,] GetWay(int y, int x, EUNITTYPE eType) {
		for(int i=0; i<10; i++)
			for(int j=0; j<9; j++)
				canWay[i,j] = GameMNG.I.map[i,j];
		for(int i=0; i<16; i++) 
			for(int j=0; j<15; j++)
				way[i,j] = 0;

		if(canWay[y,x] > 0)
			team = 1;
		else if(canWay[y,x] < 0)
			team = -1;

		if(eType == EUNITTYPE.E_SOL)
			SolWay(y, x);
		if(eType == EUNITTYPE.E_HOR)
			HorWay(y, x);

		for(int i=0; i<10; i++) {
			for(int j=0; j<9; j++) {
				if(team == 1 && canWay[i,j] <= 0)
					canWay[i,j] = way[i+virtualPos,j+virtualPos] * team;
				else if(team == -1 && canWay[i,j] >= 0)
					canWay[i,j] = way[i+virtualPos,j+virtualPos] * team;
			}
		}

		return canWay;
	}

	bool IsKingArea(int y, int x) {
		if(Mathf.Abs(x - 4) <= 1 &&
		 (	(y >= 7 && y <= 9) || ( y >= 0 && y <= 2))	)
			return true;
		return false;
	}

	bool IsKingWay(int y, int x) {
		if(Mathf.Abs(x-4) == 0 && (y == 1 || y == 8))
			return true;
		if(Mathf.Abs(x-4) == 1 && (y == 2 || y == 7 || y == 0 || y == 9))
			return true;
		return false;
	}

	void SolWay(int y, int x) {
		if(canWay[y,x] < 0) {
			if(IsKingArea(y,x)) {
				for(int i=y; i<=y+1; i++) {
					for(int j=x-1; j<=x+1; j++) {
						if(i == y && j == x) continue;
						if(IsKingWay(y,x) != true) continue;
						if(IsKingArea(i,j) != true) continue;
						way[i+virtualPos,j+virtualPos] = 9;
					}
				}

			}
			way[y+virtualPos+1,x+virtualPos] = 9;
			way[y+virtualPos,x-1+virtualPos] = 9;
			way[y+virtualPos,x+1+virtualPos] = 9;
		}
		else if(canWay[y,x] > 0){
			if(IsKingArea(y,x)) {
				for(int i=y; i>=y-1; i--) {
					for(int j=x-1; j<=x+1; j++) {
						if(i == y && j == x) continue;
						if(IsKingWay(y,x) != true) continue;
						if(IsKingArea(i,j) != true) continue;
						way[i+virtualPos,j+virtualPos] = 9;
					}
				}
			}
			way[y+virtualPos-1,x+virtualPos] = 9;
			way[y+virtualPos,x-1+virtualPos] = 9;
			way[y+virtualPos,x+1+virtualPos] = 9;
		}
	}

	void HorWay(int y, int x) {

	}

	void CarWay(int y, int x) {
		
	}

	void CowWay(int y, int x) {

	}

	void TnkWay(int y, int x) {

	}

}
