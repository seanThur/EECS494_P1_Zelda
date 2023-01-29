using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip music, rupee, heart, moveBlock, damage, bombBlow, bombDrop, enemyDie, enemyHit, fanfare, die, shield, swordFull, sword;


    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(music, Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}