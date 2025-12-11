using UnityEngine;
using TMPro;

public class StatsController : MonoBehaviour
{
    public TMP_Text players;
    public TMP_Text rooms;

    public void updateStats(string p, string r){
        players.text = p;
        rooms.text = r;
    }
}
