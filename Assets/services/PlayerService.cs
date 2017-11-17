using UnityEngine;
using System.Collections;

public class PlayerService : MonoBehaviour {

    public static IEnumerator streamSong(string identifier, string filename, bool isPlaying) {
        AudioSource audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        if (audioSource.isPlaying) { 
            audioSource.Stop();
        }

        if (!isPlaying) {
            string file = "https://archive.org/download/" + identifier + "/" + filename;
            Debug.Log(file);

            using (WWW request = new WWW(file)) {
                yield return request;

                audioSource.clip = WWWAudioExtensions.GetAudioClip(request, false, true, AudioType.OGGVORBIS);
                audioSource.Play();
            }
        }
    }
}
