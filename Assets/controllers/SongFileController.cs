using UnityEngine;
using UnityEngine.UI;

public class SongFileController : MonoBehaviour {

    public Text filename;
    public Text playLength;
    public string identifier;

    private Button button;
    private bool isPlaying;

    void Start() {
        isPlaying = false;
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(PlaySong);
    }

    private void PlaySong() {
        StartCoroutine(PlayerService.streamSong(identifier, filename.text, isPlaying));
        isPlaying = !isPlaying;
    }
}
