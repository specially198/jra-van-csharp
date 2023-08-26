using System.Data;
using static JVData_Struct;
using Application = System.Windows.Forms.Application;

namespace JraVanCsharp
{
    public partial class frmMain : Form
    {
        // JVOpen:総ダウンロードファイル数
        private int lDownloadCount;
        // コード変換インスタンス
        private ClsCodeConv clsCodeConv = null!;

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

            // コード変換インスタンス生成
            clsCodeConv = new ClsCodeConv(Application.StartupPath + "/CodeTable.csv");
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
            DataSet jvdds = new DataSet("JvDatas");
            try
            {
                // 表示ウィンドウの初期化
                rtbData.Clear();

                // 進捗表示初期設定
                // タイマー停止
                tmrDownload.Enabled = false;
                // JVData ダウンロード進捗
                prgDownload.Value = 0;
                // JVData 読み込み進捗
                prgJVRead.Value = 0;

                // 引数設定
                // JVOpen:ファイル識別子
                string strDataSpec = "RACE";
                // JVOpen:データ提供日付
                string strFromTime = "20050301000000";
                // JVOpen:オプション
                int lOption = 2;

                // JVLink 戻り値
                int lReadCount = 0;
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

                    // 進捗表示プログレスバー最大値設定
                    if (lDownloadCount == 0)
                    {
                        // ダウンロード必要無し
                        prgDownload.Maximum = 100;
                        prgDownload.Value = 100;
                    }
                    else
                    {
                        prgDownload.Maximum = lDownloadCount;
                        // タイマー開始
                        tmrDownload.Enabled = true;
                    }
                    prgJVRead.Maximum = lReadCount;

                    if (lReadCount > 0)
                    {
                        // JVRead: データ格納バッファサイズ
                        int lBuffSize = 110000;
                        // JVRead: データ格納バッファ
                        string strBuff;
                        // JVRead: ダウンロードファイル名
                        string strFileName;
                        // レース詳細情報構造体
                        JV_RA_RACE raceInfo = new JV_RA_RACE();
                        // 馬毎レース情報構造体
                        JV_SE_RACE_UMA raceUmaInfo = new JV_SE_RACE_UMA();

                        while (true)
                        {
                            // バックグラウンドでの処理を実行
                            Application.DoEvents();

                            // JVRead で1行読み込み
                            lReturnCode = axJVLink1.JVRead(out strBuff, out lBuffSize, out strFileName);
                            // リターンコードにより処理を分枝
                            switch (lReturnCode)
                            {
                                // 全ファイル読み込み終了
                                case 0:
                                    // 進捗表示
                                    prgJVRead.Value = prgJVRead.Maximum;
                                    goto readFinish;
                                // ファイル切り替わり
                                case -1:
                                    prgJVRead.Value = prgJVRead.Value + 1;
                                    continue;
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
                                        raceInfo.SetDataB(ref strBuff);

                                        // 読み込んだ情報をデータセットへ格納する
                                        AdoUtility.SetJVDataRaceDataSet(raceInfo, ref jvdds);
                                    }
                                    else if (strBuff.Substring(0, 2) == "SE")
                                    {
                                        // 馬毎レース情報構造体への展開
                                        raceUmaInfo.SetDataB(ref strBuff);

                                        // 読み込んだ情報をデータセットへ格納する
                                        AdoUtility.SetJVDataUmaRaceDataSet(raceUmaInfo, ref jvdds);
                                    }
                                    else
                                    {
                                        // レース詳細、馬毎レース情報以外は読み飛ばす
                                        axJVLink1.JVSkip();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    readFinish:;
                    }

                    // タイマ有効時は、無効化する
                    if (tmrDownload.Enabled)
                    {
                        tmrDownload.Enabled = false;
                        prgDownload.Value = prgDownload.Maximum;
                    }
                }

                // データセットに格納した各情報を画面に表示
                DataTable dtRace = jvdds.Tables["Race"]!;

                // レース詳細の件数を取得
                var raceCount = dtRace.Rows.Count;

                // レース詳細が存在しない場合
                if (raceCount == 0)
                {
                    MessageBox.Show("レース情報が存在しません");
                }
                else
                {
                    // レース詳細の第10レースを検索(複数行)
                    var races = dtRace.AsEnumerable()
                        .Where(r => r.Field<string>("RaceNum") == "10")
                        .ToList();
                    if (races.Count == 0)
                    {
                        MessageBox.Show("検索結果が0件です");
                        return;
                    }

                    // 取得結果の一件目を構造体にセット
                    JV_RA_RACE raceInfo = new JV_RA_RACE();
                    AdoUtility.SetJVDataRaceStructure(races[0], ref raceInfo);

                    // 画面表示
                    rtbData.AppendText(
                        "年:" + raceInfo.id.Year
                        + " 月日:" + raceInfo.id.MonthDay
                        + " 場:" + clsCodeConv.GetCodeName("2001", raceInfo.id.JyoCD, 3)
                        + " 回次:" + raceInfo.id.Kaiji
                        + " 日次:" + raceInfo.id.Nichiji
                        + " Ｒ:" + raceInfo.id.RaceNum
                        + " レース名:" + raceInfo.RaceInfo.Ryakusyo10 + "\n");

                    // 表示したレース詳細の馬毎レース情報を全馬分表示
                    // データセットからレース詳細のキーで馬毎レース情報の行(複数行)を検索
                    var raceUmas = jvdds.Tables["RaceUma"]!.AsEnumerable()
                        .Where(r => r.Field<string>("Year") == raceInfo.id.Year)
                        .Where(r => r.Field<string>("MonthDay") == raceInfo.id.MonthDay)
                        .Where(r => r.Field<string>("JyoCD") == raceInfo.id.JyoCD)
                        .Where(r => r.Field<string>("Kaiji") == raceInfo.id.Kaiji)
                        .Where(r => r.Field<string>("Nichiji") == raceInfo.id.Nichiji)
                        .Where(r => r.Field<string>("RaceNum") == raceInfo.id.RaceNum)
                        .ToList();

                    // 一行ずつ取り出し、画面に表示
                    foreach (var raceUma in raceUmas)
                    {
                        // 行の情報を構造体にセット
                        JV_SE_RACE_UMA raceUmaInfo = new JV_SE_RACE_UMA();
                        AdoUtility.SetJVDataUmaRaceStructure(raceUma, ref raceUmaInfo);
                        // 画面表示
                        rtbData.AppendText(
                            "枠:" + raceUmaInfo.Wakuban
                            + " 馬番:" + raceUmaInfo.Umaban
                            + " 馬名:" + raceUmaInfo.Bamei
                            + " 騎手:" + raceUmaInfo.KisyuRyakusyo + "\n");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // JVLink 終了処理
                lReturnCode = axJVLink1.JVClose();
                if (lReturnCode != 0)
                {
                    MessageBox.Show("JVClose エラー：" + lReturnCode);
                }
            }

            // 後処理
            // JV-Dataデータセットのリソース解放
            jvdds.Dispose();
        }

        private void tmrDownload_Tick(object sender, EventArgs e)
        {
            // ダウンロード済のファイル数を返す
            int lReturnCode = axJVLink1.JVStatus();
            // エラー判定
            if (lReturnCode < 0)
            {
                // エラー

                MessageBox.Show("JVStatusエラー:" + lReturnCode);
                // タイマー停止
                tmrDownload.Enabled = false;
                // JVLink終了処理
                lReturnCode = axJVLink1.JVClose();
                if (lReturnCode != 0)
                {
                    MessageBox.Show("JVCloseエラー:" + lReturnCode);
                }
            }
            else if (lReturnCode < lDownloadCount)
            {
                // ダウンロード中
                // プログレス表示
                prgDownload.Value = lReturnCode;
            }
            else if (lReturnCode == lDownloadCount)
            {
                // ダウンロード完了
                // タイマー停止
                tmrDownload.Enabled = false;
                // プログレス表示
                prgDownload.Value = lReturnCode;
            }
        }
    }
}
