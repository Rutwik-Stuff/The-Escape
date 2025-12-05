using UnityEngine;
[System.Serializable]
public class Room
{
    private int playerCount;
    private string name;
    private string id;
    private string password;

    public Room(string name, string id, int playerCount, string password){
         //to display others' rooms
        this.name = name;
        this.id = id;
        this.playerCount = playerCount;
        this.password = password;
        Debug.Log("constructor");
    }
}