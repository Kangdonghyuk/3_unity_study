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
		if(eType == EUNITTYPE.E_CAR)
			CarWay(y, x);
		if(eType == EUNITTYPE.E_COW)
			CowWay(y, x);
		if(eType == EUNITTYPE.E_TNK)
			TnkWay(y, x);
		if(eType == EUNITTYPE.E_SA || eType == EUNITTYPE.E_KING)
			SaWay(y, x);

		/*for(int i=0; i<10; i++) {
			for(int j=0; j<9; j++) {
				if(team == 1 && canWay[i,j] <= 0)
					canWay[i,j] = way[i+virtualPos,j+virtualPos] * team;
				else if(team == -1 && canWay[i,j] >= 0)
					canWay[i,j] = way[i+virtualPos,j+virtualPos] * team;
			}
		}*/

		for(int i=0; i<10; i++) {
			for(int j=0; j<9; j++) {
				if(IsSameTeam(y,x,i,j) != true)
					canWay[i,j] = way[i+virtualPos,j+virtualPos] * team;
			}
		}

		return canWay;
	}

	bool IsArea(int y, int x) {
		if(y >= 0 && y < 10 && x >= 0 && x < 9)
			return true;
		return false;
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

	bool IsSameTeam(int y, int x, int i, int j) {
		if(canWay[y,x] < 0 && canWay[i,j] < 0)
			return true;
		if(canWay[y,x] > 0 && canWay[i,j] > 0)
			return true;
		return false;
	}

	EUNITTYPE GetUnitType(int y, int x) {
		return (EUNITTYPE)Mathf.Abs(canWay[y,x]); 
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
	void CarWay(int y, int x) {
		if(IsKingWay(y,x)) {
			bool isLU = true, isRU = true, isLD = true, isRD = true;
			for(int i=1; i<=2; i++) {
				for(int j=1; j<=2; j++) {
					if(Mathf.Abs(i-j) == 1) continue;
					if(isLU == true && IsKingArea(y-i, x-j) == true) {
						if(canWay[y-i,x-j] != 0) {
							way[y-i+virtualPos, x-j+virtualPos] = 9;
							isLU = false;
						}
						else way[y-i+virtualPos, x-j+virtualPos] = 9;
					}
					if(isRU == true && IsKingArea(y-i, x+j) == true) {
						if(canWay[y-i,x+j] != 0) {
							way[y-i+virtualPos, x+j+virtualPos] = 9;
							isRU = false;
						}
						else way[y-i+virtualPos, x+j+virtualPos] = 9;
					}
					if(isLD == true && IsKingArea(y+i, x-j) == true) {
						if(canWay[y+i,x-j] != 0) {
							way[y+i+virtualPos, x-j+virtualPos] = 9;
							isLD = false;
						}
						else way[y+i+virtualPos, x-j+virtualPos] = 9;
					}
					if(isRD == true && IsKingArea(y+i, x+j) == true) {
						if(canWay[y+i,x+j] != 0) {
							way[y+i+virtualPos, x+j+virtualPos] = 9;
							isRD = false;
						}
						else way[y+i+virtualPos, x+j+virtualPos] = 9;
					}
				}
			}
		}

		bool isYAdd = true, isXAdd = true;
		bool isYSub = true, isXSub = true;
		for(int i=1; i<10; i++) {
			if(y+i < 10 && isYAdd == true) {
				if(canWay[y+i,x] != 0) {
					way[y+i+virtualPos,x+virtualPos] = 9;
					isYAdd = false;
				}
				else way[y+i+virtualPos,x+virtualPos] = 9;
			}
			if(y-i >= 0 && isYSub == true) {
				if(canWay[y-i,x] != 0) {
					way[y-i+virtualPos,x+virtualPos] = 9;
					isYSub = false;
				}
				else way[y-i+virtualPos,x+virtualPos] = 9;
			}
			if(x+i < 9 && isXAdd == true) {
				if(canWay[y,x+i] != 0) {
					way[y+virtualPos,x+i+virtualPos] = 9;
					isXAdd = false;
				}
				else way[y+virtualPos,x+i+virtualPos] = 9;
			}
			if(x-i >= 0 && isXSub == true) {
				if(canWay[y,x-i] != 0) {
					way[y+virtualPos,x-i+virtualPos] = 9;
					isXSub = false;
				}
				else way[y+virtualPos,x-i+virtualPos] = 9;
			}
		}
	}
	void TnkWay(int y, int x) {
		if(IsKingWay(y,x) == true) {
			bool isYX = false;
			int yPos = 0;
			if(Mathf.Abs(x-4) == 1 && y != 1 && y != 8) {
				if(y == 2 || y == 0)
					yPos = 1;
				if(y == 7 || y == 9)
					yPos = 8;
				if(canWay[yPos,4] != 0 && GetUnitType(yPos, 4) != EUNITTYPE.E_TNK)
					isYX = true;
			}
			if(isYX == true) {
				for(int i=-2; i<=2; i++) {
					for(int j=-2; j<=2; j++) {
						if(Mathf.Abs(i) == 2 && Mathf.Abs(j) == 2) {
							if(IsKingWay(y+i, x+j) != true) continue;
							if(GetUnitType(y+i, x+i) == EUNITTYPE.E_TNK) continue;
							way[y+i+virtualPos, x+j+virtualPos] = 9;
						}
					}
				}
			}
		}

		int isYAdd = 0, isXAdd = 0;
		int isYSub = 0, isXSub = 0;
		for(int i=1; i<10; i++) {
			if(y+i < 10) {
				if(canWay[y+i,x] != 0 && isYAdd == 0) {
					isYAdd = 1;
					if(GetUnitType(y+i, x) == EUNITTYPE.E_TNK) isYAdd = 2;
				}
				else if(isYAdd == 1) {
					if(GetUnitType(y+i, x) == EUNITTYPE.E_TNK) isYAdd = 2;
					else if(canWay[y+i, x] != 0) {
						way[y+i+virtualPos, x+virtualPos] = 9;
						isYAdd = 2;
					}
					else way[y+i+virtualPos, x+virtualPos] = 9;
				}
			}
			if(y-i >= 0) {
				if(canWay[y-i,x] != 0 && isYSub == 0) {
					isYSub = 1;
					if(GetUnitType(y-i, x) == EUNITTYPE.E_TNK) isYSub = 2;
				 }
				else if(isYSub == 1) {
					if(GetUnitType(y-i, x) == EUNITTYPE.E_TNK) isYSub = 2;
					else if(canWay[y-i, x] != 0) {
						way[y-i+virtualPos, x+virtualPos] = 9;
						isYSub = 2;
					}
					else way[y-i+virtualPos, x+virtualPos] = 9;
				}
			}
			if(x+i < 9) {
				if(canWay[y,x+i] != 0 && isXAdd == 0) {
					isXAdd = 1;
					if(GetUnitType(y, x+i) == EUNITTYPE.E_TNK) isXAdd = 2;
				}
				else if(isXAdd == 1) {
					if(GetUnitType(y, x+i) == EUNITTYPE.E_TNK) isXAdd = 2;
					else if(canWay[y, x+i] != 0) {
						way[y+virtualPos, x+i+virtualPos] = 9;
						isXAdd = 2;
					}
					else way[y+virtualPos, x+i+virtualPos] = 9;
				}
			}
			if(x-i >= 0) {
				if(canWay[y, x-i] != 0  && isXSub == 0) {
					isXSub = 1;
					if(GetUnitType(y, x-i) == EUNITTYPE.E_TNK) isXSub = 2;
				}
				else if(isXSub == 1) {
					if(GetUnitType(y, x-i) == EUNITTYPE.E_TNK) isXSub = 2;
					else if(canWay[y, x-i] != 0) {
						way[y+virtualPos, x-i+virtualPos] = 9;
						isXSub = 2;
					}
					else way[y+virtualPos, x-i+virtualPos] = 9;
				}
			}
		}
	}
	void HorWay(int y, int x) {
		if(IsKingWay(y,x) == true) {
			for(int i=-2; i<=2; i++) {
				for(int j=-2; j<=2; j++) {
					if(Mathf.Abs(Mathf.Abs(i) - Mathf.Abs(j)) == 1 &&
						i != 0 && j != 0) {
						bool canHor = false;
						int varX = x + j, varY = y + i;
						if(i > 1) varY = y + i - 1;
						if(i < -1) varY = y + i + 1;
						if(j > 1) varX = x + j - 1;
						if(j < -1) varX = x + j + 1;
						if(IsKingWay(varY, varX) == true) {
							if(canWay[varY,varX] == 0)
								canHor = true;
						}
						if(canHor == true)
							way[y+i+virtualPos, x+j+virtualPos] = 9;
					}
				}
			}
		}

		for(int i=-2; i<=2; i++) {
			for(int j=-2; j<=2; j++) {
				if(Mathf.Abs(Mathf.Abs(i) - Mathf.Abs(j)) == 1 &&
					i != 0 && j != 0) {
					int varX = x + j, varY = y + i;
					if(i > 0) varY = y + i - 1;
					if(i < 0) varY = y + i + 1;
					if(j > 0) varX = x + j - 1;
					if(j < 0) varX = x + j + 1;
					if(IsArea(varY, varX) == true) {
						if(canWay[varY,varX] != 0)
							continue;
					}
					way[y+i+virtualPos, x+j+virtualPos] = 9;
				}
			}
		}
	}
	void CowWay(int y, int x) {
		if(IsKingWay(y, x) == true) {
			for(int i=-3; i<=3; i++) {
				for(int j=-3; j<=3; j++) {
					if(Mathf.Abs(Mathf.Abs(i) - Mathf.Abs(j)) == 2 &&
						(Mathf.Abs(i) == 3 || Mathf.Abs(j) == 3)) {
						int varX = 0, varY = 0;
						if(i > 2) varY = -1;
						if(i < -2) varY = 1;
						if(j > 2) varX = -1;
						if(j < -2) varX = 1;

						bool canCow = true;
						int ty = y + i, tx = x + j;
						for(int t=0; t<2; t++) {
							ty += varY;
							tx += varX;
							if(IsArea(ty, tx) == true) {
								if(canWay[ty, tx] != 0)
									canCow = false;
								if(t == 1 && IsKingWay(ty, tx) != true)
									canCow = false;
							}
						}
						if(canCow == true)
							way[y+i+virtualPos, x+j+virtualPos] = 9;
					}
				}
			}
		}

		for(int i=-3; i<=3; i++) {
			for(int j=-3; j<=3; j++) {
				if(Mathf.Abs(Mathf.Abs(i) - Mathf.Abs(j)) == 1 &&
					Mathf.Abs(i) > 1 && Mathf.Abs(j) > 1) {
					int varX = 0, varY = 0;
					if(i > 0) varY = -1;
					if(i < 0) varY = 1;
					if(j > 0) varX = -1;
					if(j < 0) varX = 1;

					bool canCow = true;
					int ty = y + i, tx = x + j;
					for(int t=0; t<2; t++) {
						ty += varY;
						tx += varX;
						if(IsArea(ty, tx) == true) {
							if(canWay[ty, tx] != 0)
								canCow = false;
						}
					}
					if(canCow == true)
						way[y+i+virtualPos, x+j+virtualPos] = 9;
				}
			}
		}
	}
	void SaWay(int y, int x) {
		for(int i=-1; i<=1; i++) {
			for(int j=-1; j<=1; j++) {
				if(IsKingWay(y,x) == true) {
					if(IsKingArea(y+i, x+j) == true && IsArea(y+i, x+j) == true) {
						way[y+i+virtualPos, x+j+virtualPos] = 9;
					}
				}
				else {
					if(Mathf.Abs(i) - Mathf.Abs(j) == 0) continue;
					if(IsKingArea(y+i, x+j) == true && IsArea(y+i, x+j) == true) {
						way[y+i+virtualPos, x+j+virtualPos] = 9;
					}
				}
			}
		}
	}
}

// 각 이동에서 팀 체크 추가버전
/*
	void CarWay(int y, int x) {
		if(IsKingWay(y,x)) {
			bool isLU = true, isRU = true, isLD = true, isRD = true;
			for(int i=1; i<=2; i++) {
				for(int j=1; j<=2; j++) {
					if(Mathf.Abs(i-j) == 1) continue;
					if(isLU == true && IsKingArea(y-i, x-j) == true) {
						if(IsSameTeam(y,x,y-i,x-j) == true) isLU = false;
						else if(canWay[y-i,x-j] != 0) {
							way[y-i+virtualPos, x-j+virtualPos] = 9;
							isLU = false;
						}
						else way[y-i+virtualPos, x-j+virtualPos] = 9;
					}
					if(isRU == true && IsKingArea(y-i, x+j) == true) {
						if(IsSameTeam(y,x,y-i,x+j) == true) isRU = false;
						else if(canWay[y-i,x+j] != 0) {
							way[y-i+virtualPos, x+j+virtualPos] = 9;
							isRU = false;
						}
						else way[y-i+virtualPos, x+j+virtualPos] = 9;
					}
					if(isLD == true && IsKingArea(y+i, x-j) == true) {
						if(IsSameTeam(y,x,y+i,x-j) == true) isLD = false;
						else if(canWay[y+i,x-j] != 0) {
							way[y+i+virtualPos, x-j+virtualPos] = 9;
							isLD = false;
						}
						else way[y+i+virtualPos, x-j+virtualPos] = 9;
					}
					if(isRD == true && IsKingArea(y+i, x+j) == true) {
						if(IsSameTeam(y,x,y+i,x+j) == true) isRD = false;
						else if(canWay[y+i,x+j] != 0) {
							way[y+i+virtualPos, x+j+virtualPos] = 9;
							isRD = false;
						}
						else way[y+i+virtualPos, x+j+virtualPos] = 9;
					}
				}
			}
		}

		bool isYAdd = true, isXAdd = true;
		bool isYSub = true, isXSub = true;
		for(int i=1; i<10; i++) {
			if(y+i < 10 && isYAdd == true) {
				if(IsSameTeam(y,x,y+i,x) == true) isYAdd = false;
				else if(canWay[y+i,x] != 0) {
					way[y+i+virtualPos,x+virtualPos] = 9;
					isYAdd = false;
				}
				else way[y+i+virtualPos,x+virtualPos] = 9;
			}
			if(y-i >= 0 && isYSub == true) {
				if(IsSameTeam(y,x,y-i,x) == true) isYSub = false;
				else if(canWay[y-i,x] != 0) {
					way[y-i+virtualPos,x+virtualPos] = 9;
					isYSub = false;
				}
				else way[y-i+virtualPos,x+virtualPos] = 9;
			}
			if(x+i < 9 && isXAdd == true) {
				if(IsSameTeam(y,x,y,x+i) == true) isXAdd = false;
				else if(canWay[y,x+i] != 0) {
					way[y+virtualPos,x+i+virtualPos] = 9;
					isXAdd = false;
				}
				else way[y+virtualPos,x+i+virtualPos] = 9;
			}
			if(x-i >= 0 && isXSub == true) {
				if(IsSameTeam(y,x,y,x-i) == true) isXSub = false;
				else if(canWay[y,x-i] != 0) {
					way[y+virtualPos,x-i+virtualPos] = 9;
					isXSub = false;
				}
				else way[y+virtualPos,x-i+virtualPos] = 9;
			}
		}
	}
	void TnkWay(int y, int x) {
		if(IsKingWay(y,x) == true) {
			bool isYX = false;
			int yPos = 0;
			if(Mathf.Abs(x-4) == 1 && y != 1 && y != 8) {
				if(y == 2 || y == 0)
					yPos = 1;
				if(y == 7 || y == 9)
					yPos = 8;
				if(canWay[yPos,4] != 0 && GetUnitType(yPos, 4) != EUNITTYPE.E_TNK)
					isYX = true;
			}
			if(isYX == true) {
				for(int i=-2; i<=2; i++) {
					for(int j=-2; j<=2; j++) {
						if(Mathf.Abs(i) == 2 && Mathf.Abs(j) == 2) {
							if(IsKingWay(y+i, x+j) != true) continue;
							if(IsSameTeam(y, x, y+i, x+i) == true) continue;
							if(GetUnitType(y+i, x+i) == EUNITTYPE.E_TNK) continue;
							way[y+i+virtualPos, x+j+virtualPos] = 9;
						}
					}
				}
			}
		}

		int isYAdd = 0, isXAdd = 0;
		int isYSub = 0, isXSub = 0;
		for(int i=1; i<10; i++) {
			if(y+i < 10) {
				if(GetUnitType(y+i, x) != EUNITTYPE.E_TNK &&
				 canWay[y+i,x] != 0 && isYAdd == 0)
					isYAdd = 1;
				else if(isYAdd == 1) {
					if(IsSameTeam(y, x, y+i, x) == true) isYAdd = 2;
					else if(GetUnitType(y+i, x) == EUNITTYPE.E_TNK) isYAdd = 2;
					else if(canWay[y+i, x] != 0) {
						way[y+i+virtualPos, x+virtualPos] = 9;
						isYAdd = 2;
					}
					else way[y+i+virtualPos, x+virtualPos] = 9;
				}
			}
			if(y-i >= 0) {
				if(GetUnitType(y-i, x) != EUNITTYPE.E_TNK &&
				 canWay[y-i,x] != 0  && isYSub == 0)
					isYSub = 1;
				else if(isYSub == 1) {
					if(IsSameTeam(y, x, y-i, x) == true) isYSub = 2;
					else if(GetUnitType(y-i, x) == EUNITTYPE.E_TNK) isYSub = 2;
					else if(canWay[y-i, x] != 0) {
						way[y-i+virtualPos, x+virtualPos] = 9;
						isYSub = 2;
					}
					else way[y-i+virtualPos, x+virtualPos] = 9;
				}
			}
			if(x+i < 9) {
				if(GetUnitType(y, x+i) != EUNITTYPE.E_TNK &&
				 canWay[y,x+i] != 0 && isXAdd == 0)
					isXAdd = 1;
				else if(isXAdd == 1) {
					if(IsSameTeam(y, x, y, x+i) == true) isXAdd = 2;
					else if(GetUnitType(y, x+i) == EUNITTYPE.E_TNK) isXAdd = 2;
					else if(canWay[y, x+i] != 0) {
						way[y+virtualPos, x+i+virtualPos] = 9;
						isXAdd = 2;
					}
					else way[y+virtualPos, x+i+virtualPos] = 9;
				}
			}
			if(x-i >= 0) {
				if(GetUnitType(y, x-i) != EUNITTYPE.E_TNK &&
				 canWay[y, x-i] != 0  && isXSub == 0)
					isXSub = 1;
				else if(isXSub == 1) {
					if(IsSameTeam(y, x, y, x-i) == true) isXSub = 2;
					else if(GetUnitType(y, x-i) == EUNITTYPE.E_TNK) isXSub = 2;
					else if(canWay[y, x-i] != 0) {
						way[y+virtualPos, x-i+virtualPos] = 9;
						isXSub = 2;
					}
					else way[y+virtualPos, x-i+virtualPos] = 9;
				}
			}
		}
	} */