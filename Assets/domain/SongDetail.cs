using System;


public class SongDetail {
    public string title;
    public string description;
    public string date;

    public SongDetail(string title, string description, string date) {
        this.title = title;
        this.description = description;

        if (date != null) {
            this.date = DateTime.Parse(date).Year.ToString();
        }
    }
}