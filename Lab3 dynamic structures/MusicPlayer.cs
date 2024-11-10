using System;
using System.Collections.Generic;

namespace Lab3_dynamic_structures
{
    

    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }

        public Song(string title, string artist)
        {
            Title = title;
            Artist = artist;
        }

        public override string ToString()
        {
            return $"{Title} by {Artist}";
        }
    }

    public class Playlist
    {
        private List<Song> songs = new List<Song>();

        public void AddSong(Song song)
        {
            songs.Add(song);
        }

        public void RemoveSong(Song song)
        {
            songs.Remove(song);
        }

        public void PlaySong(Song song)
        {
            Console.WriteLine($"Playing {song}");
        }

        public void DisplaySongs()
        {
            for (int i = 0; i < songs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {songs[i]}");
            }
        }
    }
}
