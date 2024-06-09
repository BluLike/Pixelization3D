using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractScript : MonoBehaviour
{
    public GameObject Player;
    private Animator PlayerAnimator;

    public Animator FadeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = Player.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.F))
        {
            Vector3 lookPos = new (transform.position.x, Player.transform.position.y, transform.position.z);
            Player.transform.LookAt(lookPos);
            PlayerAnimator.SetTrigger("interact");
            FadeAnimator.SetTrigger("Start");

            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(LevelIndex);
    }
}
