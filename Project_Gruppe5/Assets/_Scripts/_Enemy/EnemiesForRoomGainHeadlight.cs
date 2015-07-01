using UnityEngine;

public class EnemiesForRoomGainHeadlight : Enemies {
	public DoorOpen[] doors;
	public int[] enemiesLeftForOpen;

	public override void Less(int amnt){
		base.Less (amnt);

		for(int i = enemiesLeftForOpen.Length; i == 0; i--){
			Debug.Log (enemiesLeftForOpen.Length);
			Debug.Log(size == enemiesLeftForOpen[i]);
			if(size == enemiesLeftForOpen[i])
				doors[i].Open(true);
		}
	}
}

