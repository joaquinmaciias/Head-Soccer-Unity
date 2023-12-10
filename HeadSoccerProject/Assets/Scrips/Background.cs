using UnityEngine;
using System.Collections;

public class BGScaler : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // Obtenemos el componente SpriteRenderer del objeto al que est� adjunto este script.
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // Obtenemos la escala actual del objeto.
        Vector3 tempScale = transform.localScale;

        // Obtenemos la altura y el ancho del SpriteRenderer (fondo).
        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;

        // Obtenemos la altura del mundo de juego en funci�n del tama�o de la c�mara ortogr�fica.
        float worldHeight = Camera.main.orthographicSize * 2f;

        // Calculamos el ancho del mundo de juego en funci�n de la relaci�n de aspecto de la pantalla.
        float worldWidth = worldHeight * Screen.width / Screen.height;

        // Escalamos el objeto en funci�n de la relaci�n entre el tama�o del mundo y el tama�o del fondo.
        // Esto asegura que el fondo llene el �rea de juego independientemente de la relaci�n de aspecto.
        tempScale.y = worldHeight / height;
        tempScale.x = worldWidth / width;

        // Aplicamos la escala actualizada al objeto.
        transform.localScale = tempScale;
    }
}





