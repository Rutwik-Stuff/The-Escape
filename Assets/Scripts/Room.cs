using UnityEngine;
public class Room : MonoBehaviour
{
    private string[] playerlist;
    private string name;
    private string id;
    private int password;

    public Room(string name, string id, string[] playerlist){ //to display others' rooms
        this.name = name;
        this.id = id;
        this.playerlist = playerlist;
    }
    public Room(string name, int password){ //to create own , new room
        this.name = name;
        this.password = password;
    }
}