using System.Data;
using System.Text;
using static JVData_Struct;

namespace JraVanCsharp
{
    class AdoUtility
    {
        public static void SetJVDataRaceDataSet(JV_RA_RACE raceDat, ref DataSet jvdds)
        {
            if (jvdds.Tables["Race"] is null)
            {
                DataTable dt = new DataTable("Race");
                dt.Columns.Add("Year");
                dt.Columns.Add("MonthDay");
                dt.Columns.Add("JyoCD");
                dt.Columns.Add("Kaiji");
                dt.Columns.Add("Nichiji");
                dt.Columns.Add("RaceNum");
                dt.Columns.Add("RecordSpec");
                dt.Columns.Add("DataKubun");
                dt.Columns.Add("MakeDate");
                dt.Columns.Add("YoubiCD");
                dt.Columns.Add("TokuNum");
                dt.Columns.Add("Hondai");
                dt.Columns.Add("Fukudai");
                dt.Columns.Add("Kakko");
                dt.Columns.Add("HondaiEng");
                dt.Columns.Add("FukudaiEng");
                dt.Columns.Add("KakkoEng");
                dt.Columns.Add("Ryakusyo10");
                dt.Columns.Add("Ryakusyo6");
                dt.Columns.Add("Ryakusyo3");
                dt.Columns.Add("Kubun");
                dt.Columns.Add("Nkai");
                dt.Columns.Add("GradeCD");
                dt.Columns.Add("GradeCDBefore");
                dt.Columns.Add("SyubetuCD");
                dt.Columns.Add("KigoCD");
                dt.Columns.Add("JyuryoCD");
                dt.Columns.Add("JyokenCD1");
                dt.Columns.Add("JyokenCD2");
                dt.Columns.Add("JyokenCD3");
                dt.Columns.Add("JyokenCD4");
                dt.Columns.Add("JyokenCD5");
                dt.Columns.Add("JyokenName");
                dt.Columns.Add("Kyori");
                dt.Columns.Add("KyoriBefore");
                dt.Columns.Add("TrackCD");
                dt.Columns.Add("TrackCDBefore");
                dt.Columns.Add("CourseKubunCD");
                dt.Columns.Add("CourseKubunCDBefore");
                dt.Columns.Add("Honsyokin1");
                dt.Columns.Add("Honsyokin2");
                dt.Columns.Add("Honsyokin3");
                dt.Columns.Add("Honsyokin4");
                dt.Columns.Add("Honsyokin5");
                dt.Columns.Add("Honsyokin6");
                dt.Columns.Add("Honsyokin7");
                dt.Columns.Add("HonsyokinBefore1");
                dt.Columns.Add("HonsyokinBefore2");
                dt.Columns.Add("HonsyokinBefore3");
                dt.Columns.Add("HonsyokinBefore4");
                dt.Columns.Add("HonsyokinBefore5");
                dt.Columns.Add("Fukasyokin1");
                dt.Columns.Add("Fukasyokin2");
                dt.Columns.Add("Fukasyokin3");
                dt.Columns.Add("Fukasyokin4");
                dt.Columns.Add("Fukasyokin5");
                dt.Columns.Add("FukasyokinBefore1");
                dt.Columns.Add("FukasyokinBefore2");
                dt.Columns.Add("FukasyokinBefore3");
                dt.Columns.Add("HassoTime");
                dt.Columns.Add("HassoTimeBefore");
                dt.Columns.Add("TorokuTosu");
                dt.Columns.Add("SyussoTosu");
                dt.Columns.Add("NyusenTosu");
                dt.Columns.Add("TenkoCD");
                dt.Columns.Add("SibaBabaCD");
                dt.Columns.Add("DirtBabaCD");
                dt.Columns.Add("LapTime1");
                dt.Columns.Add("LapTime2");
                dt.Columns.Add("LapTime3");
                dt.Columns.Add("LapTime4");
                dt.Columns.Add("LapTime5");
                dt.Columns.Add("LapTime6");
                dt.Columns.Add("LapTime7");
                dt.Columns.Add("LapTime8");
                dt.Columns.Add("LapTime9");
                dt.Columns.Add("LapTime10");
                dt.Columns.Add("LapTime11");
                dt.Columns.Add("LapTime12");
                dt.Columns.Add("LapTime13");
                dt.Columns.Add("LapTime14");
                dt.Columns.Add("LapTime15");
                dt.Columns.Add("LapTime16");
                dt.Columns.Add("LapTime17");
                dt.Columns.Add("LapTime18");
                dt.Columns.Add("LapTime19");
                dt.Columns.Add("LapTime20");
                dt.Columns.Add("LapTime21");
                dt.Columns.Add("LapTime22");
                dt.Columns.Add("LapTime23");
                dt.Columns.Add("LapTime24");
                dt.Columns.Add("LapTime25");
                dt.Columns.Add("SyogaiMileTime");
                dt.Columns.Add("HaronTimeS3");
                dt.Columns.Add("HaronTimeS4");
                dt.Columns.Add("HaronTimeL3");
                dt.Columns.Add("HaronTimeL4");
                dt.Columns.Add("Corner1");
                dt.Columns.Add("Syukaisu1");
                dt.Columns.Add("Jyuni1");
                dt.Columns.Add("Corner2");
                dt.Columns.Add("Syukaisu2");
                dt.Columns.Add("Jyuni2");
                dt.Columns.Add("Corner3");
                dt.Columns.Add("Syukaisu3");
                dt.Columns.Add("Jyuni3");
                dt.Columns.Add("Corner4");
                dt.Columns.Add("Syukaisu4");
                dt.Columns.Add("Jyuni4");
                dt.Columns.Add("RecordUpKubun");

                jvdds.Tables.Add(dt);
            }

            // 格納済みデータから主キーが同じ行を検索する
            DataTable dtRace = jvdds.Tables["Race"]!;
            var race = dtRace.AsEnumerable()
                .Where(r => r.Field<string>("Year") == raceDat.id.Year)
                .Where(r => r.Field<string>("MonthDay") == raceDat.id.MonthDay)
                .Where(r => r.Field<string>("JyoCD") == raceDat.id.JyoCD)
                .Where(r => r.Field<string>("Kaiji") == raceDat.id.Kaiji)
                .Where(r => r.Field<string>("Nichiji") == raceDat.id.Nichiji)
                .Where(r => r.Field<string>("RaceNum") == raceDat.id.RaceNum)
                .FirstOrDefault();

            // 主キーの同じデータが存在したか否か
            if (race is null)
            {
                // 存在しなかった場合、新しいRowを作成する
                DataRow row = dtRace.NewRow();

                // 主キーのセット
                // 開催年
                row["Year"] = raceDat.id.Year;
                // 開催月日
                row["MonthDay"] = raceDat.id.MonthDay;
                // 競馬場コード
                row["JyoCD"] = raceDat.id.JyoCD;
                // 開催回[第N回]
                row["Kaiji"] = raceDat.id.Kaiji;
                // 開催日目[N日目]
                row["Nichiji"] = raceDat.id.Nichiji;
                // レース番号
                row["RaceNum"] = raceDat.id.RaceNum;
                dtRace.Rows.Add(row);

                race = row;
            }

            // データのセット
            race["RecordSpec"] = raceDat.head.RecordSpec;              // レコード種別
            race["DataKubun"] = raceDat.head.DataKubun;                // データ区分
            race["MakeDate"] = raceDat.head.MakeDate.Year
                                + raceDat.head.MakeDate.Month
                                + raceDat.head.MakeDate.Day;           // データ作成年月日
            race["YoubiCD"] = raceDat.RaceInfo.YoubiCD;                // 曜日コード
            race["TokuNum"] = raceDat.RaceInfo.TokuNum;                // 特別競走番号
            race["Hondai"] = raceDat.RaceInfo.Hondai;                  // 競走名本題
            race["Fukudai"] = raceDat.RaceInfo.Fukudai;                // 競走名副題
            race["Kakko"] = raceDat.RaceInfo.Kakko;                    // 競走名カッコ内
            race["HondaiEng"] = raceDat.RaceInfo.HondaiEng;            // 競走名本題欧字
            race["FukudaiEng"] = raceDat.RaceInfo.FukudaiEng;          // 競走名副題欧字
            race["KakkoEng"] = raceDat.RaceInfo.KakkoEng;              // 競走名カッコ内欧字
            race["Ryakusyo10"] = raceDat.RaceInfo.Ryakusyo10;          // 競走名略称１０字
            race["Ryakusyo6"] = raceDat.RaceInfo.Ryakusyo6;            // 競走名略称６字
            race["Ryakusyo3"] = raceDat.RaceInfo.Ryakusyo3;            // 競走名略称３字
            race["Kubun"] = raceDat.RaceInfo.Kubun;                    // 競走名区分
            race["Nkai"] = raceDat.RaceInfo.Nkai;                      // 重賞回次[第N回]
            race["GradeCD"] = raceDat.GradeCD;                         // グレードコード
            race["GradeCDBefore"] = raceDat.GradeCDBefore;             // 変更前グレードコード
            race["SyubetuCD"] = raceDat.JyokenInfo.SyubetuCD;          // 競走種別コード
            race["KigoCD"] = raceDat.JyokenInfo.KigoCD;                // 競走記号コード
            race["JyuryoCD"] = raceDat.JyokenInfo.JyuryoCD;            // 重量種別コード
            race["JyokenCD1"] = raceDat.JyokenInfo.JyokenCD[0];        // 競走条件コード1
            race["JyokenCD2"] = raceDat.JyokenInfo.JyokenCD[1];        // 競走条件コード2
            race["JyokenCD3"] = raceDat.JyokenInfo.JyokenCD[2];        // 競走条件コード3
            race["JyokenCD4"] = raceDat.JyokenInfo.JyokenCD[3];        // 競走条件コード4
            race["JyokenCD5"] = raceDat.JyokenInfo.JyokenCD[4];        // 競走条件コード5
            race["JyokenName"] = raceDat.JyokenName;                   // 競走条件名称
            race["Kyori"] = raceDat.Kyori;                             // 距離
            race["KyoriBefore"] = raceDat.KyoriBefore;                 // 変更前距離
            race["TrackCD"] = raceDat.TrackCD;                         // トラックコード
            race["TrackCDBefore"] = raceDat.TrackCDBefore;             // 変更前トラックコード
            race["CourseKubunCD"] = raceDat.CourseKubunCD;             // コース区分
            race["CourseKubunCDBefore"] = raceDat.CourseKubunCDBefore; // 変更前コース区分
            race["Honsyokin1"] = raceDat.Honsyokin[0];                 // 本賞金1
            race["Honsyokin2"] = raceDat.Honsyokin[1];                 // 本賞金2
            race["Honsyokin3"] = raceDat.Honsyokin[2];                 // 本賞金3
            race["Honsyokin4"] = raceDat.Honsyokin[3];                 // 本賞金4
            race["Honsyokin5"] = raceDat.Honsyokin[4];                 // 本賞金5
            race["Honsyokin6"] = raceDat.Honsyokin[5];                 // 本賞金6
            race["Honsyokin7"] = raceDat.Honsyokin[6];                 // 本賞金7
            race["HonsyokinBefore1"] = raceDat.HonsyokinBefore[0];     // 変更前本賞金1
            race["HonsyokinBefore2"] = raceDat.HonsyokinBefore[1];     // 変更前本賞金2
            race["HonsyokinBefore3"] = raceDat.HonsyokinBefore[2];     // 変更前本賞金3
            race["HonsyokinBefore4"] = raceDat.HonsyokinBefore[3];     // 変更前本賞金4
            race["HonsyokinBefore5"] = raceDat.HonsyokinBefore[4];     // 変更前本賞金5
            race["Fukasyokin1"] = raceDat.Fukasyokin[0];               // 付加賞金1
            race["Fukasyokin2"] = raceDat.Fukasyokin[1];               // 付加賞金2
            race["Fukasyokin3"] = raceDat.Fukasyokin[2];               // 付加賞金3
            race["Fukasyokin4"] = raceDat.Fukasyokin[3];               // 付加賞金4
            race["Fukasyokin5"] = raceDat.Fukasyokin[4];               // 付加賞金5
            race["FukasyokinBefore1"] = raceDat.FukasyokinBefore[0];   // 変更前付加賞金1
            race["FukasyokinBefore2"] = raceDat.FukasyokinBefore[1];   // 変更前付加賞金2
            race["FukasyokinBefore3"] = raceDat.FukasyokinBefore[2];   // 変更前付加賞金3
            race["HassoTime"] = raceDat.HassoTime;                     // 発走時刻
            race["HassoTimeBefore"] = raceDat.HassoTimeBefore;         // 変更前発走時刻
            race["TorokuTosu"] = raceDat.TorokuTosu;                   // 登録頭数
            race["SyussoTosu"] = raceDat.SyussoTosu;                   // 出走頭数
            race["NyusenTosu"] = raceDat.NyusenTosu;                   // 入線頭数
            race["TenkoCD"] = raceDat.TenkoBaba.TenkoCD;               // 天候コード
            race["SibaBabaCD"] = raceDat.TenkoBaba.SibaBabaCD;         // 芝馬場状態コード
            race["DirtBabaCD"] = raceDat.TenkoBaba.DirtBabaCD;         // ダート馬場状態コード
            race["LapTime1"] = raceDat.LapTime[0];                     // ラップタイム1
            race["LapTime2"] = raceDat.LapTime[1];                     // ラップタイム2
            race["LapTime3"] = raceDat.LapTime[2];                     // ラップタイム3
            race["LapTime4"] = raceDat.LapTime[3];                     // ラップタイム4
            race["LapTime5"] = raceDat.LapTime[4];                     // ラップタイム5
            race["LapTime6"] = raceDat.LapTime[5];                     // ラップタイム6
            race["LapTime7"] = raceDat.LapTime[6];                     // ラップタイム7
            race["LapTime8"] = raceDat.LapTime[7];                     // ラップタイム8
            race["LapTime9"] = raceDat.LapTime[8];                     // ラップタイム9
            race["LapTime10"] = raceDat.LapTime[9];                    // ラップタイム10
            race["LapTime11"] = raceDat.LapTime[10];                   // ラップタイム11
            race["LapTime12"] = raceDat.LapTime[11];                   // ラップタイム12
            race["LapTime13"] = raceDat.LapTime[12];                   // ラップタイム13
            race["LapTime14"] = raceDat.LapTime[13];                   // ラップタイム14
            race["LapTime15"] = raceDat.LapTime[14];                   // ラップタイム15
            race["LapTime16"] = raceDat.LapTime[15];                   // ラップタイム16
            race["LapTime17"] = raceDat.LapTime[16];                   // ラップタイム17
            race["LapTime18"] = raceDat.LapTime[17];                   // ラップタイム18
            race["LapTime19"] = raceDat.LapTime[18];                   // ラップタイム19
            race["LapTime20"] = raceDat.LapTime[19];                   // ラップタイム20
            race["LapTime21"] = raceDat.LapTime[20];                   // ラップタイム21
            race["LapTime22"] = raceDat.LapTime[21];                   // ラップタイム22
            race["LapTime23"] = raceDat.LapTime[22];                   // ラップタイム23
            race["LapTime24"] = raceDat.LapTime[23];                   // ラップタイム24
            race["LapTime25"] = raceDat.LapTime[24];                   // ラップタイム25
            race["SyogaiMileTime"] = raceDat.SyogaiMileTime;           // 障害マイルタイム
            race["HaronTimeS3"] = raceDat.HaronTimeS3;                 // 前３ハロンタイム
            race["HaronTimeS4"] = raceDat.HaronTimeS4;                 // 前４ハロンタイム
            race["HaronTimeL3"] = raceDat.HaronTimeL3;                 // 後３ハロンタイム
            race["HaronTimeL4"] = raceDat.HaronTimeL4;                 // 後４ハロンタイム
            race["Corner1"] = raceDat.CornerInfo[0].Corner;            // コーナー通過順1コーナー
            race["Syukaisu1"] = raceDat.CornerInfo[0].Syukaisu;        // コーナー通過順1周回数
            race["Jyuni1"] = raceDat.CornerInfo[0].Jyuni;              // コーナー通過順1各通過順位
            race["Corner2"] = raceDat.CornerInfo[1].Corner;            // コーナー通過順2コーナー
            race["Syukaisu2"] = raceDat.CornerInfo[1].Syukaisu;        // コーナー通過順2周回数
            race["Jyuni2"] = raceDat.CornerInfo[1].Jyuni;              // コーナー通過順2各通過順位
            race["Corner3"] = raceDat.CornerInfo[2].Corner;            // コーナー通過順3コーナー
            race["Syukaisu3"] = raceDat.CornerInfo[2].Syukaisu;        // コーナー通過順3周回数
            race["Jyuni3"] = raceDat.CornerInfo[2].Jyuni;              // コーナー通過順3各通過順位
            race["Corner4"] = raceDat.CornerInfo[3].Corner;            // コーナー通過順4コーナー
            race["Syukaisu4"] = raceDat.CornerInfo[3].Syukaisu;        // コーナー通過順4周回数
            race["Jyuni4"] = raceDat.CornerInfo[3].Jyuni;              // コーナー通過順4各通過順位
            race["RecordUpKubun"] = raceDat.RecordUpKubun;             // レコード更新区分
        }

        public static void SetJVDataRaceStructure(DataRow dr, ref JV_RA_RACE raceDat)
        {
            // 構造体の配列の初期化
            raceDat.Initialize();
            raceDat.JyokenInfo.Initialize();

            // データのセット
            raceDat.head.RecordSpec = dr.Field<string>("RecordSpec")!;              // レコード種別
            raceDat.head.DataKubun = dr.Field<string>("DataKubun")!;                // データ区分

            var makeDate = Encoding.GetEncoding("Shift_JIS").GetBytes(dr.Field<string>("MakeDate")!);
            raceDat.head.MakeDate.SetDataB(makeDate);                               // データ作成年月日
            raceDat.id.Year = dr.Field<string>("Year")!;                            // 開催年
            raceDat.id.MonthDay = dr.Field<string>("MonthDay")!;                    // 開催月日
            raceDat.id.JyoCD = dr.Field<string>("JyoCD")!;                          // 競馬場コード
            raceDat.id.Kaiji = dr.Field<string>("Kaiji")!;                          // 開催回[第N回]
            raceDat.id.Nichiji = dr.Field<string>("Nichiji")!;                      // 開催日目[N日目]
            raceDat.id.RaceNum = dr.Field<string>("RaceNum")!;                      // レース番号
            raceDat.RaceInfo.YoubiCD = dr.Field<string>("YoubiCD")!;                // 曜日コード
            raceDat.RaceInfo.TokuNum = dr.Field<string>("TokuNum")!;                // 特別競走番号
            raceDat.RaceInfo.Hondai = dr.Field<string>("Hondai")!;                  // 競走名本題
            raceDat.RaceInfo.Fukudai = dr.Field<string>("Fukudai")!;                // 競走名副題
            raceDat.RaceInfo.Kakko = dr.Field<string>("Kakko")!;                    // 競走名カッコ内
            raceDat.RaceInfo.HondaiEng = dr.Field<string>("HondaiEng")!;            // 競走名本題欧字
            raceDat.RaceInfo.FukudaiEng = dr.Field<string>("FukudaiEng")!;          // 競走名副題欧字
            raceDat.RaceInfo.KakkoEng = dr.Field<string>("KakkoEng")!;              // 競走名カッコ内欧字
            raceDat.RaceInfo.Ryakusyo10 = dr.Field<string>("Ryakusyo10")!;          // 競走名略称１０字
            raceDat.RaceInfo.Ryakusyo6 = dr.Field<string>("Ryakusyo6")!;            // 競走名略称６字
            raceDat.RaceInfo.Ryakusyo3 = dr.Field<string>("Ryakusyo3")!;            // 競走名略称３字
            raceDat.RaceInfo.Kubun = dr.Field<string>("Kubun")!;                    // 競走名区分
            raceDat.RaceInfo.Nkai = dr.Field<string>("Nkai")!;                      // 重賞回次[第N回]
            raceDat.GradeCD = dr.Field<string>("GradeCD")!;                         // グレードコード
            raceDat.GradeCDBefore = dr.Field<string>("GradeCDBefore")!;             // 変更前グレードコード
            raceDat.JyokenInfo.SyubetuCD = dr.Field<string>("SyubetuCD")!;          // 競走種別コード
            raceDat.JyokenInfo.KigoCD = dr.Field<string>("KigoCD")!;                // 競走記号コード
            raceDat.JyokenInfo.JyuryoCD = dr.Field<string>("JyuryoCD")!;            // 重量種別コード
            raceDat.JyokenInfo.JyokenCD[0] = dr.Field<string>("JyokenCD1")!;        // 競走条件コード1
            raceDat.JyokenInfo.JyokenCD[1] = dr.Field<string>("JyokenCD2")!;        // 競走条件コード2
            raceDat.JyokenInfo.JyokenCD[2] = dr.Field<string>("JyokenCD3")!;        // 競走条件コード3
            raceDat.JyokenInfo.JyokenCD[3] = dr.Field<string>("JyokenCD4")!;        // 競走条件コード4
            raceDat.JyokenInfo.JyokenCD[4] = dr.Field<string>("JyokenCD5")!;        // 競走条件コード5
            raceDat.JyokenName = dr.Field<string>("JyokenName")!;                   // 競走条件名称
            raceDat.Kyori = dr.Field<string>("Kyori")!;                             // 距離
            raceDat.KyoriBefore = dr.Field<string>("KyoriBefore")!;                 // 変更前距離
            raceDat.TrackCD = dr.Field<string>("TrackCD")!;                         // トラックコード
            raceDat.TrackCDBefore = dr.Field<string>("TrackCDBefore")!;             // 変更前トラックコード
            raceDat.CourseKubunCD = dr.Field<string>("CourseKubunCD")!;             // コース区分
            raceDat.CourseKubunCDBefore = dr.Field<string>("CourseKubunCDBefore")!; // 変更前コース区分
            raceDat.Honsyokin[0] = dr.Field<string>("Honsyokin1")!;                 // 本賞金1
            raceDat.Honsyokin[1] = dr.Field<string>("Honsyokin2")!;                 // 本賞金2
            raceDat.Honsyokin[2] = dr.Field<string>("Honsyokin3")!;                 // 本賞金3
            raceDat.Honsyokin[3] = dr.Field<string>("Honsyokin4")!;                 // 本賞金4
            raceDat.Honsyokin[4] = dr.Field<string>("Honsyokin5")!;                 // 本賞金5
            raceDat.Honsyokin[5] = dr.Field<string>("Honsyokin6")!;                 // 本賞金6
            raceDat.Honsyokin[6] = dr.Field<string>("Honsyokin7")!;                 // 本賞金7
            raceDat.HonsyokinBefore[0] = dr.Field<string>("HonsyokinBefore1")! ;    // 変更前本賞金1
            raceDat.HonsyokinBefore[1] = dr.Field<string>("HonsyokinBefore2")!;     // 変更前本賞金2
            raceDat.HonsyokinBefore[2] = dr.Field<string>("HonsyokinBefore3")!;     // 変更前本賞金3
            raceDat.HonsyokinBefore[3] = dr.Field<string>("HonsyokinBefore4")!;     // 変更前本賞金4
            raceDat.HonsyokinBefore[4] = dr.Field<string>("HonsyokinBefore5")!;     // 変更前本賞金5
            raceDat.Fukasyokin[0] = dr.Field<string>("Fukasyokin1")!;               // 付加賞金1
            raceDat.Fukasyokin[1] = dr.Field<string>("Fukasyokin2")!;               // 付加賞金2
            raceDat.Fukasyokin[2] = dr.Field<string>("Fukasyokin3")!;               // 付加賞金3
            raceDat.Fukasyokin[3] = dr.Field<string>("Fukasyokin4")!;               // 付加賞金4
            raceDat.Fukasyokin[4] = dr.Field<string>("Fukasyokin5")!;               // 付加賞金5
            raceDat.FukasyokinBefore[0] = dr.Field<string>("FukasyokinBefore1")!;   // 変更前付加賞金1
            raceDat.FukasyokinBefore[1] = dr.Field<string>("FukasyokinBefore2")!;   // 変更前付加賞金2
            raceDat.FukasyokinBefore[2] = dr.Field<string>("FukasyokinBefore3")!;   // 変更前付加賞金3
            raceDat.HassoTime = dr.Field<string>("HassoTime")!;                     // 発走時刻
            raceDat.HassoTimeBefore = dr.Field<string>("HassoTimeBefore")!;         // 変更前発走時刻
            raceDat.TorokuTosu = dr.Field<string>("TorokuTosu")!;                   // 登録頭数
            raceDat.SyussoTosu = dr.Field<string>("SyussoTosu")!;                   // 出走頭数
            raceDat.NyusenTosu = dr.Field<string>("NyusenTosu")!;                   // 入線頭数
            raceDat.TenkoBaba.TenkoCD = dr.Field<string>("TenkoCD")!;               // 天候コード
            raceDat.TenkoBaba.SibaBabaCD = dr.Field<string>("SibaBabaCD")!;         // 芝馬場状態コード
            raceDat.TenkoBaba.DirtBabaCD = dr.Field<string>("DirtBabaCD")!;         // ダート馬場状態コード
            raceDat.LapTime[0] = dr.Field<string>("LapTime1")!;                     // ラップタイム1
            raceDat.LapTime[1] = dr.Field<string>("LapTime2")!;                     // ラップタイム2
            raceDat.LapTime[2] = dr.Field<string>("LapTime3")!;                     // ラップタイム3
            raceDat.LapTime[3] = dr.Field<string>("LapTime4")!;                     // ラップタイム4
            raceDat.LapTime[4] = dr.Field<string>("LapTime5")!;                     // ラップタイム5
            raceDat.LapTime[5] = dr.Field<string>("LapTime6")!;                     // ラップタイム6
            raceDat.LapTime[6] = dr.Field<string>("LapTime7")!;                     // ラップタイム7
            raceDat.LapTime[7] = dr.Field<string>("LapTime8")!;                     // ラップタイム8
            raceDat.LapTime[8] = dr.Field<string>("LapTime9")!;                     // ラップタイム9
            raceDat.LapTime[9] = dr.Field<string>("LapTime10")!;                    // ラップタイム10
            raceDat.LapTime[10] = dr.Field<string>("LapTime11")!;                   // ラップタイム11
            raceDat.LapTime[11] = dr.Field<string>("LapTime12")!;                   // ラップタイム12
            raceDat.LapTime[12] = dr.Field<string>("LapTime13")!;                   // ラップタイム13
            raceDat.LapTime[13] = dr.Field<string>("LapTime14")!;                   // ラップタイム14
            raceDat.LapTime[14] = dr.Field<string>("LapTime15")!;                   // ラップタイム15
            raceDat.LapTime[15] = dr.Field<string>("LapTime16")!;                   // ラップタイム16
            raceDat.LapTime[16] = dr.Field<string>("LapTime17")!;                   // ラップタイム17
            raceDat.LapTime[17] = dr.Field<string>("LapTime18")!;                   // ラップタイム18
            raceDat.LapTime[18] = dr.Field<string>("LapTime19")!;                   // ラップタイム19
            raceDat.LapTime[19] = dr.Field<string>("LapTime20")!;                   // ラップタイム20
            raceDat.LapTime[20] = dr.Field<string>("LapTime21")!;                   // ラップタイム21
            raceDat.LapTime[21] = dr.Field<string>("LapTime22")!;                   // ラップタイム22
            raceDat.LapTime[22] = dr.Field<string>("LapTime23")!;                   // ラップタイム23
            raceDat.LapTime[23] = dr.Field<string>("LapTime24")!;                   // ラップタイム24
            raceDat.LapTime[24] = dr.Field<string>("LapTime25")!;                   // ラップタイム25
            raceDat.SyogaiMileTime = dr.Field<string>("SyogaiMileTime")!;           // 障害マイルタイム
            raceDat.HaronTimeS3 = dr.Field<string>("HaronTimeS3")!;                 // 前３ハロンタイム
            raceDat.HaronTimeS4 = dr.Field<string>("HaronTimeS4")!;                 // 前４ハロンタイム
            raceDat.HaronTimeL3 = dr.Field<string>("HaronTimeL3")!;                 // 後３ハロンタイム
            raceDat.HaronTimeL4 = dr.Field<string>("HaronTimeL4")!;                 // 後４ハロンタイム
            raceDat.CornerInfo[0].Corner = dr.Field<string>("Corner1")!;            // コーナー通過順1コーナー
            raceDat.CornerInfo[0].Syukaisu = dr.Field<string>("Syukaisu1")!;        // コーナー通過順1周回数
            raceDat.CornerInfo[0].Jyuni = dr.Field<string>("Jyuni1")!;              // コーナー通過順1各通過順位
            raceDat.CornerInfo[1].Corner = dr.Field<string>("Corner2")!;            // コーナー通過順2コーナー
            raceDat.CornerInfo[1].Syukaisu = dr.Field<string>("Syukaisu2")!;        // コーナー通過順2周回数
            raceDat.CornerInfo[1].Jyuni = dr.Field<string>("Jyuni2")!;              // コーナー通過順2各通過順位
            raceDat.CornerInfo[2].Corner = dr.Field<string>("Corner3")!;            // コーナー通過順3コーナー
            raceDat.CornerInfo[2].Syukaisu = dr.Field<string>("Syukaisu3")!;        // コーナー通過順3周回数
            raceDat.CornerInfo[2].Jyuni = dr.Field<string>("Jyuni3")!;              // コーナー通過順3各通過順位
            raceDat.CornerInfo[3].Corner = dr.Field<string>("Corner4")!;            // コーナー通過順4コーナー
            raceDat.CornerInfo[3].Syukaisu = dr.Field<string>("Syukaisu4")!;        // コーナー通過順4周回数
            raceDat.CornerInfo[3].Jyuni = dr.Field<string>("Jyuni4")!;              // コーナー通過順4各通過順位
            raceDat.RecordUpKubun = dr.Field<string>("RecordUpKubun")!;             // レコード更新区分
        }

        public static void SetJVDataUmaRaceDataSet(JV_SE_RACE_UMA raceUmaDat, ref DataSet jvdds)
        {
            if (jvdds.Tables["RaceUma"] is null)
            {
                DataTable dt = new DataTable("RaceUma");
                dt.Columns.Add("Year");
                dt.Columns.Add("MonthDay");
                dt.Columns.Add("JyoCD");
                dt.Columns.Add("Kaiji");
                dt.Columns.Add("Nichiji");
                dt.Columns.Add("RaceNum");
                dt.Columns.Add("Umaban");
                dt.Columns.Add("KettoNum");
                dt.Columns.Add("RecordSpec");
                dt.Columns.Add("DataKubun");
                dt.Columns.Add("MakeDate");
                dt.Columns.Add("Wakuban");
                dt.Columns.Add("Bamei");
                dt.Columns.Add("UmaKigoCD");
                dt.Columns.Add("SexCD");
                dt.Columns.Add("HinsyuCD");
                dt.Columns.Add("KeiroCD");
                dt.Columns.Add("Barei");
                dt.Columns.Add("TozaiCD");
                dt.Columns.Add("ChokyosiCode");
                dt.Columns.Add("ChokyosiRyakusyo");
                dt.Columns.Add("BanusiCode");
                dt.Columns.Add("BanusiName");
                dt.Columns.Add("Fukusyoku");
                dt.Columns.Add("reserved1");
                dt.Columns.Add("Futan");
                dt.Columns.Add("FutanBefore");
                dt.Columns.Add("Blinker");
                dt.Columns.Add("reserved2");
                dt.Columns.Add("KisyuCode");
                dt.Columns.Add("KisyuCodeBefore");
                dt.Columns.Add("KisyuRyakusyo");
                dt.Columns.Add("KisyuRyakusyoBefore");
                dt.Columns.Add("MinaraiCD");
                dt.Columns.Add("MinaraiCDBefore");
                dt.Columns.Add("BaTaijyu");
                dt.Columns.Add("ZogenFugo");
                dt.Columns.Add("ZogenSa");
                dt.Columns.Add("IJyoCD");
                dt.Columns.Add("NyusenJyuni");
                dt.Columns.Add("KakuteiJyuni");
                dt.Columns.Add("DochakuKubun");
                dt.Columns.Add("DochakuTosu");
                dt.Columns.Add("Time");
                dt.Columns.Add("ChakusaCD");
                dt.Columns.Add("ChakusaCDP");
                dt.Columns.Add("ChakusaCDPP");
                dt.Columns.Add("Jyuni1c");
                dt.Columns.Add("Jyuni2c");
                dt.Columns.Add("Jyuni3c");
                dt.Columns.Add("Jyuni4c");
                dt.Columns.Add("Odds");
                dt.Columns.Add("Ninki");
                dt.Columns.Add("Honsyokin");
                dt.Columns.Add("Fukasyokin");
                dt.Columns.Add("reserved3");
                dt.Columns.Add("reserved4");
                dt.Columns.Add("HaronTimeL4");
                dt.Columns.Add("HaronTimeL3");
                dt.Columns.Add("KettoNum1");
                dt.Columns.Add("Bamei1");
                dt.Columns.Add("KettoNum2");
                dt.Columns.Add("Bamei2");
                dt.Columns.Add("KettoNum3");
                dt.Columns.Add("Bamei3");
                dt.Columns.Add("TimeDiff");
                dt.Columns.Add("RecordUpKubun");
                dt.Columns.Add("DMKubun");
                dt.Columns.Add("DMTime");
                dt.Columns.Add("DMGosaP");
                dt.Columns.Add("DMGosaM");
                dt.Columns.Add("DMJyuni");
                dt.Columns.Add("KyakusituKubun");

                jvdds.Tables.Add(dt);
            }

            // 格納済みデータから主キーが同じ行を検索する
            DataTable dtUmaRace = jvdds.Tables["RaceUma"]!;
            var umaRace = dtUmaRace.AsEnumerable()
                .Where(r => r.Field<string>("Year") == raceUmaDat.id.Year)
                .Where(r => r.Field<string>("MonthDay") == raceUmaDat.id.MonthDay)
                .Where(r => r.Field<string>("JyoCD") == raceUmaDat.id.JyoCD)
                .Where(r => r.Field<string>("Kaiji") == raceUmaDat.id.Kaiji)
                .Where(r => r.Field<string>("Nichiji") == raceUmaDat.id.Nichiji)
                .Where(r => r.Field<string>("RaceNum") == raceUmaDat.id.RaceNum)
                .Where(r => r.Field<string>("Umaban") == raceUmaDat.Umaban)
                .Where(r => r.Field<string>("KettoNum") == raceUmaDat.KettoNum)
                .FirstOrDefault();

            // 主キーの同じデータが存在したか否か
            if (umaRace is null)
            {
                // 存在しなかった場合、新しいRowを作成する
                DataRow row = dtUmaRace.NewRow();

                // 主キーのセット
                // 開催年
                row["Year"] = raceUmaDat.id.Year;
                // 開催月日
                row["MonthDay"] = raceUmaDat.id.MonthDay;
                // 競馬場コード
                row["JyoCD"] = raceUmaDat.id.JyoCD;
                // 開催回[第N回]
                row["Kaiji"] = raceUmaDat.id.Kaiji;
                // 開催日目[N日目]
                row["Nichiji"] = raceUmaDat.id.Nichiji;
                // レース番号
                row["RaceNum"] = raceUmaDat.id.RaceNum;
                // 馬番
                row["Umaban"] = raceUmaDat.Umaban;
                // 血統登録番号
                row["KettoNum"] = raceUmaDat.KettoNum;
                dtUmaRace.Rows.Add(row);

                umaRace = row;
            }

            // データのセット
            umaRace["RecordSpec"] = raceUmaDat.head.RecordSpec;              // レコード種別
            umaRace["DataKubun"] = raceUmaDat.head.DataKubun;                // データ区分
            umaRace["MakeDate"] = raceUmaDat.head.MakeDate.Year
                                    + raceUmaDat.head.MakeDate.Month
                                    + raceUmaDat.head.MakeDate.Day;          // データ作成年月日
            umaRace["Wakuban"] = raceUmaDat.Wakuban;                         // 枠番
            umaRace["Bamei"] = raceUmaDat.Bamei;                             // 馬名
            umaRace["UmaKigoCD"] = raceUmaDat.UmaKigoCD;                     // 馬記号コード
            umaRace["SexCD"] = raceUmaDat.SexCD;                             // 性別コード
            umaRace["HinsyuCD"] = raceUmaDat.HinsyuCD;                       // 品種コード
            umaRace["KeiroCD"] = raceUmaDat.KeiroCD;                         // 毛色コード
            umaRace["Barei"] = raceUmaDat.Barei;                             // 馬齢
            umaRace["TozaiCD"] = raceUmaDat.TozaiCD;                         // 東西所属コード
            umaRace["ChokyosiCode"] = raceUmaDat.ChokyosiCode;               // 調教師コード
            umaRace["ChokyosiRyakusyo"] = raceUmaDat.ChokyosiRyakusyo;       // 調教師名略称
            umaRace["BanusiCode"] = raceUmaDat.BanusiCode;                   // 馬主コード
            umaRace["BanusiName"] = raceUmaDat.BanusiName;                   // 馬主名
            umaRace["Fukusyoku"] = raceUmaDat.Fukusyoku;                     // 服色標示
            umaRace["reserved1"] = raceUmaDat.reserved1;                     // 予備
            umaRace["Futan"] = raceUmaDat.Futan;                             // 負担重量
            umaRace["FutanBefore"] = raceUmaDat.FutanBefore;                 // 変更前負担重量
            umaRace["Blinker"] = raceUmaDat.Blinker;                         // ブリンカー使用区分
            umaRace["reserved2"] = raceUmaDat.reserved2;                     // 予備
            umaRace["KisyuCode"] = raceUmaDat.KisyuCode;                     // 騎手コード
            umaRace["KisyuCodeBefore"] = raceUmaDat.KisyuCodeBefore;         // 変更前騎手コード
            umaRace["KisyuRyakusyo"] = raceUmaDat.KisyuRyakusyo;             // 騎手名略称
            umaRace["KisyuRyakusyoBefore"] = raceUmaDat.KisyuRyakusyoBefore; // 変更前騎手名略称
            umaRace["MinaraiCD"] = raceUmaDat.MinaraiCD;                     // 騎手見習コード
            umaRace["MinaraiCDBefore"] = raceUmaDat.MinaraiCDBefore;         // 変更前騎手見習コード
            umaRace["BaTaijyu"] = raceUmaDat.BaTaijyu;                       // 馬体重
            umaRace["ZogenFugo"] = raceUmaDat.ZogenFugo;                     // 増減符号
            umaRace["ZogenSa"] = raceUmaDat.ZogenSa;                         // 増減差
            umaRace["IJyoCD"] = raceUmaDat.IJyoCD;                           // 異常区分コード
            umaRace["NyusenJyuni"] = raceUmaDat.NyusenJyuni;                 // 入線順位
            umaRace["KakuteiJyuni"] = raceUmaDat.KakuteiJyuni;               // 確定着順
            umaRace["DochakuKubun"] = raceUmaDat.DochakuKubun;               // 同着区分
            umaRace["DochakuTosu"] = raceUmaDat.DochakuTosu;                 // 同着頭数
            umaRace["Time"] = raceUmaDat.Time;                               // 走破タイム
            umaRace["ChakusaCD"] = raceUmaDat.ChakusaCD;                     // 着差コード
            umaRace["ChakusaCDP"] = raceUmaDat.ChakusaCDP;                   // +着差コード
            umaRace["ChakusaCDPP"] = raceUmaDat.ChakusaCDPP;                 // ++着差コード
            umaRace["Jyuni1c"] = raceUmaDat.Jyuni1c;                         // 1コーナーでの順位
            umaRace["Jyuni2c"] = raceUmaDat.Jyuni2c;                         // 2コーナーでの順位
            umaRace["Jyuni3c"] = raceUmaDat.Jyuni3c;                         // 3コーナーでの順位
            umaRace["Jyuni4c"] = raceUmaDat.Jyuni4c;                         // 4コーナーでの順位
            umaRace["Odds"] = raceUmaDat.Odds;                               // 単勝オッズ
            umaRace["Ninki"] = raceUmaDat.Ninki;                             // 単勝人気順
            umaRace["Honsyokin"] = raceUmaDat.Honsyokin;                     // 獲得本賞金
            umaRace["Fukasyokin"] = raceUmaDat.Fukasyokin;                   // 獲得付加賞金
            umaRace["reserved3"] = raceUmaDat.reserved3;                     // 予備
            umaRace["reserved4"] = raceUmaDat.reserved4;                     // 予備
            umaRace["HaronTimeL4"] = raceUmaDat.HaronTimeL4;                 // 後４ハロンタイム
            umaRace["HaronTimeL3"] = raceUmaDat.HaronTimeL3;                 // 後３ハロンタイム
            umaRace["KettoNum1"] = raceUmaDat.ChakuUmaInfo[0].KettoNum;      // 相手馬1血統登録番号
            umaRace["Bamei1"] = raceUmaDat.ChakuUmaInfo[0].Bamei;            // 相手馬1馬名
            umaRace["KettoNum2"] = raceUmaDat.ChakuUmaInfo[1].KettoNum;      // 相手馬2血統登録番号
            umaRace["Bamei2"] = raceUmaDat.ChakuUmaInfo[1].Bamei;            // 相手馬2馬名
            umaRace["KettoNum3"] = raceUmaDat.ChakuUmaInfo[2].KettoNum;      // 相手馬3血統登録番号
            umaRace["Bamei3"] = raceUmaDat.ChakuUmaInfo[2].Bamei;            // 相手馬3馬名
            umaRace["TimeDiff"] = raceUmaDat.TimeDiff;                       // タイム差
            umaRace["RecordUpKubun"] = raceUmaDat.RecordUpKubun;             // レコード更新区分
            umaRace["DMKubun"] = raceUmaDat.DMKubun;                         // マイニング区分
            umaRace["DMTime"] = raceUmaDat.DMTime;                           // マイニング予想走破タイム
            umaRace["DMGosaP"] = raceUmaDat.DMGosaP;                         // 予測誤差(信頼度)＋
            umaRace["DMGosaM"] = raceUmaDat.DMGosaM;                         // 予測誤差(信頼度)－
            umaRace["DMJyuni"] = raceUmaDat.DMJyuni;                         // マイニング予想順位
            umaRace["KyakusituKubun"] = raceUmaDat.KyakusituKubun;           // 今回レース脚質判定
        }

        public static void SetJVDataUmaRaceStructure(DataRow dr, ref JV_SE_RACE_UMA raceUmaDat)
        {
            // 構造体の配列の初期化
            raceUmaDat.Initialize();

            // データのセット
            raceUmaDat.head.RecordSpec = dr.Field<string>("RecordSpec")!;              // レコード種別
            raceUmaDat.head.DataKubun = dr.Field<string>("DataKubun")!;                // データ区分

            var makeDate = Encoding.GetEncoding("Shift_JIS").GetBytes(dr.Field<string>("MakeDate")!);
            raceUmaDat.head.MakeDate.SetDataB(makeDate);                               // データ作成年月日
            raceUmaDat.id.Year = dr.Field<string>("Year")!;                            // 開催年
            raceUmaDat.id.MonthDay = dr.Field<string>("MonthDay")!;                    // 開催月日
            raceUmaDat.id.JyoCD = dr.Field<string>("JyoCD")!;                          // 競馬場コード
            raceUmaDat.id.Kaiji = dr.Field<string>("Kaiji")!;                          // 開催回[第N回]
            raceUmaDat.id.Nichiji = dr.Field<string>("Nichiji")!;                      // 開催日目[N日目]
            raceUmaDat.id.RaceNum = dr.Field<string>("RaceNum")!;                      // レース番号
            raceUmaDat.Wakuban = dr.Field<string>("Wakuban")!;                         // 枠番
            raceUmaDat.Umaban = dr.Field<string>("Umaban")!;                           // 馬番
            raceUmaDat.KettoNum = dr.Field<string>("KettoNum")!;                       // 血統登録番号
            raceUmaDat.Bamei = dr.Field<string>("Bamei")!;                             // 馬名
            raceUmaDat.UmaKigoCD = dr.Field<string>("UmaKigoCD")!;                     // 馬記号コード
            raceUmaDat.SexCD = dr.Field<string>("SexCD")!;                             // 性別コード
            raceUmaDat.HinsyuCD = dr.Field<string>("HinsyuCD")!;                       // 品種コード
            raceUmaDat.KeiroCD = dr.Field<string>("KeiroCD")!;                         // 毛色コード
            raceUmaDat.Barei = dr.Field<string>("Barei")!;                             // 馬齢
            raceUmaDat.TozaiCD = dr.Field<string>("TozaiCD")!;                         // 東西所属コード
            raceUmaDat.ChokyosiCode = dr.Field<string>("ChokyosiCode")!;               // 調教師コード
            raceUmaDat.ChokyosiRyakusyo = dr.Field<string>("ChokyosiRyakusyo")!;       // 調教師名略称
            raceUmaDat.BanusiCode = dr.Field<string>("BanusiCode")!;                   // 馬主コード
            raceUmaDat.BanusiName = dr.Field<string>("BanusiName")!;                   // 馬主名
            raceUmaDat.Fukusyoku = dr.Field<string>("Fukusyoku")!;                     // 服色標示
            raceUmaDat.reserved1 = dr.Field<string>("reserved1")!;                     // 予備
            raceUmaDat.Futan = dr.Field<string>("Futan")!;                             // 負担重量
            raceUmaDat.FutanBefore = dr.Field<string>("FutanBefore")!;                 // 変更前負担重量
            raceUmaDat.Blinker = dr.Field<string>("Blinker")!;                         // ブリンカー使用区分
            raceUmaDat.reserved2 = dr.Field<string>("reserved2")!;                     // 予備
            raceUmaDat.KisyuCode = dr.Field<string>("KisyuCode")!;                     // 騎手コード
            raceUmaDat.KisyuCodeBefore = dr.Field<string>("KisyuCodeBefore")!;         // 変更前騎手コード
            raceUmaDat.KisyuRyakusyo = dr.Field<string>("KisyuRyakusyo")!;             // 騎手名略称
            raceUmaDat.KisyuRyakusyoBefore = dr.Field<string>("KisyuRyakusyoBefore")!; // 変更前騎手名略称
            raceUmaDat.MinaraiCD = dr.Field<string>("MinaraiCD")!;                     // 騎手見習コード
            raceUmaDat.MinaraiCDBefore = dr.Field<string>("MinaraiCDBefore")!;         // 変更前騎手見習コード
            raceUmaDat.BaTaijyu = dr.Field<string>("BaTaijyu")!;                       // 馬体重
            raceUmaDat.ZogenFugo = dr.Field<string>("ZogenFugo")!;                     // 増減符号
            raceUmaDat.ZogenSa = dr.Field<string>("ZogenSa")!;                         // 増減差
            raceUmaDat.IJyoCD = dr.Field<string>("IJyoCD")!;                           // 異常区分コード
            raceUmaDat.NyusenJyuni = dr.Field<string>("NyusenJyuni")!;                 // 入線順位
            raceUmaDat.KakuteiJyuni = dr.Field<string>("KakuteiJyuni")!;               // 確定着順
            raceUmaDat.DochakuKubun = dr.Field<string>("DochakuKubun")!;               // 同着区分
            raceUmaDat.DochakuTosu = dr.Field<string>("DochakuTosu")!;                 // 同着頭数
            raceUmaDat.Time = dr.Field<string>("Time")!;                               // 走破タイム
            raceUmaDat.ChakusaCD = dr.Field<string>("ChakusaCD")!;                     // 着差コード
            raceUmaDat.ChakusaCDP = dr.Field<string>("ChakusaCDP")!;                   // +着差コード
            raceUmaDat.ChakusaCDPP = dr.Field<string>("ChakusaCDPP")!;                 // ++着差コード
            raceUmaDat.Jyuni1c = dr.Field<string>("Jyuni1c")!;                         // 1コーナーでの順位
            raceUmaDat.Jyuni2c = dr.Field<string>("Jyuni2c")!;                         // 2コーナーでの順位
            raceUmaDat.Jyuni3c = dr.Field<string>("Jyuni3c")!;                         // 3コーナーでの順位
            raceUmaDat.Jyuni4c = dr.Field<string>("Jyuni4c")!;                         // 4コーナーでの順位
            raceUmaDat.Odds = dr.Field<string>("Odds")!;                               // 単勝オッズ
            raceUmaDat.Ninki = dr.Field<string>("Ninki")!;                             // 単勝人気順
            raceUmaDat.Honsyokin = dr.Field<string>("Honsyokin")!;                     // 獲得本賞金
            raceUmaDat.Fukasyokin = dr.Field<string>("Fukasyokin")!;                   // 獲得付加賞金
            raceUmaDat.reserved3 = dr.Field<string>("reserved3")!;                     // 予備
            raceUmaDat.reserved4 = dr.Field<string>("reserved4")!;                     // 予備
            raceUmaDat.HaronTimeL4 = dr.Field<string>("HaronTimeL4")!;                 // 後４ハロンタイム
            raceUmaDat.HaronTimeL3 = dr.Field<string>("HaronTimeL3")!;                 // 後３ハロンタイム
            raceUmaDat.ChakuUmaInfo[0].KettoNum = dr.Field<string>("KettoNum1")!;      // 相手馬1血統登録番号
            raceUmaDat.ChakuUmaInfo[0].Bamei = dr.Field<string>("Bamei1")!;            // 相手馬1馬名
            raceUmaDat.ChakuUmaInfo[1].KettoNum = dr.Field<string>("KettoNum2")!;      // 相手馬2血統登録番号
            raceUmaDat.ChakuUmaInfo[1].Bamei = dr.Field<string>("Bamei2")!;            // 相手馬2馬名
            raceUmaDat.ChakuUmaInfo[2].KettoNum = dr.Field<string>("KettoNum3")!;      // 相手馬3血統登録番号
            raceUmaDat.ChakuUmaInfo[2].Bamei = dr.Field<string>("Bamei3")!;            // 相手馬3馬名
            raceUmaDat.TimeDiff = dr.Field<string>("TimeDiff")!;                       // タイム差
            raceUmaDat.RecordUpKubun = dr.Field<string>("RecordUpKubun")!;             // レコード更新区分
            raceUmaDat.DMKubun = dr.Field<string>("DMKubun")!;                         // マイニング区分
            raceUmaDat.DMTime = dr.Field<string>("DMTime")!;                           // マイニング予想走破タイム
            raceUmaDat.DMGosaP = dr.Field<string>("DMGosaP")!;                         // 予測誤差(信頼度)＋
            raceUmaDat.DMGosaM = dr.Field<string>("DMGosaM")!;                         // 予測誤差(信頼度)－
            raceUmaDat.DMJyuni = dr.Field<string>("DMJyuni")!;                         // マイニング予想順位
            raceUmaDat.KyakusituKubun = dr.Field<string>("KyakusituKubun")!;           // 今回レース脚質判定
        }
    }
}
