using TMPro;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public int points;
    public bool increasePts = true;
    public string pointsPreText = "Points: ";
    public string collectTag = "collect";
    public TextMeshProUGUI pointsText;

    void Start()
    {
        points = 0;
        if(pointsText != null) pointsText.text = pointsPreText + points.ToString();
    }
    void CollectCoin()
    {
        if (!increasePts) return;
        points++;
        pointsText.text = pointsPreText + points.ToString();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(collectTag)) return;
        CollectCoin();
        Destroy(other.gameObject);
    }

}
