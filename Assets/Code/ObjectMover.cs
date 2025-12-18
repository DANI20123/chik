using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [Tooltip("Скорость движения объекта вниз")]
    public float speed = 5f;

    [Tooltip("Y-координата, при достижении которой объект будет уничтожен")]
    public float destroyYPosition = -10f; // Например, уничтожать ниже камеры

    void Update()
    {
        // Двигаем объект вниз
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Проверяем, нужно ли уничтожить объект
        if (transform.position.y < destroyYPosition)
        {
            Destroy(gameObject);
        }
    }
}