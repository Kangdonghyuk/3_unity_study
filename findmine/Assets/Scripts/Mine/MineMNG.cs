using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMNG : MonoBehaviour {
    public static MineMNG I;

    const int minesSize = 10;
    const int mineCount = 10;

    Mine[][] mines;
    GameObject minePrefab;
    GameObject mineTempObject;


    private void Awake()
    {
        I = this;
        minePrefab = (GameObject)Resources.Load("Prefabs/Mine/Mine");
    }

    // Use this for initialization
    void Start () {
        mines = new Mine[minesSize][];
        for (int i = 0; i < minesSize; i++)
        {
            mines[i] = new Mine[minesSize];
            for (int j = 0; j < minesSize; j++) {
                mineTempObject = (GameObject)Instantiate(minePrefab);
                mineTempObject.transform.parent = transform;
                mines[i][j] = mineTempObject.GetComponent<Mine>();
                mines[i][j].Create(minesSize, i, j);
            }
        }

        int mCount = 0;
        while(mCount < mineCount) {
            int x = Random.Range(0, 10);
            int y = Random.Range(0, 10);
            if (mines[y][x].IsMine()) continue;
            mines[y][x].SetMine();
            mCount++;
        }

        for (int i = 0; i < minesSize; i++) {
            for (int j = 0; j < minesSize; j++) {
                if (mines[i][j].IsMine() == false)
                {
                    int count = 0;
                    for (int ti = i - 1; ti <= i + 1; ti++)
                    {
                        for (int tj = j - 1; tj <= j + 1; tj++)
                        {
                            if (ti == i && tj == j) continue;
                            if (ti < 0 || ti >= minesSize || tj < 0 || tj >= minesSize) continue;
                            if (mines[ti][tj].IsMine() == true) count++;
                        }
                    }
                    mines[i][j].SetCount(count);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickMine(int y, int x) {
        mines[y][x].SetShow(true);
        if (mines[y][x].IsMine() == false) {
            if (mines[y][x].GetCount() == 0) OpenAround(y, x);
        }
    }

    void OpenAround(int y, int x) {
		for (int ti = y - 1; ti <= y + 1; ti++)
		{
			for (int tj = x - 1; tj <= x + 1; tj++)
			{
				if (ti == y && tj == x) continue;
				if (ti < 0 || ti >= minesSize || tj < 0 || tj >= minesSize) continue;
                if (mines[ti][tj].IsMine() == true) continue;
                if (Mathf.Abs(ti - y) == 1 && Mathf.Abs(tj - x) == 1)
                {
                    if (mines[ti][tj].GetCount() != 0)
                        mines[ti][tj].SetShow(true);
                }
                else
                {
					if (mines[ti][tj].GetCount() != 0)
						mines[ti][tj].SetShow(true);
                    if (mines[ti][tj].GetCount() == 0 && mines[ti][tj].IsShow() == false)
                    {
                        mines[ti][tj].SetShow(true);
                        OpenAround(ti, tj);
                    }
                }
			}
		}
    }
}
