using UnityEngine;
[System.Serializable]
public class Room
{
    public int playerCount;
    public string name;
    public string id;
    public string password;

    public Room(string name, string id, int playerCount, string password){
         //to display others' rooms
        this.name = name;
        this.id = id;
        this.playerCount = playerCount;
        this.password = password;
        Debug.Log("constructor");
    }
}