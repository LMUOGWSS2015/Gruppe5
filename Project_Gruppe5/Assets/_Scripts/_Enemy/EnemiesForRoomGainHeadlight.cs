using UnityEngine;

public class EnemiesForRoomGainHeadlight : Enemies {
	public DoorOpen[] doors;
	public int[] enemiesLeftForOpen;

	public override void Less(int amnt){
		base.Less (amnt);

		for(int i = enemiesLeftForOpen.Length-1; i >= 0; i--){
			if(size == enemiesLeftForOpen[i])
				doors[i].Open(true);
		}
	}
}

