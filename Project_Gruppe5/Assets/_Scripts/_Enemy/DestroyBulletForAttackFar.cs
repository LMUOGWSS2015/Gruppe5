using UnityEngine;
using System.Collections;

public class DestroyBulletForAttackFar : DestroyOnContact{
	protected override void Awake(){
		tags.Add ("Bullet");
	}
}
