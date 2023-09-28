using MusicPlaylist.LinkedList;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MusicPlaylist
{
    public partial class MainWindow : Window
    {
        private DoublyLinkedList playlist;
        public ObservableCollection<Song> PlaylistCollection { get; private set; }


        public MainWindow()
        {
            InitializeComponent();
            playlist = new DoublyLinkedList();
            AddDummyData();

            PlaylistCollection = new ObservableCollection<Song>();
            //Add each node to the observable collection
            Song current = playlist.GetNextNode();

            while (current != null)
            {
                PlaylistCollection.Add(current);
                current = playlist.GetNextNode();
            }
            playlist.ResetCurrent();

            listBox.ItemsSource = PlaylistCollection;
            DataContext = this;
        }

        private void AddDummyData()
        {
            playlist.AddNode("Song 1", "Artist 1", new TimeSpan(0, 3, 30));
            playlist.AddNode("Song 2", "Artist 2", new TimeSpan(0, 4, 30));
            playlist.AddNode("Song 3", "Artist 3", new TimeSpan(0, 5, 30));
            playlist.AddNode("Song 4", "Artist 4", new TimeSpan(0, 6, 30));
            playlist.AddNode("Song 5", "Artist 5", new TimeSpan(0, 7, 30));
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string songTitle = titleTxt.Text;
            string songArtist = artistTxt.Text;
            TimeSpan songDuration = new TimeSpan(0, Convert.ToInt32(minutesTxt.Text), Convert.ToInt32(secondsTxt.Text));

            playlist.AddNode(songTitle, songArtist, songDuration);

            //Add the new node to the observable collection
            Song last = playlist.GetLastNode();
            PlaylistCollection.Add(last);

            //Clear the text boxes
            titleTxt.Text = "";
            artistTxt.Text = "";
            minutesTxt.Text = "";
            secondsTxt.Text = "";
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Get the selected song
            Song selectedSong = (Song)listBox.SelectedItem;

            //Remove the song from the playlist
            playlist.RemoveNode(selectedSong.Title);

            //Get the text from the text box
            string currentSong = currentSongTxt.Text;

            //Split the text into an array
            string[] currentSongArray = currentSong.Split('\n');
            string title = currentSongArray[0].Trim().Substring("Title:".Length).Trim();

            //Check if the selected song is the current song
            if (selectedSong.Title.Equals(title))
            {
                Song nextSong = null;
                if (listBox.SelectedIndex == 0)
                {
                    nextSong = playlist.GetFirstNode();
                    
                }
                else if (listBox.SelectedIndex == listBox.Items.Count - 1)
                {
                    nextSong = playlist.GetLastNode();

                }
                else
                {
                    nextSong = playlist.GetNextNode();
                }
                currentSongTxt.Text = $"Title: \t\t{nextSong.Title}\n" +
                                          $"Artist: \t\t{nextSong.Artist}\n" +
                                          $"Duration: \t{nextSong.Duration}";
            }
            //Remove the song from the observable collection
            PlaylistCollection.Remove(selectedSong);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            // Get the previous song in the playlist
            Song previousSong = playlist.GetPreviousNode();

            if (previousSong != null)
            {
                // Update the currentSongTxt TextBox with the information of the previous song
                currentSongTxt.Text = $"Title: \t\t{previousSong.Title}\n" +
                                      $"Artist: \t\t{previousSong.Artist}\n" +
                                      $"Duration: \t{previousSong.Duration}";
            }

        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            Song nextSong = playlist.GetNextNode();

            if (nextSong != null)
            {
                // Update the currentSongTxt TextBox with the information of the next song
                currentSongTxt.Text = $"Title: \t\t{nextSong.Title}\n" +
                                      $"Artist: \t\t{nextSong.Artist}\n" +
                                      $"Duration: \t{nextSong.Duration}";
            }

        }

        private void ShuffleBtn_Click(object sender, RoutedEventArgs e)
        {
            // Clear the observable collection
            PlaylistCollection.Clear();

            // Shuffle the playlist
            playlist.Shuffle();

            // Add each node to the observable collection
            Song current = playlist.GetNextNode();
            currentSongTxt.Text = $"Title: \t\t{current.Title}\n" +
                $"Artist: \t\t{current.Artist}\n" +
                $"Duration: \t{current.Duration}";

            while (current != null)
            {
                PlaylistCollection.Add(current);
                current = playlist.GetNextNode();
            }

        }
    }
}
