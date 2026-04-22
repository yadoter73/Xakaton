using UnityEngine;
using Unity.Cinemachine; // Для Unity 6

public class CinemachineMenuOffset : MonoBehaviour
{
    [Header("Смещение (Движение)")]
    public float moveRangeX = 1.0f;     // На сколько юнитов камера отъезжает влево/вправо

    [Header("Поворот (Вращение)")]
    public float rotationRangeY = 5.0f; // На сколько градусов камера поворачивается

    [Header("Плавность")]
    public float smoothSpeed = 2.0f;    // Скорость доводки камеры

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Запоминаем стартовую точку
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        // Получаем позицию мыши от -1 до 1
        float mouseX = (Input.mousePosition.x / Screen.width) * 2f - 1f;

        // --- РАСЧЕТ ПОЗИЦИИ ---
        // Движение влево при мышке слева (mouseX отрицательный -> позиция уменьшается)
        Vector3 targetPos = initialPosition;
        targetPos.x += mouseX * moveRangeX;

        // --- РАСЧЕТ ПОВОРОТА ---
        // Инвертируем: если mouseX -1 (лево), поворот должен быть +rotationRangeY (право)
        float targetRotationY = -mouseX * rotationRangeY;
        Quaternion targetRot = initialRotation * Quaternion.Euler(0, targetRotationY, 0);

        // --- ПРИМЕНЕНИЕ (Lerp для мягкости) ---
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * smoothSpeed);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * smoothSpeed);
    }
}