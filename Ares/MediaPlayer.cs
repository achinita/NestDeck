using NAudio.Wave;
using Newtonsoft.Json;

using Windows.Media.Control;

namespace Nest_Deck
{
    internal class MediaPlayer
    {
        private GlobalSystemMediaTransportControlsSessionManager gsmtcsm;
        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;

        public MediaPlayer()
        {
            InitMediaEvents();
            waveOut = new WaveOutEvent();
        }

        public void SystemSound_Tap()
        {
            this.Play("sound\\tap.mp3", true);
        }
        public void SystemSound_Switch()
        {
            this.Play("sound\\switchon.mp3", true);
        }
        public string Play(string filePath, bool PlayOverOtherSounds = false)
        {
            try
            {
                if (PlayOverOtherSounds == false) waveOut.Stop();
                if (filePath != "")
                {
                    waveOut = new WaveOutEvent();
                    AudioFileReader audioFileReader = new AudioFileReader(filePath);
                    waveOut.Init(audioFileReader);
                    waveOut.Play();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Error playing audio file :" + ex.Message;
            }
        }

        public void Stop()
        {
            waveOut.Stop();
        }
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        //Current playing media
        private async void InitMediaEvents()
        {
            gsmtcsm = await GetSystemMediaTransportControlsSessionManager();
            Gsmtcsm_CurrentSessionChanged(gsmtcsm, null);
            gsmtcsm.CurrentSessionChanged += Gsmtcsm_CurrentSessionChanged;
        }
        public event EventHandler SongChanged;

        protected virtual void OnSongChanged()
        {

            SongChanged?.Invoke(this, null);
        }
        static string LastString = "";
        private async void Gsmtcsm_CurrentSessionChanged(GlobalSystemMediaTransportControlsSessionManager sender, CurrentSessionChangedEventArgs args)
        {
            var s = sender.GetCurrentSession();
            if (s != null)
            {
                s.MediaPropertiesChanged += S_MediaPropertiesChanged;
                S_MediaPropertiesChanged(s, null);
                s.PlaybackInfoChanged += S_PlaybackInfoChanged;
                S_PlaybackInfoChanged(s, null);
            }
            //GC.Collect();
        }
        [JsonProperty]
        public string MediaName { get; set; }
        [JsonProperty]
        public bool IsPlaying { get; set; }
        private void S_PlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender, PlaybackInfoChangedEventArgs args)
        {
            try
            {
                GlobalSystemMediaTransportControlsSessionPlaybackInfo playbackInfo = sender.GetPlaybackInfo();
                string newMedia = LastString;
                bool playStatus = playbackInfo.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing;
                if (MediaName != newMedia || IsPlaying != playStatus)
                {
                    MediaName = LastString;
                    IsPlaying = playbackInfo.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing;
                    OnSongChanged();
                }
            }
            catch
            {
                //Do nothing
            }

        }

        private async void S_MediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender, MediaPropertiesChangedEventArgs args)
        {
            try
            {
                GlobalSystemMediaTransportControlsSessionMediaProperties mediaProperties = await sender.TryGetMediaPropertiesAsync();
                if (mediaProperties != null)
                {
                    //testThumbnail(mediaProperties.Thumbnail);
                    //string Curr = ($"{mediaProperties.Artist} - {mediaProperties.Title} - {mediaProperties.TrackNumber}");
                    string Curr = ($"{mediaProperties.Artist} - {mediaProperties.Title}");
                    if (Curr.Trim() == "-") Curr = "";

                    if (!Curr.Equals(LastString))
                    {
                        //Console.WriteLine(Curr);
                        LastString = Curr;
                        S_PlaybackInfoChanged(sender, null);
                    }
                }
            }
            catch
            {
                //Do nothing
            }
        }
        /*
        private static void testThumbnail (IRandomAccessStreamReference thumbnailStream)
        {
            // Step 1: Get the IRandomAccessStreamWithContentType from the IRandomAccessStreamReference
            var streamWithContentType = thumbnailStream.OpenReadAsync(); // Returns IRandomAccessStreamWithContentType

            // Step 2: Create a buffer and read the stream content into it
            using (var inputStream = streamWithContentType.GetResults().GetInputStreamAt(0)) // Create an input stream starting at position 0
            {
                using (var dataReader = new DataReader(inputStream))
                {
                    uint size = (uint)streamWithContentType.Size; // Get the size of the stream
                    dataReader.LoadAsync(size); // Load data from the stream

                    byte[] buffer = new byte[size]; // Create a buffer of the correct size
                    dataReader.ReadBytes(buffer); // Read the stream data into the buffer

                    // Step 3: Write the buffer content to the destination file
                    await FileIO.WriteBytesAsync(destinationFile, buffer); // Write the buffer to the destination file
                }
            }
        }*/
        private static async Task<GlobalSystemMediaTransportControlsSessionManager> GetSystemMediaTransportControlsSessionManager() =>
            await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();

    }
}
