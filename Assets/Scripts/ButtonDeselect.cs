using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonDeselect : MonoBehaviour
{
    public ActiveRoomsController controller;

    public void onClick(string factory){
        if(factory == "join"){
            StartCoroutine(join());
        } else if(factory == "stop"){
            StartCoroutine(stop());
        } else if(factory == "players"){
            StartCoroutine(players());
        }
        
    }

    IEnumerator join(){
        yield return null;
        yield return null;
        yield return null;

        controller.join();
    }

    IEnumerator stop(){
        yield return null;
        yield return null;
        yield return null;

        controller.stop();
    }

    IEnumerator players(){
        yield return null;
        yield return null;
        yield return null;

        controller.players();
    }


}
