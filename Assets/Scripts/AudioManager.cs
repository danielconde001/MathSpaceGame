using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioClip mainMenuBgm;
    [SerializeField] AudioClip inGameBgm;
    [SerializeField] AudioClip[] shootSFX;

    [SerializeField] AudioClip levelUpSFX;
    [SerializeField] AudioClip recievePowerupSFX;
    [SerializeField] AudioClip spaceshipAmbienceSFX;

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("AudioManager");
                instance = newGameObject.AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;

        if (bgmSource == null)
        {
            bgmSource = transform.Find("BGM").GetComponent<AudioSource>();
        }

        if (sfxSource == null)
        {
            sfxSource = transform.Find("SFX").GetComponent<AudioSource>();
        }
    }

    public void PlayLevelUpSFX()
    {
        if (levelUpSFX != null)
        {
            sfxSource.PlayOneShot(levelUpSFX);
        }
    }

    public void PlayReceivePowerUpSFX()
    {
        if (recievePowerupSFX != null)
        {
            sfxSource.PlayOneShot(recievePowerupSFX);
        }
    }

    public void PlayShootSFX()
    {
        if (shootSFX.Length > 0)
        {
            int rnd = Random.Range(0, shootSFX.Length);
            PlayerManager.Instance.GetPlayer().ShootAudioSource.PlayOneShot(shootSFX[rnd]);
        }
    }

    public void PlayShipAmbienceSFX()
    {
        if (spaceshipAmbienceSFX != null)
        {
            PlayerManager.Instance.GetPlayer().
                SpaceshipAmbienceAudioSource.PlayOneShot(spaceshipAmbienceSFX);
        }
    }

    public void PlayHitSFXAtLocation(Vector3 p_location)
    {
        GameObject hitSfxObj = Resources.Load<GameObject>("Audio/HitSFX");
        Instantiate(hitSfxObj, p_location, Quaternion.identity);
    }

    public void PlayExplosionSFXAtLocation(Vector3 p_location)
    {
        GameObject hitSfxObj = Resources.Load<GameObject>("Audio/ExplosionSFX");
        Instantiate(hitSfxObj, p_location, Quaternion.identity);
    }
}
