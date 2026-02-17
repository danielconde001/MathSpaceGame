using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioClip mainMenuBgm;
    [SerializeField] AudioClip inGameBgm;

    [SerializeField] AudioClip[] playerShootSFX;
    [SerializeField] AudioClip enemyShootSFX;
    [SerializeField] AudioClip enemyAlarmSFX;
    [SerializeField] AudioClip enemyFlyInSFX;
    [SerializeField] AudioClip levelUpSFX;
    [SerializeField] AudioClip recievePowerupSFX;
    [SerializeField] AudioClip spaceshipAmbienceSFX;
    [SerializeField] AudioClip playerHitSFX;
    [SerializeField] AudioClip spaceshipDeathSFX;

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

    public void PlayPlayerShootSFX()
    {
        if (playerShootSFX.Length > 0)
        {
            int rnd = Random.Range(0, playerShootSFX.Length);
            PlayerManager.Instance.GetPlayer().ShootAudioSource.PlayOneShot(playerShootSFX[rnd]);
        }
    }

    public void PlayEnemyShootSFX(AudioSource p_audioSource)
    {
        p_audioSource.PlayOneShot(enemyShootSFX);
    }

    public void PlaySpaceshipAmbienceSFX()
    {
        if (spaceshipAmbienceSFX != null)
        {
            PlayerManager.Instance.GetPlayer().
                SpaceshipAmbienceAudioSource.PlayOneShot(spaceshipAmbienceSFX);
        }
    }

    public void PlayEnemyAlarmSFX()
    {
        sfxSource.PlayOneShot(enemyAlarmSFX);
    }

    public void PlayEnemyFlyInSFX()
    {
        sfxSource.PlayOneShot(enemyFlyInSFX);
    }

    public void PlayHitSFX(Vector3 p_location)
    {
        GameObject hitSfxObj = Resources.Load<GameObject>("Audio/HitSFX");
        Instantiate(hitSfxObj, p_location, Quaternion.identity);
    }

    public void PlayPlayerHitSFX()
    {
        sfxSource.PlayOneShot(playerHitSFX);
    }

    public void PlayEnemyDeathSFX()
    {
        sfxSource.PlayOneShot(spaceshipDeathSFX);
    }

    public void PlayPlayerDeathSFX()
    {
        sfxSource.PlayOneShot(spaceshipDeathSFX);
    }

    public void PlaySpaceCrystalDeathSFX(Vector3 p_location)
    {
        GameObject hitSfxObj = Resources.Load<GameObject>("Audio/CrystalDeathSFX");
        Instantiate(hitSfxObj, p_location, Quaternion.identity);
    }
}
