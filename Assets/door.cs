using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteract : MonoBehaviour
{
    public Animator animator;
    public Transform player;

    public float interactDistance = 3f;

    public GameObject textUI;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    private bool isPlayerNear = false;
    private bool isBusy = false;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        isPlayerNear = distance <= interactDistance;

        if (textUI != null)
            textUI.SetActive(isPlayerNear && !isBusy);

        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isBusy)
        {
            StartCoroutine(OpenAndClose());
        }
    }

    IEnumerator OpenAndClose()
    {
        isBusy = true;

        // открыть
        animator.Play("DoorOpen");

        if (openSound != null && audioSource != null)
            audioSource.PlayOneShot(openSound);

        yield return new WaitForSeconds(5f);

        // закрыть
        animator.Play("DoorClose");

        if (closeSound != null && audioSource != null)
            audioSource.PlayOneShot(closeSound);

        yield return new WaitForSeconds(1f);

        isBusy = false;
    }
}