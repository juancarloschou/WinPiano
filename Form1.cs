using NAudio.Wave;

namespace WinPiano
{
    public partial class Form1 : Form
    {
        private bool protectKeys; // To protect from inifite keypress chain reactions

        public Form1()
        {
            InitializeComponent();
        }

        //control key press
        private bool[] keyStates = new bool[256]; // Array to store the state of each key
        private bool[] keyFirstPress = new bool[256]; // Array to store the state of each key

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Control.IsKeyLocked(Keys.NumLock))
            {
                NativeMethods.SimulateKeyPressNumLock();
            }
            
            //this.KeyPreview = true;

            loadSounds();
            drawPiano();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.NumLock)
            {
                if (protectKeys)
                    return;

                if (!(new Microsoft.VisualBasic.Devices.Keyboard().NumLock))
                {
                    protectKeys = true;
                    NativeMethods.SimulateKeyPressNumLock();
                    protectKeys = false;
                }
            }
            else if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true; // Prevent default Tab behavior
                //SelectNextControl(ActiveControl, true, true, true, true); // Move focus to next control in tab order
            }

            int keyCode = (int)e.KeyCode;

            if (!keyFirstPress[keyCode])
            {
                keyFirstPress[keyCode] = true;
                playKeyNote(keyCode);
            }

            if (keyCode >= 0 && keyCode < keyStates.Length)
            {
                keyStates[keyCode] = true;
            }
            playKeyboard();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            int keyCode = (int)e.KeyCode;

            keyFirstPress[keyCode] = false;
            stopKeyNote(keyCode);

            if (keyCode >= 0 && keyCode < keyStates.Length)
            {
                keyStates[keyCode] = false;
            }
            playKeyboard();
        }



        //piano functions 
        void drawPiano()
        {
            label1.Text = "----------------------------|-----------------------------|--------------------------------" + Environment.NewLine +
                            "    1   2       4   5   6   |   8   9       '   ¡   DEL   |     INI   REP       /   *   -  " + Environment.NewLine +
                            "TAB | Q | W | E | R | T | Y | U | I | O | P | ` | + | ENT | SUP | FIN | AVP | 7 | 8 | 9 | +" + Environment.NewLine +
                            "----------------------------|-----------------------------|--------------------------------";
        }

        void playKeyboard()
        {
            string keys = "";
            for (int i = 0; i < 256; i++)
            {
                if (keyStates[i])
                {
                    keys = keys + "Key state: " + i + " . ";
                }
            }
            label2.Text = keys;

        }

        MediaFoundationReader noteC3;
        MediaFoundationReader noteCs3;
        MediaFoundationReader noteD3;
        MediaFoundationReader noteDs3;
        MediaFoundationReader noteE3;
        MediaFoundationReader noteF3;
        MediaFoundationReader noteFs3;
        MediaFoundationReader noteG3;
        MediaFoundationReader noteGs3;
        MediaFoundationReader noteA3;
        MediaFoundationReader noteAs3;
        MediaFoundationReader noteB3;

        MediaFoundationReader noteC4;
        MediaFoundationReader noteCs4;
        MediaFoundationReader noteD4;
        MediaFoundationReader noteDs4;
        MediaFoundationReader noteE4;
        MediaFoundationReader noteF4;
        MediaFoundationReader noteFs4;
        MediaFoundationReader noteG4;
        MediaFoundationReader noteGs4;
        MediaFoundationReader noteA4;
        MediaFoundationReader noteAs4;
        MediaFoundationReader noteB4;

        MediaFoundationReader noteC5;
        MediaFoundationReader noteCs5;
        MediaFoundationReader noteD5;
        MediaFoundationReader noteDs5;
        MediaFoundationReader noteE5;
        MediaFoundationReader noteF5;
        MediaFoundationReader noteFs5;
        MediaFoundationReader noteG5;
        MediaFoundationReader noteGs5;
        MediaFoundationReader noteA5;
        MediaFoundationReader noteAs5;
        MediaFoundationReader noteB5;

        // Create an instance of WaveOut object
        WaveOut waveC3;
        WaveOut waveCs3;
        WaveOut waveD3;
        WaveOut waveDs3;
        WaveOut waveE3;
        WaveOut waveF3;
        WaveOut waveFs3;
        WaveOut waveG3;
        WaveOut waveGs3;
        WaveOut waveA3;
        WaveOut waveAs3;
        WaveOut waveB3;

        WaveOut waveC4;
        WaveOut waveCs4;
        WaveOut waveD4;
        WaveOut waveDs4;
        WaveOut waveE4;
        WaveOut waveF4;
        WaveOut waveFs4;
        WaveOut waveG4;
        WaveOut waveGs4;
        WaveOut waveA4;
        WaveOut waveAs4;
        WaveOut waveB4;

        WaveOut waveC5;
        WaveOut waveCs5;
        WaveOut waveD5;
        WaveOut waveDs5;
        WaveOut waveE5;
        WaveOut waveF5;
        WaveOut waveFs5;
        WaveOut waveG5;
        WaveOut waveGs5;
        WaveOut waveA5;
        WaveOut waveAs5;
        WaveOut waveB5;

        double skipSecondsEvenEvenLess = 0;
        double skipSecondsEvenLess = 0.2;
        double skipSecondsLess = 0.35;
        double skipSeconds = 0.5; //normal
        double skipSecondsMore = 0.65;
        double skipSecondsEvenMore = 0.8;
        double skipSecondsEvenEvenMore = 0.95;

        int skipBytesFromSeconds(MediaFoundationReader audioFile, double seconds) 
        {
            // Get the format of the audio file
            var format = audioFile.WaveFormat;

            // Calculate the number of bytes to skip to skip the first 0.5 seconds
            var bytesToSkip = (int)(seconds * format.SampleRate * (format.BitsPerSample / 8) * format.Channels);

            //returns the bytes to skip
            return bytesToSkip;
        }

        void skipNote(MediaFoundationReader audioFile)
        {
            if (ReferenceEquals(audioFile, noteF3))
            {
                audioFile.Position = skipBytesFromSeconds(audioFile, skipSecondsEvenEvenMore);
            }
            else if (ReferenceEquals(audioFile, noteE3) || ReferenceEquals(audioFile, noteGs3) || ReferenceEquals(audioFile, noteA3) || ReferenceEquals(audioFile, noteB3) || ReferenceEquals(audioFile, noteD4) || ReferenceEquals(audioFile, noteE4) || ReferenceEquals(audioFile, noteG4) || ReferenceEquals(audioFile, noteCs5) || ReferenceEquals(audioFile, noteDs5) || ReferenceEquals(audioFile, noteF5))
            {
                audioFile.Position = skipBytesFromSeconds(audioFile, skipSecondsEvenMore);
            }
            else if (ReferenceEquals(audioFile, noteC3) || ReferenceEquals(audioFile, noteCs3) || ReferenceEquals(audioFile, noteD3) || ReferenceEquals(audioFile, noteE3) || ReferenceEquals(audioFile, noteC4) || ReferenceEquals(audioFile, noteCs4) || ReferenceEquals(audioFile, noteDs4) || ReferenceEquals(audioFile, noteF4) || ReferenceEquals(audioFile, noteGs4) || ReferenceEquals(audioFile, noteE5) || ReferenceEquals(audioFile, noteB5))
            {
                audioFile.Position = skipBytesFromSeconds(audioFile, skipSecondsMore);
            }
            else if (ReferenceEquals(audioFile, noteDs3) || ReferenceEquals(audioFile, noteAs3) || ReferenceEquals(audioFile, noteA4))
            {
                audioFile.Position = skipBytesFromSeconds(audioFile, skipSecondsLess);
            }
            else if (ReferenceEquals(audioFile, noteB3))
            {
                audioFile.Position = skipBytesFromSeconds(audioFile, skipSecondsEvenLess);
            }
            //else if (false)
            //{
            //    audioFile.Position = skipBytesFromSeconds(audioFile, skipSecondsEvenEvenLess);
            //}
            else
            {
                audioFile.Position = skipBytesFromSeconds(audioFile, skipSeconds);
            }

        }

        void loadSounds()
        {
            string directory = @"C:\Desarrollo\PdC\Programming\WinPiano\Piano\";

            noteC3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.C3.wav"));
            noteCs3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Db3.wav"));
            noteD3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.D3.wav"));
            noteDs3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Eb3.wav"));
            noteE3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.E3.wav"));
            noteF3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.F3.wav"));
            noteFs3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Gb3.wav"));
            noteG3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.G3.wav"));
            noteGs3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Ab3.wav"));
            noteA3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.A3.wav"));
            noteAs3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Bb3.wav"));
            noteB3 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.B3.wav"));

            noteC4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.C4.wav"));
            noteCs4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Db4.wav"));
            noteD4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.D4.wav"));
            noteDs4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Eb4.wav"));
            noteE4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.E4.wav"));
            noteF4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.F4.wav"));
            noteFs4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Gb4.wav"));
            noteG4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.G4.wav"));
            noteGs4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Ab4.wav"));
            noteA4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.A4.wav"));
            noteAs4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Bb4.wav"));
            noteB4 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.B4.wav"));

            noteC5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.C5.wav"));
            noteCs5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Db5.wav"));
            noteD5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.D5.wav"));
            noteDs5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Eb5.wav"));
            noteE5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.E5.wav"));
            noteF5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.F5.wav"));
            noteFs5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Gb5.wav"));
            noteG5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.G5.wav"));
            noteGs5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Ab5.wav"));
            noteA5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.A5.wav"));
            noteAs5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.Bb5.wav"));
            noteB5 = new MediaFoundationReader(Path.Combine(directory, "Piano.ff.B5.wav"));

            MediaFoundationReader[] notes = { noteC3, noteCs3, noteD3, noteDs3, noteE3, noteF3, noteFs3, noteG3, noteGs3, noteA3, noteAs3, noteB3,
                noteC4, noteCs4, noteD4, noteDs4, noteE4, noteF4, noteFs4, noteG4, noteGs4, noteA4, noteAs4, noteB4,
                noteC5, noteCs5, noteD5, noteDs5, noteE5, noteF5, noteFs5, noteG5, noteGs5, noteA5, noteAs5, noteB5 };

            foreach (MediaFoundationReader note in notes)
            {
                skipNote(note);
            }

            waveC3 = new WaveOut();
            waveCs3 = new WaveOut();
            waveD3 = new WaveOut();
            waveDs3 = new WaveOut();
            waveE3 = new WaveOut();
            waveF3 = new WaveOut();
            waveFs3 = new WaveOut();
            waveG3 = new WaveOut();
            waveGs3 = new WaveOut();
            waveA3 = new WaveOut();
            waveAs3 = new WaveOut();
            waveB3 = new WaveOut();

            waveC4 = new WaveOut();
            waveCs4 = new WaveOut();
            waveD4 = new WaveOut();
            waveDs4 = new WaveOut();
            waveE4 = new WaveOut();
            waveF4 = new WaveOut();
            waveFs4 = new WaveOut();
            waveG4 = new WaveOut();
            waveGs4 = new WaveOut();
            waveA4 = new WaveOut();
            waveAs4 = new WaveOut();
            waveB4 = new WaveOut();

            waveC5 = new WaveOut();
            waveCs5 = new WaveOut();
            waveD5 = new WaveOut();
            waveDs5 = new WaveOut();
            waveE5 = new WaveOut();
            waveF5 = new WaveOut();
            waveFs5 = new WaveOut();
            waveG5 = new WaveOut();
            waveGs5 = new WaveOut();
            waveA5 = new WaveOut();
            waveAs5 = new WaveOut();
            waveB5 = new WaveOut();

            waveC3.Init(noteC3);
            waveCs3.Init(noteCs3);
            waveD3.Init(noteD3);
            waveDs3.Init(noteDs3);
            waveE3.Init(noteE3);
            waveF3.Init(noteF3);
            waveFs3.Init(noteFs3);
            waveG3.Init(noteG3);
            waveGs3.Init(noteGs3);
            waveA3.Init(noteA3);
            waveAs3.Init(noteAs3);
            waveB3.Init(noteB3);

            waveC4.Init(noteC4);
            waveCs4.Init(noteCs4);
            waveD4.Init(noteD4);
            waveDs4.Init(noteDs4);
            waveE4.Init(noteE4);
            waveF4.Init(noteF4);
            waveFs4.Init(noteFs4);
            waveG4.Init(noteG4);
            waveGs4.Init(noteGs4);
            waveA4.Init(noteA4);
            waveAs4.Init(noteAs4);
            waveB4.Init(noteB4);

            waveC5.Init(noteC5);
            waveCs5.Init(noteCs5);
            waveD5.Init(noteD5);
            waveDs5.Init(noteDs5);
            waveE5.Init(noteE5);
            waveF5.Init(noteF5);
            waveFs5.Init(noteFs5);
            waveG5.Init(noteG5);
            waveGs5.Init(noteGs5);
            waveA5.Init(noteA5);
            waveAs5.Init(noteAs5);
            waveB5.Init(noteB5);
        }

        void playKeyNote(int keyCode)
        {
            switch (keyCode)
            {
                // octave 3
                case (int)Keys.Tab:
                    waveC3.Play();
                    break;
                case (int)Keys.D1:
                    waveCs3.Play();
                    break;
                case (int)Keys.Q:
                    waveD3.Play();
                    break;
                case (int)Keys.D2:
                    waveDs3.Play();
                    break;
                case (int)Keys.W:
                    waveE3.Play();
                    break;
                case (int)Keys.E:
                    waveF3.Play();
                    break;
                case (int)Keys.D4:
                    waveFs3.Play();
                    break;
                case (int)Keys.R:
                    waveG3.Play();
                    break;
                case (int)Keys.D5:
                    waveGs3.Play();
                    break;
                case (int)Keys.T:
                    waveA3.Play();
                    break;
                case (int)Keys.D6:
                    waveAs3.Play();
                    break;
                case (int)Keys.Y:
                    waveB3.Play();
                    break;

                // octave 4
                case (int)Keys.U:
                    waveC4.Play();
                    break;
                case (int)Keys.D8:
                    waveCs4.Play();
                    break;
                case (int)Keys.I:
                    waveD4.Play();
                    break;
                case (int)Keys.D9:
                    waveDs4.Play();
                    break;
                case (int)Keys.O:
                    waveE4.Play();
                    break;
                case (int)Keys.P:
                    waveF4.Play();
                    break;
                case 219: // ':
                    waveFs4.Play();
                    break;
                case 186: // ^:
                    waveG4.Play();
                    break;
                case 221: // !:
                    waveGs4.Play();
                    break;
                case 187: // +:
                    waveA4.Play();
                    break;
                case 8: // Delete:
                    waveAs4.Play();
                    break;
                case (int)Keys.Enter:
                    waveB4.Play();
                    break;

                // octave 5
                case 46: // SUP:
                    waveC5.Play();
                    break;
                case 36: // INI:
                    waveCs5.Play();
                    break;
                case 35: // FIN:
                    waveD5.Play();
                    break;
                case 33: // REP:
                    waveDs5.Play();
                    break;
                case 34: // AVP:
                    waveE5.Play();
                    break;
                case 103: // Num7:
                    waveF5.Play();
                    break;
                case 111: // /:
                    waveFs5.Play();
                    break;
                case 104: //Num8:
                    waveG5.Play();
                    break;
                case 106: // *:
                    waveGs5.Play();
                    break;
                case 105: // Num9:
                    waveA5.Play();
                    break;
                case 109: // -:
                    waveAs5.Play();
                    break;
                case 107: // +:
                    waveB5.Play();
                    break;

            }
        }

        void stopKeyNote(int keyCode)
        {
            switch (keyCode)
            {
                // octave 3
                case (int)Keys.Tab:
                    waveC3.Stop();
                    skipNote(noteC3);
                    break;
                case (int)Keys.D1:
                    waveCs3.Stop();
                    skipNote(noteCs3);
                    break;
                case (int)Keys.Q:
                    waveD3.Stop();
                    skipNote(noteD3);
                    break;
                case (int)Keys.D2:
                    waveDs3.Stop();
                    skipNote(noteDs3);
                    break;
                case (int)Keys.W:
                    waveE3.Stop();
                    skipNote(noteE3);
                    break;
                case (int)Keys.E:
                    waveF3.Stop();
                    skipNote(noteF3);
                    break;
                case (int)Keys.D4:
                    waveFs3.Stop();
                    skipNote(noteFs3);
                    break;
                case (int)Keys.R:
                    waveG3.Stop();
                    skipNote(noteG3);
                    break;
                case (int)Keys.D5:
                    waveGs3.Stop();
                    skipNote(noteGs3);
                    break;
                case (int)Keys.T:
                    waveA3.Stop();
                    skipNote(noteA3);
                    break;
                case (int)Keys.D6:
                    waveAs3.Stop();
                    skipNote(noteAs3);
                    break;
                case (int)Keys.Y:
                    waveB3.Stop();
                    skipNote(noteB3);
                    break;

                // octave 4
                case (int)Keys.U:
                    waveC4.Stop();
                    skipNote(noteC4);
                    break;
                case (int)Keys.D8:
                    waveCs4.Stop();
                    skipNote(noteCs4);
                    break;
                case (int)Keys.I:
                    waveD4.Stop();
                    skipNote(noteD4);
                    break;
                case (int)Keys.D9:
                    waveDs4.Stop();
                    skipNote(noteDs4);
                    break;
                case (int)Keys.O:
                    waveE4.Stop();
                    skipNote(noteE4);
                    break;
                case (int)Keys.P:
                    waveF4.Stop();
                    skipNote(noteF4);
                    break;
                case 219: // '
                    waveFs4.Stop();
                    skipNote(noteFs4);
                    break;
                case 186: // ^
                    waveG4.Stop();
                    skipNote(noteG4);
                    break;
                case 221: // !
                    waveGs4.Stop();
                    skipNote(noteGs4);
                    break;
                case 187: // +
                    waveA4.Stop();
                    skipNote(noteA4);
                    break;
                case 8: // DEL
                    waveAs4.Stop();
                    skipNote(noteAs4);
                    break;
                case (int)Keys.Enter:
                    waveB4.Stop();
                    skipNote(noteB4);
                    break;

                // octave 5
                case 46: // SUP
                    waveC5.Stop();
                    skipNote(noteC5);
                    break;
                case 36: // INI
                    waveCs5.Stop();
                    skipNote(noteCs5);
                    break;
                case 35: // FIN
                    waveD5.Stop();
                    skipNote(noteD5);
                    break;
                case 33: // REP
                    waveDs5.Stop();
                    skipNote(noteDs5);
                    break;
                case 34: // AVP
                    waveE5.Stop();
                    skipNote(noteE5);
                    break;
                case 103: // Num7
                    waveF5.Stop();
                    skipNote(noteF5);
                    break;
                case 111: // /
                    waveFs5.Stop();
                    skipNote(noteFs5);
                    break;
                case 104: // Num8
                    waveG5.Stop();
                    skipNote(noteG5);
                    break;
                case 106: // *
                    waveGs5.Stop();
                    skipNote(noteGs5);
                    break;
                case 105: // Num9
                    waveA5.Stop();
                    skipNote(noteA5);
                    break;
                case 109: // -
                    waveAs5.Stop();
                    skipNote(noteAs5);
                    break;
                case 107: // +
                    waveB5.Stop();
                    skipNote(noteB5);
                    break;

            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    loadSounds();

        //    for (int i = 0; i < keyStates.Length; i++)
        //    {
        //        keyStates[i] = false;
        //    }
        //    for (int i = 0; i < keyFirstPress.Length; i++)
        //    {
        //        keyFirstPress[i] = false;
        //    }

        //    //SelectNextControl(Form1, true, true, true, true); // Move focus to next control in tab order

        //    // Set focus to the parent form
        //    this.FindForm()?.Focus();


        //}
    }
}