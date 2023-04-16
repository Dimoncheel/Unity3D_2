using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayControl : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private GameObject coin;

    private TMPro.TextMeshProUGUI coinDistanceText;
    private Image compasArrowImage;
    private Image staminaImage;

    GameObject leftHint;
    GameObject rightHint;
    GameObject staminaBar;
    GameObject coinsLocator;
    private Renderer coinRender;
    void Start()
    {
        coinDistanceText = GameObject.Find("CoinDistanceText").GetComponent<TMPro.TextMeshProUGUI>();
        
        compasArrowImage = GameObject.Find("CompasArrow").GetComponent<Image>();
        staminaImage = GameObject.Find("StaminaImage").GetComponent<Image>();
        leftHint = GameObject.Find("LeftHint");
        rightHint = GameObject.Find("RightHint");
        staminaBar = GameObject.Find("StaminaBar");
        coinsLocator = GameObject.Find("CoinsLocator");
      
        coinRender=coin.GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float coinDistance = Vector3.Distance(character.transform.position, coin.transform.position);
       
        float r = 1 / (1+ coinDistance/5);
     
        coinDistanceText.color = new Color(r, 1-r, 1-r);
        coinDistanceText.text = coinDistance.ToString("0.0");

        Vector3 coinDirection = coin.transform.position - character.transform.position;
        coinDirection.y = 0;
        
        Vector3 cameraDirection = Camera.main.transform.forward;
        cameraDirection.y = 0;
        float angleCoinCamera = Vector3.SignedAngle(coinDirection, cameraDirection, Vector3.up); 
        compasArrowImage.transform.rotation = Quaternion.Euler(0, 0, -angleCoinCamera);

        if (coinRender.isVisible)
        {
            leftHint.SetActive(false);
            rightHint.SetActive(false);
        }
        else
        {
            rightHint.SetActive(angleCoinCamera < 0);
            leftHint.SetActive(angleCoinCamera > 0);
        }

        staminaImage.fillAmount = character.Stamina;
    }

    private void LateUpdate()
    {
        coinDistanceText.enabled = GameSettings.CoinDistanceEnabled;
        if (!GameSettings.DirectionHintsEnabled)
        { 
            leftHint.SetActive(GameSettings.DirectionHintsEnabled);
            rightHint.SetActive(GameSettings.DirectionHintsEnabled);
        }
        compasArrowImage.enabled = GameSettings.DirectionHintsEnabled;
        staminaBar.SetActive(GameSettings.StaminaEnabled);
        coinsLocator.SetActive(GameSettings.CoinDistanceEnabled);
        
    }
}
