using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BulletConfig", order = 1)]
public class BulletData : ScriptableObject
{
    public Bullet BulletConfig;
    [SerializeField]private Sprite _spriteBullet;

    public Sprite SpriteBullet { get => _spriteBullet; set => _spriteBullet = value; }
}
