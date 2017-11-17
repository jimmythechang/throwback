using UnityEngine;
using UnityEngine.UI;

public class RecordingService: MonoBehaviour {

    private bool isRecording;
    private string deviceName;

    public AudioSource audioSource;
    private AudioClip voiceover;

    // TODO: wire up the AudioRenderer so that in-game audio is recorded.
    private AudioRenderer audioRenderer;

    void Start() {

        isRecording = false;
        deviceName = Microphone.devices[0];

        Button button = GetComponent<Button>();
        button.onClick.AddListener(Record);
    }

    private void Record() {
        if (!isRecording) {
            voiceover = Microphone.Start(deviceName, false, 10, 44100);
            
        }
        else {
            Microphone.End(deviceName);
            audioSource.clip = voiceover;
            audioSource.Play();
        }

        isRecording = !isRecording;
    }
}
