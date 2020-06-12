using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using MjpegProcessor;
using CefSharp;
using CefSharp.WinForms;

using WebEye.Controls.WinForms;

using System.Net;
using System.Threading;

using System.Net.Sockets;

using LumiSoft.Net.UDP;
using LumiSoft.Net.Codec;
using LumiSoft.Media.Wave;

using System.IO;

namespace mjpeg
{
    /*
     * rtsp://admin:winda1984@10.203.83.1:554/onvif1 (rtsp yoosee)
     * http://ip:port/video.mjpg (mjpeg yawcam)
     * http://ip:port/camera/mjpeg (mjpeg cam2web)
     * http://admin:@192.168.43.10/video.cgi (webcgi dlink dcs930)
     * */

    public partial class ROSITA : Form
    {
        //MjpegDecoder _mjpeg;
        private ChromiumWebBrowser chromeBrowser;
        private WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl streamPlayerControl1;

        private bool isLoading = false;

        private bool m_IsRunning = false;
        private bool m_IsSendingMic = false;
        private bool m_IsSendingTest = false;
        private UdpServer m_pUdpServer = null;
        private WaveIn m_pWaveIn = null;
        private WaveOut m_pWaveOut = null;
        private int m_Codec = 0;
        private FileStream m_pRecordStream = null;
        private IPEndPoint m_pTargetEP = null;
        private string m_PlayFile = "";
        private System.Windows.Forms.Timer m_pTimer = null;

        bool ShowRight = true;

        //Image buffer;

        //int bufferCount = 0;
        //bool viewCam = false;
        //int currentBufferCount = 0;
        //private static System.Timers.Timer timer;
        private string myDocPath;

        public ROSITA()
        {
            DoubleBuffered = true;
            InitializeComponent();
            InitializeChromium();
            InitializeWebEye();
            //_mjpeg = new MjpegDecoder();
            //_mjpeg.FrameReady += mjpeg_FrameReady;
            textBox1.Text = "http://192.168.43.219:8081/video.mjpg";
            textBox1.Text = "http://192.168.43.219:8000/camera/mjpeg";
        }

        private void InitializeWebEye()
        {
            streamPlayerControl1 = new WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl();
            // Add it to the form and fill it to the form window.
            panelWeb.Controls.Add(streamPlayerControl1);
            streamPlayerControl1.Dock = DockStyle.Fill;
            streamPlayerControl1.Visible = false;
            streamPlayerControl1.StreamFailed += StreamPlayerControl1_StreamFailed;
        }

        private void StreamPlayerControl1_StreamStopped(object sender, EventArgs e)
        {
            streamPlayerControl1.Hide();
            panel1.Show();
        }

        private void StreamPlayerControl1_StreamFailed(object sender, WebEye.Controls.WinForms.StreamPlayerControl.StreamFailedEventArgs e)
        {
            streamPlayerControl1.Hide();
            MessageBox.Show(e.ToString());
        }

        //private void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
        //{
        //    viewCam = true;

        //    buffer = e.Bitmap;
        //    bufferCount++;
        //    //Text = bufferCount.ToString();
        //}

        public void InitializeChromium()
        {
            // Create a browser component
            //chromeBrowser = new ChromiumWebBrowser("http://admin:@192.168.43.10/video.cgi");
            //chromeBrowser = new ChromiumWebBrowser("http://localhost:8001/camera/mjpeg");

            chromeBrowser = new ChromiumWebBrowser("");
            // Add it to the form and fill it to the form window.
            panelWeb.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;

            //Wait for the page to finish loading (all resources will have been loaded, rendering is likely still happening)
            chromeBrowser.LoadingStateChanged += (sender, args) =>
            {
                //Wait for the Page to finish loading
                Console.WriteLine("Loading State Changed GoBack {0} GoForward {1} CanReload {2} IsLoading {3}", args.CanGoBack, args.CanGoForward, args.CanReload, args.IsLoading);
                if (!args.CanReload && args.IsLoading)
                {
                    isLoading = true;
                }
                else {
                    isLoading = false;

                }
            };

            chromeBrowser.LoadError += (sender, args) =>
            {
                Console.WriteLine("Loading State Changed Code {0} Frame {1} URL {2} Text {3}", args.ErrorCode, args.Frame, args.FailedUrl, args.ErrorText);

            };

            //Wait for the MainFrame to finish loading
            chromeBrowser.FrameLoadEnd += (sender, args) =>
            {
                //Wait for the MainFrame to finish loading
                //                if (args.Frame.IsMain)
                {
                    Console.WriteLine("MainFrame finished loading Status code {0}", args.HttpStatusCode);
                    if (args.HttpStatusCode == 200)
                    {
                        chromeBrowser.Reload();
                    }
                    if (args.HttpStatusCode < 0)
                    {
                        //finished, OK, streaming interupted
                        chromeBrowser.Reload();
                    }
                    if (args.HttpStatusCode == 0)
                    {
                        //The client request wasn't successful.
                        // Create a timer with a two second interval.
                        var t = Task.Factory.StartNew(() =>
                        {
                            Console.WriteLine("Start");
                            return Task.Factory.StartNew(() =>
                            {
                                Task.Delay(2000).Wait();
                                chromeBrowser.Reload();
                                Console.WriteLine("Done");
                            });
                        });
                        t.Wait();
                        Console.WriteLine("All done");
                    }
                }
            };

            chromeBrowser.MouseClick += ChromeBrowser_MouseClick;

        }

        private void ChromeBrowser_MouseClick(object sender, MouseEventArgs e)
        {
            chromeBrowser.SetZoomLevel(3);
        }

        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            var b = ((ChromiumWebBrowser)sender);

            this.InvokeOnUiThreadIfRequired(() => b.Focus());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                if (textBox1.Text.Substring(0, 4) == "http")
                    OpenUrl(textBox1.Text);
                else if (textBox1.Text.Substring(0, 4) == "rtsp")
                    OpenRtsp(textBox1.Text);
                else
                    MessageBox.Show("Aalamat tidak tepat");
            }

            //timer = new System.Timers.Timer();
            //timer.Interval = 3000;

            //timer.Elapsed += OnTimedEvent;
            //timer.AutoReset = true;
            //timer.Enabled = true;
        }
        private void OpenRtsp(string url)
        {
            try
            {
                //_mjpeg.ParseStream(new Uri(textBox1.Text));
                if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                {
                    chromeBrowser.Visible = false;
                    streamPlayerControl1.Visible = true;
                    //streamPlayerControl1.StartPlay(new Uri("rtsp://admin:winda1984@10.203.83.1:554/onvif1"));
                    streamPlayerControl1.StartPlay(new Uri(url));
                }
                SaveSetting(textBox1);
            }
            catch (Exception ee) { MessageBox.Show(ee.Message); }
        }

        private void OpenUrl(string url)
        {
            try
            {
                //_mjpeg.ParseStream(new Uri(textBox1.Text));
                if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                {
                    chromeBrowser.Visible = true;
                    streamPlayerControl1.Visible = Visible;
                    chromeBrowser.Load(url);
                }
                SaveSetting(textBox1);
            }
            catch (Exception ee) { MessageBox.Show(ee.Message); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            myDocPath += "\\" + Application.ProductName;

            if (!Directory.Exists(myDocPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(myDocPath);
                if (!di.Exists) { MessageBox.Show(Application.ProductName, di.FullName + " tidak dapat dibuat", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            m_pInDevices.Text = ReadSetting(m_pInDevices);
            m_pOutDevices.Text = ReadSetting(m_pOutDevices);
            //m_pCodec.Text = ReadSetting(m_pCodec);
            m_pLoacalIP.Text = ReadSetting(m_pLoacalIP);
            m_pLocalPort.Maximum = ReadSetting(m_pLocalPort);
            m_pLocalPort.Value = ReadSetting(m_pLocalPort);

            m_pRemoteIP.Text = ReadSetting(m_pRemoteIP);
            m_pRemotePort.Maximum = ReadSetting(m_pRemotePort);
            m_pRemotePort.Value = ReadSetting(m_pRemotePort);

            textBox1.Text = ReadSetting(textBox1);


            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;

            //pictureBox1.Width = Width - panel1.Width;
            //m_pCodec.Items.Add("G711 a-law");
            //m_pCodec.Items.Add("G711 u-law");
            //m_pCodec.SelectedIndex = 0;

            m_pLoacalIP.Items.Add("127.0.0.1");
            foreach (IPAddress ip in System.Net.Dns.GetHostAddresses(""))
            {
                m_pLoacalIP.Items.Add(ip.ToString());
            }
            m_pLoacalIP.SelectedIndex = m_pLoacalIP.Items.IndexOf(ReadSetting(m_pLoacalIP));

            m_pLocalPort.Maximum = 99999;
            //m_pLocalPort.Value = 11000;

            m_pRemoteIP.Enabled = false;

            m_pRemotePort.Maximum = 99999;
            //m_pRemotePort.Value = 11000;
            m_pRemotePort.Enabled = false;

            LoadWaveDevices();

        }
        
        private void m_pToggleRun_Click(object sender, EventArgs e)
        {

            SaveSetting(m_pInDevices);
            SaveSetting(m_pOutDevices);
            //SaveSetting(m_pCodec);
            SaveSetting(m_pInDevices);
            SaveSetting(m_pLoacalIP);
            SaveSetting(m_pLocalPort);
            {
                if (m_IsRunning)
                {
                    m_IsRunning = false;
                    m_IsSendingTest = false;

                    m_pUdpServer.Dispose();
                    m_pUdpServer = null;

                    m_pWaveOut.Dispose();
                    m_pWaveOut = null;

                    if (m_pRecordStream != null)
                    {
                        m_pRecordStream.Dispose();
                        m_pRecordStream = null;
                    }

                    m_pTimer.Dispose();
                    m_pTimer = null;

                    m_pInDevices.Enabled = true;
                    m_pOutDevices.Enabled = true;
                    //m_pCodec.Enabled = true;
                    m_pToggleRun.Text = "Start";
                    //m_pRecord.Enabled = true;
                    //m_pRecordFile.Enabled = true;
                    //m_pRecordFileBrowse.Enabled = true;
                    m_pRemoteIP.Enabled = false;
                    m_pRemotePort.Enabled = false;
                    m_pToggleMic.Text = "Start";
                    m_pToggleMic.Enabled = false;
                    //m_pSendTestSound.Enabled = false;
                    //m_pSendTestSound.Text = "Start";
                    //m_pPlayTestSound.Enabled = false;
                    //m_pPlayTestSound.Text = "Play";
                }
                else
                {
                    if (m_pOutDevices.SelectedIndex == -1)
                    {
                        MessageBox.Show(this, "Please select output device !", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //if (m_pRecord.Checked && m_pRecordFile.Text == "")
                    //{
                    //    MessageBox.Show(this, "Please specify record file !", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    //if (m_pRecord.Checked)
                    //{
                    //    m_pRecordStream = File.Create(m_pRecordFile.Text);
                    //}

                    m_IsRunning = true;

                    //m_Codec = m_pCodec.SelectedIndex;
                    m_Codec = 0;

                    m_pWaveOut = new WaveOut(WaveOut.Devices[m_pOutDevices.SelectedIndex], 8000, 16, 1);

                    m_pUdpServer = new UdpServer();
                    m_pUdpServer.Bindings = new IPEndPoint[] { new IPEndPoint(IPAddress.Parse(m_pLoacalIP.Text), (int)m_pLocalPort.Value) };
                    m_pUdpServer.PacketReceived += new PacketReceivedHandler(m_pUdpServer_PacketReceived);
                    m_pUdpServer.Start();

                    m_pTimer = new System.Windows.Forms.Timer();
                    m_pTimer.Interval = 1000;
                    m_pTimer.Tick += new EventHandler(m_pTimer_Tick);
                    m_pTimer.Enabled = true;

                    m_pInDevices.Enabled = false;
                    m_pOutDevices.Enabled = false;
                    //m_pCodec.Enabled = false;
                    m_pToggleRun.Text = "Stop";
                    //m_pRecord.Enabled = false;
                    //m_pRecordFile.Enabled = false;
                    //m_pRecordFileBrowse.Enabled = false;
                    m_pRemoteIP.Enabled = true;
                    m_pRemotePort.Enabled = true;
                    m_pToggleMic.Enabled = true;
                    //m_pSendTestSound.Enabled = true;
                    //m_pSendTestSound.Text = "Start";
                    //m_pPlayTestSound.Enabled = true;
                    //m_pPlayTestSound.Text = "Play";
                }
            }
        }

        private void SaveSetting(NumericUpDown control)
        {
            SaveSetting(control.Name, control.Value.ToString());
        }
        private void SaveSetting(TextBox control)
        {
            SaveSetting(control.Name, control.Text);
        }

        private void SaveSetting(ComboBox control)
        {
            SaveSetting(control.Name, control.Text);
        }

        private void SaveSetting(string controlName, string controlText)
        {
            String filename = myDocPath + "\\" + controlName + ".txt";
            
            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.WriteLine(controlText);
                sw.Close();
            }
        }

        private string ReadSetting(ComboBox control)
        {
            return ReadSetting(control.Name);
        }
        private string ReadSetting(TextBox control)
        {
            return ReadSetting(control.Name);
        }
        private decimal ReadSetting(NumericUpDown control)
        {
            decimal value = 0;
            decimal.TryParse( ReadSetting(control.Name), out value);
            return value;
        }


        private string ReadSetting(string controlName)
        {
            String filename = myDocPath + "\\" + controlName + ".txt";
            string text = "";
            if (File.Exists(filename))
            {
                string[] infotext = System.IO.File.ReadAllLines(filename);
                foreach (string line in infotext)
                {
                    text = line;
                }
            }
            return text;
        }


        #region method m_pRecord_CheckedChanged

        //private void m_pRecord_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (m_pRecord.Checked)
        //    {
        //        m_pRecordFile.Enabled = true;
        //        m_pRecordFileBrowse.Enabled = true;
        //    }
        //    else
        //    {
        //        m_pRecordFile.Enabled = false;
        //        m_pRecordFileBrowse.Enabled = false;
        //    }
        //}

        #endregion

        #region method m_pRecordFileBrowse_Click

        //private void m_pRecordFileBrowse_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog dlg = new SaveFileDialog();
        //    dlg.FileName = "record.raw";
        //    if (dlg.ShowDialog(this) == DialogResult.OK)
        //    {
        //        m_pRecordFile.Text = dlg.FileName;
        //    }
        //}

        #endregion

        #region method m_pToggleMic_Click

        private void m_pToggleMic_Click(object sender, EventArgs e)
        {
            SaveSetting(m_pRemoteIP);
            SaveSetting(m_pRemotePort);

            if (m_IsSendingMic)
            {
                m_IsSendingMic = false;

                m_pWaveIn.Dispose();
                m_pWaveIn = null;

                OnAudioStopped();
            }
            else
            {
                if (m_pInDevices.SelectedIndex == -1)
                {
                    MessageBox.Show(this, "Please select input device !", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    m_pTargetEP = new IPEndPoint(IPAddress.Parse(m_pRemoteIP.Text), (int)m_pRemotePort.Value);
                }
                catch
                {
                    MessageBox.Show(this, "Invalid target IP address or port !", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                m_IsSendingMic = true;

                m_pWaveIn = new WaveIn(WaveIn.Devices[m_pInDevices.SelectedIndex], 8000, 16, 1, 400);
                m_pWaveIn.BufferFull += new BufferFullHandler(m_pWaveIn_BufferFull);
                m_pWaveIn.Start();

                m_pToggleMic.Text = "Stop";
                //m_pSendTestSound.Enabled = false;
            }
        }

        #endregion

        #region method m_pSendTestSound_Click

        private void m_pSendTestSound_Click(object sender, EventArgs e)
        {
            if (m_IsSendingTest)
            {
                m_IsSendingTest = false;

                OnAudioStopped();
            }
            else
            {
                try
                {
                    m_pTargetEP = new IPEndPoint(IPAddress.Parse(m_pRemoteIP.Text), (int)m_pRemotePort.Value);
                }
                catch
                {
                    MessageBox.Show(this, "Invalid target IP address or port !", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\audio";
                if (dlg.ShowDialog(null) == DialogResult.OK)
                {
                    m_PlayFile = dlg.FileName;

                    //m_pSendTestSound.Text = "Stop";
                    m_pToggleMic.Enabled = false;
                    //m_pPlayTestSound.Enabled = false;

                    m_IsSendingTest = true;

                    Thread tr = new Thread(new ThreadStart(this.SendTest));
                    tr.Start();
                }
            }
        }

        #endregion

        #region method m_pPlayTestSound_Click

        private void m_pPlayTestSound_Click(object sender, EventArgs e)
        {
            if (m_IsSendingTest)
            {
                m_IsSendingTest = false;

                OnAudioStopped();
            }
            else
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\audio";
                if (dlg.ShowDialog(null) == DialogResult.OK)
                {
                    m_PlayFile = dlg.FileName;

                    m_IsSendingTest = true;

                    m_pToggleMic.Enabled = false;
                    //m_pSendTestSound.Enabled = false;
                    //m_pPlayTestSound.Text = "Stop";

                    Thread tr = new Thread(new ThreadStart(this.PlayTestAudio));
                    tr.Start();
                }
            }
        }

        #endregion

        #region method m_pUdpServer_PacketReceived

        /// <summary>
        /// This method is called when we got UDP packet. 
        /// </summary>
        /// <param name="e">Event data.</param>
        private void m_pUdpServer_PacketReceived(UdpPacket_eArgs e)
        {
            // Decompress data.
            byte[] decodedData = null;
            if (m_Codec == 0)
            {
                decodedData = G711.Decode_aLaw(e.Data, 0, e.Data.Length);
            }
            else if (m_Codec == 1)
            {
                decodedData = G711.Decode_uLaw(e.Data, 0, e.Data.Length);
            }

            // We just play received packet.
            m_pWaveOut.Play(decodedData, 0, decodedData.Length);

            // Record if recoring enabled.
            if (m_pRecordStream != null)
            {
                m_pRecordStream.Write(decodedData, 0, decodedData.Length);
            }
        }

        #endregion

        #region method m_pWaveIn_BufferFull

        /// <summary>
        /// This method is called when recording buffer is full and we need to process it.
        /// </summary>
        /// <param name="buffer">Recorded data.</param>
        private void m_pWaveIn_BufferFull(byte[] buffer)
        {
            // Compress data. 
            byte[] encodedData = null;
            if (m_Codec == 0)
            {
                encodedData = G711.Encode_aLaw(buffer, 0, buffer.Length);
            }
            else if (m_Codec == 1)
            {
                encodedData = G711.Encode_uLaw(buffer, 0, buffer.Length);
            }

            // We just sent buffer to target end point.
            m_pUdpServer.SendPacket(encodedData, 0, encodedData.Length, m_pTargetEP);
        }

        #endregion

        #region method wfrm_Main_FormClosed

        /// <summary>
        /// This method is called when this form is closed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void wfrm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_pUdpServer != null)
            {
                m_pUdpServer.Dispose();
                m_pUdpServer = null;
            }
            if (m_pWaveIn != null)
            {
                m_pWaveIn.Dispose();
                m_pWaveIn = null;
            }
            if (m_pWaveOut != null)
            {
                m_pWaveOut.Dispose();
                m_pWaveOut = null;
            }
            if (m_pRecordStream != null)
            {
                m_pRecordStream.Dispose();
                m_pRecordStream = null;
            }
            if (m_pTimer != null)
            {
                m_pTimer.Dispose();
                m_pTimer = null;
            }
        }

        #endregion

        #region method m_pTimer_Tick

        /// <summary>
        /// This method is called when we need to refresh UI statistics data.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void m_pTimer_Tick(object sender, EventArgs e)
        {
            //m_pPacketsReceived.Text = m_pUdpServer.PacketsReceived.ToString();
            //m_pBytesReceived.Text = m_pUdpServer.BytesReceived.ToString();
            //m_pPacketsSent.Text = m_pUdpServer.PacketsSent.ToString();
            //m_pBytesSent.Text = m_pUdpServer.BytesSent.ToString();
        }

        #endregion




        #region method SendTest

        /// <summary>
        /// Sends test sound to target end point.
        /// </summary>
        private void SendTest()
        {
            try
            {
                using (FileStream fs = File.OpenRead(m_PlayFile))
                {
                    byte[] buffer = new byte[400];
                    int readedCount = fs.Read(buffer, 0, buffer.Length);
                    long lastSendTime = DateTime.Now.Ticks;
                    while (m_IsSendingTest && readedCount > 0)
                    {
                        // Compress data.
                        byte[] encodedData = null;
                        if (m_Codec == 0)
                        {
                            encodedData = G711.Encode_aLaw(buffer, 0, buffer.Length);
                        }
                        else if (m_Codec == 1)
                        {
                            encodedData = G711.Encode_uLaw(buffer, 0, buffer.Length);
                        }

                        // Send and read next.
                        m_pUdpServer.SendPacket(encodedData, 0, encodedData.Length, m_pTargetEP);
                        readedCount = fs.Read(buffer, 0, buffer.Length);

                        Thread.Sleep(25);

                        lastSendTime = DateTime.Now.Ticks;
                    }
                }

                if (m_IsRunning)
                {
                    this.Invoke(new MethodInvoker(this.OnAudioStopped));
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(null, "Error: " + x.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region method PlayTestAudio

        /// <summary>
        /// Plays test audio.
        /// </summary>
        private void PlayTestAudio()
        {
            try
            {
                using (FileStream fs = File.OpenRead(m_PlayFile))
                {
                    byte[] buffer = new byte[400];
                    int readedCount = fs.Read(buffer, 0, buffer.Length);
                    long lastSendTime = DateTime.Now.Ticks;
                    while (m_IsSendingTest && readedCount > 0)
                    {
                        // Send and read next.
                        m_pWaveOut.Play(buffer, 0, readedCount);
                        readedCount = fs.Read(buffer, 0, buffer.Length);

                        Thread.Sleep(25);

                        lastSendTime = DateTime.Now.Ticks;
                    }
                }

                if (m_IsRunning)
                {
                    this.Invoke(new MethodInvoker(this.OnAudioStopped));
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(null, "Error: " + x.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region method OnAudioStopped

        /// <summary>
        /// This method is called when audio sending/playing completes.
        /// NOTE: This method must be called from UI thread.
        /// </summary>
        private void OnAudioStopped()
        {
            //m_pToggleMic.Enabled = true;
            //m_pSendTestSound.Enabled = true;
            //m_pSendTestSound.Text = "Start";
            //m_pPlayTestSound.Enabled = true;
            //m_pPlayTestSound.Text = "Play";

            m_IsSendingTest = false;
        }

        #endregion


        #region method LoadWaveDevices

        /// <summary>
        /// Loads available wave input and output devices to UI.
        /// </summary>
        private void LoadWaveDevices()
        {
            // Load input devices.
            m_pInDevices.Items.Clear();
            foreach (WavInDevice device in WaveIn.Devices)
            {
                m_pInDevices.Items.Add(device.Name);
            }
            if (m_pInDevices.Items.Count > 0)
            {
                m_pInDevices.SelectedIndex = 0;
            }

            // Load output devices.
            m_pOutDevices.Items.Clear();
            foreach (WavOutDevice device in WaveOut.Devices)
            {
                m_pOutDevices.Items.Add(device.Name);
            }
            if (m_pOutDevices.Items.Count > 0)
            {
                m_pOutDevices.SelectedIndex = 0;
            }
        }

        #endregion

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //pictureBox1.Image = buffer;
            // Draw with double-buffering.
            //Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //using (Graphics gr = Graphics.FromImage(bm))
            //{
            //    DrawButterfly(gr, wid, hgt);
            //}
           // pictureBox1.Refresh();
            //pictureBox1.Image = buffer;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            ShowRight = false;
            btnShowRight.Text = "<<";
        }

        private void btnShowRight_Click(object sender, EventArgs e)
        {
            if (!ShowRight)
            {
                panel1.Show();
                ShowRight = true;
                btnShowRight.Text = ">>";
            }else
            {
                panel1.Hide();
                ShowRight = false;
                btnShowRight.Text = "<<";

            }
        }

        private void panelWeb_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.ScaleTransform(2, 2);

        }

        private void autoConnect_Click(object sender, EventArgs e)
        {
            m_pToggleRun_Click(sender, e);
            m_pToggleMic_Click(sender, e);
            button1_Click(sender, e);
            button2_Click(sender, e);
        }

        //private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        //{
        //    if (viewCam)
        //    {
        //        if (bufferCount > 1000) bufferCount = 0;
        //        if (currentBufferCount != bufferCount)
        //        {
        //            currentBufferCount = bufferCount;
        //            //LoadingText.Visible = false;
        //        }
        //        else
        //        {
        //                //_mjpeg = null;
        //                //_mjpeg = new MjpegDecoder();
        //                //_mjpeg.FrameReady += mjpeg_FrameReady;

        //                _mjpeg.ParseStream(new Uri(textBox1.Text));

        //            //LoadingText.Visible = true;
        //        }
        //    }
        //}


    }


    public static class ControlExtensions
    {
        /// <summary>
        /// Executes the Action asynchronously on the UI thread, does not block execution on the calling thread.
        /// </summary>
        /// <param name="control">the control for which the update is required</param>
        /// <param name="action">action to be performed on the control</param>
        public static void InvokeOnUiThreadIfRequired(this Control control, Action action)
        {
            //If you are planning on using a similar function in your own code then please be sure to
            //have a quick read over https://stackoverflow.com/questions/1874728/avoid-calling-invoke-when-the-control-is-disposed
            //No action
            if (control.Disposing || control.IsDisposed || !control.IsHandleCreated)
            {
                return;
            }

            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
