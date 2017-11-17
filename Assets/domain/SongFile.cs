
public class SongFile {
    public string identifier;
    public string filename;
    public float playLength;

    public SongFile(string filename, float playLength) {
        this.filename = filename;
        this.playLength = playLength;
    }
}