using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public float WeaponSwitchSpeed = 5f;
    public Vector3 position1;
    public Vector3 position2;
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public bool Selected1;
    public bool Selected2;
    public bool Selected3;


    public GameObject ShotgunCrossHair;
    public GameObject PistolCrossHair;
    public GameObject UziCrossHair;

    public GameObject ShotgunAmmoUI;
    public GameObject PistolAmmoUI;
    public GameObject UziAmmoUI;


    public int ControllerInt = 1;
    public KeyCode ChangeWeaponControllerInput = KeyCode.JoystickButton3;

    public GameObject Minigun;
    public GameObject MiniGunCrossHair;
    public GameObject MiniGunAmmoUI;
    public bool MinigunIsEnabled;

    public Image MinigunFillerImage;

    public ScoreManager ScoreManagerScript;

    public Animator MinigunSliderAnimationObject;


    public WeaponController PistolScript;
    public ShotgunController ShotgunScript;
    public UziController UziScript;
    public MinigunController MinigunScript;


    public GameObject PistolSoundObject;
    public GameObject UziSoundObject;
    public GameObject ShotgunSoundObject;
    public GameObject MinigunSoundObject;


    float Timer;

    void Start()
    {
        ShotgunAmmoUI.SetActive(false);
        PistolAmmoUI.SetActive(true);
        StartCoroutine(SelectWeapon());
    }

    void Update()
    {
        if (MinigunIsEnabled == false)
        {
            int i = 0;

            int previousSelectedWeapon = selectedWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon <= 0)
                    selectedWeapon = transform.childCount - 1;
                else
                    selectedWeapon--;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedWeapon = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            {
                selectedWeapon = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            {
                selectedWeapon = 2;
            }

            //Controller
            if (Input.GetKeyDown(ChangeWeaponControllerInput))
            {
                ControllerInt = ControllerInt * -1;
            }


            //if (ControllerInt == 1)
            //{
            //    selectedWeapon = 0;
            //    PistolCrossHair.SetActive(true);
            //    PistolAmmoUI.SetActive(true);
            //    ShotgunCrossHair.SetActive(false);
            //    ShotgunAmmoUI.SetActive(false);
            //}
            //if (ControllerInt == -1)
            //{
            //    selectedWeapon = 1;
            //    PistolCrossHair.SetActive(false);
            //    PistolAmmoUI.SetActive(false);
            //    ShotgunCrossHair.SetActive(true);
            //    ShotgunAmmoUI.SetActive(true);
            //}

            if (selectedWeapon == 0)
            {
                PistolCrossHair.SetActive(true);
                PistolAmmoUI.SetActive(true);
                ShotgunCrossHair.SetActive(false);
                ShotgunAmmoUI.SetActive(false);
                UziAmmoUI.SetActive(false);
                UziCrossHair.SetActive(false);

                MiniGunAmmoUI.SetActive(false);
                MiniGunCrossHair.SetActive(false);
                Minigun.SetActive(false);
            }
            if (selectedWeapon == 1)
            {
                PistolCrossHair.SetActive(false);
                PistolAmmoUI.SetActive(false);
                ShotgunCrossHair.SetActive(true);
                ShotgunAmmoUI.SetActive(true);
                UziAmmoUI.SetActive(false);
                UziCrossHair.SetActive(false);
                MiniGunAmmoUI.SetActive(false);
                MiniGunCrossHair.SetActive(false);
                Minigun.SetActive(false);
            }
            if (selectedWeapon == 2)
            {
                PistolCrossHair.SetActive(false);
                PistolAmmoUI.SetActive(false);
                ShotgunCrossHair.SetActive(false);
                ShotgunAmmoUI.SetActive(false);
                UziAmmoUI.SetActive(true);
                UziCrossHair.SetActive(true);
                MiniGunAmmoUI.SetActive(false);
                MiniGunCrossHair.SetActive(false);
                Minigun.SetActive(false);
            }


            if (previousSelectedWeapon != selectedWeapon)
            {
                StartCoroutine(SelectWeapon());
                SelectWeapon();
            }

            #region Selection bools
            if (selectedWeapon == 0)
            {
                Selected1 = true;
            }
            else
            {
                Selected1 = false;
            }

            if (selectedWeapon == 1)
            {
                Selected2 = true;
            }
            else
            {
                Selected2 = false;
            }

            if (selectedWeapon == 2)
            {
                Selected3 = true;
            }
            else
            {
                Selected3 = false;
            }



            #endregion
            #region transitions
            if (Selected1 == true && weapon1.activeSelf == true)
            {
                weapon1.transform.localPosition = Vector3.Lerp(weapon1.transform.localPosition, position1, WeaponSwitchSpeed * Time.deltaTime);
            }
            else
            {
                weapon1.transform.localPosition = Vector3.Lerp(weapon1.transform.localPosition, position2, WeaponSwitchSpeed * Time.deltaTime);
            }
            if (Selected2 == true && weapon2.activeSelf == true)
            {
                weapon2.transform.localPosition = Vector3.Lerp(weapon2.transform.localPosition, position1, WeaponSwitchSpeed * Time.deltaTime);
            }
            else
            {
                weapon2.transform.localPosition = Vector3.Lerp(weapon2.transform.localPosition, position2, WeaponSwitchSpeed * Time.deltaTime);
            }
            if (Selected3 == true && weapon3.activeSelf == true)
            {
                weapon3.transform.localPosition = Vector3.Lerp(weapon3.transform.localPosition, position1, WeaponSwitchSpeed * Time.deltaTime);
            }
            else
            {
                weapon3.transform.localPosition = Vector3.Lerp(weapon3.transform.localPosition, position2, WeaponSwitchSpeed * Time.deltaTime);
            }
            #endregion
        }
        else
        {
            StartCoroutine(SelectWeapon());
            PistolCrossHair.SetActive(false);
            PistolAmmoUI.SetActive(false);
            ShotgunCrossHair.SetActive(false);
            ShotgunAmmoUI.SetActive(false);
            UziAmmoUI.SetActive(false);
            UziCrossHair.SetActive(false);

            MiniGunAmmoUI.SetActive(true);
            MiniGunCrossHair.SetActive(true);
            StartCoroutine(MinigunObject());
        }

        if (MinigunFillerImage.fillAmount == 1 && Input.GetKeyDown(KeyCode.E))
        {
            MinigunIsEnabled = true;
            MinigunSliderAnimationObject.SetBool("Start", true);

            ScoreManagerScript.MinigunValue = 0;
            MinigunFillerImage.fillAmount = Mathf.Lerp(MinigunFillerImage.fillAmount, 0, 0.5f * Time.deltaTime);
        }

        Timer += Time.deltaTime;

        if (Timer > 0.5f)
        {
            GunAudioManager(0.6f, 0.7f, 0.6f, 0.7f);
        }
        else
        {
            GunAudioManager(0f, 0f,0f,0f);
        }
    }

    IEnumerator MinigunObject()
    {
        yield return new WaitForSeconds(0.3f);
        Minigun.SetActive(true);
        yield return new WaitForSeconds(14.2f);
        Minigun.SetActive(false);
        MinigunIsEnabled = false;
    }

    IEnumerator SelectWeapon()
    {

        yield return new WaitForSeconds(0.2f);
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon && !MinigunIsEnabled)
            {
                weapon.gameObject.SetActive(true);
            }
            else if (MinigunIsEnabled)
            {
                weapon.gameObject.SetActive(false);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
                
            i++;
        }
    }

    public void GunAudioManager( float PistolVolume, float ShotgunVolume, float UziVolume, float MinigunVolume)
    {
        if (PistolScript.NowShooting == true)
        {
            GameObject InstantiatedSound = Instantiate(PistolSoundObject);
            AudioSource PistolSound = InstantiatedSound.GetComponent<AudioSource>();
            PistolSound.volume = PistolVolume;
            Destroy(InstantiatedSound, 10f);
        }

        if (ShotgunScript.NowShooting == true)
        {
            GameObject InstantiatedSound = Instantiate(ShotgunSoundObject);
            AudioSource PistolSound = InstantiatedSound.GetComponent<AudioSource>();
            PistolSound.volume = ShotgunVolume;
            Destroy(InstantiatedSound, 10f);
        }

        if (UziScript.NowShooting == true)
        {
            GameObject InstantiatedSound = Instantiate(UziSoundObject);
            AudioSource PistolSound = InstantiatedSound.GetComponent<AudioSource>();
            PistolSound.volume = UziVolume;
            Destroy(InstantiatedSound, 10f);
        }

        if (MinigunScript.NowShooting == true)
        {
            GameObject InstantiatedSound = Instantiate(MinigunSoundObject);
            AudioSource PistolSound = InstantiatedSound.GetComponent<AudioSource>();
            PistolSound.volume = MinigunVolume;
            Destroy(InstantiatedSound, 10f);
        }
    }
}
