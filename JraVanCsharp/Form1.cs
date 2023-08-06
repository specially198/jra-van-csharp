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
