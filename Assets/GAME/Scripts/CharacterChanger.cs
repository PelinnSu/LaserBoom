using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    private GameObject player;

    private void Awake()
    {
        player = gameObject; // current player
    }

    private void OnEnable()
    {
        PlayerWaypointCounter.AllLapsFinishedAction += OnAllLapsCompleted;
    }

    private void OnDisable()
    {
        PlayerWaypointCounter.AllLapsFinishedAction -= OnAllLapsCompleted;
    }

    private void OnAllLapsCompleted()
    {
        GameObject newCharacter = Instantiate(characterPrefab, player.transform.position, player.transform.rotation);

        player.SetActive(false);
        player = newCharacter;
    }
}
