using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    bool isMine;
    bool isShow;
    int aroundMineCount;
    int x, y;
    TextMesh aroundMineCountText;
    Vector3 minePosition;
    Color mineColor;

    private void Awake()
    {
        aroundMineCountText = transform.GetChild(0).GetComponent<TextMesh>();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Create(int size, int y, int x) {
        this.x = x; this.y = y;
        minePosition.x = x * 0.6f - size / 2 * 0.6f;
        minePosition.y = y * 0.6f - size / 2 * 0.6f;
        transform.position = minePosition;

		mineColor = Color.white;
		aroundMineCountText.color = mineColor;

        SetShow(false);
		isMine = false;

		aroundMineCount = 0;
    }

    public void SetMine() {
        isMine = true;
        aroundMineCountText.text = "X";
    }
    public bool IsMine()
    {
        return isMine;
    }
    public bool IsShow() {
        return isShow;
    }
    public void SetShow(bool isShow) {
        this.isShow = isShow;
        if (isShow == true)
            mineColor.a = 1.0f;
        else if (isShow == false)
            mineColor.a = 0.0f;
        aroundMineCountText.color = mineColor;
    }
    public void SetCount(int count) {
        aroundMineCount = count;
        aroundMineCountText.text = aroundMineCount.ToString();
    }
    public int GetCount() {
        return aroundMineCount;
    }

    private void OnMouseDown()
    {
        SetShow(true);
        MineMNG.I.ClickMine(y, x);
    }
}
