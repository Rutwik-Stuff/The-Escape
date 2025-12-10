using UnityEngine;
[System.Serializable]
public class Room
{
    public int playerCount;
    public string name;
    public string host;
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

    public Room(string name, string host, string id, int playerCount){
         //to display others' rooms
        this.name = name;
        this.id = id;
        this.playerCount = playerCount;
        this.host = host;
        Debug.Log("constructor");
    }

}