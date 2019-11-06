using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Diagnostics;

namespace Collector
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        string usbPath = "SYSTEM\\CurrentControlSet\\Enum\\USBSTOR";
        string unistallPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
        string storeMediumPath = "SYSTEM\\CurrentControlSet\\Enum\\SCSI";


        List<string> usbList = new List<string>();
        List<string> usbPathList = new List<string>();
        List<string> usbPrintList = new List<string>();

        List<string> unintallList = new List<string>();
        List<string> unintallPathList = new List<string>();


        List<string> storeSecList = new List<string>();
        List<string> storeSecPathList = new List<string>();
        List<string> storeSecNameList = new List<string>();

        List<string> storeList = new List<string>();
        List<string> storePathList = new List<string>();
        List<string> storeNameList = new List<string>();

        DataTable goodDt = new DataTable();
        DataTable badDt = new DataTable();

        DataTable resultDt = new DataTable();

        DataTable fixGoodDt = new DataTable();
        DataTable fixBadDt = new DataTable();

        DataTable leftTopDt = new DataTable();
        DataTable leftBottomDt = new DataTable();
        DataTable rightTopDt = new DataTable();
        DataTable rightBottomDt = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
            GetUsbRegistry();
            GetUnistallRegistry();
            GetStoreRegistry();
            SettingHomeDt();
            SettingResultDt();
            SettingFixDt();
            SettingImprovementDt();
        }


        private void SettingHomeDt() {

            goodDt.Columns.Add("titleGood");
            goodDt.Columns.Add("homeGood");
            
            goodDt.Rows.Add(new string[] { "운영체제","사용자 비밀번호 설정확인" });
            goodDt.Rows.Add(new string[] { "", "운영체제 최신버전 확인" });
            goodDt.Rows.Add(new string[] { "", "보안시스템 설치 확인" });
            goodDt.Rows.Add(new string[] { "저장매체", "보안시스템 설치 확인" });

            badDt.Columns.Add("titleBad");
            badDt.Columns.Add("homeBad");

            badDt.Rows.Add(new string[] { "운영체제", "사용자 인증방식 복잡 정도 확인" });
            badDt.Rows.Add(new string[] { "", "보안시스템 해지 흔적 확인" });
            
            badDt.Rows.Add(new string[] { "저장매체", "(이동식)저장매체 연결 흔적 확인" });
            badDt.Rows.Add(new string[] { "", "고정형 저장장치 연결 흔적 확인" });
            badDt.Rows.Add(new string[] { "", "응용 프로그램 최신버전 확인" });
            badDt.Rows.Add(new string[] { "", "보안시스템 해지 흔적 확인" });

            GoodContent.ItemsSource = goodDt.DefaultView;
            BadContent.ItemsSource = badDt.DefaultView;
        }

        private void SettingResultDt()
        {

            resultDt.Columns.Add("Title");
            resultDt.Columns.Add("Elements");
            resultDt.Columns.Add("Status");
            resultDt.Columns.Add("Path");

            resultDt.Rows.Add(new string[] { "운영체제", "사용자 비밀번호 설정확인", "O", "Services\\Netlogon\\Parameters" });
            resultDt.Rows.Add(new string[] { "", "사용자 인증방식 복잡 정도 확인", "X", "Windows\\CurrentVersion\\Uninstall" });
            resultDt.Rows.Add(new string[] { "","운영체제 최신버전 확인", "O", "Windows\\CurrentVersion\\WindowsUpdate\\Trace" });

            resultDt.Rows.Add(new string[] { "","보안시스템 설치 확인", "O", "SOFTWARE\\Ahnlab\\V3Lite4" });
            resultDt.Rows.Add(new string[] { "","", "", "SOFTWARE\\Ahnlab\\Safe Transaction" });
            resultDt.Rows.Add(new string[] { "", "", "", "SOFTWARE\\MarkAny\\ImageSAFERv5" });
            resultDt.Rows.Add(new string[] { "", "", "", "SOFTWARE\\McAfee" });

            resultDt.Rows.Add(new string[] { "", "보안시스템 해지 흔적 확인", "X", "Windows\\CurrentVersion\\Uninstall" });

            for (int i = 0; i < usbPathList.Count; i++)
            {
                if (i != 0)
                {
                    resultDt.Rows.Add(new string[] { "","", "", usbPrintList[i] });
                }
                else
                {
                    resultDt.Rows.Add(new string[] { "저장매체", "(이동식)저장매체 연결 흔적 확인", "X", usbPrintList[i] });
                }
            }

            for (int j = 0; j < storeNameList.Count; j++)
            {
                if (j != 0)
                {
                    resultDt.Rows.Add(new string[] { "", "", "", storeNameList[j] });
                }
                else
                {
                    resultDt.Rows.Add(new string[] { "", "고정형 저장장치 연결 흔적 확인", "X", storeNameList[j] });
                }
            }

            resultDt.Rows.Add(new string[] { "", "응용 프로그램 최신버전 확인", "X", "SYSTEM\\ControlSet001\\Enum\\ROOT" });
            resultDt.Rows.Add(new string[] { "", "보안시스템 설치 확인", "O", "SOFTWARE\\Ahnlab\\V3Lite4" });
            resultDt.Rows.Add(new string[] { "", "", "", "SOFTWARE\\McAfee" });
            resultDt.Rows.Add(new string[] { "", "보안시스템 해지 흔적 확인", "X", "Windows\\CurrentVersion\\Uninstall" });
            //보안 소프트웨어 설치



            resultData.ItemsSource = resultDt.DefaultView;
            //resultData.Columns[1].CellStyle = new Style(typeof(DataGridCell));
            //resultData.Columns[1].CellStyle.Setters.Add(new Setter(DataGridCell.HorizontalAlignmentProperty, HorizontalAlignment.Center));
            //resultData.Columns[1].CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.White)));
            //resultData.Columns[1].CellStyle.Setters.Add(new Setter(DataGridCell.BorderBrushProperty, new SolidColorBrush(Colors.White)));
            //resultData.Columns[1].CellStyle.Setters.Add(new Setter(DataGridCell.FontSizeProperty, Convert.ToDouble(20)));
        }

        private void SettingFixDt()
        {
            fixGoodDt.Columns.Add("Title");
            fixGoodDt.Columns.Add("Elements");
            fixGoodDt.Columns.Add("Path");

            fixGoodDt.Rows.Add(new string[] { "운영체제", "사용자 비밀번호 설정확인",  "Services\\Netlogon\\Parameters" });
            fixGoodDt.Rows.Add(new string[] { "", "운영체제 최신버전 확인", "Windows\\CurrentVersion\\WindowsUpdate\\Trace" });
            fixGoodDt.Rows.Add(new string[] { "", "보안시스템 설치 확인",  "SOFTWARE\\Ahnlab\\V3Lite4" });
            fixGoodDt.Rows.Add(new string[] { "", "",  "SOFTWARE\\Ahnlab\\Safe Transaction" });
            fixGoodDt.Rows.Add(new string[] { "", "",  "SOFTWARE\\MarkAny\\ImageSAFERv5" });
            fixGoodDt.Rows.Add(new string[] { "", "",  "SOFTWARE\\McAfee" });
            fixGoodDt.Rows.Add(new string[] { "저장매체", "보안시스템 설치 확인", "SOFTWARE\\Ahnlab\\V3Lite4" });
            fixGoodDt.Rows.Add(new string[] { "", "", "SOFTWARE\\McAfee" });

            fixBadDt.Columns.Add("Title");
            fixBadDt.Columns.Add("Elements");
            fixBadDt.Columns.Add("Path");
            fixBadDt.Columns.Add("Advice");

            fixBadDt.Rows.Add(new string[] { "운영체제", "사용자 인증방식 복잡 정도 확인", "Windows\\CurrentVersion\\WindowsUpdate\\Trace", "사용자 인증 보안시스템 설치 권장" });
            fixBadDt.Rows.Add(new string[] { "", "보안시스템 해지 흔적 확인", "Windows\\CurrentVersion\\Uninstall", "보안시스템 해지 흔적 확인(소명서 제출 요망)" });
            
            for (int i = 0; i < usbPathList.Count; i++)
            {
                if (i > 1)
                {
                    fixBadDt.Rows.Add(new string[] { "", "", usbPrintList[i], "" });
                }
                if(i == 0)
                {
                    fixBadDt.Rows.Add(new string[] { "저장매체", "(이동식)저장매체 연결 흔적 확인", usbPrintList[i],"비인가 이동식 저장매체 6개 연결" });
                }
                if (i == 1)
                {
                    fixBadDt.Rows.Add(new string[] { "", "", usbPrintList[i], "(소명서 제출 요망)" });
                }
            }

            for (int j = 0; j < storeNameList.Count; j++)
            {
                if (j > 1)
                {
                    fixBadDt.Rows.Add(new string[] { "", "", storeNameList[j], "" });
                }
                if (j == 0)
                {
                    fixBadDt.Rows.Add(new string[] { "", "고정형 저장장치 연결 흔적 확인", storeNameList[j], "비인가 고정형 저장매체 5개 연결" });
                }
                if (j == 1)
                {
                    fixBadDt.Rows.Add(new string[] { "", "", storeNameList[j], "(소명서 제출 요망)" });
                }

            }

            fixBadDt.Rows.Add(new string[] { "", "응용 프로그램 최신버전 확인", "SYSTEM\\ControlSet001\\Enum\\ROOT", "응용 프로그램 업데이트 권장" });

            fixBadDt.Rows.Add(new string[] { "", "보안시스템 해지 흔적 확인", "Windows\\CurrentVersion\\Uninstall", "보안시스템 해지 흔적 확인(소명서 제출 요망)" });


            goodData.ItemsSource = fixGoodDt.DefaultView;
            badData.ItemsSource = fixBadDt.DefaultView;
        }

        private void SettingImprovementDt()
        {
            leftTopDt.Columns.Add("Title");
            leftTopDt.Columns.Add("Elements");
            leftTopDt.Columns.Add("Status");
            leftTopDt.Columns.Add("Path");


            leftBottomDt.Columns.Add("Title");
            leftBottomDt.Columns.Add("Elements");
            leftBottomDt.Columns.Add("Status");
            leftBottomDt.Columns.Add("Path");


            rightTopDt.Columns.Add("Title");
            rightTopDt.Columns.Add("Elements");
            rightTopDt.Columns.Add("Status");
            rightTopDt.Columns.Add("Path");


            rightBottomDt.Columns.Add("Title");
            rightBottomDt.Columns.Add("Elements");
            rightBottomDt.Columns.Add("Status");
            rightBottomDt.Columns.Add("Path");

            leftTopDt.Rows.Add(new string[] { "운영체제", "운영체제 최신버전 확인", "X", "Windows\\CurrentVersion\\WindowsUpdate" });


            leftBottomDt.Rows.Add(new string[] { "운영체제", "사용자 인증방식 복잡 정도 확인", "X", "Windows\\CurrentVersion\\Uninstall" });
            leftBottomDt.Rows.Add(new string[] { "", "보안시스템 해지 확인", "X", "Windows\\CurrentVersion\\Uninstall" });

            leftBottomDt.Rows.Add(new string[] { "저장매체", "(이동식)저장매체 연결 흔적 확인", "X", "Enum\\USBSTOR" });
            leftBottomDt.Rows.Add(new string[] { "", "고정형 저장장치 연결 흔적 확인", "X", "Enum\\SCSI" });

            leftBottomDt.Rows.Add(new string[] { "", "응용 프로그램 최신버전 확인", "X", "SYSTEM\\ControlSet001\\Enum\\ROOT" });
            leftBottomDt.Rows.Add(new string[] { "", "보안시스템 해지 확인", "X", "Windows\\CurrentVersion\\Uninstall" });

             

            rightTopDt.Rows.Add(new string[] { "운영체제", "운영체제 최신버전 확인", "O", "Windows\\CurrentVersion\\WindowsUpdate\\Trace" });

            rightBottomDt.Rows.Add(new string[] { "운영체제", "사용자 인증방식 복잡 정도 확인", "X", "Windows\\CurrentVersion\\Uninstall" });
            rightBottomDt.Rows.Add(new string[] { "", "보안시스템 해지 확인", "X", "Windows\\CurrentVersion\\Uninstall" });

            for (int i = 0; i < usbPathList.Count; i++)
            {
                if (i != 0)
                {
                    rightBottomDt.Rows.Add(new string[] { "", "", "", usbPrintList[i] });
                }
                else
                {
                    rightBottomDt.Rows.Add(new string[] { "저장매체", "(이동식)저장매체 연결 흔적 확인", "X", usbPrintList[i] });
                }
            }

            for (int j = 0; j < storeNameList.Count; j++)
            {
                if (j != 0)
                {
                    rightBottomDt.Rows.Add(new string[] { "", "", "", storeNameList[j] });
                }
                else
                {
                    rightBottomDt.Rows.Add(new string[] { "", "고정형 저장장치 연결 흔적 확인", "X", storeNameList[j] });
                }
            }

            rightBottomDt.Rows.Add(new string[] { "", "응용 프로그램 최신버전 확인", "X", "SYSTEM\\ControlSet001\\Enum\\ROOT" });
            rightBottomDt.Rows.Add(new string[] { "", "보안시스템 해지 흔적 확인", "X", "Windows\\CurrentVersion\\Uninstall" });

            LeftTopData.ItemsSource = leftTopDt.DefaultView;
            LeftBottomData.ItemsSource = leftBottomDt.DefaultView;
            RightTopData.ItemsSource = rightTopDt.DefaultView;
            RightBottomData.ItemsSource = rightBottomDt.DefaultView;
        }

        private void ButtonPopupLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HistoryMenuBotton_Click(object sender, RoutedEventArgs e)
        {
            HomeContents.Visibility = Visibility.Collapsed;
            RateContents.Visibility = Visibility.Collapsed;
            FixContents.Visibility = Visibility.Collapsed;
            ImprovementContents.Visibility = Visibility.Collapsed;


            //HistoryContents.Visibility = Visibility.Visible;
        }

        private void ImprovementMenuBotton_Click(object sender, RoutedEventArgs e)
        {
            HomeContents.Visibility = Visibility.Collapsed;
            RateContents.Visibility = Visibility.Collapsed;
            FixContents.Visibility = Visibility.Collapsed;
            ImprovementContents.Visibility = Visibility.Visible;
            //HistoryContents.Visibility = Visibility.Collapsed;
        }

        private void FixMenuBotton_Click(object sender, RoutedEventArgs e)
        {
            HomeContents.Visibility = Visibility.Collapsed;
            RateContents.Visibility = Visibility.Collapsed;
            FixContents.Visibility = Visibility.Visible;
            ImprovementContents.Visibility = Visibility.Collapsed;
            //HistoryContents.Visibility = Visibility.Collapsed;
        }

        private void RatingMenuBotton_Click(object sender, RoutedEventArgs e)
        {
            HomeContents.Visibility = Visibility.Collapsed;
            RateContents.Visibility = Visibility.Visible;   
            FixContents.Visibility = Visibility.Collapsed;
            ImprovementContents.Visibility = Visibility.Collapsed;
            //HistoryContents.Visibility = Visibility.Collapsed;
        }

        private void HomeMenuBotton_Click(object sender, RoutedEventArgs e)
        {
            HomeContents.Visibility = Visibility.Visible;
            RateContents.Visibility = Visibility.Collapsed;
            FixContents.Visibility = Visibility.Collapsed;
            ImprovementContents.Visibility = Visibility.Collapsed;
            //HistoryContents.Visibility = Visibility.Collapsed;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }



        private void GetUsbRegistry()
        {
            RegistryKey machineReg = Registry.LocalMachine;

            string tempPath_1;
            string tempPath_2;

            List<string> tempusblist = new List<string>();

            try
            {
                using ((machineReg = machineReg.OpenSubKey(usbPath, true)))
                {
                    if (!(machineReg == null))
                    {
                        string[] temp = machineReg.GetSubKeyNames();

                        foreach (string str in temp)
                        {
                            tempusblist.Add(str);
                        }

                        for (int i = 0; i < tempusblist.Count; i++)
                        {
                            RegistryKey tempReg = Registry.LocalMachine;

                            tempPath_1 = usbPath + "\\" + tempusblist[i];
                            tempReg = tempReg.OpenSubKey(tempPath_1, true);

                            string[] temp2 = tempReg.GetSubKeyNames();

                            foreach(string str in temp2)
                            {
                                tempPath_2 = tempPath_1 + "\\" + str;

                                usbList.Add(str);
                                usbPathList.Add(tempPath_2);
                            }
                        }
                    }
                }

                for(int i = 0; i< usbPathList.Count; i++)
                {
                    string temp = usbPathList[i].Substring(usbPath.Length+1);
                    usbPrintList.Add(temp);
                }

            }
            catch
            {

            }
        }
        private void GetUnistallRegistry()
        {
            RegistryKey machineReg = Registry.LocalMachine;

            string tempPath_1;
            
            List<string> tempusblist = new List<string>();

            try
            {
                using ((machineReg = machineReg.OpenSubKey(unistallPath, true)))
                {
                    if (!(machineReg == null))
                    {
                        string[] temp = machineReg.GetSubKeyNames();

                        foreach (string str in temp)
                        {
                            tempusblist.Add(str);
                        }
                        
                        for (int i = 0; i < tempusblist.Count; i++)
                        {
                            RegistryKey tempReg = Registry.LocalMachine;

                            int checkCnt = 0;
                            int uninstallCheck = 0;
                            tempPath_1 = unistallPath + "\\" + tempusblist[i];
                            tempReg = tempReg.OpenSubKey(tempPath_1, true);

                            string[] temp2 = tempReg.GetValueNames();
                            if (temp2.Length != 0)
                            {
                                foreach (string str in temp2)
                                {
                                    if (str == "DisplayName")
                                    {
                                        checkCnt = 1;
                                    }
                                    if (str == "QuietUninstallString")
                                    {
                                        uninstallCheck = 1;
                                    }

                                }
                            }
                            
                            if(checkCnt == 1)
                            {
                                
                                string temp3 = tempReg.GetValue("DisplayName").ToString();
                                unintallList.Add(temp3);
                                unintallPathList.Add(tempPath_1);
                            }
                            else
                            {
                                unintallList.Add(tempusblist[i]);
                                unintallPathList.Add(tempPath_1);
                            }

                            if(uninstallCheck == 1)
                            {
                                
                            }

                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void GetStoreRegistry()
        {
            RegistryKey machineReg = Registry.LocalMachine;

            string tempPath_1;

            List<string> tempusblist = new List<string>();

            try
            {
                using ((machineReg = machineReg.OpenSubKey(storeMediumPath, true)))
                {
                    if (!(machineReg == null))
                    {
                        string[] temp = machineReg.GetSubKeyNames();

                        foreach (string str in temp)
                        {
                            tempusblist.Add(str);
                        }
                        
                        for (int i = 0; i < tempusblist.Count; i++)
                        {
                            RegistryKey tempReg = Registry.LocalMachine;

                            tempPath_1 = storeMediumPath + "\\" + tempusblist[i];
                            tempReg = tempReg.OpenSubKey(tempPath_1, true);

                            string[] temp2 = tempReg.GetSubKeyNames();
                            
                            foreach (string str in temp2)
                            {
                                storeList.Add(str);
                                storePathList.Add(tempPath_1);
                                storeNameList.Add(tempusblist[i] + "\\" + str);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

    }
}
