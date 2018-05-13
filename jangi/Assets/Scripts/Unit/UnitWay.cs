﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWay : MonoBehaviour {
	public static UnitWay I;
	public int[,] way = new int[12,11];
	public int[,] canWay = new int[10, 9];
	int team;

	void Awake() {
		I = this;
	}

	public int[,] GetWay(int y, int x, EUNITTYPE eType) {
		for(int i=0; i<10; i++)
			for(int j=0; j<9; j++)
				canWay[i,j] = GameMNG.I.map[i,j];
		for(int i=0; i<12; i++) 
			for(int j=0; j<11; j++)
				way[i,j] = 0;

		if(canWay[y,x] > 0)
			team = 1;
		else if(canWay[y,x] < 0)
			team = -1;

		if(eType == EUNITTYPE.E_SOL)
			SolWay(y, x, eType);
		if(eType == EUNITTYPE.E_HOR)
			HorWay(y, x, eType);

		for(int i=0; i<10; i++) {
			for(int j=0; j<9; j++) {
				if(team == 1 && canWay[i,j] <= 0)
					canWay[i,j] = way[i+1,j+1] * team;
				else if(team == -1 && canWay[i,j] >= 0)
					canWay[i,j] = way[i+1,j+1] * team;
			}
		}

		return canWay;
	}

	void SolWay(int y, int x, EUNITTYPE eType) {
		if(canWay[y,x] < 0) {
			if(Mathf.Abs(x - 4) <= 1 && y >= 7 && y <= 9) {
				for(int i=y; i<y+1; i++) {
					for(int j=x-1; j<x+1; j++) {
						if(i == y && j == x) continue;
						if(Mathf.Abs(x-4) > 1) continue;
						way[y+1,x+1] = 9;
					}
				}
			}
			way[y+1+1,x+1] = 9;
			way[y+1,x-1+1] = 9;
			way[y+1,x+1+1] = 9;
		}
		else if(canWay[y,x] > 0){
			if(Mathf.Abs(x - 4) <= 1 && y >= 0 && y <= 2) {
				for(int i=y; i<y-1; i--) {
					for(int j=x-1; j<x+1; j++) {
						if(i == y && j == x) continue;
						if(Mathf.Abs(x-4) > 1) continue;
						way[y+1,x+1] = 9;
					}
				}
			}
			way[y+1-1,x+1] = 9;
			way[y+1,x-1+1] = 9;
			way[y+1,x+1+1] = 9;
		}
	}

	void HorWay(int y, int x, EUNITTYPE eType) {

	}

}
