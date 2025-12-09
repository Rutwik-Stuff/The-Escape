using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonDeselect : MonoBehaviour
{
    public ActiveRoomsController controller;

    public void onClick(){
        StartCoroutine(smallDelay());
    }

    IEnumerator smallDelay(){
        yield return null;
        yield return null;
        yield return null;

        controller.join();
    }
}
