using UnityEngine;

public class Enemies : MonoBehaviour {
	public int size = 0;

	public void Less(int amnt){
		size -= amnt;
		if (size <= 0)
			GameObject.FindGameObjectWithTag ("DDoors").GetComponent<DoubleDoorsOpen> ().enemiesDead = true;
	}

	public void More(int amnt){
		size += amnt;
	}
}
