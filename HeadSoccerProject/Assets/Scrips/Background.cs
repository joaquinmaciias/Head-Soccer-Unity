using UnityEngine;
using System.Collections;

public class BGScaler : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // Obtenemos el componente SpriteRenderer del objeto al que está adjunto este script.
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // Obtenemos la escala actual del objeto.
        Vector3 tempScale = transform.localScale;

        // Obtenemos la altura y el ancho del SpriteRenderer (fondo).
        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;

        // Obtenemos la altura del mundo de juego en función del tamaño de la cámara ortográfica.
        float worldHeight = Camera.main.orthographicSize * 2f;

        // Calculamos el ancho del mundo de juego en función de la relación de aspecto de la pantalla.
        float worldWidth = worldHeight * Screen.width / Screen.height;

        // Escalamos el objeto en función de la relación entre el tamaño del mundo y el tamaño del fondo.
        // Esto asegura que el fondo llene el área de juego independientemente de la relación de aspecto.
        tempScale.y = worldHeight / height;
        tempScale.x = worldWidth / width;

        // Aplicamos la escala actualizada al objeto.
        transform.localScale = tempScale;
    }
}





