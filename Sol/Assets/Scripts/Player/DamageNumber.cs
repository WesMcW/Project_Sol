using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageNumber : MonoBehaviour{

    public TextMeshProUGUI text;
    Color color;

    public float spin = 1f;
    public float growTime = .25f;
    public float holdTime = .5f;
    public float spinOutTime = .5f;

    void Start(){
        //Test Values
        //SetValues(Random.Range(3, 15), Random.Range(.01f, 1f), false);

        StartCoroutine(Animate());
    }

    /// <summary>
    /// To use this, it simply needs to be instantiated by either whatever is dealing
    /// the damage or whatever is getting hit. Most likely it would be best to include
    /// this into the IEnemy's TakeHit function. All enemies will then need a reference
    /// to this prefab.
    /// 
    /// After it is instantiated, whatever instantiated it needs to call SetValues. Then
    /// the rest is handled by this script.
    /// </summary>

    public void SetValues(int damage, float percentHealthLeft, bool criticalHit) {
        float r, g, b;
        if (percentHealthLeft > .5) {
            r = (50f - 2f * percentHealthLeft * 150f) / 255f; //(scales from 50 - 200)
            g = 200f / 255f;
            b = 50f / 255f;
        }else {
            r = 200f / 255f;
            g = (50f + 2f * percentHealthLeft * 150f) / 255f; //(scales from 200 - 50);
            b = 50f / 255f;
        }
        color = new Color(r, g, b);
        text.color = color;

        string output = damage.ToString();

        if (criticalHit) {
            output += "!";
            text.fontStyle = FontStyles.Bold;
        }

        text.text = output;
    }

    IEnumerator Animate(){

        transform.localScale = new Vector3(0, 0, 0);
        if (Random.Range(0f, 100f) < 50f){
            spin *= -1;
        }

        while (transform.localScale.x < 1f){
            float growthFactor = Mathf.Clamp((1 / growTime) * Time.deltaTime, 0, 1);
            transform.localScale += new Vector3(growthFactor, growthFactor, growthFactor);
            yield return null;
        }

        float freezeUntill = Time.time + holdTime;
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), 0);
        while (Time.time < freezeUntill){
            transform.Translate(direction * Time.deltaTime);
            yield return null;
        }

        //yield return new WaitForSeconds(holdTime);

        while (transform.localScale.x > 0f){
            float growthFactor = Mathf.Clamp((1 / spinOutTime) * Time.deltaTime, 0, 1);
            transform.localScale -= new Vector3(growthFactor, growthFactor, growthFactor);

            transform.position -= new Vector3(0, 1 * Time.deltaTime, 0);
            transform.Rotate(0, 0, spin * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
        
    }

}
