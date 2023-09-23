using ActUtlTypeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MelsecNetFW
{
    public partial class MainForm : Form
    {
        private ActUtlType _mplc = new ActUtlType();
        private bool b_PlcConnect = false;
        private bool b_TheAppRun = true;
        private CData _Data;
        public MainForm()
        {
            InitializeComponent();
            txt_Station.Text = "0";
            Thread _MainThread = new Thread(MainThread);
            _MainThread.Start();
            _MainThread.IsBackground = true;
        }

        private void syslogDisplay(string sMessage)
        {
            string _sBuff = DateTime.Now.ToString("[HH:mm:ss.fff]") + " " + sMessage + Environment.NewLine;
            txt_syslog.Invoke(new Action(() =>
            {
                txt_syslog.AppendText(_sBuff);
            }));
        }
        private bool sysSaveToFile(string RegisterName, string Triger, string Data)
        {
            string _sBuff = DateTime.Now.ToString("[HH:mm:ss.fff]") + "," + Data;
            string _Path = Environment.CurrentDirectory + "\\sysLog\\" + RegisterName + " " + Triger + "\\";
            if (!Directory.Exists(_Path)) Directory.CreateDirectory(_Path);
            _Path += DateTime.Now.ToString("yyyyMMdd HH") + ".csv";
            try
            {
                using (StreamWriter _sysLog = new StreamWriter(_Path, true))
                {
                    _sysLog.WriteLine(_sBuff);
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                int iResult = -1;
                if (b_PlcConnect)
                {
                    iResult = _mplc.Close();
                    if (iResult == 0)
                    {
                        b_PlcConnect = false;
                        btn_Connect.Text = "Kết nối";
                        btn_Connect.BackColor = Color.Transparent;
                        txt_Station.Enabled = true;
                        rtb_Setting.Enabled = true;
                    }
                    else MessageBox.Show("Lỗi ngắt kết nối PLC\n0x" + iResult.ToString("X"), "Đã xảy ra lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    rtb_Setting.Text = rtb_Setting.Text.ToUpper();
                    string[] _Config = rtb_Setting.Lines.ToArray();
                    if (_Config.Length <= 0)
                    {
                        syslogDisplay("Cần cài đặt trước khi kết nối");
                        return;
                    }
                    _Data = new CData(_Config.Length);
                    for (ushort i = 0; i < _Config.Length; i++)
                    {
                        if (_Config[i].Split(',').Length != 3)
                        {
                            syslogDisplay("Định dạng cài đặt không chính xác");
                            return;
                        }
                        string m_RegisterName = _Config[i].Split(',')[0].Trim();
                        string m_RegisterTriger = _Config[i].Split(',')[1].Trim();
                        ushort Length = Convert.ToUInt16(_Config[i].Split(',')[2].Trim());
                        _Data.RegisterName[i] = m_RegisterName;
                        _Data.RegisterTriger[i] = m_RegisterTriger;
                        _Data.DataLength[i] = Length;
                    }
                    syslogDisplay("[system] config set complete");
                    iResult = _mplc.Open();
                    if (iResult == 0)
                    {
                        b_PlcConnect = true;
                        btn_Connect.Text = "Ngắt kết nối";
                        btn_Connect.BackColor = Color.LightGreen;
                        txt_Station.Enabled = false;
                        rtb_Setting.Enabled = false;
                    }
                    else MessageBox.Show("Lỗi kết nối PLC\n0x" + iResult.ToString("X"), "Đã xảy ra lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                syslogDisplay(ex.Source + " : " + ex.Message);
            }
        }
        private void MainThread()
        {
            string _sStep = string.Empty;
            while (b_TheAppRun)
            {
                Thread.Sleep(1);
                this.Invoke(new Action(() =>
                {
                    this.Text = "PLC syslog " + DateTime.Now.ToString("HH:mm:ss.fff");
                }));
                if (!b_PlcConnect) continue;

                //Random _random = new Random();
                //_mplc.SetDevice2("D0", (short)(_random.Next(65, 90) << 8 | _random.Next(65, 90)));

                grb_Infor.Invoke(new Action(() =>
                {
                    grb_Infor.Text = "Thông tin: " + txt_syslog.Lines.Length.ToString("(0)") + " Step: " + _sStep;
                    if (txt_syslog.Lines.Length > 1000) txt_syslog.Clear();
                }));
                _sStep = string.Empty;
                for (int i = 0; i < _Data.Length; i++)
                {
                    _sStep += _Data.sq_Auto[i].ToString() + " ";
                    int iResult = -1;
                    switch (_Data.sq_Auto[i])
                    {
                        case 0: // Wait triger ON
                            {
                                iResult = _mplc.GetDevice(_Data.RegisterTriger[i], out _Data.TrigerData[i]);
                                if (iResult == 0 && _Data.TrigerData[i] == 1)
                                {
                                    syslogDisplay("[Sequen" + i.ToString() + "] Triger " + _Data.RegisterTriger[i] + " turned On");
                                    _Data.sq_Auto[i]++;
                                }
                                else if(iResult != 0)
                                {
                                    syslogDisplay("[Sequen" + i.ToString() + "] Lỗi đọc dữ liệu thanh ghi " + _Data.RegisterTriger[i] + ": 0x" + iResult.ToString("X"));
                                }
                                break;
                            }
                        case 1: // Read data & convert to string
                            {
                                short[] _ReadBuffer = new short[_Data.DataLength[i]];
                                iResult = _mplc.ReadDeviceBlock2(_Data.RegisterName[i], _Data.DataLength[i], out _ReadBuffer[0]);
                                if (iResult == 0)
                                {
                                    byte[] _ReadBuffer8 = new byte[_Data.DataLength[i] * 2];
                                    for (int index = 0; index < _ReadBuffer.Length; index++)
                                    {
                                        _ReadBuffer8[index * 2] = (byte)(_ReadBuffer[index] & 0x00FF);
                                        _ReadBuffer8[index * 2 + 1] = (byte)(_ReadBuffer[index] >> 8);
                                    }
                                    _Data.RegisterData[i] = string.Empty;
                                    _Data.RegisterData[i] = Encoding.ASCII.GetString(_ReadBuffer8);
                                    //syslogDisplay("[Sequen" + i.ToString() + "] Data form " + _Data.RegisterName[i] + ": " + _Data.RegisterData[i]);
                                    syslogDisplay("[Sequen" + i.ToString() + "] Convert to string");
                                    _Data.sq_Auto[i]++;
                                }
                                else
                                {
                                    syslogDisplay("[Sequen" + i.ToString() + "] Lỗi đọc dữ liệu thanh ghi " + _Data.RegisterName[i] + ": 0x" + iResult.ToString("X"));
                                }
                                break;
                            }
                        case 2: // Save data to file
                            {
                                if(!sysSaveToFile(_Data.RegisterName[i], _Data.RegisterTriger[i], _Data.RegisterData[i]))
                                {
                                    b_PlcConnect = false;
                                    _mplc.Close();
                                    btn_Connect.Invoke(new Action(() =>
                                    {
                                        btn_Connect.Text = "Kết nối";
                                        btn_Connect.BackColor = Color.Transparent;
                                        txt_Station.Enabled = true;
                                        rtb_Setting.Enabled = true;
                                    }));
                                }
                                syslogDisplay("[Sequen" + i.ToString() + "] Save data to file");
                                syslogDisplay("[Sequen" + i.ToString() + "] Wait triger OFF");
                                _Data.sq_Auto[i]++;
                                break;
                            }
                        case 3: // Wait triger Off
                            {
                                iResult = _mplc.GetDevice(_Data.RegisterTriger[i], out _Data.TrigerData[i]);
                                if (iResult == 0 && _Data.TrigerData[i] == 0)
                                {
                                    syslogDisplay("[Sequen" + i.ToString() + "] Triger " + _Data.RegisterTriger[i] + " turned Off");
                                    _Data.sq_Auto[i] = 0;
                                }
                                else if (iResult != 0)
                                {
                                    syslogDisplay("[Sequen" + i.ToString() + "] Lỗi đọc dữ liệu thanh ghi " + _Data.RegisterTriger[i] + ": 0x" + iResult.ToString("X"));
                                }
                                break;
                            }
                    }
                }
            }
        }
        private void btn_Export_Click(object sender, EventArgs e)
        {
            if(rtb_Setting.Lines.Length <= 0)
            {
                MessageBox.Show("Cài đặt đang rỗng", "Đã xảy ra lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }
            SaveFileDialog _Save = new SaveFileDialog();
            _Save.InitialDirectory = Environment.CurrentDirectory;
            _Save.Filter = "ini files (*.ini)|*.ini|All files (*.*)|*.*";
            _Save.FilterIndex = 1;
            //_Save.RestoreDirectory = true;

            if (_Save.ShowDialog() == DialogResult.OK)
            {
                string _Path = _Save.FileName;
                using (StreamWriter _sysLog = new StreamWriter(_Path))
                {
                    _sysLog.Write(rtb_Setting.Text);
                }
            }
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            OpenFileDialog _Open = new OpenFileDialog();
            _Open.InitialDirectory = Environment.CurrentDirectory;
            _Open.Filter = "ini files (*.ini)|*.ini|All files (*.*)|*.*";
            _Open.FilterIndex = 1;
            //_Open.RestoreDirectory = true;
            if(_Open.ShowDialog() == DialogResult.OK)
            {
                string[] _readLine = File.ReadAllLines(_Open.FileName);
                if( _readLine.Length <= 0 )
                {
                    MessageBox.Show("File cài đặt rỗng", "Đã xảy ra lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string _configText = string.Empty;
                foreach (string line in _readLine)
                {
                    if (line.Split(',').Length != 3)
                    {
                        MessageBox.Show("Cài đặt trong file không đúng (" + line + ")", "Đã xảy ra lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (_configText != string.Empty) _configText += Environment.NewLine + line;
                    else _configText = line;
                }
                rtb_Setting.Text = _configText;
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            b_TheAppRun = false;
        }
    }
}
