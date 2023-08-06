using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JVData_Struct;

namespace JraVanCsharp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // 引数設定
            string sid = "Test";

            // JVLink 初期化
            int lReturnCode = axJVLink1.JVInit(sid);

            // エラー判定
            if (lReturnCode != 0)
            {
                MessageBox.Show("JVInit エラー コード：" + lReturnCode);
            }
        }

        private void mnuConfJV_Click(object sender, EventArgs e)
        {
            // 設定画面表示
            long lReturnCode = axJVLink1.JVSetUIProperties();
            if (lReturnCode != 0)
            {
                MessageBox.Show("JVSetUIPropertiesエラー コード：" + lReturnCode);
            }
        }

        private void btnGetJVData_Click(object sender, EventArgs e)
        {
            int lReturnCode;
            try
            {
                // 引数設定
                // JVOpen:ファイル識別子
                string strDataSpec = "RACE";
                // JVOpen:データ提供日付
                string strFromTime = "20050301000000";
                // JVOpen:オプション
                int lOption = 2;

                // JVLink 戻り値
                int lReadCount = 0;
                // JVOpen: 総ダウンロードファイル数
                int lDownloadCount = 0;
                // JVOpen: 最新ファイルのタイムスタンプ
                string strLastFileTimestamp;

                // JVLink ダウンロード処理
                lReturnCode = axJVLink1.JVOpen(
                    strDataSpec, strFromTime, lOption, ref lReadCount, ref lDownloadCount, out strLastFileTimestamp);

                // エラー判定
                if (lReturnCode != 0)
                {
                    MessageBox.Show("JVOpen エラー：" + lReturnCode);
                }
                else
                {
                    MessageBox.Show(
                        "戻り値 : " + lReturnCode + "\n"
                        + "読み込みファイル数 : " + lReadCount + "\n"
                        + "ダウンロードファイル数 : " + lDownloadCount + "\n"
                        + "タイムスタンプ : " + strLastFileTimestamp);

                    if (lReadCount > 0)
                    {
                        // JVRead: データ格納バッファサイズ
                        int lBuffSize = 110000;
                        // JVRead: データ格納バッファ
                        string strBuff;
                        // JVRead: ダウンロードファイル名
                        string strFileName;
                        // レース詳細情報構造体
                        JV_RA_RACE RaceInfo = new JV_RA_RACE();

                        while (true)
                        {
                            // JVRead で1行読み込み
                            lReturnCode = axJVLink1.JVRead(out strBuff, out lBuffSize, out strFileName);
                            // リターンコードにより処理を分枝
                            switch (lReturnCode)
                            {
                                // 全ファイル読み込み終了
                                case 0:
                                    goto readFinish;
                                // ファイル切り替わり
                                case -1:
                                // ダウンロード中
                                case -3:
                                    continue;
                                // Init されてない
                                case -201:
                                    MessageBox.Show("JVInit が行われていません。");
                                    goto readFinish;
                                // Open されてない
                                case -203:
                                    MessageBox.Show("JVOpen が行われていません。");
                                    goto readFinish;
                                // ファイルがない
                                case -503:
                                    MessageBox.Show(strFileName + "が存在しません。");
                                    goto readFinish;
                                // 正常読み込み
                                case int i when i > 0:
                                    // レコード種別 ID の識別
                                    if (strBuff.Substring(0, 2) == "RA")
                                    {
                                        // レース詳細のみ処理

                                        // レース詳細構造体への展開
                                        RaceInfo.SetDataB(ref strBuff);
                                        // データ表示
                                        rtbData.AppendText(
                                            "年:" + RaceInfo.id.Year
                                            + " 月日:" + RaceInfo.id.MonthDay
                                            + " 場:" + RaceInfo.id.JyoCD
                                            + " 回次:" + RaceInfo.id.Kaiji
                                            + " 日次:" + RaceInfo.id.Nichiji
                                            + " Ｒ:" + RaceInfo.id.RaceNum
                                            + " レース名:" + RaceInfo.RaceInfo.Ryakusyo10 + "\n");
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    readFinish:;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return;
            }

            // JVLink 終了処理
            lReturnCode = axJVLink1.JVClose();
            if (lReturnCode != 0)
            {
                MessageBox.Show("JVClose エラー：" + lReturnCode);
            }
        }
    }
}
