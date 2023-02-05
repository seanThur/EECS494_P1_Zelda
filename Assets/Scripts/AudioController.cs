using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController audioInstance;
    [SerializeField] private AudioSource musicSource, effectSource;
    [SerializeField] public AudioClip music, rupee, heartKey, arrowBoomerang, bombBlow, bombDrop, enemyDie, enemyHit, fanfare,
    linkDie, linkHurt, lowHealth, refillLoop, shield, swordCombined, sword, text;

    private void Awake()
    {
        if(audioInstance == null)
        {
            audioInstance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        musicSource.loop = true;
        musicSource.Play();
    }

    public void playEffect(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

}
