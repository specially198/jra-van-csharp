using System.Text;

namespace JraVanCsharp
{
    public record CsvData(string CodeNo, string Code, List<string> NameList);

    class ClsCodeConv
    {
        // コード名称データ
        private Dictionary<string, CsvData> csvDataDictionary = new Dictionary<string, CsvData>();

        public ClsCodeConv(string fileName)
        {
            readCsv(fileName);
        }

        private void readCsv(string fileName)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var sr = new StreamReader(fileName, Encoding.GetEncoding("sjis")))
                {
                    while (sr.Peek() > -1)
                    {
                        // カンマ区切りで分割
                        var line = sr.ReadLine()?.Split(',');
                        if (line is null) continue;

                        // 名称（3項目目）以降はカンマ区切りになっているので、分割したのを元に戻して保持
                        // (例)
                        // 2001,01,札幌競馬場,札,札幌,札幌,SAPPORO,
                        // →以下をリストで保持
                        // 札幌競馬場,札,札幌,札幌,SAPPORO
                        var nameList = new List<string>();
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (i >= 2)
                            {
                                nameList.Add(line[i]);
                            }
                        }

                        CsvData csvData = new(line[0], line[1], nameList);
                        csvDataDictionary.Add(line[0] + "_" + line[1], csvData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string getCodeName(string codeNo, string? code, int no = 1)
        {
            CsvData csvData = csvDataDictionary[codeNo + "_" + code];
            var nameList = csvData.NameList;

            // 指定番目の名称
            var name = nameList[no - 1];

            return name;
        }
    }
}
