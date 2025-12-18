using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    [Tooltip("Префаб объекта, который будет спавниться")]
    public GameObject[] objectToSpawn;

    [Tooltip("Массив пустых игровых объектов, которые служат точками спавна")]
    public GameObject[] spawnPoints;

    [Tooltip("Интервал между спавном объектов (в секундах)")]
    public float spawnInterval = 2f;

    [Header("Дополнительные настройки (необязательно)")]
    [Tooltip("Родительский объект для спавненных объектов (для порядка в иерархии)")]
    public Transform objectsParent; // Для организации в иерархии

    private Coroutine spawnRoutine;

    void Start()
    {
        // Проверка на наличие необходимых компонентов
        if (objectToSpawn == null)
        {
            Debug.LogError("Spawner: 'Object To Spawn' не назначен! Отключите Spawner.", this);
            enabled = false;
            return;
        }
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawner: 'Spawn Points' массив пуст или не назначен! Отключите Spawner.", this);
            enabled = false;
            return;
        }

        // Запускаем бесконечную корутину спавна
        spawnRoutine = StartCoroutine(SpawnObjectsRoutine());
    }

    private IEnumerator SpawnObjectsRoutine()
    {
        // Бесконечный цикл
        while (true)
        {
            // Ждем заданный интервал перед следующим спавном
            yield return new WaitForSeconds(spawnInterval);

            // Выбираем случайную точку спавна из массива
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex].transform;

            // Спавним объект
            GameObject newObject = Instantiate(objectToSpawn[Random.Range(0,objectToSpawn.Length)], spawnPoint.position, Quaternion.identity);

            // Если назначен родитель, делаем спавненный объект дочерним
            if (objectsParent != null)
            {
                newObject.transform.SetParent(objectsParent);
            }

            // Убеждаемся, что у объекта есть ObjectMover
            // (он должен быть на префабе, но эта проверка не помешает)
            ObjectMover mover = newObject.GetComponent<ObjectMover>();
            
        }
    }

    // Метод для остановки спавна (можно вызвать из других скриптов или событий)
    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            Debug.Log("Спавн остановлен.");
        }
    }
}