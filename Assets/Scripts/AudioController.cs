using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip music, rupee, heart, damage, bombBlow, bombDrop, enemyDie, enemyHit, fanfare, die, shield, swordFull, sword;


    // Start is called before the first frame update
    void Start()
    {
        AudioClip[] clips = GetComponents<AudioClip>();

        music = clips[0];
        rupee = clips[1];
        heart = clips[2];
        damage = clips[3];
        bombBlow = clips[4];
        bombDrop = clips[5];
        enemyDie = clips[6];
        enemyHit = clips[7];
        fanfare = clips[8];
        die = clips[9];
        shield = clips[10];
        swordFull = clips[11];
        sword = clips[12];

        AudioSource.PlayClipAtPoint(music, Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}