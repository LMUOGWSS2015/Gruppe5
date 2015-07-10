using UnityEngine;

public class Enemies : MonoBehaviour {
	protected int size = 0;

	public virtual void Less(int amnt){
		size -= amnt;
		if (size <= 0)
			this.gameObject.GetComponent<DoubleDoorsOpen> ().SetEnemiesDead(true);
	}

	public virtual void More(int amnt){
		size += amnt;
	}
}
